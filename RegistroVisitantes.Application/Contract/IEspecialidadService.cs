using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Especialidad;

namespace RegistroVisitantes.Application.Contract
{
    public interface IEspecialidadService : IBaseService<EspecialidadDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(EspecialidadDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, EspecialidadDTO dto);
    }
}
