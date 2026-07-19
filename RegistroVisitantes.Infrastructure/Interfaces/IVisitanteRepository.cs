using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IVisitanteRepository
    {
        Task<IEnumerable<Visitante>> GetAllAsync();
        Task<Visitante?> GetByIdAsync(int id);
        Task<Visitante> CreateAsync(Visitante visitante);
        Task<Visitante?> UpdateAsync(int id, Visitante visitante);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
