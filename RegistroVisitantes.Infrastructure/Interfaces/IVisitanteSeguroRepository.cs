using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IVisitanteSeguroRepository
    {
        Task<IEnumerable<VisitanteSeguro>> GetAllAsync();
        Task<VisitanteSeguro?> GetByIdAsync(int id);
        Task<VisitanteSeguro> CreateAsync(VisitanteSeguro VisitanteSeguro);
        Task<VisitanteSeguro?> UpdateAsync(int id, VisitanteSeguro VisitanteSeguro);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
