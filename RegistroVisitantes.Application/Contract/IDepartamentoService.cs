using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Departamento;

namespace RegistroVisitantes.Application.Contract
{
    public interface IDepartamentoService : IBaseService<DepartamentoDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(DepartamentoDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, DepartamentoDTO dto);
    }
}
