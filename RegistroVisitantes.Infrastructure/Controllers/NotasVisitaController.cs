using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.NotaVisita;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticosController : ControllerBase
    {
        private readonly INotaVisitaService _NotaVisitaService;

        public DiagnosticosController(INotaVisitaService NotaVisitaService)
        {
            _NotaVisitaService = NotaVisitaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotaVisitaDTO>>> GetNotasVisita()
        {
            var NotasVisita = await _NotaVisitaService.GetAllAsync();
            return Ok(NotasVisita);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotaVisitaDTO>> GetNotaVisita(int id)
        {
            var NotaVisita = await _NotaVisitaService.GetByIdAsync(id);
            if (NotaVisita == null)
                return NotFound();

            return Ok(NotaVisita);
        }

        [HttpPost]
        public async Task<ActionResult<NotaVisitaDTO>> PostNotaVisita(NotaVisitaDTO NotaVisitaDTO)
        {
            var result = await _NotaVisitaService.CreateWithValidationAsync(NotaVisitaDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetNotaVisita), new { id = ((NotaVisitaDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotaVisita(int id, NotaVisitaDTO NotaVisitaDTO)
        {
            var result = await _NotaVisitaService.UpdateWithValidationAsync(id, NotaVisitaDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotaVisita(int id)
        {
            var deleted = await _NotaVisitaService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<IEnumerable<NotaVisitaDTO>>> SearchByCodigo(string codigo)
        {
            var NotasVisita = await _NotaVisitaService.SearchByCodigoAsync(codigo);
            return Ok(NotasVisita);
        }
    }
}
