using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IPermisoVisitaRepository
    {
        Task<IEnumerable<PermisoVisita>> GetAllAsync();
        Task<PermisoVisita?> GetByIdAsync(int id);
        Task<PermisoVisita> CreateAsync(PermisoVisita PermisoVisita);
        Task<PermisoVisita?> UpdateAsync(int id, PermisoVisita PermisoVisita);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
