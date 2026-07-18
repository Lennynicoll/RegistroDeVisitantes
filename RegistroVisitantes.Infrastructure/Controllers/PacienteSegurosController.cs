using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.PacienteSeguro;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteSegurosController : ControllerBase
    {
        private readonly IPacienteSeguroService _pacienteSeguroService;

        public PacienteSegurosController(IPacienteSeguroService pacienteSeguroService)
        {
            _pacienteSeguroService = pacienteSeguroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteSeguroDTO>>> GetPacienteSeguros()
        {
            var pacienteSeguros = await _pacienteSeguroService.GetAllAsync();
            return Ok(pacienteSeguros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteSeguroDTO>> GetPacienteSeguro(int id)
        {
            var pacienteSeguro = await _pacienteSeguroService.GetByIdAsync(id);
            if (pacienteSeguro == null)
                return NotFound();

            return Ok(pacienteSeguro);
        }

        [HttpPost]
        public async Task<ActionResult<PacienteSeguroDTO>> PostPacienteSeguro(PacienteSeguroDTO pacienteSeguroDto)
        {
            var result = await _pacienteSeguroService.CreateWithValidationAsync(pacienteSeguroDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetPacienteSeguro), new { id = ((PacienteSeguroDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPacienteSeguro(int id, PacienteSeguroDTO pacienteSeguroDto)
        {
            var result = await _pacienteSeguroService.UpdateWithValidationAsync(id, pacienteSeguroDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePacienteSeguro(int id)
        {
            var deleted = await _pacienteSeguroService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("paciente/{pacienteId}")]
        public async Task<ActionResult<IEnumerable<PacienteSeguroDTO>>> GetByPacienteId(int pacienteId)
        {
            var pacienteSeguros = await _pacienteSeguroService.GetByPacienteIdAsync(pacienteId);
            return Ok(pacienteSeguros);
        }
    }
}
