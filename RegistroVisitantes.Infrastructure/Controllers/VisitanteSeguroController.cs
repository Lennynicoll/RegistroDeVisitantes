using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.VisitanteSeguro;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteSegurosController : ControllerBase
    {
        private readonly IVisitanteSeguroService _VisitanteSeguroService;

        public PacienteSegurosController(IVisitanteSeguroService VisitanteSeguroService)
        {
            _VisitanteSeguroService = VisitanteSeguroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitanteSeguroDTO>>> GetVisitanteSeguros()
        {
            var VisitanteSeguros = await _VisitanteSeguroService.GetAllAsync();
            return Ok(VisitanteSeguros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VisitanteSeguroDTO>> GetVisitanteSeguro(int id)
        {
            var VisitanteSeguro = await _VisitanteSeguroService.GetByIdAsync(id);
            if (VisitanteSeguro == null)
                return NotFound();

            return Ok(VisitanteSeguro);
        }

        [HttpPost]
        public async Task<ActionResult<VisitanteSeguroDTO>> PostVisitanteSeguro(VisitanteSeguroDTO VisitanteSeguroDTO)
        {
            var result = await _VisitanteSeguroService.CreateWithValidationAsync(VisitanteSeguroDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetVisitanteSeguro), new { id = ((VisitanteSeguroDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitanteSeguro(int id, VisitanteSeguroDTO VisitanteSeguroDTO)
        {
            var result = await _VisitanteSeguroService.UpdateWithValidationAsync(id, VisitanteSeguroDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitanteSeguro(int id)
        {
            var deleted = await _VisitanteSeguroService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("RegistroVisitante/{RegistroVisitanteId}")]
        public async Task<ActionResult<IEnumerable<VisitanteSeguroDTO>>> GetByRegistroVisitanteId(int RegistroVisitanteId)
        {
            var VisitanteSeguros = await _VisitanteSeguroService.GetByPacienteIdAsync(RegistroVisitanteId);
            return Ok(VisitanteSeguros);
        }
    }
}
