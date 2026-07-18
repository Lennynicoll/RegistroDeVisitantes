using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroVisitantes.Data;
using RegistroVisitantes.Models;
using RegistroVisitantes.DTOs;

namespace RegistroVisitantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VisitasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitaDTO>>> GetVisitas()
        {
            var visitas = await _context.Visitas
                .Select(v => new VisitaDTO
                {
                    Id = v.Id,
                    FechaHora = v.FechaHora,
                    Motivo = v.Motivo,
                    VisitanteId = v.VisitanteId
                }).ToListAsync();

            return Ok(visitas);
        }

        [HttpPost]
        public async Task<ActionResult<VisitaDTO>> PostVisita(VisitaDTO visitaDto)
        {
            var visita = new Visita
            {
                FechaHora = visitaDto.FechaHora,
                Motivo = visitaDto.Motivo,
                VisitanteId = visitaDto.VisitanteId
            };

            _context.Visitas.Add(visita);
            await _context.SaveChangesAsync();

            visitaDto.Id = visita.Id;

            return CreatedAtAction(nameof(GetVisitas), new { id = visita.Id }, visitaDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisita(int id, VisitaDTO visitaDto)
        {
            if (id != visitaDto.Id)
            {
                return BadRequest();
            }

            var visita = await _context.Visitas.FindAsync(id);
            if (visita == null)
            {
                return NotFound();
            }

            visita.FechaHora = visitaDto.FechaHora;
            visita.Motivo = visitaDto.Motivo;
            visita.VisitanteId = visitaDto.VisitanteId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisita(int id)
        {
            var visita = await _context.Visitas.FindAsync(id);
            if (visita == null)
            {
                return NotFound();
            }

            _context.Visitas.Remove(visita);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VisitaExists(int id)
        {
            return _context.Visitas.Any(e => e.Id == id);
        }
    }
}
