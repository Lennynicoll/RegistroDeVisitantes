using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.RegistroVisita;

namespace RegistroVisitantes.Application.Contract
{
    public interface IRegistroVisitaService : IBaseService<RegistroVisitaDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(RegistroVisitaDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, RegistroVisitaDTO dto);
        Task<IEnumerable<RegistroVisitaDTO>> GetByPacienteIdAsync(int RegistroVisitanteId);
    }
}
