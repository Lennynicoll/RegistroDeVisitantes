using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.SeguroMedico;

namespace RegistroVisitantes.Application.Contract
{
    public interface ISeguroMedicoService : IBaseService<SeguroMedicoDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(SeguroMedicoDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, SeguroMedicoDTO dto);
    }
}
