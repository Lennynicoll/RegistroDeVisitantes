using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Medicamento;

namespace RegistroVisitantes.Application.Contract
{
    public interface IMedicamentoService : IBaseService<MedicamentoDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(MedicamentoDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, MedicamentoDTO dto);
        Task<IEnumerable<MedicamentoDTO>> SearchByNameAsync(string nombre);
    }
}
