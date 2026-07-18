using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Medicamento;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class MedicamentoService : BaseService<MedicamentoDTO>, IMedicamentoService
    {
        private readonly IMedicamentoRepository _medicamentoRepository;

        public MedicamentoService(IMedicamentoRepository medicamentoRepository)
        {
            _medicamentoRepository = medicamentoRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(MedicamentoDTO dto)
        {
            var errors = ValidateMedicamento(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var medicamento = new Medicamento
            {
                Nombre = dto.Nombre,
                Fabricante = dto.Fabricante,
                Forma = dto.Forma,
                Concentracion = dto.Concentracion,
                Indicaciones = dto.Indicaciones,
                Precio = dto.Precio,
                Stock = dto.Stock
            };

            var created = await _medicamentoRepository.CreateAsync(medicamento);

            return ServiceResult.Ok(MapToDTO(created), "Medicamento creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, MedicamentoDTO dto)
        {
            var existing = await _medicamentoRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Medicamento con ID {id} no encontrado");

            var errors = ValidateMedicamento(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var medicamento = new Medicamento
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

            var updated = await _medicamentoRepository.UpdateAsync(id, medicamento);

            return ServiceResult.Ok(MapToDTO(updated!), "Medicamento actualizado exitosamente");
        }

        public async Task<IEnumerable<MedicamentoDTO>> SearchByNameAsync(string nombre)
        {
            var medicamentos = await _medicamentoRepository.GetAllAsync();
            return medicamentos
                .Where(m => m.Nombre.Contains(nombre))
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<MedicamentoDTO>> GetAllAsync()
        {
            var medicamentos = await _medicamentoRepository.GetAllAsync();
            return medicamentos.Select(MapToDTO);
        }

        public override async Task<MedicamentoDTO?> GetByIdAsync(int id)
        {
            var medicamento = await _medicamentoRepository.GetByIdAsync(id);
            if (medicamento == null) return null;

            return MapToDTO(medicamento);
        }

        private static MedicamentoDTO MapToDTO(Medicamento entity)
        {
            return new MedicamentoDTO
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

        private List<string> ValidateMedicamento(MedicamentoDTO dto)
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
