using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.DetallePermiso;

namespace RegistroVisitantes.Application.Contract
{
    public interface IDetallePermisoService : IBaseService<DetallePermisoDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(DetallePermisoDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, DetallePermisoDTO dto);
        Task<IEnumerable<DetallePermisoDTO>> GetByRecetaMedicaIdAsync(int PermisoVisitaId);
    }
}
