using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.DetalleReceta;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallesRecetaController : ControllerBase
    {
        private readonly IDetalleRecetaService _detalleRecetaService;

        public DetallesRecetaController(IDetalleRecetaService detalleRecetaService)
        {
            _detalleRecetaService = detalleRecetaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleRecetaDTO>>> GetDetallesReceta()
        {
            var detalles = await _detalleRecetaService.GetAllAsync();
            return Ok(detalles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleRecetaDTO>> GetDetalleReceta(int id)
        {
            var detalle = await _detalleRecetaService.GetByIdAsync(id);
            if (detalle == null)
                return NotFound();

            return Ok(detalle);
        }

        [HttpPost]
        public async Task<ActionResult<DetalleRecetaDTO>> PostDetalleReceta(DetalleRecetaDTO detalleRecetaDto)
        {
            var result = await _detalleRecetaService.CreateWithValidationAsync(detalleRecetaDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetDetalleReceta), new { id = ((DetalleRecetaDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleReceta(int id, DetalleRecetaDTO detalleRecetaDto)
        {
            var result = await _detalleRecetaService.UpdateWithValidationAsync(id, detalleRecetaDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleReceta(int id)
        {
            var deleted = await _detalleRecetaService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("receta/{recetaMedicaId}")]
        public async Task<ActionResult<IEnumerable<DetalleRecetaDTO>>> GetByRecetaMedicaId(int recetaMedicaId)
        {
            var detalles = await _detalleRecetaService.GetByRecetaMedicaIdAsync(recetaMedicaId);
            return Ok(detalles);
        }
    }
}
