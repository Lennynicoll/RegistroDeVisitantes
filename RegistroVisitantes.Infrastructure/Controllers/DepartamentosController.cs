using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.Departamento;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly IDepartamentoService _departamentoService;

        public DepartamentosController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartamentoDTO>>> GetDepartamentos()
        {
            var departamentos = await _departamentoService.GetAllAsync();
            return Ok(departamentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartamentoDTO>> GetDepartamento(int id)
        {
            var departamento = await _departamentoService.GetByIdAsync(id);
            if (departamento == null)
                return NotFound();

            return Ok(departamento);
        }

        [HttpPost]
        public async Task<ActionResult<DepartamentoDTO>> PostDepartamento(DepartamentoDTO departamentoDto)
        {
            var result = await _departamentoService.CreateWithValidationAsync(departamentoDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetDepartamento), new { id = ((DepartamentoDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamento(int id, DepartamentoDTO departamentoDto)
        {
            var result = await _departamentoService.UpdateWithValidationAsync(id, departamentoDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartamento(int id)
        {
            var deleted = await _departamentoService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
