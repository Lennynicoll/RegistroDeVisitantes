using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IDetalleRecetaRepository
    {
        Task<IEnumerable<DetalleReceta>> GetAllAsync();
        Task<DetalleReceta?> GetByIdAsync(int id);
        Task<DetalleReceta> CreateAsync(DetalleReceta detalleReceta);
        Task<DetalleReceta?> UpdateAsync(int id, DetalleReceta detalleReceta);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
