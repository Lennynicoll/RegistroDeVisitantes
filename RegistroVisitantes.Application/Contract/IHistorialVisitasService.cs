using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.HistorialVisitas;

namespace RegistroVisitantes.Application.Contract
{
    public interface IHistorialVisitasService : IBaseService<HistorialVisitasDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(HistorialVisitasDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, HistorialVisitasDTO dto);
        Task<IEnumerable<HistorialVisitasDTO>> GetByPacienteIdAsync(int RegistroVisitanteId);
    }
}
