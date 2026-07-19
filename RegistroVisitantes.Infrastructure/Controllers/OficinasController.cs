using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.Oficina;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentosController : ControllerBase
    {
        private readonly IOficinaService _OficinaService;

        public MedicamentosController(IOficinaService OficinaService)
        {
            _OficinaService = OficinaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OficinaDTO>>> GetMedicamentos()
        {
            var Oficinas = await _OficinaService.GetAllAsync();
            return Ok(Oficinas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OficinaDTO>> GetMedicamento(int id)
        {
            var Oficina = await _OficinaService.GetByIdAsync(id);
            if (Oficina == null)
                return NotFound();

            return Ok(Oficina);
        }

        [HttpPost]
        public async Task<ActionResult<OficinaDTO>> PostMedicamento(OficinaDTO OficinaDTO)
        {
            var result = await _OficinaService.CreateWithValidationAsync(OficinaDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetMedicamento), new { id = ((OficinaDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicamento(int id, OficinaDTO OficinaDTO)
        {
            var result = await _OficinaService.UpdateWithValidationAsync(id, OficinaDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicamento(int id)
        {
            var deleted = await _OficinaService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("nombre/{nombre}")]
        public async Task<ActionResult<IEnumerable<OficinaDTO>>> SearchByName(string nombre)
        {
            var Oficinas = await _OficinaService.SearchByNameAsync(nombre);
            return Ok(Oficinas);
        }
    }
}
