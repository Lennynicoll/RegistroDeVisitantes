using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IHistorialClinicoRepository
    {
        Task<IEnumerable<HistorialClinico>> GetAllAsync();
        Task<HistorialClinico?> GetByIdAsync(int id);
        Task<HistorialClinico> CreateAsync(HistorialClinico historialClinico);
        Task<HistorialClinico?> UpdateAsync(int id, HistorialClinico historialClinico);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
