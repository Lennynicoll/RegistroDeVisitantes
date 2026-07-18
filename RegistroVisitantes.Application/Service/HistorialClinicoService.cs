using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.HistorialClinico;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class HistorialClinicoService : BaseService<HistorialClinicoDTO>, IHistorialClinicoService
    {
        private readonly IHistorialClinicoRepository _historialClinicoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IDiagnosticoRepository _diagnosticoRepository;

        public HistorialClinicoService(IHistorialClinicoRepository historialClinicoRepository, IPacienteRepository pacienteRepository, IDiagnosticoRepository diagnosticoRepository)
        {
            _historialClinicoRepository = historialClinicoRepository;
            _pacienteRepository = pacienteRepository;
            _diagnosticoRepository = diagnosticoRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(HistorialClinicoDTO dto)
        {
            var errors = await ValidateHistorialClinico(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var historial = new HistorialClinico
            {
                Fecha = dto.Fecha,
                Descripcion = dto.Descripcion,
                Observaciones = dto.Observaciones,
                PacienteId = dto.PacienteId,
                DiagnosticoId = dto.DiagnosticoId
            };

            var created = await _historialClinicoRepository.CreateAsync(historial);

            return ServiceResult.Ok(MapToDTO(created), "Historial clínico creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, HistorialClinicoDTO dto)
        {
            var existing = await _historialClinicoRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Historial clínico con ID {id} no encontrado");

            var errors = await ValidateHistorialClinico(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var historial = new HistorialClinico
            {
                Id = id,
                Fecha = dto.Fecha,
                Descripcion = dto.Descripcion,
                Observaciones = dto.Observaciones,
                PacienteId = dto.PacienteId,
                DiagnosticoId = dto.DiagnosticoId
            };

            var updated = await _historialClinicoRepository.UpdateAsync(id, historial);

            return ServiceResult.Ok(MapToDTO(updated!), "Historial clínico actualizado exitosamente");
        }

        public async Task<IEnumerable<HistorialClinicoDTO>> GetByPacienteIdAsync(int pacienteId)
        {
            var historiales = await _historialClinicoRepository.GetAllAsync();
            return historiales
                .Where(h => h.PacienteId == pacienteId)
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<HistorialClinicoDTO>> GetAllAsync()
        {
            var historiales = await _historialClinicoRepository.GetAllAsync();
            return historiales.Select(MapToDTO);
        }

        public override async Task<HistorialClinicoDTO?> GetByIdAsync(int id)
        {
            var historial = await _historialClinicoRepository.GetByIdAsync(id);
            if (historial == null) return null;

            return MapToDTO(historial);
        }

        private static HistorialClinicoDTO MapToDTO(HistorialClinico entity)
        {
            return new HistorialClinicoDTO
            {
                Id = entity.Id,
                Fecha = entity.Fecha,
                Descripcion = entity.Descripcion,
                Observaciones = entity.Observaciones,
                PacienteId = entity.PacienteId,
                DiagnosticoId = entity.DiagnosticoId
            };
        }

        private async Task<List<string>> ValidateHistorialClinico(HistorialClinicoDTO dto)
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

            if (dto.PacienteId <= 0)
                errors.Add("El ID del paciente es requerido");
            else
            {
                var pacienteExists = await _pacienteRepository.GetByIdAsync(dto.PacienteId);
                if (pacienteExists == null)
                    errors.Add($"El paciente con ID {dto.PacienteId} no existe");
            }

            if (dto.DiagnosticoId <= 0)
                errors.Add("El ID del diagnóstico es requerido");
            else
            {
                var diagnosticoExists = await _diagnosticoRepository.GetByIdAsync(dto.DiagnosticoId);
                if (diagnosticoExists == null)
                    errors.Add($"El diagnóstico con ID {dto.DiagnosticoId} no existe");
            }

            return errors;
        }
    }
}
