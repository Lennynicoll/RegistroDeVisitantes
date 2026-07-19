using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.VisitanteSeguro;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class VisitanteSeguroService : BaseService<VisitanteSeguroDTO>, IVisitanteSeguroService
    {
        private readonly IVisitanteSeguroRepository _VisitanteSeguroRepository;
        private readonly IRegistroVisitanteRepository _RegistroVisitanteRepository;
        private readonly ISeguridadEdificioRepository _SeguridadEdificioRepository;

        public VisitanteSeguroService(IVisitanteSeguroRepository VisitanteSeguroRepository, IRegistroVisitanteRepository RegistroVisitanteRepository, ISeguridadEdificioRepository SeguridadEdificioRepository)
        {
            _VisitanteSeguroRepository = VisitanteSeguroRepository;
            _RegistroVisitanteRepository = RegistroVisitanteRepository;
            _SeguridadEdificioRepository = SeguridadEdificioRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(VisitanteSeguroDTO dto)
        {
            var errors = await ValidateVisitanteSeguro(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var VisitanteSeguro = new VisitanteSeguro
            {
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                NumeroPoliza = dto.NumeroPoliza,
                RegistroVisitanteId = dto.RegistroVisitanteId,
                SeguridadEdificioId = dto.SeguridadEdificioId
            };

            var created = await _VisitanteSeguroRepository.CreateAsync(VisitanteSeguro);

            return ServiceResult.Ok(MapToDTO(created), "Seguro de RegistroVisitante creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, VisitanteSeguroDTO dto)
        {
            var existing = await _VisitanteSeguroRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Seguro de RegistroVisitante con ID {id} no encontrado");

            var errors = await ValidateVisitanteSeguro(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var VisitanteSeguro = new VisitanteSeguro
            {
                Id = id,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                NumeroPoliza = dto.NumeroPoliza,
                RegistroVisitanteId = dto.RegistroVisitanteId,
                SeguridadEdificioId = dto.SeguridadEdificioId
            };

            var updated = await _VisitanteSeguroRepository.UpdateAsync(id, VisitanteSeguro);

            return ServiceResult.Ok(MapToDTO(updated!), "Seguro de RegistroVisitante actualizado exitosamente");
        }

        public async Task<IEnumerable<VisitanteSeguroDTO>> GetByPacienteIdAsync(int RegistroVisitanteId)
        {
            var seguros = await _VisitanteSeguroRepository.GetAllAsync();
            return seguros
                .Where(s => s.RegistroVisitanteId == RegistroVisitanteId)
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<VisitanteSeguroDTO>> GetAllAsync()
        {
            var seguros = await _VisitanteSeguroRepository.GetAllAsync();
            return seguros.Select(MapToDTO);
        }

        public override async Task<VisitanteSeguroDTO?> GetByIdAsync(int id)
        {
            var seguro = await _VisitanteSeguroRepository.GetByIdAsync(id);
            if (seguro == null) return null;

            return MapToDTO(seguro);
        }

        private static VisitanteSeguroDTO MapToDTO(VisitanteSeguro entity)
        {
            return new VisitanteSeguroDTO
            {
                Id = entity.Id,
                FechaInicio = entity.FechaInicio,
                FechaFin = entity.FechaFin,
                NumeroPoliza = entity.NumeroPoliza,
                RegistroVisitanteId = entity.RegistroVisitanteId,
                SeguridadEdificioId = entity.SeguridadEdificioId
            };
        }

        private async Task<List<string>> ValidateVisitanteSeguro(VisitanteSeguroDTO dto)
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

            if (dto.RegistroVisitanteId <= 0)
                errors.Add("El ID del RegistroVisitante es requerido");
            else
            {
                var pacienteExists = await _RegistroVisitanteRepository.GetByIdAsync(dto.RegistroVisitanteId);
                if (pacienteExists == null)
                    errors.Add($"El RegistroVisitante con ID {dto.RegistroVisitanteId} no existe");
            }

            if (dto.SeguridadEdificioId <= 0)
                errors.Add("El ID del seguro médico es requerido");
            else
            {
                var seguroExists = await _SeguridadEdificioRepository.GetByIdAsync(dto.SeguridadEdificioId);
                if (seguroExists == null)
                    errors.Add($"El seguro médico con ID {dto.SeguridadEdificioId} no existe");
            }

            return errors;
        }
    }
}
