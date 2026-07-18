using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.Diagnostico;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticosController : ControllerBase
    {
        private readonly IDiagnosticoService _diagnosticoService;

        public DiagnosticosController(IDiagnosticoService diagnosticoService)
        {
            _diagnosticoService = diagnosticoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiagnosticoDTO>>> GetDiagnosticos()
        {
            var diagnosticos = await _diagnosticoService.GetAllAsync();
            return Ok(diagnosticos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiagnosticoDTO>> GetDiagnostico(int id)
        {
            var diagnostico = await _diagnosticoService.GetByIdAsync(id);
            if (diagnostico == null)
                return NotFound();

            return Ok(diagnostico);
        }

        [HttpPost]
        public async Task<ActionResult<DiagnosticoDTO>> PostDiagnostico(DiagnosticoDTO diagnosticoDto)
        {
            var result = await _diagnosticoService.CreateWithValidationAsync(diagnosticoDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetDiagnostico), new { id = ((DiagnosticoDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiagnostico(int id, DiagnosticoDTO diagnosticoDto)
        {
            var result = await _diagnosticoService.UpdateWithValidationAsync(id, diagnosticoDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnostico(int id)
        {
            var deleted = await _diagnosticoService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<IEnumerable<DiagnosticoDTO>>> SearchByCodigo(string codigo)
        {
            var diagnosticos = await _diagnosticoService.SearchByCodigoAsync(codigo);
            return Ok(diagnosticos);
        }
    }
}
