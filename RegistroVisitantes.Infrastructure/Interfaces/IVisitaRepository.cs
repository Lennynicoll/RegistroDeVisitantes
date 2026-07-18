using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IVisitaRepository
    {
        Task<IEnumerable<Visita>> GetAllAsync();
        Task<Visita?> GetByIdAsync(int id);
        Task<Visita> CreateAsync(Visita visita);
        Task<Visita?> UpdateAsync(int id, Visita visita);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
