using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Medico;

namespace RegistroVisitantes.Application.Contract
{
    public interface IMedicoService : IBaseService<MedicoDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(MedicoDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, MedicoDTO dto);
        Task<IEnumerable<MedicoDTO>> GetByEspecialidadIdAsync(int especialidadId);
    }
}
