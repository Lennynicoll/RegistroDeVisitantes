using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroVisitantes.Data;
using RegistroVisitantes.Models;
using RegistroVisitantes.DTOs;

namespace RegistroVisitantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitantesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VisitantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitanteDTO>>> GetVisitantes()
        {
            var visitantes = await _context.Visitantes
                .Select(v => new VisitanteDTO
                {
                    Id = v.Id,
                    Nombre = v.Nombre,
                    Apellido = v.Apellido,
                    Cedula = v.Cedula
                }).ToListAsync();

            return Ok(visitantes);
        }

        [HttpPost]
        public async Task<ActionResult<VisitanteDTO>> PostVisitante(VisitanteDTO visitanteDto)
        {
            var visitante = new Visitante
            {
                Nombre = visitanteDto.Nombre,
                Apellido = visitanteDto.Apellido,
                Cedula = visitanteDto.Cedula
            };

            _context.Visitantes.Add(visitante);
            await _context.SaveChangesAsync();

            visitanteDto.Id = visitante.Id;

            return CreatedAtAction(nameof(GetVisitantes), new { id = visitante.Id }, visitanteDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitante(int id, VisitanteDTO visitanteDto)
        {
            if (id != visitanteDto.Id)
            {
                return BadRequest();
            }

            var visitante = await _context.Visitantes.FindAsync(id);
            if (visitante == null)
            {
                return NotFound();
            }

            visitante.Nombre = visitanteDto.Nombre;
            visitante.Apellido = visitanteDto.Apellido;
            visitante.Cedula = visitanteDto.Cedula;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitanteExists(id))
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
        public async Task<IActionResult> DeleteVisitante(int id)
        {
            var visitante = await _context.Visitantes.FindAsync(id);
            if (visitante == null)
            {
                return NotFound();
            }

            _context.Visitantes.Remove(visitante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VisitanteExists(int id)
        {
            return _context.Visitantes.Any(e => e.Id == id);
        }
    }
}
