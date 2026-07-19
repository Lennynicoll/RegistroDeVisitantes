using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Oficina;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class OficinaService : BaseService<OficinaDTO>, IOficinaService
    {
        private readonly IOficinaRepository _OficinaRepository;

        public OficinaService(IOficinaRepository OficinaRepository)
        {
            _OficinaRepository = OficinaRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(OficinaDTO dto)
        {
            var errors = ValidateMedicamento(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var Oficina = new Oficina
            {
                Nombre = dto.Nombre,
                Fabricante = dto.Fabricante,
                Forma = dto.Forma,
                Concentracion = dto.Concentracion,
                Indicaciones = dto.Indicaciones,
                Precio = dto.Precio,
                Stock = dto.Stock
            };

            var created = await _OficinaRepository.CreateAsync(Oficina);

            return ServiceResult.Ok(MapToDTO(created), "Oficina creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, OficinaDTO dto)
        {
            var existing = await _OficinaRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Oficina con ID {id} no encontrado");

            var errors = ValidateMedicamento(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var Oficina = new Oficina
            {
                Id = id,
                Nombre = dto.Nombre,
                Fabricante = dto.Fabricante,
                Forma = dto.Forma,
                Concentracion = dto.Concentracion,
                Indicaciones = dto.Indicaciones,
                Precio = dto.Precio,
                Stock = dto.Stock
            };

            var updated = await _OficinaRepository.UpdateAsync(id, Oficina);

            return ServiceResult.Ok(MapToDTO(updated!), "Oficina actualizado exitosamente");
        }

        public async Task<IEnumerable<OficinaDTO>> SearchByNameAsync(string nombre)
        {
            var Oficinas = await _OficinaRepository.GetAllAsync();
            return Oficinas
                .Where(m => m.Nombre.Contains(nombre))
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<OficinaDTO>> GetAllAsync()
        {
            var Oficinas = await _OficinaRepository.GetAllAsync();
            return Oficinas.Select(MapToDTO);
        }

        public override async Task<OficinaDTO?> GetByIdAsync(int id)
        {
            var Oficina = await _OficinaRepository.GetByIdAsync(id);
            if (Oficina == null) return null;

            return MapToDTO(Oficina);
        }

        private static OficinaDTO MapToDTO(Oficina entity)
        {
            return new OficinaDTO
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Fabricante = entity.Fabricante,
                Forma = entity.Forma,
                Concentracion = entity.Concentracion,
                Indicaciones = entity.Indicaciones,
                Precio = entity.Precio,
                Stock = entity.Stock
            };
        }

        private List<string> ValidateMedicamento(OficinaDTO dto)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                errors.Add("El nombre es requerido");
            else if (dto.Nombre.Length > 150)
                errors.Add("El nombre no puede exceder 150 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Fabricante))
                errors.Add("El fabricante es requerido");
            else if (dto.Fabricante.Length > 150)
                errors.Add("El fabricante no puede exceder 150 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Forma))
                errors.Add("La forma es requerida");
            else if (dto.Forma.Length > 50)
                errors.Add("La forma no puede exceder 50 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Concentracion))
                errors.Add("La concentración es requerida");
            else if (dto.Concentracion.Length > 50)
                errors.Add("La concentración no puede exceder 50 caracteres");

            if (!string.IsNullOrWhiteSpace(dto.Indicaciones) && dto.Indicaciones.Length > 500)
                errors.Add("Las indicaciones no pueden exceder 500 caracteres");

            if (dto.Precio < 0)
                errors.Add("El precio no puede ser negativo");

            if (dto.Stock < 0)
                errors.Add("El stock no puede ser negativo");

            return errors;
        }
    }
}
