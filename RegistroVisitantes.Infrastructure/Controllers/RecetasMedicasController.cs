using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.RecetaMedica;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetasMedicasController : ControllerBase
    {
        private readonly IRecetaMedicaService _recetaMedicaService;

        public RecetasMedicasController(IRecetaMedicaService recetaMedicaService)
        {
            _recetaMedicaService = recetaMedicaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecetaMedicaDTO>>> GetRecetasMedicas()
        {
            var recetas = await _recetaMedicaService.GetAllAsync();
            return Ok(recetas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecetaMedicaDTO>> GetRecetaMedica(int id)
        {
            var receta = await _recetaMedicaService.GetByIdAsync(id);
            if (receta == null)
                return NotFound();

            return Ok(receta);
        }

        [HttpPost]
        public async Task<ActionResult<RecetaMedicaDTO>> PostRecetaMedica(RecetaMedicaDTO recetaMedicaDto)
        {
            var result = await _recetaMedicaService.CreateWithValidationAsync(recetaMedicaDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetRecetaMedica), new { id = ((RecetaMedicaDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecetaMedica(int id, RecetaMedicaDTO recetaMedicaDto)
        {
            var result = await _recetaMedicaService.UpdateWithValidationAsync(id, recetaMedicaDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecetaMedica(int id)
        {
            var deleted = await _recetaMedicaService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("cita/{citaId}")]
        public async Task<ActionResult<IEnumerable<RecetaMedicaDTO>>> GetByCitaId(int citaId)
        {
            var recetas = await _recetaMedicaService.GetByCitaIdAsync(citaId);
            return Ok(recetas);
        }
    }
}
