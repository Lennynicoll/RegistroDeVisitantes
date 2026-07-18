using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.RecetaMedica;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class RecetaMedicaService : BaseService<RecetaMedicaDTO>, IRecetaMedicaService
    {
        private readonly IRecetaMedicaRepository _recetaMedicaRepository;
        private readonly ICitaRepository _citaRepository;

        public RecetaMedicaService(IRecetaMedicaRepository recetaMedicaRepository, ICitaRepository citaRepository)
        {
            _recetaMedicaRepository = recetaMedicaRepository;
            _citaRepository = citaRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(RecetaMedicaDTO dto)
        {
            var errors = await ValidateRecetaMedica(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var receta = new RecetaMedica
            {
                FechaEmision = dto.FechaEmision,
                Observaciones = dto.Observaciones,
                CitaId = dto.CitaId
            };

            var created = await _recetaMedicaRepository.CreateAsync(receta);

            return ServiceResult.Ok(MapToDTO(created), "Receta médica creada exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, RecetaMedicaDTO dto)
        {
            var existing = await _recetaMedicaRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Receta médica con ID {id} no encontrada");

            var errors = await ValidateRecetaMedica(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var receta = new RecetaMedica
            {
                Id = id,
                FechaEmision = dto.FechaEmision,
                Observaciones = dto.Observaciones,
                CitaId = dto.CitaId
            };

            var updated = await _recetaMedicaRepository.UpdateAsync(id, receta);

            return ServiceResult.Ok(MapToDTO(updated!), "Receta médica actualizada exitosamente");
        }

        public async Task<IEnumerable<RecetaMedicaDTO>> GetByCitaIdAsync(int citaId)
        {
            var recetas = await _recetaMedicaRepository.GetAllAsync();
            return recetas
                .Where(r => r.CitaId == citaId)
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<RecetaMedicaDTO>> GetAllAsync()
        {
            var recetas = await _recetaMedicaRepository.GetAllAsync();
            return recetas.Select(MapToDTO);
        }

        public override async Task<RecetaMedicaDTO?> GetByIdAsync(int id)
        {
            var receta = await _recetaMedicaRepository.GetByIdAsync(id);
            if (receta == null) return null;

            return MapToDTO(receta);
        }

        private static RecetaMedicaDTO MapToDTO(RecetaMedica entity)
        {
            return new RecetaMedicaDTO
            {
                Id = entity.Id,
                FechaEmision = entity.FechaEmision,
                Observaciones = entity.Observaciones,
                CitaId = entity.CitaId
            };
        }

        private async Task<List<string>> ValidateRecetaMedica(RecetaMedicaDTO dto)
        {
            var errors = new List<string>();

            if (dto.FechaEmision == default)
                errors.Add("La fecha de emisión es requerida");

            if (!string.IsNullOrWhiteSpace(dto.Observaciones) && dto.Observaciones.Length > 500)
                errors.Add("Las observaciones no pueden exceder 500 caracteres");

            if (dto.CitaId <= 0)
                errors.Add("El ID de la cita es requerido");
            else
            {
                var citaExists = await _citaRepository.GetByIdAsync(dto.CitaId);
                if (citaExists == null)
                    errors.Add($"La cita con ID {dto.CitaId} no existe");
            }

            return errors;
        }
    }
}
