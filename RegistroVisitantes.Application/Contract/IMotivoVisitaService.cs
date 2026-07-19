using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.MotivoVisita;

namespace RegistroVisitantes.Application.Contract
{
    public interface IMotivoVisitaService : IBaseService<MotivoVisitaDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(MotivoVisitaDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, MotivoVisitaDTO dto);
    }
}
