using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IDiagnosticoRepository
    {
        Task<IEnumerable<Diagnostico>> GetAllAsync();
        Task<Diagnostico?> GetByIdAsync(int id);
        Task<Diagnostico> CreateAsync(Diagnostico diagnostico);
        Task<Diagnostico?> UpdateAsync(int id, Diagnostico diagnostico);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
