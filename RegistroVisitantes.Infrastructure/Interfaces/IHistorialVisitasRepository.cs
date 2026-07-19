using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IHistorialVisitasRepository
    {
        Task<IEnumerable<HistorialVisitas>> GetAllAsync();
        Task<HistorialVisitas?> GetByIdAsync(int id);
        Task<HistorialVisitas> CreateAsync(HistorialVisitas HistorialVisitas);
        Task<HistorialVisitas?> UpdateAsync(int id, HistorialVisitas HistorialVisitas);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
