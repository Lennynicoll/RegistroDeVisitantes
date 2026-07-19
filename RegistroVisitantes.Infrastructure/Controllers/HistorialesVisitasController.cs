using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.HistorialVisitas;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialesClinicosController : ControllerBase
    {
        private readonly IHistorialVisitasService _HistorialVisitasService;

        public HistorialesClinicosController(IHistorialVisitasService HistorialVisitasService)
        {
            _HistorialVisitasService = HistorialVisitasService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialVisitasDTO>>> GetHistorialesVisitas()
        {
            var historiales = await _HistorialVisitasService.GetAllAsync();
            return Ok(historiales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialVisitasDTO>> GetHistorialVisitas(int id)
        {
            var historial = await _HistorialVisitasService.GetByIdAsync(id);
            if (historial == null)
                return NotFound();

            return Ok(historial);
        }

        [HttpPost]
        public async Task<ActionResult<HistorialVisitasDTO>> PostHistorialVisitas(HistorialVisitasDTO HistorialVisitasDTO)
        {
            var result = await _HistorialVisitasService.CreateWithValidationAsync(HistorialVisitasDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetHistorialVisitas), new { id = ((HistorialVisitasDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialVisitas(int id, HistorialVisitasDTO HistorialVisitasDTO)
        {
            var result = await _HistorialVisitasService.UpdateWithValidationAsync(id, HistorialVisitasDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialVisitas(int id)
        {
            var deleted = await _HistorialVisitasService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("RegistroVisitante/{RegistroVisitanteId}")]
        public async Task<ActionResult<IEnumerable<HistorialVisitasDTO>>> GetByRegistroVisitanteId(int RegistroVisitanteId)
        {
            var historiales = await _HistorialVisitasService.GetByPacienteIdAsync(RegistroVisitanteId);
            return Ok(historiales);
        }
    }
}
