using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.HistorialClinico;

namespace RegistroVisitantes.Application.Contract
{
    public interface IHistorialClinicoService : IBaseService<HistorialClinicoDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(HistorialClinicoDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, HistorialClinicoDTO dto);
        Task<IEnumerable<HistorialClinicoDTO>> GetByPacienteIdAsync(int pacienteId);
    }
}
