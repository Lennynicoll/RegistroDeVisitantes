using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.Especialidad;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly IEspecialidadService _especialidadService;

        public EspecialidadesController(IEspecialidadService especialidadService)
        {
            _especialidadService = especialidadService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadDTO>>> GetEspecialidades()
        {
            var especialidades = await _especialidadService.GetAllAsync();
            return Ok(especialidades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadDTO>> GetEspecialidad(int id)
        {
            var especialidad = await _especialidadService.GetByIdAsync(id);
            if (especialidad == null)
                return NotFound();

            return Ok(especialidad);
        }

        [HttpPost]
        public async Task<ActionResult<EspecialidadDTO>> PostEspecialidad(EspecialidadDTO especialidadDto)
        {
            var result = await _especialidadService.CreateWithValidationAsync(especialidadDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetEspecialidad), new { id = ((EspecialidadDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidad(int id, EspecialidadDTO especialidadDto)
        {
            var result = await _especialidadService.UpdateWithValidationAsync(id, especialidadDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidad(int id)
        {
            var deleted = await _especialidadService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
