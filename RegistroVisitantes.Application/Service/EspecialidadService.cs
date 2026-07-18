using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Especialidad;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class EspecialidadService : BaseService<EspecialidadDTO>, IEspecialidadService
    {
        private readonly IEspecialidadRepository _especialidadRepository;

        public EspecialidadService(IEspecialidadRepository especialidadRepository)
        {
            _especialidadRepository = especialidadRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(EspecialidadDTO dto)
        {
            var errors = ValidateEspecialidad(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var especialidad = new Especialidad
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            var created = await _especialidadRepository.CreateAsync(especialidad);

            return ServiceResult.Ok(MapToDTO(created), "Especialidad creada exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, EspecialidadDTO dto)
        {
            var existing = await _especialidadRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Especialidad con ID {id} no encontrada");

            var errors = ValidateEspecialidad(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var especialidad = new Especialidad
            {
                Id = id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            var updated = await _especialidadRepository.UpdateAsync(id, especialidad);

            return ServiceResult.Ok(MapToDTO(updated!), "Especialidad actualizada exitosamente");
        }

        public override async Task<IEnumerable<EspecialidadDTO>> GetAllAsync()
        {
            var especialidades = await _especialidadRepository.GetAllAsync();
            return especialidades.Select(MapToDTO);
        }

        public override async Task<EspecialidadDTO?> GetByIdAsync(int id)
        {
            var especialidad = await _especialidadRepository.GetByIdAsync(id);
            if (especialidad == null) return null;

            return MapToDTO(especialidad);
        }

        private static EspecialidadDTO MapToDTO(Especialidad entity)
        {
            return new EspecialidadDTO
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion
            };
        }

        private List<string> ValidateEspecialidad(EspecialidadDTO dto)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                errors.Add("El nombre es requerido");
            else if (dto.Nombre.Length > 100)
                errors.Add("El nombre no puede exceder 100 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                errors.Add("La descripción es requerida");
            else if (dto.Descripcion.Length > 500)
                errors.Add("La descripción no puede exceder 500 caracteres");

            return errors;
        }
    }
}
