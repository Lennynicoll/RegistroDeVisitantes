using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IRegistroVisitanteRepository
    {
        Task<IEnumerable<RegistroVisitante>> GetAllAsync();
        Task<RegistroVisitante?> GetByIdAsync(int id);
        Task<RegistroVisitante> CreateAsync(RegistroVisitante RegistroVisitante);
        Task<RegistroVisitante?> UpdateAsync(int id, RegistroVisitante RegistroVisitante);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
