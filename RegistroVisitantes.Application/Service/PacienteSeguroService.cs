using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.PacienteSeguro;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class PacienteSeguroService : BaseService<PacienteSeguroDTO>, IPacienteSeguroService
    {
        private readonly IPacienteSeguroRepository _pacienteSeguroRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ISeguroMedicoRepository _seguroMedicoRepository;

        public PacienteSeguroService(IPacienteSeguroRepository pacienteSeguroRepository, IPacienteRepository pacienteRepository, ISeguroMedicoRepository seguroMedicoRepository)
        {
            _pacienteSeguroRepository = pacienteSeguroRepository;
            _pacienteRepository = pacienteRepository;
            _seguroMedicoRepository = seguroMedicoRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(PacienteSeguroDTO dto)
        {
            var errors = await ValidatePacienteSeguro(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var pacienteSeguro = new PacienteSeguro
            {
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                NumeroPoliza = dto.NumeroPoliza,
                PacienteId = dto.PacienteId,
                SeguroMedicoId = dto.SeguroMedicoId
            };

            var created = await _pacienteSeguroRepository.CreateAsync(pacienteSeguro);

            return ServiceResult.Ok(MapToDTO(created), "Seguro de paciente creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, PacienteSeguroDTO dto)
        {
            var existing = await _pacienteSeguroRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Seguro de paciente con ID {id} no encontrado");

            var errors = await ValidatePacienteSeguro(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var pacienteSeguro = new PacienteSeguro
            {
                Id = id,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                NumeroPoliza = dto.NumeroPoliza,
                PacienteId = dto.PacienteId,
                SeguroMedicoId = dto.SeguroMedicoId
            };

            var updated = await _pacienteSeguroRepository.UpdateAsync(id, pacienteSeguro);

            return ServiceResult.Ok(MapToDTO(updated!), "Seguro de paciente actualizado exitosamente");
        }

        public async Task<IEnumerable<PacienteSeguroDTO>> GetByPacienteIdAsync(int pacienteId)
        {
            var seguros = await _pacienteSeguroRepository.GetAllAsync();
            return seguros
                .Where(s => s.PacienteId == pacienteId)
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<PacienteSeguroDTO>> GetAllAsync()
        {
            var seguros = await _pacienteSeguroRepository.GetAllAsync();
            return seguros.Select(MapToDTO);
        }

        public override async Task<PacienteSeguroDTO?> GetByIdAsync(int id)
        {
            var seguro = await _pacienteSeguroRepository.GetByIdAsync(id);
            if (seguro == null) return null;

            return MapToDTO(seguro);
        }

        private static PacienteSeguroDTO MapToDTO(PacienteSeguro entity)
        {
            return new PacienteSeguroDTO
            {
                Id = entity.Id,
                FechaInicio = entity.FechaInicio,
                FechaFin = entity.FechaFin,
                NumeroPoliza = entity.NumeroPoliza,
                PacienteId = entity.PacienteId,
                SeguroMedicoId = entity.SeguroMedicoId
            };
        }

        private async Task<List<string>> ValidatePacienteSeguro(PacienteSeguroDTO dto)
        {
            var errors = new List<string>();

            if (dto.FechaInicio == default)
                errors.Add("La fecha de inicio es requerida");

            if (dto.FechaFin == default)
                errors.Add("La fecha de fin es requerida");
            else if (dto.FechaFin <= dto.FechaInicio)
                errors.Add("La fecha de fin debe ser posterior a la fecha de inicio");

            if (string.IsNullOrWhiteSpace(dto.NumeroPoliza))
                errors.Add("El número de póliza es requerido");
            else if (dto.NumeroPoliza.Length > 50)
                errors.Add("El número de póliza no puede exceder 50 caracteres");

            if (dto.PacienteId <= 0)
                errors.Add("El ID del paciente es requerido");
            else
            {
                var pacienteExists = await _pacienteRepository.GetByIdAsync(dto.PacienteId);
                if (pacienteExists == null)
                    errors.Add($"El paciente con ID {dto.PacienteId} no existe");
            }

            if (dto.SeguroMedicoId <= 0)
                errors.Add("El ID del seguro médico es requerido");
            else
            {
                var seguroExists = await _seguroMedicoRepository.GetByIdAsync(dto.SeguroMedicoId);
                if (seguroExists == null)
                    errors.Add($"El seguro médico con ID {dto.SeguroMedicoId} no existe");
            }

            return errors;
        }
    }
}
