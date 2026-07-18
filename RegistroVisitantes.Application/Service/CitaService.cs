using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Cita;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class CitaService : BaseService<CitaDTO>, ICitaService
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMedicoRepository _medicoRepository;

        public CitaService(ICitaRepository citaRepository, IPacienteRepository pacienteRepository, IMedicoRepository medicoRepository)
        {
            _citaRepository = citaRepository;
            _pacienteRepository = pacienteRepository;
            _medicoRepository = medicoRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(CitaDTO dto)
        {
            var errors = await ValidateCita(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var cita = new Cita
            {
                FechaHora = dto.FechaHora,
                Motivo = dto.Motivo,
                Estado = dto.Estado,
                Observaciones = dto.Observaciones,
                PacienteId = dto.PacienteId,
                MedicoId = dto.MedicoId
            };

            var created = await _citaRepository.CreateAsync(cita);

            return ServiceResult.Ok(MapToDTO(created), "Cita creada exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, CitaDTO dto)
        {
            var existing = await _citaRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Cita con ID {id} no encontrada");

            var errors = await ValidateCita(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var cita = new Cita
            {
                Id = id,
                FechaHora = dto.FechaHora,
                Motivo = dto.Motivo,
                Estado = dto.Estado,
                Observaciones = dto.Observaciones,
                PacienteId = dto.PacienteId,
                MedicoId = dto.MedicoId
            };

            var updated = await _citaRepository.UpdateAsync(id, cita);

            return ServiceResult.Ok(MapToDTO(updated!), "Cita actualizada exitosamente");
        }

        public async Task<IEnumerable<CitaDTO>> GetByPacienteIdAsync(int pacienteId)
        {
            var citas = await _citaRepository.GetAllAsync();
            return citas
                .Where(c => c.PacienteId == pacienteId)
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<CitaDTO>> GetAllAsync()
        {
            var citas = await _citaRepository.GetAllAsync();
            return citas.Select(MapToDTO);
        }

        public override async Task<CitaDTO?> GetByIdAsync(int id)
        {
            var cita = await _citaRepository.GetByIdAsync(id);
            if (cita == null) return null;

            return MapToDTO(cita);
        }

        private static CitaDTO MapToDTO(Cita entity)
        {
            return new CitaDTO
            {
                Id = entity.Id,
                FechaHora = entity.FechaHora,
                Motivo = entity.Motivo,
                Estado = entity.Estado,
                Observaciones = entity.Observaciones,
                PacienteId = entity.PacienteId,
                MedicoId = entity.MedicoId
            };
        }

        private async Task<List<string>> ValidateCita(CitaDTO dto)
        {
            var errors = new List<string>();

            if (dto.FechaHora == default)
                errors.Add("La fecha y hora son requeridas");

            if (string.IsNullOrWhiteSpace(dto.Motivo))
                errors.Add("El motivo es requerido");
            else if (dto.Motivo.Length < 3)
                errors.Add("El motivo debe tener al menos 3 caracteres");
            else if (dto.Motivo.Length > 200)
                errors.Add("El motivo no puede exceder 200 caracteres");

            if (!string.IsNullOrWhiteSpace(dto.Estado) && dto.Estado.Length > 50)
                errors.Add("El estado no puede exceder 50 caracteres");

            if (!string.IsNullOrWhiteSpace(dto.Observaciones) && dto.Observaciones.Length > 500)
                errors.Add("Las observaciones no pueden exceder 500 caracteres");

            if (dto.PacienteId <= 0)
                errors.Add("El ID del paciente es requerido");
            else
            {
                var pacienteExists = await _pacienteRepository.GetByIdAsync(dto.PacienteId);
                if (pacienteExists == null)
                    errors.Add($"El paciente con ID {dto.PacienteId} no existe");
            }

            if (dto.MedicoId <= 0)
                errors.Add("El ID del médico es requerido");
            else
            {
                var medicoExists = await _medicoRepository.GetByIdAsync(dto.MedicoId);
                if (medicoExists == null)
                    errors.Add($"El médico con ID {dto.MedicoId} no existe");
            }

            return errors;
        }
    }
}
