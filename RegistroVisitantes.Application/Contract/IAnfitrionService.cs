using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Anfitrion;

namespace RegistroVisitantes.Application.Contract
{
    public interface IAnfitrionService : IBaseService<AnfitrionDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(AnfitrionDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, AnfitrionDTO dto);
        Task<IEnumerable<AnfitrionDTO>> GetByEspecialidadIdAsync(int MotivoVisitaId);
    }
}
