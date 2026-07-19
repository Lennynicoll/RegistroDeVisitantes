using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.RegistroVisita;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly IRegistroVisitaService _RegistroVisitaService;

        public CitasController(IRegistroVisitaService RegistroVisitaService)
        {
            _RegistroVisitaService = RegistroVisitaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroVisitaDTO>>> GetCitas()
        {
            var RegistroVisitas = await _RegistroVisitaService.GetAllAsync();
            return Ok(RegistroVisitas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroVisitaDTO>> GetCita(int id)
        {
            var RegistroVisita = await _RegistroVisitaService.GetByIdAsync(id);
            if (RegistroVisita == null)
                return NotFound();

            return Ok(RegistroVisita);
        }

        [HttpPost]
        public async Task<ActionResult<RegistroVisitaDTO>> PostCita(RegistroVisitaDTO RegistroVisitaDTO)
        {
            var result = await _RegistroVisitaService.CreateWithValidationAsync(RegistroVisitaDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetCita), new { id = ((RegistroVisitaDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, RegistroVisitaDTO RegistroVisitaDTO)
        {
            var result = await _RegistroVisitaService.UpdateWithValidationAsync(id, RegistroVisitaDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var deleted = await _RegistroVisitaService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("RegistroVisitante/{RegistroVisitanteId}")]
        public async Task<ActionResult<IEnumerable<RegistroVisitaDTO>>> GetByRegistroVisitanteId(int RegistroVisitanteId)
        {
            var RegistroVisitas = await _RegistroVisitaService.GetByPacienteIdAsync(RegistroVisitanteId);
            return Ok(RegistroVisitas);
        }
    }
}
