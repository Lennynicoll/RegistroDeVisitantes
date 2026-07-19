using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IMotivoVisitaRepository
    {
        Task<IEnumerable<MotivoVisita>> GetAllAsync();
        Task<MotivoVisita?> GetByIdAsync(int id);
        Task<MotivoVisita> CreateAsync(MotivoVisita MotivoVisita);
        Task<MotivoVisita?> UpdateAsync(int id, MotivoVisita MotivoVisita);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
