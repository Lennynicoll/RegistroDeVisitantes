using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.SeguroMedico;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegurosMedicosController : ControllerBase
    {
        private readonly ISeguroMedicoService _seguroMedicoService;

        public SegurosMedicosController(ISeguroMedicoService seguroMedicoService)
        {
            _seguroMedicoService = seguroMedicoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeguroMedicoDTO>>> GetSegurosMedicos()
        {
            var seguros = await _seguroMedicoService.GetAllAsync();
            return Ok(seguros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeguroMedicoDTO>> GetSeguroMedico(int id)
        {
            var seguro = await _seguroMedicoService.GetByIdAsync(id);
            if (seguro == null)
                return NotFound();

            return Ok(seguro);
        }

        [HttpPost]
        public async Task<ActionResult<SeguroMedicoDTO>> PostSeguroMedico(SeguroMedicoDTO seguroMedicoDto)
        {
            var result = await _seguroMedicoService.CreateWithValidationAsync(seguroMedicoDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetSeguroMedico), new { id = ((SeguroMedicoDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeguroMedico(int id, SeguroMedicoDTO seguroMedicoDto)
        {
            var result = await _seguroMedicoService.UpdateWithValidationAsync(id, seguroMedicoDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeguroMedico(int id)
        {
            var deleted = await _seguroMedicoService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
