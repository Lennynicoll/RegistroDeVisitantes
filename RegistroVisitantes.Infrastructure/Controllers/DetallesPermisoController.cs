using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.DetallePermiso;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallesRecetaController : ControllerBase
    {
        private readonly IDetallePermisoService _DetallePermisoService;

        public DetallesRecetaController(IDetallePermisoService DetallePermisoService)
        {
            _DetallePermisoService = DetallePermisoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallePermisoDTO>>> GetDetallesReceta()
        {
            var detalles = await _DetallePermisoService.GetAllAsync();
            return Ok(detalles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePermisoDTO>> GetDetalleReceta(int id)
        {
            var detalle = await _DetallePermisoService.GetByIdAsync(id);
            if (detalle == null)
                return NotFound();

            return Ok(detalle);
        }

        [HttpPost]
        public async Task<ActionResult<DetallePermisoDTO>> PostDetalleReceta(DetallePermisoDTO DetallePermisoDTO)
        {
            var result = await _DetallePermisoService.CreateWithValidationAsync(DetallePermisoDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetDetalleReceta), new { id = ((DetallePermisoDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleReceta(int id, DetallePermisoDTO DetallePermisoDTO)
        {
            var result = await _DetallePermisoService.UpdateWithValidationAsync(id, DetallePermisoDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleReceta(int id)
        {
            var deleted = await _DetallePermisoService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("receta/{PermisoVisitaId}")]
        public async Task<ActionResult<IEnumerable<DetallePermisoDTO>>> GetByPermisoVisitaId(int PermisoVisitaId)
        {
            var detalles = await _DetallePermisoService.GetByRecetaMedicaIdAsync(PermisoVisitaId);
            return Ok(detalles);
        }
    }
}
