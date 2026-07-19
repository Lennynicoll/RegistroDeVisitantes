using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.HistorialVisitas;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class HistorialVisitasService : BaseService<HistorialVisitasDTO>, IHistorialVisitasService
    {
        private readonly IHistorialVisitasRepository _HistorialVisitasRepository;
        private readonly IRegistroVisitanteRepository _RegistroVisitanteRepository;
        private readonly INotaVisitaRepository _NotaVisitaRepository;

        public HistorialVisitasService(IHistorialVisitasRepository HistorialVisitasRepository, IRegistroVisitanteRepository RegistroVisitanteRepository, INotaVisitaRepository NotaVisitaRepository)
        {
            _HistorialVisitasRepository = HistorialVisitasRepository;
            _RegistroVisitanteRepository = RegistroVisitanteRepository;
            _NotaVisitaRepository = NotaVisitaRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(HistorialVisitasDTO dto)
        {
            var errors = await ValidateHistorialVisitas(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var historial = new HistorialVisitas
            {
                Fecha = dto.Fecha,
                Descripcion = dto.Descripcion,
                Observaciones = dto.Observaciones,
                RegistroVisitanteId = dto.RegistroVisitanteId,
                NotaVisitaId = dto.NotaVisitaId
            };

            var created = await _HistorialVisitasRepository.CreateAsync(historial);

            return ServiceResult.Ok(MapToDTO(created), "Historial clínico creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, HistorialVisitasDTO dto)
        {
            var existing = await _HistorialVisitasRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Historial clínico con ID {id} no encontrado");

            var errors = await ValidateHistorialVisitas(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var historial = new HistorialVisitas
            {
                Id = id,
                Fecha = dto.Fecha,
                Descripcion = dto.Descripcion,
                Observaciones = dto.Observaciones,
                RegistroVisitanteId = dto.RegistroVisitanteId,
                NotaVisitaId = dto.NotaVisitaId
            };

            var updated = await _HistorialVisitasRepository.UpdateAsync(id, historial);

            return ServiceResult.Ok(MapToDTO(updated!), "Historial clínico actualizado exitosamente");
        }

        public async Task<IEnumerable<HistorialVisitasDTO>> GetByPacienteIdAsync(int RegistroVisitanteId)
        {
            var historiales = await _HistorialVisitasRepository.GetAllAsync();
            return historiales
                .Where(h => h.RegistroVisitanteId == RegistroVisitanteId)
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<HistorialVisitasDTO>> GetAllAsync()
        {
            var historiales = await _HistorialVisitasRepository.GetAllAsync();
            return historiales.Select(MapToDTO);
        }

        public override async Task<HistorialVisitasDTO?> GetByIdAsync(int id)
        {
            var historial = await _HistorialVisitasRepository.GetByIdAsync(id);
            if (historial == null) return null;

            return MapToDTO(historial);
        }

        private static HistorialVisitasDTO MapToDTO(HistorialVisitas entity)
        {
            return new HistorialVisitasDTO
            {
                Id = entity.Id,
                Fecha = entity.Fecha,
                Descripcion = entity.Descripcion,
                Observaciones = entity.Observaciones,
                RegistroVisitanteId = entity.RegistroVisitanteId,
                NotaVisitaId = entity.NotaVisitaId
            };
        }

        private async Task<List<string>> ValidateHistorialVisitas(HistorialVisitasDTO dto)
        {
            var errors = new List<string>();

            if (dto.Fecha == default)
                errors.Add("La fecha es requerida");

            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                errors.Add("La descripción es requerida");
            else if (dto.Descripcion.Length < 3)
                errors.Add("La descripción debe tener al menos 3 caracteres");
            else if (dto.Descripcion.Length > 1000)
                errors.Add("La descripción no puede exceder 1000 caracteres");

            if (!string.IsNullOrWhiteSpace(dto.Observaciones) && dto.Observaciones.Length > 1000)
                errors.Add("Las observaciones no pueden exceder 1000 caracteres");

            if (dto.RegistroVisitanteId <= 0)
                errors.Add("El ID del RegistroVisitante es requerido");
            else
            {
                var pacienteExists = await _RegistroVisitanteRepository.GetByIdAsync(dto.RegistroVisitanteId);
                if (pacienteExists == null)
                    errors.Add($"El RegistroVisitante con ID {dto.RegistroVisitanteId} no existe");
            }

            if (dto.NotaVisitaId <= 0)
                errors.Add("El ID del diagnóstico es requerido");
            else
            {
                var diagnosticoExists = await _NotaVisitaRepository.GetByIdAsync(dto.NotaVisitaId);
                if (diagnosticoExists == null)
                    errors.Add($"El diagnóstico con ID {dto.NotaVisitaId} no existe");
            }

            return errors;
        }
    }
}
