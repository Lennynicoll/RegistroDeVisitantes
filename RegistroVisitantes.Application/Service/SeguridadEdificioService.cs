using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.SeguridadEdificio;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class SeguridadEdificioService : BaseService<SeguridadEdificioDTO>, ISeguridadEdificioService
    {
        private readonly ISeguridadEdificioRepository _SeguridadEdificioRepository;

        public SeguridadEdificioService(ISeguridadEdificioRepository SeguridadEdificioRepository)
        {
            _SeguridadEdificioRepository = SeguridadEdificioRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(SeguridadEdificioDTO dto)
        {
            var errors = ValidateSeguroMedico(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var seguro = new SeguridadEdificio
            {
                Nombre = dto.Nombre,
                Empresa = dto.Empresa,
                Telefono = dto.Telefono,
                Cobertura = dto.Cobertura
            };

            var created = await _SeguridadEdificioRepository.CreateAsync(seguro);

            return ServiceResult.Ok(MapToDTO(created), "Seguro médico creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, SeguridadEdificioDTO dto)
        {
            var existing = await _SeguridadEdificioRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Seguro médico con ID {id} no encontrado");

            var errors = ValidateSeguroMedico(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var seguro = new SeguridadEdificio
            {
                Id = id,
                Nombre = dto.Nombre,
                Empresa = dto.Empresa,
                Telefono = dto.Telefono,
                Cobertura = dto.Cobertura
            };

            var updated = await _SeguridadEdificioRepository.UpdateAsync(id, seguro);

            return ServiceResult.Ok(MapToDTO(updated!), "Seguro médico actualizado exitosamente");
        }

        public override async Task<IEnumerable<SeguridadEdificioDTO>> GetAllAsync()
        {
            var seguros = await _SeguridadEdificioRepository.GetAllAsync();
            return seguros.Select(MapToDTO);
        }

        public override async Task<SeguridadEdificioDTO?> GetByIdAsync(int id)
        {
            var seguro = await _SeguridadEdificioRepository.GetByIdAsync(id);
            if (seguro == null) return null;

            return MapToDTO(seguro);
        }

        private static SeguridadEdificioDTO MapToDTO(SeguridadEdificio entity)
        {
            return new SeguridadEdificioDTO
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Empresa = entity.Empresa,
                Telefono = entity.Telefono,
                Cobertura = entity.Cobertura
            };
        }

        private List<string> ValidateSeguroMedico(SeguridadEdificioDTO dto)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                errors.Add("El nombre es requerido");
            else if (dto.Nombre.Length > 150)
                errors.Add("El nombre no puede exceder 150 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Empresa))
                errors.Add("La empresa es requerida");
            else if (dto.Empresa.Length > 150)
                errors.Add("La empresa no puede exceder 150 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Telefono))
                errors.Add("El teléfono es requerido");
            else if (dto.Telefono.Length < 7)
                errors.Add("El teléfono debe tener al menos 7 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Cobertura))
                errors.Add("La cobertura es requerida");
            else if (dto.Cobertura.Length > 200)
                errors.Add("La cobertura no puede exceder 200 caracteres");

            return errors;
        }
    }
}
