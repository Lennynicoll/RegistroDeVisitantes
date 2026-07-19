using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.MotivoVisita;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class MotivoVisitaService : BaseService<MotivoVisitaDTO>, IMotivoVisitaService
    {
        private readonly IMotivoVisitaRepository _MotivoVisitaRepository;

        public MotivoVisitaService(IMotivoVisitaRepository MotivoVisitaRepository)
        {
            _MotivoVisitaRepository = MotivoVisitaRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(MotivoVisitaDTO dto)
        {
            var errors = ValidateEspecialidad(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var MotivoVisita = new MotivoVisita
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            var created = await _MotivoVisitaRepository.CreateAsync(MotivoVisita);

            return ServiceResult.Ok(MapToDTO(created), "MotivoVisita creada exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, MotivoVisitaDTO dto)
        {
            var existing = await _MotivoVisitaRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"MotivoVisita con ID {id} no encontrada");

            var errors = ValidateEspecialidad(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var MotivoVisita = new MotivoVisita
            {
                Id = id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            var updated = await _MotivoVisitaRepository.UpdateAsync(id, MotivoVisita);

            return ServiceResult.Ok(MapToDTO(updated!), "MotivoVisita actualizada exitosamente");
        }

        public override async Task<IEnumerable<MotivoVisitaDTO>> GetAllAsync()
        {
            var MotivosVisita = await _MotivoVisitaRepository.GetAllAsync();
            return MotivosVisita.Select(MapToDTO);
        }

        public override async Task<MotivoVisitaDTO?> GetByIdAsync(int id)
        {
            var MotivoVisita = await _MotivoVisitaRepository.GetByIdAsync(id);
            if (MotivoVisita == null) return null;

            return MapToDTO(MotivoVisita);
        }

        private static MotivoVisitaDTO MapToDTO(MotivoVisita entity)
        {
            return new MotivoVisitaDTO
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion
            };
        }

        private List<string> ValidateEspecialidad(MotivoVisitaDTO dto)
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
