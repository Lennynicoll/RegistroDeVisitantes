using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.PermisoVisita;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class PermisoVisitaService : BaseService<PermisoVisitaDTO>, IPermisoVisitaService
    {
        private readonly IPermisoVisitaRepository _PermisoVisitaRepository;
        private readonly IRegistroVisitaRepository _RegistroVisitaRepository;

        public PermisoVisitaService(IPermisoVisitaRepository PermisoVisitaRepository, IRegistroVisitaRepository RegistroVisitaRepository)
        {
            _PermisoVisitaRepository = PermisoVisitaRepository;
            _RegistroVisitaRepository = RegistroVisitaRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(PermisoVisitaDTO dto)
        {
            var errors = await ValidateRecetaMedica(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var receta = new PermisoVisita
            {
                FechaEmision = dto.FechaEmision,
                Observaciones = dto.Observaciones,
                RegistroVisitaId = dto.RegistroVisitaId
            };

            var created = await _PermisoVisitaRepository.CreateAsync(receta);

            return ServiceResult.Ok(MapToDTO(created), "Receta médica creada exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, PermisoVisitaDTO dto)
        {
            var existing = await _PermisoVisitaRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Receta médica con ID {id} no encontrada");

            var errors = await ValidateRecetaMedica(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var receta = new PermisoVisita
            {
                Id = id,
                FechaEmision = dto.FechaEmision,
                Observaciones = dto.Observaciones,
                RegistroVisitaId = dto.RegistroVisitaId
            };

            var updated = await _PermisoVisitaRepository.UpdateAsync(id, receta);

            return ServiceResult.Ok(MapToDTO(updated!), "Receta médica actualizada exitosamente");
        }

        public async Task<IEnumerable<PermisoVisitaDTO>> GetByCitaIdAsync(int RegistroVisitaId)
        {
            var recetas = await _PermisoVisitaRepository.GetAllAsync();
            return recetas
                .Where(r => r.RegistroVisitaId == RegistroVisitaId)
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<PermisoVisitaDTO>> GetAllAsync()
        {
            var recetas = await _PermisoVisitaRepository.GetAllAsync();
            return recetas.Select(MapToDTO);
        }

        public override async Task<PermisoVisitaDTO?> GetByIdAsync(int id)
        {
            var receta = await _PermisoVisitaRepository.GetByIdAsync(id);
            if (receta == null) return null;

            return MapToDTO(receta);
        }

        private static PermisoVisitaDTO MapToDTO(PermisoVisita entity)
        {
            return new PermisoVisitaDTO
            {
                Id = entity.Id,
                FechaEmision = entity.FechaEmision,
                Observaciones = entity.Observaciones,
                RegistroVisitaId = entity.RegistroVisitaId
            };
        }

        private async Task<List<string>> ValidateRecetaMedica(PermisoVisitaDTO dto)
        {
            var errors = new List<string>();

            if (dto.FechaEmision == default)
                errors.Add("La fecha de emisión es requerida");

            if (!string.IsNullOrWhiteSpace(dto.Observaciones) && dto.Observaciones.Length > 500)
                errors.Add("Las observaciones no pueden exceder 500 caracteres");

            if (dto.RegistroVisitaId <= 0)
                errors.Add("El ID de la RegistroVisita es requerido");
            else
            {
                var citaExists = await _RegistroVisitaRepository.GetByIdAsync(dto.RegistroVisitaId);
                if (citaExists == null)
                    errors.Add($"La RegistroVisita con ID {dto.RegistroVisitaId} no existe");
            }

            return errors;
        }
    }
}
