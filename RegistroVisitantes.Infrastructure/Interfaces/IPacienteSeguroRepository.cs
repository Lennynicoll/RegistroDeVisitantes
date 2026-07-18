using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Interfaces
{
    public interface IPacienteSeguroRepository
    {
        Task<IEnumerable<PacienteSeguro>> GetAllAsync();
        Task<PacienteSeguro?> GetByIdAsync(int id);
        Task<PacienteSeguro> CreateAsync(PacienteSeguro pacienteSeguro);
        Task<PacienteSeguro?> UpdateAsync(int id, PacienteSeguro pacienteSeguro);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
