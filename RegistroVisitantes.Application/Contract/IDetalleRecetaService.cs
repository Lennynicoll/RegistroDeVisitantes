using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.DetalleReceta;

namespace RegistroVisitantes.Application.Contract
{
    public interface IDetalleRecetaService : IBaseService<DetalleRecetaDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(DetalleRecetaDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, DetalleRecetaDTO dto);
        Task<IEnumerable<DetalleRecetaDTO>> GetByRecetaMedicaIdAsync(int recetaMedicaId);
    }
}
