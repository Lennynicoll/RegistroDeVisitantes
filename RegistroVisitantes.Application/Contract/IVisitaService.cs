using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Visita;

namespace RegistroVisitantes.Application.Contract
{
    public interface IVisitaService : IBaseService<VisitaDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(VisitaDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, VisitaDTO dto);
        Task<IEnumerable<VisitaDTO>> GetByVisitanteIdAsync(int visitanteId);
    }
}
