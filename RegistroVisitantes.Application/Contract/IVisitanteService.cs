using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Visitante;

namespace RegistroVisitantes.Application.Contract
{
    public interface IVisitanteService : IBaseService<VisitanteDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(VisitanteDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, VisitanteDTO dto);
        Task<IEnumerable<VisitanteDTO>> SearchByCedulaAsync(string cedula);
    }
}
