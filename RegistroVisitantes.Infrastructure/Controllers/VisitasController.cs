using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;
using RegistroVisitantes.Infrastructure.Models;

namespace RegistroVisitantes.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitasController : ControllerBase
    {
        private readonly IVisitaRepository _visitaRepository;

        public VisitasController(IVisitaRepository visitaRepository)
        {
            _visitaRepository = visitaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitaDTO>>> GetVisitas()
        {
            var visitas = await _visitaRepository.GetAllAsync();
            var visitasDto = visitas.Select(v => new VisitaDTO
            {
                Id = v.Id,
                FechaHora = v.FechaHora,
                Motivo = v.Motivo,
                Comentarios = v.Comentarios,
                VisitanteId = v.VisitanteId
            }).ToList();

            return Ok(visitasDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VisitaDTO>> GetVisita(int id)
        {
            var visita = await _visitaRepository.GetByIdAsync(id);
            if (visita == null)
            {
                return NotFound();
            }

            var visitaDto = new VisitaDTO
            {
                Id = visita.Id,
                FechaHora = visita.FechaHora,
                Motivo = visita.Motivo,
                Comentarios = visita.Comentarios,
                VisitanteId = visita.VisitanteId
            };

            return Ok(visitaDto);
        }

        [HttpPost]
        public async Task<ActionResult<VisitaDTO>> PostVisita(VisitaDTO visitaDto)
        {
            var visita = new Visita
            {
                FechaHora = visitaDto.FechaHora,
                Motivo = visitaDto.Motivo,
                Comentarios = visitaDto.Comentarios,
                VisitanteId = visitaDto.VisitanteId
            };

            var created = await _visitaRepository.CreateAsync(visita);

            visitaDto.Id = created.Id;

            return CreatedAtAction(nameof(GetVisita), new { id = created.Id }, visitaDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisita(int id, VisitaDTO visitaDto)
        {
            if (id != visitaDto.Id)
            {
                return BadRequest();
            }

            var visita = new Visita
            {
                Id = visitaDto.Id,
                FechaHora = visitaDto.FechaHora,
                Motivo = visitaDto.Motivo,
                Comentarios = visitaDto.Comentarios,
                VisitanteId = visitaDto.VisitanteId
            };

            var updated = await _visitaRepository.UpdateAsync(id, visita);
            if (updated == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisita(int id)
        {
            var deleted = await _visitaRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
