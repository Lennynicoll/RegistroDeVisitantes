using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IRegistroVisitaRepository
    {
        Task<IEnumerable<RegistroVisita>> GetAllAsync();
        Task<RegistroVisita?> GetByIdAsync(int id);
        Task<RegistroVisita> CreateAsync(RegistroVisita RegistroVisita);
        Task<RegistroVisita?> UpdateAsync(int id, RegistroVisita RegistroVisita);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
