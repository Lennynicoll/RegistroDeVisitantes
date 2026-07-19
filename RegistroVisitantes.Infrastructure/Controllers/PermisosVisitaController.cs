using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.PermisoVisita;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetasMedicasController : ControllerBase
    {
        private readonly IPermisoVisitaService _PermisoVisitaService;

        public RecetasMedicasController(IPermisoVisitaService PermisoVisitaService)
        {
            _PermisoVisitaService = PermisoVisitaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermisoVisitaDTO>>> GetRecetasMedicas()
        {
            var recetas = await _PermisoVisitaService.GetAllAsync();
            return Ok(recetas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermisoVisitaDTO>> GetRecetaMedica(int id)
        {
            var receta = await _PermisoVisitaService.GetByIdAsync(id);
            if (receta == null)
                return NotFound();

            return Ok(receta);
        }

        [HttpPost]
        public async Task<ActionResult<PermisoVisitaDTO>> PostRecetaMedica(PermisoVisitaDTO PermisoVisitaDTO)
        {
            var result = await _PermisoVisitaService.CreateWithValidationAsync(PermisoVisitaDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetRecetaMedica), new { id = ((PermisoVisitaDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecetaMedica(int id, PermisoVisitaDTO PermisoVisitaDTO)
        {
            var result = await _PermisoVisitaService.UpdateWithValidationAsync(id, PermisoVisitaDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecetaMedica(int id)
        {
            var deleted = await _PermisoVisitaService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("RegistroVisita/{RegistroVisitaId}")]
        public async Task<ActionResult<IEnumerable<PermisoVisitaDTO>>> GetByRegistroVisitaId(int RegistroVisitaId)
        {
            var recetas = await _PermisoVisitaService.GetByCitaIdAsync(RegistroVisitaId);
            return Ok(recetas);
        }
    }
}
