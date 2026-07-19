using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.MotivoVisita;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly IMotivoVisitaService _MotivoVisitaService;

        public EspecialidadesController(IMotivoVisitaService MotivoVisitaService)
        {
            _MotivoVisitaService = MotivoVisitaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotivoVisitaDTO>>> GetEspecialidades()
        {
            var MotivosVisita = await _MotivoVisitaService.GetAllAsync();
            return Ok(MotivosVisita);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MotivoVisitaDTO>> GetEspecialidad(int id)
        {
            var MotivoVisita = await _MotivoVisitaService.GetByIdAsync(id);
            if (MotivoVisita == null)
                return NotFound();

            return Ok(MotivoVisita);
        }

        [HttpPost]
        public async Task<ActionResult<MotivoVisitaDTO>> PostEspecialidad(MotivoVisitaDTO MotivoVisitaDTO)
        {
            var result = await _MotivoVisitaService.CreateWithValidationAsync(MotivoVisitaDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetEspecialidad), new { id = ((MotivoVisitaDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidad(int id, MotivoVisitaDTO MotivoVisitaDTO)
        {
            var result = await _MotivoVisitaService.UpdateWithValidationAsync(id, MotivoVisitaDTO);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidad(int id)
        {
            var deleted = await _MotivoVisitaService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
