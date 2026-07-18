using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Diagnostico;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class DiagnosticoService : BaseService<DiagnosticoDTO>, IDiagnosticoService
    {
        private readonly IDiagnosticoRepository _diagnosticoRepository;

        public DiagnosticoService(IDiagnosticoRepository diagnosticoRepository)
        {
            _diagnosticoRepository = diagnosticoRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(DiagnosticoDTO dto)
        {
            var errors = ValidateDiagnostico(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var diagnostico = new Diagnostico
            {
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            var created = await _diagnosticoRepository.CreateAsync(diagnostico);

            return ServiceResult.Ok(MapToDTO(created), "Diagnóstico creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, DiagnosticoDTO dto)
        {
            var existing = await _diagnosticoRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Diagnóstico con ID {id} no encontrado");

            var errors = ValidateDiagnostico(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var diagnostico = new Diagnostico
            {
                Id = id,
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            var updated = await _diagnosticoRepository.UpdateAsync(id, diagnostico);

            return ServiceResult.Ok(MapToDTO(updated!), "Diagnóstico actualizado exitosamente");
        }

        public async Task<IEnumerable<DiagnosticoDTO>> SearchByCodigoAsync(string codigo)
        {
            var diagnosticos = await _diagnosticoRepository.GetAllAsync();
            return diagnosticos
                .Where(d => d.Codigo.Contains(codigo))
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<DiagnosticoDTO>> GetAllAsync()
        {
            var diagnosticos = await _diagnosticoRepository.GetAllAsync();
            return diagnosticos.Select(MapToDTO);
        }

        public override async Task<DiagnosticoDTO?> GetByIdAsync(int id)
        {
            var diagnostico = await _diagnosticoRepository.GetByIdAsync(id);
            if (diagnostico == null) return null;

            return MapToDTO(diagnostico);
        }

        private static DiagnosticoDTO MapToDTO(Diagnostico entity)
        {
            return new DiagnosticoDTO
            {
                Id = entity.Id,
                Codigo = entity.Codigo,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion
            };
        }

        private List<string> ValidateDiagnostico(DiagnosticoDTO dto)
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
