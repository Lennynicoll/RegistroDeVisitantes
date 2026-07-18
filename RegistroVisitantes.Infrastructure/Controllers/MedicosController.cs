using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Dtos.Medico;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicosController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicoDTO>>> GetMedicos()
        {
            var medicos = await _medicoService.GetAllAsync();
            return Ok(medicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoDTO>> GetMedico(int id)
        {
            var medico = await _medicoService.GetByIdAsync(id);
            if (medico == null)
                return NotFound();

            return Ok(medico);
        }

        [HttpPost]
        public async Task<ActionResult<MedicoDTO>> PostMedico(MedicoDTO medicoDto)
        {
            var result = await _medicoService.CreateWithValidationAsync(medicoDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return CreatedAtAction(nameof(GetMedico), new { id = ((MedicoDTO)result.Data!).Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedico(int id, MedicoDTO medicoDto)
        {
            var result = await _medicoService.UpdateWithValidationAsync(id, medicoDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedico(int id)
        {
            var deleted = await _medicoService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("especialidad/{especialidadId}")]
        public async Task<ActionResult<IEnumerable<MedicoDTO>>> GetByEspecialidadId(int especialidadId)
        {
            var medicos = await _medicoService.GetByEspecialidadIdAsync(especialidadId);
            return Ok(medicos);
        }
    }
}
