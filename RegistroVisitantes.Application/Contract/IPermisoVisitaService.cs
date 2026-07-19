using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.PermisoVisita;

namespace RegistroVisitantes.Application.Contract
{
    public interface IPermisoVisitaService : IBaseService<PermisoVisitaDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(PermisoVisitaDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, PermisoVisitaDTO dto);
        Task<IEnumerable<PermisoVisitaDTO>> GetByCitaIdAsync(int RegistroVisitaId);
    }
}
