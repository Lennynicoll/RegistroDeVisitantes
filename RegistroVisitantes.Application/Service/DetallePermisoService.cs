using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.DetallePermiso;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class DetallePermisoService : BaseService<DetallePermisoDTO>, IDetallePermisoService
    {
        private readonly IDetallePermisoRepository _DetallePermisoRepository;
        private readonly IPermisoVisitaRepository _PermisoVisitaRepository;
        private readonly IOficinaRepository _OficinaRepository;

        public DetallePermisoService(IDetallePermisoRepository DetallePermisoRepository, IPermisoVisitaRepository PermisoVisitaRepository, IOficinaRepository OficinaRepository)
        {
            _DetallePermisoRepository = DetallePermisoRepository;
            _PermisoVisitaRepository = PermisoVisitaRepository;
            _OficinaRepository = OficinaRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(DetallePermisoDTO dto)
        {
            var errors = await ValidateDetalleReceta(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var detalle = new DetallePermiso
            {
                Dosis = dto.Dosis,
                Frecuencia = dto.Frecuencia,
                DuracionDias = dto.DuracionDias,
                Indicaciones = dto.Indicaciones,
                PermisoVisitaId = dto.PermisoVisitaId,
                OficinaId = dto.OficinaId
            };

            var created = await _DetallePermisoRepository.CreateAsync(detalle);

            return ServiceResult.Ok(MapToDTO(created), "Detalle de receta creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, DetallePermisoDTO dto)
        {
            var existing = await _DetallePermisoRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Detalle de receta con ID {id} no encontrado");

            var errors = await ValidateDetalleReceta(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var detalle = new DetallePermiso
            {
                Id = id,
                Dosis = dto.Dosis,
                Frecuencia = dto.Frecuencia,
                DuracionDias = dto.DuracionDias,
                Indicaciones = dto.Indicaciones,
                PermisoVisitaId = dto.PermisoVisitaId,
                OficinaId = dto.OficinaId
            };

            var updated = await _DetallePermisoRepository.UpdateAsync(id, detalle);

            return ServiceResult.Ok(MapToDTO(updated!), "Detalle de receta actualizado exitosamente");
        }

        public async Task<IEnumerable<DetallePermisoDTO>> GetByRecetaMedicaIdAsync(int PermisoVisitaId)
        {
            var detalles = await _DetallePermisoRepository.GetAllAsync();
            return detalles
                .Where(d => d.PermisoVisitaId == PermisoVisitaId)
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<DetallePermisoDTO>> GetAllAsync()
        {
            var detalles = await _DetallePermisoRepository.GetAllAsync();
            return detalles.Select(MapToDTO);
        }

        public override async Task<DetallePermisoDTO?> GetByIdAsync(int id)
        {
            var detalle = await _DetallePermisoRepository.GetByIdAsync(id);
            if (detalle == null) return null;

            return MapToDTO(detalle);
        }

        private static DetallePermisoDTO MapToDTO(DetallePermiso entity)
        {
            return new DetallePermisoDTO
            {
                Id = entity.Id,
                Dosis = entity.Dosis,
                Frecuencia = entity.Frecuencia,
                DuracionDias = entity.DuracionDias,
                Indicaciones = entity.Indicaciones,
                PermisoVisitaId = entity.PermisoVisitaId,
                OficinaId = entity.OficinaId
            };
        }

        private async Task<List<string>> ValidateDetalleReceta(DetallePermisoDTO dto)
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

            if (dto.PermisoVisitaId <= 0)
                errors.Add("El ID de la receta médica es requerido");
            else
            {
                var recetaExists = await _PermisoVisitaRepository.GetByIdAsync(dto.PermisoVisitaId);
                if (recetaExists == null)
                    errors.Add($"La receta médica con ID {dto.PermisoVisitaId} no existe");
            }

            if (dto.OficinaId <= 0)
                errors.Add("El ID del Oficina es requerido");
            else
            {
                var medicamentoExists = await _OficinaRepository.GetByIdAsync(dto.OficinaId);
                if (medicamentoExists == null)
                    errors.Add($"El Oficina con ID {dto.OficinaId} no existe");
            }

            return errors;
        }
    }
}
