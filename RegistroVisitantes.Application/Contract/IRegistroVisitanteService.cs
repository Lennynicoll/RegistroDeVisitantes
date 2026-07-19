using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.RegistroVisitante;

namespace RegistroVisitantes.Application.Contract
{
    public interface IRegistroVisitanteService : IBaseService<RegistroVisitanteDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(RegistroVisitanteDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, RegistroVisitanteDTO dto);
        Task<IEnumerable<RegistroVisitanteDTO>> SearchByCedulaAsync(string cedula);
    }
}
