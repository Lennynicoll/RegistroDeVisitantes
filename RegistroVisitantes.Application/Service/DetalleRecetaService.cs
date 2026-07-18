using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.DetalleReceta;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class DetalleRecetaService : BaseService<DetalleRecetaDTO>, IDetalleRecetaService
    {
        private readonly IDetalleRecetaRepository _detalleRecetaRepository;
        private readonly IRecetaMedicaRepository _recetaMedicaRepository;
        private readonly IMedicamentoRepository _medicamentoRepository;

        public DetalleRecetaService(IDetalleRecetaRepository detalleRecetaRepository, IRecetaMedicaRepository recetaMedicaRepository, IMedicamentoRepository medicamentoRepository)
        {
            _detalleRecetaRepository = detalleRecetaRepository;
            _recetaMedicaRepository = recetaMedicaRepository;
            _medicamentoRepository = medicamentoRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(DetalleRecetaDTO dto)
        {
            var errors = await ValidateDetalleReceta(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var detalle = new DetalleReceta
            {
                Dosis = dto.Dosis,
                Frecuencia = dto.Frecuencia,
                DuracionDias = dto.DuracionDias,
                Indicaciones = dto.Indicaciones,
                RecetaMedicaId = dto.RecetaMedicaId,
                MedicamentoId = dto.MedicamentoId
            };

            var created = await _detalleRecetaRepository.CreateAsync(detalle);

            return ServiceResult.Ok(MapToDTO(created), "Detalle de receta creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, DetalleRecetaDTO dto)
        {
            var existing = await _detalleRecetaRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Detalle de receta con ID {id} no encontrado");

            var errors = await ValidateDetalleReceta(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var detalle = new DetalleReceta
            {
                Id = id,
                Dosis = dto.Dosis,
                Frecuencia = dto.Frecuencia,
                DuracionDias = dto.DuracionDias,
                Indicaciones = dto.Indicaciones,
                RecetaMedicaId = dto.RecetaMedicaId,
                MedicamentoId = dto.MedicamentoId
            };

            var updated = await _detalleRecetaRepository.UpdateAsync(id, detalle);

            return ServiceResult.Ok(MapToDTO(updated!), "Detalle de receta actualizado exitosamente");
        }

        public async Task<IEnumerable<DetalleRecetaDTO>> GetByRecetaMedicaIdAsync(int recetaMedicaId)
        {
            var detalles = await _detalleRecetaRepository.GetAllAsync();
            return detalles
                .Where(d => d.RecetaMedicaId == recetaMedicaId)
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<DetalleRecetaDTO>> GetAllAsync()
        {
            var detalles = await _detalleRecetaRepository.GetAllAsync();
            return detalles.Select(MapToDTO);
        }

        public override async Task<DetalleRecetaDTO?> GetByIdAsync(int id)
        {
            var detalle = await _detalleRecetaRepository.GetByIdAsync(id);
            if (detalle == null) return null;

            return MapToDTO(detalle);
        }

        private static DetalleRecetaDTO MapToDTO(DetalleReceta entity)
        {
            return new DetalleRecetaDTO
            {
                Id = entity.Id,
                Dosis = entity.Dosis,
                Frecuencia = entity.Frecuencia,
                DuracionDias = entity.DuracionDias,
                Indicaciones = entity.Indicaciones,
                RecetaMedicaId = entity.RecetaMedicaId,
                MedicamentoId = entity.MedicamentoId
            };
        }

        private async Task<List<string>> ValidateDetalleReceta(DetalleRecetaDTO dto)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.Dosis))
                errors.Add("La dosis es requerida");
            else if (dto.Dosis.Length > 100)
                errors.Add("La dosis no puede exceder 100 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Frecuencia))
                errors.Add("La frecuencia es requerida");
            else if (dto.Frecuencia.Length > 100)
                errors.Add("La frecuencia no puede exceder 100 caracteres");

            if (dto.DuracionDias <= 0)
                errors.Add("La duración en días debe ser mayor a 0");

            if (!string.IsNullOrWhiteSpace(dto.Indicaciones) && dto.Indicaciones.Length > 500)
                errors.Add("Las indicaciones no pueden exceder 500 caracteres");

            if (dto.RecetaMedicaId <= 0)
                errors.Add("El ID de la receta médica es requerido");
            else
            {
                var recetaExists = await _recetaMedicaRepository.GetByIdAsync(dto.RecetaMedicaId);
                if (recetaExists == null)
                    errors.Add($"La receta médica con ID {dto.RecetaMedicaId} no existe");
            }

            if (dto.MedicamentoId <= 0)
                errors.Add("El ID del medicamento es requerido");
            else
            {
                var medicamentoExists = await _medicamentoRepository.GetByIdAsync(dto.MedicamentoId);
                if (medicamentoExists == null)
                    errors.Add($"El medicamento con ID {dto.MedicamentoId} no existe");
            }

            return errors;
        }
    }
}
