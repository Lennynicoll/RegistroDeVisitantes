using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IOficinaRepository
    {
        Task<IEnumerable<Oficina>> GetAllAsync();
        Task<Oficina?> GetByIdAsync(int id);
        Task<Oficina> CreateAsync(Oficina Oficina);
        Task<Oficina?> UpdateAsync(int id, Oficina Oficina);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
