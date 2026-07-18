using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Cita;

namespace RegistroVisitantes.Application.Contract
{
    public interface ICitaService : IBaseService<CitaDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(CitaDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, CitaDTO dto);
        Task<IEnumerable<CitaDTO>> GetByPacienteIdAsync(int pacienteId);
    }
}
