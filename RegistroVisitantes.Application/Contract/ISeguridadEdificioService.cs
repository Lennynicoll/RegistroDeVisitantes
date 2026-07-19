using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.SeguridadEdificio;

namespace RegistroVisitantes.Application.Contract
{
    public interface ISeguridadEdificioService : IBaseService<SeguridadEdificioDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(SeguridadEdificioDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, SeguridadEdificioDTO dto);
    }
}
