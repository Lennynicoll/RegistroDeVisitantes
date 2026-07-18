using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.RecetaMedica;

namespace RegistroVisitantes.Application.Contract
{
    public interface IRecetaMedicaService : IBaseService<RecetaMedicaDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(RecetaMedicaDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, RecetaMedicaDTO dto);
        Task<IEnumerable<RecetaMedicaDTO>> GetByCitaIdAsync(int citaId);
    }
}
