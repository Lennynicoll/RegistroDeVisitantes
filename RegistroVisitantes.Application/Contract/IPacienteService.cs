using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Paciente;

namespace RegistroVisitantes.Application.Contract
{
    public interface IPacienteService : IBaseService<PacienteDTO>
    {
        Task<ServiceResult> CreateWithValidationAsync(PacienteDTO dto);
        Task<ServiceResult> UpdateWithValidationAsync(int id, PacienteDTO dto);
        Task<IEnumerable<PacienteDTO>> SearchByCedulaAsync(string cedula);
    }
}
