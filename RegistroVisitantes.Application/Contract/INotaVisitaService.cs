using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.NotaVisita;

namespace RegistroVisitantes.Application.Contract
{
    public interface INotaVisitaService : IBaseService<NotaVisitaDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(NotaVisitaDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, NotaVisitaDTO dto);
        Task<IEnumerable<NotaVisitaDTO>> SearchByCodigoAsync(string codigo);
    }
}
