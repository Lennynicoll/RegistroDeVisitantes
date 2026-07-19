using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Oficina;

namespace RegistroVisitantes.Application.Contract
{
    public interface IOficinaService : IBaseService<OficinaDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(OficinaDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, OficinaDTO dto);
        Task<IEnumerable<OficinaDTO>> SearchByNameAsync(string nombre);
    }
}
