using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Departamento;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class DepartamentoService : BaseService<DepartamentoDTO>, IDepartamentoService
    {
        private readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoService(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(DepartamentoDTO dto)
        {
            var errors = ValidateDepartamento(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var departamento = new Departamento
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Ubicacion = dto.Ubicacion
            };

            var created = await _departamentoRepository.CreateAsync(departamento);

            return ServiceResult.Ok(MapToDTO(created), "Departamento creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, DepartamentoDTO dto)
        {
            var existing = await _departamentoRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Departamento con ID {id} no encontrado");

            var errors = ValidateDepartamento(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var departamento = new Departamento
            {
                Id = id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Ubicacion = dto.Ubicacion
            };

            var updated = await _departamentoRepository.UpdateAsync(id, departamento);

            return ServiceResult.Ok(MapToDTO(updated!), "Departamento actualizado exitosamente");
        }

        public override async Task<IEnumerable<DepartamentoDTO>> GetAllAsync()
        {
            var departamentos = await _departamentoRepository.GetAllAsync();
            return departamentos.Select(MapToDTO);
        }

        public override async Task<DepartamentoDTO?> GetByIdAsync(int id)
        {
            var departamento = await _departamentoRepository.GetByIdAsync(id);
            if (departamento == null) return null;

            return MapToDTO(departamento);
        }

        private static DepartamentoDTO MapToDTO(Departamento entity)
        {
            return new DepartamentoDTO
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion,
                Ubicacion = entity.Ubicacion
            };
        }

        private List<string> ValidateDepartamento(DepartamentoDTO dto)
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

            if (string.IsNullOrWhiteSpace(dto.Ubicacion))
                errors.Add("La ubicación es requerida");
            else if (dto.Ubicacion.Length > 200)
                errors.Add("La ubicación no puede exceder 200 caracteres");

            return errors;
        }
    }
}
