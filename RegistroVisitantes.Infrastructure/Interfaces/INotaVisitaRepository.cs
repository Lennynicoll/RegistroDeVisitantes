using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface INotaVisitaRepository
    {
        Task<IEnumerable<NotaVisita>> GetAllAsync();
        Task<NotaVisita?> GetByIdAsync(int id);
        Task<NotaVisita> CreateAsync(NotaVisita NotaVisita);
        Task<NotaVisita?> UpdateAsync(int id, NotaVisita NotaVisita);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
