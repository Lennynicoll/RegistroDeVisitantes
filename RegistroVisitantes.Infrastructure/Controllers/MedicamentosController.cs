using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.Medicamento;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentosController : ControllerBase
    {
        private readonly IMedicamentoService _medicamentoService;

        public MedicamentosController(IMedicamentoService medicamentoService)
        {
            _medicamentoService = medicamentoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicamentoDTO>>> GetMedicamentos()
        {
            var medicamentos = await _medicamentoService.GetAllAsync();
            return Ok(medicamentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicamentoDTO>> GetMedicamento(int id)
        {
            var medicamento = await _medicamentoService.GetByIdAsync(id);
            if (medicamento == null)
                return NotFound();

            return Ok(medicamento);
        }

        [HttpPost]
        public async Task<ActionResult<MedicamentoDTO>> PostMedicamento(MedicamentoDTO medicamentoDto)
        {
            var result = await _medicamentoService.CreateWithValidationAsync(medicamentoDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetMedicamento), new { id = ((MedicamentoDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicamento(int id, MedicamentoDTO medicamentoDto)
        {
            var result = await _medicamentoService.UpdateWithValidationAsync(id, medicamentoDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicamento(int id)
        {
            var deleted = await _medicamentoService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("nombre/{nombre}")]
        public async Task<ActionResult<IEnumerable<MedicamentoDTO>>> SearchByName(string nombre)
        {
            var medicamentos = await _medicamentoService.SearchByNameAsync(nombre);
            return Ok(medicamentos);
        }
    }
}
