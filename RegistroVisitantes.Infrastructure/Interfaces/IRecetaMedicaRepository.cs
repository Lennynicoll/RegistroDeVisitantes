using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IRecetaMedicaRepository
    {
        Task<IEnumerable<RecetaMedica>> GetAllAsync();
        Task<RecetaMedica?> GetByIdAsync(int id);
        Task<RecetaMedica> CreateAsync(RecetaMedica recetaMedica);
        Task<RecetaMedica?> UpdateAsync(int id, RecetaMedica recetaMedica);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
