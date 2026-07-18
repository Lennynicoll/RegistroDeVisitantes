using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.HistorialClinico;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialesClinicosController : ControllerBase
    {
        private readonly IHistorialClinicoService _historialClinicoService;

        public HistorialesClinicosController(IHistorialClinicoService historialClinicoService)
        {
            _historialClinicoService = historialClinicoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialClinicoDTO>>> GetHistorialesClinicos()
        {
            var historiales = await _historialClinicoService.GetAllAsync();
            return Ok(historiales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialClinicoDTO>> GetHistorialClinico(int id)
        {
            var historial = await _historialClinicoService.GetByIdAsync(id);
            if (historial == null)
                return NotFound();

            return Ok(historial);
        }

        [HttpPost]
        public async Task<ActionResult<HistorialClinicoDTO>> PostHistorialClinico(HistorialClinicoDTO historialClinicoDto)
        {
            var result = await _historialClinicoService.CreateWithValidationAsync(historialClinicoDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetHistorialClinico), new { id = ((HistorialClinicoDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialClinico(int id, HistorialClinicoDTO historialClinicoDto)
        {
            var result = await _historialClinicoService.UpdateWithValidationAsync(id, historialClinicoDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialClinico(int id)
        {
            var deleted = await _historialClinicoService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("paciente/{pacienteId}")]
        public async Task<ActionResult<IEnumerable<HistorialClinicoDTO>>> GetByPacienteId(int pacienteId)
        {
            var historiales = await _historialClinicoService.GetByPacienteIdAsync(pacienteId);
            return Ok(historiales);
        }
    }
}
