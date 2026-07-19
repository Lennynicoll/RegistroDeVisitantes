using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.Anfitrion;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private readonly IAnfitrionService _AnfitrionService;

        public MedicosController(IAnfitrionService AnfitrionService)
        {
            _AnfitrionService = AnfitrionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnfitrionDTO>>> GetMedicos()
        {
            var Anfitriones = await _AnfitrionService.GetAllAsync();
            return Ok(Anfitriones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnfitrionDTO>> GetMedico(int id)
        {
            var Anfitrion = await _AnfitrionService.GetByIdAsync(id);
            if (Anfitrion == null)
                return NotFound();

            return Ok(Anfitrion);
        }

        [HttpPost]
        public async Task<ActionResult<AnfitrionDTO>> PostMedico(AnfitrionDTO AnfitrionDTO)
        {
            var result = await _AnfitrionService.CreateWithValidationAsync(AnfitrionDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetMedico), new { id = ((AnfitrionDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedico(int id, AnfitrionDTO AnfitrionDTO)
        {
            var result = await _AnfitrionService.UpdateWithValidationAsync(id, AnfitrionDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedico(int id)
        {
            var deleted = await _AnfitrionService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("MotivoVisita/{MotivoVisitaId}")]
        public async Task<ActionResult<IEnumerable<AnfitrionDTO>>> GetByMotivoVisitaId(int MotivoVisitaId)
        {
            var Anfitriones = await _AnfitrionService.GetByEspecialidadIdAsync(MotivoVisitaId);
            return Ok(Anfitriones);
        }
    }
}
