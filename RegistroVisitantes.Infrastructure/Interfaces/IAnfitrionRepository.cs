using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IAnfitrionRepository
    {
        Task<IEnumerable<Anfitrion>> GetAllAsync();
        Task<Anfitrion?> GetByIdAsync(int id);
        Task<Anfitrion> CreateAsync(Anfitrion Anfitrion);
        Task<Anfitrion?> UpdateAsync(int id, Anfitrion Anfitrion);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
