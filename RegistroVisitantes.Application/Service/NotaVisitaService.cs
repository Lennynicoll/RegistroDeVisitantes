using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.NotaVisita;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class NotaVisitaService : BaseService<NotaVisitaDTO>, INotaVisitaService
    {
        private readonly INotaVisitaRepository _NotaVisitaRepository;

        public NotaVisitaService(INotaVisitaRepository NotaVisitaRepository)
        {
            _NotaVisitaRepository = NotaVisitaRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(NotaVisitaDTO dto)
        {
            var errors = ValidateNotaVisita(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var NotaVisita = new NotaVisita
            {
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            var created = await _NotaVisitaRepository.CreateAsync(NotaVisita);

            return ServiceResult.Ok(MapToDTO(created), "Diagnóstico creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, NotaVisitaDTO dto)
        {
            var existing = await _NotaVisitaRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Diagnóstico con ID {id} no encontrado");

            var errors = ValidateNotaVisita(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var NotaVisita = new NotaVisita
            {
                Id = id,
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            var updated = await _NotaVisitaRepository.UpdateAsync(id, NotaVisita);

            return ServiceResult.Ok(MapToDTO(updated!), "Diagnóstico actualizado exitosamente");
        }

        public async Task<IEnumerable<NotaVisitaDTO>> SearchByCodigoAsync(string codigo)
        {
            var NotasVisita = await _NotaVisitaRepository.GetAllAsync();
            return NotasVisita
                .Where(d => d.Codigo.Contains(codigo))
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<NotaVisitaDTO>> GetAllAsync()
        {
            var NotasVisita = await _NotaVisitaRepository.GetAllAsync();
            return NotasVisita.Select(MapToDTO);
        }

        public override async Task<NotaVisitaDTO?> GetByIdAsync(int id)
        {
            var NotaVisita = await _NotaVisitaRepository.GetByIdAsync(id);
            if (NotaVisita == null) return null;

            return MapToDTO(NotaVisita);
        }

        private static NotaVisitaDTO MapToDTO(NotaVisita entity)
        {
            return new NotaVisitaDTO
            {
                Id = entity.Id,
                Codigo = entity.Codigo,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion
            };
        }

        private List<string> ValidateNotaVisita(NotaVisitaDTO dto)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.Codigo))
                errors.Add("El código es requerido");
            else if (dto.Codigo.Length > 20)
                errors.Add("El código no puede exceder 20 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                errors.Add("El nombre es requerido");
            else if (dto.Nombre.Length > 150)
                errors.Add("El nombre no puede exceder 150 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                errors.Add("La descripción es requerida");
            else if (dto.Descripcion.Length > 500)
                errors.Add("La descripción no puede exceder 500 caracteres");

            return errors;
        }
    }
}
