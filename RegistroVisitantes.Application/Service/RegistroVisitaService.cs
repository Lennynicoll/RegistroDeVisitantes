using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.RegistroVisita;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class RegistroVisitaService : BaseService<RegistroVisitaDTO>, IRegistroVisitaService
    {
        private readonly IRegistroVisitaRepository _RegistroVisitaRepository;
        private readonly IRegistroVisitanteRepository _RegistroVisitanteRepository;
        private readonly IAnfitrionRepository _AnfitrionRepository;

        public RegistroVisitaService(IRegistroVisitaRepository RegistroVisitaRepository, IRegistroVisitanteRepository RegistroVisitanteRepository, IAnfitrionRepository AnfitrionRepository)
        {
            _RegistroVisitaRepository = RegistroVisitaRepository;
            _RegistroVisitanteRepository = RegistroVisitanteRepository;
            _AnfitrionRepository = AnfitrionRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(RegistroVisitaDTO dto)
        {
            var errors = await ValidateCita(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var RegistroVisita = new RegistroVisita
            {
                FechaHora = dto.FechaHora,
                Motivo = dto.Motivo,
                Estado = dto.Estado,
                Observaciones = dto.Observaciones,
                RegistroVisitanteId = dto.RegistroVisitanteId,
                AnfitrionId = dto.AnfitrionId
            };

            var created = await _RegistroVisitaRepository.CreateAsync(RegistroVisita);

            return ServiceResult.Ok(MapToDTO(created), "RegistroVisita creada exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, RegistroVisitaDTO dto)
        {
            var existing = await _RegistroVisitaRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"RegistroVisita con ID {id} no encontrada");

            var errors = await ValidateCita(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var RegistroVisita = new RegistroVisita
            {
                Id = id,
                FechaHora = dto.FechaHora,
                Motivo = dto.Motivo,
                Estado = dto.Estado,
                Observaciones = dto.Observaciones,
                RegistroVisitanteId = dto.RegistroVisitanteId,
                AnfitrionId = dto.AnfitrionId
            };

            var updated = await _RegistroVisitaRepository.UpdateAsync(id, RegistroVisita);

            return ServiceResult.Ok(MapToDTO(updated!), "RegistroVisita actualizada exitosamente");
        }

        public async Task<IEnumerable<RegistroVisitaDTO>> GetByPacienteIdAsync(int RegistroVisitanteId)
        {
            var RegistroVisitas = await _RegistroVisitaRepository.GetAllAsync();
            return RegistroVisitas
                .Where(c => c.RegistroVisitanteId == RegistroVisitanteId)
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<RegistroVisitaDTO>> GetAllAsync()
        {
            var RegistroVisitas = await _RegistroVisitaRepository.GetAllAsync();
            return RegistroVisitas.Select(MapToDTO);
        }

        public override async Task<RegistroVisitaDTO?> GetByIdAsync(int id)
        {
            var RegistroVisita = await _RegistroVisitaRepository.GetByIdAsync(id);
            if (RegistroVisita == null) return null;

            return MapToDTO(RegistroVisita);
        }

        private static RegistroVisitaDTO MapToDTO(RegistroVisita entity)
        {
            return new RegistroVisitaDTO
            {
                Id = entity.Id,
                FechaHora = entity.FechaHora,
                Motivo = entity.Motivo,
                Estado = entity.Estado,
                Observaciones = entity.Observaciones,
                RegistroVisitanteId = entity.RegistroVisitanteId,
                AnfitrionId = entity.AnfitrionId
            };
        }

        private async Task<List<string>> ValidateCita(RegistroVisitaDTO dto)
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

            if (dto.RegistroVisitanteId <= 0)
                errors.Add("El ID del RegistroVisitante es requerido");
            else
            {
                var pacienteExists = await _RegistroVisitanteRepository.GetByIdAsync(dto.RegistroVisitanteId);
                if (pacienteExists == null)
                    errors.Add($"El RegistroVisitante con ID {dto.RegistroVisitanteId} no existe");
            }

            if (dto.AnfitrionId <= 0)
                errors.Add("El ID del médico es requerido");
            else
            {
                var medicoExists = await _AnfitrionRepository.GetByIdAsync(dto.AnfitrionId);
                if (medicoExists == null)
                    errors.Add($"El médico con ID {dto.AnfitrionId} no existe");
            }

            return errors;
        }
    }
}
