using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;
using RegistroVisitantes.Infrastructure.Models;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitantesController : ControllerBase
    {
        private readonly IVisitanteRepository _visitanteRepository;

        public VisitantesController(IVisitanteRepository visitanteRepository)
        {
            _visitanteRepository = visitanteRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitanteDTO>>> GetVisitantes()
        {
            var visitantes = await _visitanteRepository.GetAllAsync();
            var visitantesDto = visitantes.Select(v => new VisitanteDTO
            {
                Id = v.Id,
                Nombre = v.Nombre,
                Apellido = v.Apellido,
                Cedula = v.Cedula,
                Correo = v.Correo,
                Telefono = v.Telefono
            }).ToList();

            return Ok(visitantesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VisitanteDTO>> GetVisitante(int id)
        {
            var visitante = await _visitanteRepository.GetByIdAsync(id);
            if (visitante == null)
            {
                return NotFound();
            }

            var visitanteDto = new VisitanteDTO
            {
                Id = visitante.Id,
                Nombre = visitante.Nombre,
                Apellido = visitante.Apellido,
                Cedula = visitante.Cedula,
                Correo = visitante.Correo,
                Telefono = visitante.Telefono
            };

            return Ok(visitanteDto);
        }

        [HttpPost]
        public async Task<ActionResult<VisitanteDTO>> PostVisitante(VisitanteDTO visitanteDto)
        {
            var visitante = new Visitante
            {
                Nombre = visitanteDto.Nombre,
                Apellido = visitanteDto.Apellido,
                Cedula = visitanteDto.Cedula,
                Correo = visitanteDto.Correo,
                Telefono = visitanteDto.Telefono
            };

            var created = await _visitanteRepository.CreateAsync(visitante);

            visitanteDto.Id = created.Id;

            return CreatedAtAction(nameof(GetVisitante), new { id = created.Id }, visitanteDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitante(int id, VisitanteDTO visitanteDto)
        {
            if (id != visitanteDto.Id)
            {
                return BadRequest();
            }

            var visitante = new Visitante
            {
                Id = visitanteDto.Id,
                Nombre = visitanteDto.Nombre,
                Apellido = visitanteDto.Apellido,
                Cedula = visitanteDto.Cedula,
                Correo = visitanteDto.Correo,
                Telefono = visitanteDto.Telefono
            };

            var updated = await _visitanteRepository.UpdateAsync(id, visitante);
            if (updated == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitante(int id)
        {
            var deleted = await _visitanteRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
