using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.VisitanteSeguro;

namespace RegistroVisitantes.Application.Contract
{
    public interface IVisitanteSeguroService : IBaseService<VisitanteSeguroDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(VisitanteSeguroDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, VisitanteSeguroDTO dto);
        Task<IEnumerable<VisitanteSeguroDTO>> GetByPacienteIdAsync(int RegistroVisitanteId);
    }
}
