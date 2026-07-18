using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.Visitante;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitantesController : ControllerBase
    {
        private readonly IVisitanteService _visitanteService;

        public VisitantesController(IVisitanteService visitanteService)
        {
            _visitanteService = visitanteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitanteDTO>>> GetVisitantes()
        {
            var visitantes = await _visitanteService.GetAllAsync();
            return Ok(visitantes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VisitanteDTO>> GetVisitante(int id)
        {
            var visitante = await _visitanteService.GetByIdAsync(id);
            if (visitante == null)
                return NotFound();

            return Ok(visitante);
        }

        [HttpPost]
        public async Task<ActionResult<VisitanteDTO>> PostVisitante(VisitanteDTO visitanteDto)
        {
            var result = await _visitanteService.CreateWithValidationAsync(visitanteDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetVisitante), new { id = ((VisitanteDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitante(int id, VisitanteDTO visitanteDto)
        {
            var result = await _visitanteService.UpdateWithValidationAsync(id, visitanteDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitante(int id)
        {
            var deleted = await _visitanteService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("cedula/{cedula}")]
        public async Task<ActionResult<IEnumerable<VisitanteDTO>>> SearchByCedula(string cedula)
        {
            var visitantes = await _visitanteService.SearchByCedulaAsync(cedula);
            return Ok(visitantes);
        }
    }
}
