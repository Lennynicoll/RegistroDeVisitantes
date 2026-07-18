using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.PacienteSeguro;

namespace RegistroVisitantes.Application.Contract
{
    public interface IPacienteSeguroService : IBaseService<PacienteSeguroDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(PacienteSeguroDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, PacienteSeguroDTO dto);
        Task<IEnumerable<PacienteSeguroDTO>> GetByPacienteIdAsync(int pacienteId);
    }
}
