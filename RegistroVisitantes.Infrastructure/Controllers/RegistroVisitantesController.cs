using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.RegistroVisitante;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IRegistroVisitanteService _RegistroVisitanteService;

        public PacientesController(IRegistroVisitanteService RegistroVisitanteService)
        {
            _RegistroVisitanteService = RegistroVisitanteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroVisitanteDTO>>> GetPacientes()
        {
            var RegistroVisitantes = await _RegistroVisitanteService.GetAllAsync();
            return Ok(RegistroVisitantes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroVisitanteDTO>> GetPaciente(int id)
        {
            var RegistroVisitante = await _RegistroVisitanteService.GetByIdAsync(id);
            if (RegistroVisitante == null)
                return NotFound();

            return Ok(RegistroVisitante);
        }

        [HttpPost]
        public async Task<ActionResult<RegistroVisitanteDTO>> PostPaciente(RegistroVisitanteDTO RegistroVisitanteDTO)
        {
            var result = await _RegistroVisitanteService.CreateWithValidationAsync(RegistroVisitanteDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetPaciente), new { id = ((RegistroVisitanteDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, RegistroVisitanteDTO RegistroVisitanteDTO)
        {
            var result = await _RegistroVisitanteService.UpdateWithValidationAsync(id, RegistroVisitanteDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var deleted = await _RegistroVisitanteService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("cedula/{cedula}")]
        public async Task<ActionResult<IEnumerable<RegistroVisitanteDTO>>> SearchByCedula(string cedula)
        {
            var RegistroVisitantes = await _RegistroVisitanteService.SearchByCedulaAsync(cedula);
            return Ok(RegistroVisitantes);
        }
    }
}
