using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IDetallePermisoRepository
    {
        Task<IEnumerable<DetallePermiso>> GetAllAsync();
        Task<DetallePermiso?> GetByIdAsync(int id);
        Task<DetallePermiso> CreateAsync(DetallePermiso DetallePermiso);
        Task<DetallePermiso?> UpdateAsync(int id, DetallePermiso DetallePermiso);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
