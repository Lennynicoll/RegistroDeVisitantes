using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface ISeguridadEdificioRepository
    {
        Task<IEnumerable<SeguridadEdificio>> GetAllAsync();
        Task<SeguridadEdificio?> GetByIdAsync(int id);
        Task<SeguridadEdificio> CreateAsync(SeguridadEdificio SeguridadEdificio);
        Task<SeguridadEdificio?> UpdateAsync(int id, SeguridadEdificio SeguridadEdificio);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
