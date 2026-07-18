using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface ISeguroMedicoRepository
    {
        Task<IEnumerable<SeguroMedico>> GetAllAsync();
        Task<SeguroMedico?> GetByIdAsync(int id);
        Task<SeguroMedico> CreateAsync(SeguroMedico seguroMedico);
        Task<SeguroMedico?> UpdateAsync(int id, SeguroMedico seguroMedico);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
