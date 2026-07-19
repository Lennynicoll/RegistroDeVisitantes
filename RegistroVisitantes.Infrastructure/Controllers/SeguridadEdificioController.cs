using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.SeguridadEdificio;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegurosMedicosController : ControllerBase
    {
        private readonly ISeguridadEdificioService _SeguridadEdificioService;

        public SegurosMedicosController(ISeguridadEdificioService SeguridadEdificioService)
        {
            _SeguridadEdificioService = SeguridadEdificioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeguridadEdificioDTO>>> GetSegurosMedicos()
        {
            var seguros = await _SeguridadEdificioService.GetAllAsync();
            return Ok(seguros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeguridadEdificioDTO>> GetSeguroMedico(int id)
        {
            var seguro = await _SeguridadEdificioService.GetByIdAsync(id);
            if (seguro == null)
                return NotFound();

            return Ok(seguro);
        }

        [HttpPost]
        public async Task<ActionResult<SeguridadEdificioDTO>> PostSeguroMedico(SeguridadEdificioDTO SeguridadEdificioDTO)
        {
            var result = await _SeguridadEdificioService.CreateWithValidationAsync(SeguridadEdificioDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetSeguroMedico), new { id = ((SeguridadEdificioDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeguroMedico(int id, SeguridadEdificioDTO SeguridadEdificioDTO)
        {
            var result = await _SeguridadEdificioService.UpdateWithValidationAsync(id, SeguridadEdificioDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeguroMedico(int id)
        {
            var deleted = await _SeguridadEdificioService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
