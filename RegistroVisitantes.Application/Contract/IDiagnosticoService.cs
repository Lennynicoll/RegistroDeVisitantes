using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Diagnostico;

namespace RegistroVisitantes.Application.Contract
{
    public interface IDiagnosticoService : IBaseService<DiagnosticoDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(DiagnosticoDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, DiagnosticoDTO dto);
        Task<IEnumerable<DiagnosticoDTO>> SearchByCodigoAsync(string codigo);
    }
}
