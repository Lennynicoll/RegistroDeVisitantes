using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Visita;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class VisitaService : BaseService<VisitaDTO>, IVisitaService
    {
        private readonly IVisitaRepository _visitaRepository;
        private readonly IVisitanteRepository _visitanteRepository;

        public VisitaService(IVisitaRepository visitaRepository, IVisitanteRepository visitanteRepository)
        {
            _visitaRepository = visitaRepository;
            _visitanteRepository = visitanteRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(VisitaDTO dto)
        {
            var errors = await ValidateVisita(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var visita = new Visita
            {
                FechaHora = dto.FechaHora,
                Motivo = dto.Motivo,
                Comentarios = dto.Comentarios,
                VisitanteId = dto.VisitanteId
            };

            var created = await _visitaRepository.CreateAsync(visita);

            return ServiceResult.Ok(new VisitaDTO
            {
                Id = created.Id,
                FechaHora = created.FechaHora,
                Motivo = created.Motivo,
                Comentarios = created.Comentarios,
                VisitanteId = created.VisitanteId
            }, "Visita creada exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, VisitaDTO dto)
        {
            var existing = await _visitaRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Visita con ID {id} no encontrada");

            var errors = await ValidateVisita(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var visita = new Visita
            {
                Id = id,
                FechaHora = dto.FechaHora,
                Motivo = dto.Motivo,
                Comentarios = dto.Comentarios,
                VisitanteId = dto.VisitanteId
            };

            var updated = await _visitaRepository.UpdateAsync(id, visita);

            return ServiceResult.Ok(new VisitaDTO
            {
                Id = updated!.Id,
                FechaHora = updated.FechaHora,
                Motivo = updated.Motivo,
                Comentarios = updated.Comentarios,
                VisitanteId = updated.VisitanteId
            }, "Visita actualizada exitosamente");
        }

        public async Task<IEnumerable<VisitaDTO>> GetByVisitanteIdAsync(int visitanteId)
        {
            var visitas = await _visitaRepository.GetAllAsync();
            return visitas
                .Where(v => v.VisitanteId == visitanteId)
                .Select(v => new VisitaDTO
                {
                    Id = v.Id,
                    FechaHora = v.FechaHora,
                    Motivo = v.Motivo,
                    Comentarios = v.Comentarios,
                    VisitanteId = v.VisitanteId
                });
        }

        public override async Task<IEnumerable<VisitaDTO>> GetAllAsync()
        {
            var visitas = await _visitaRepository.GetAllAsync();
            return visitas.Select(v => new VisitaDTO
            {
                Id = v.Id,
                FechaHora = v.FechaHora,
                Motivo = v.Motivo,
                Comentarios = v.Comentarios,
                VisitanteId = v.VisitanteId
            });
        }

        public override async Task<VisitaDTO?> GetByIdAsync(int id)
        {
            var visita = await _visitaRepository.GetByIdAsync(id);
            if (visita == null) return null;

            return new VisitaDTO
            {
                Id = visita.Id,
                FechaHora = visita.FechaHora,
                Motivo = visita.Motivo,
                Comentarios = visita.Comentarios,
                VisitanteId = visita.VisitanteId
            };
        }

        private async Task<List<string>> ValidateVisita(VisitaDTO dto)
        {
            var errors = new List<string>();

            if (dto.FechaHora == default)
                errors.Add("La fecha y hora son requeridas");

            if (string.IsNullOrWhiteSpace(dto.Motivo))
                errors.Add("El motivo es requerido");
            else if (dto.Motivo.Length < 3)
                errors.Add("El motivo debe tener al menos 3 caracteres");

            if (dto.VisitanteId <= 0)
                errors.Add("El ID del visitante es requerido");
            else
            {
                var visitanteExists = await _visitanteRepository.GetByIdAsync(dto.VisitanteId);
                if (visitanteExists == null)
                    errors.Add($"El visitante con ID {dto.VisitanteId} no existe");
            }

            return errors;
        }
    }
}
