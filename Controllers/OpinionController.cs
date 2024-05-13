using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP1_API.Models;
using Microsoft.AspNetCore.JsonPatch;


namespace TP1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpinionController : ControllerBase
    {
        private readonly OpinionContext _context;
        private static int contadorId = 0;

        public OpinionController(OpinionContext context)
        {
            _context = context;
        }
        
        // GET: api/Opinion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Opinion>>> GetOpinions()
        {
            return await _context.Opinions.ToListAsync();
        }

        // GET: api/Opinion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Opinion>> GetOpinion(int id)
        {
            var opinion = await _context.Opinions.FindAsync(id);

            if (opinion == null)
            {
                return NotFound();
            }

            return opinion;
        }

        // GET: api/Opinion/user/Brandon
        [HttpGet("user/{user}")]
        public async Task<ActionResult<IEnumerable<Opinion>>> GetOpinion(string user)
        {
            var opinions = await _context.Opinions.Where(o => o.user == user).ToListAsync();

            if (opinions == null)
            {
                return NotFound();
            }

            return opinions;
        }

        // PUT: api/Opinion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOpinion(int id, Opinion opinion)
        {
            if (id != opinion.id)
            {
                return BadRequest();
            }

            _context.Entry(opinion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpinionExists(id))
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

        // POST: api/Opinion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Opinion>> PostOpinion(Opinion opinion)
        {
            
            contadorId++; // Incrementar el contador de ID.
            opinion.id = contadorId; // Asignar el nuevo ID al objeto Opinion.
            _context.Opinions.Add(opinion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOpinion", new { id = opinion.id }, opinion);
        }

        // PATCH: api/Opinion/1
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchOpinion(int id, [FromBody] JsonPatchDocument<Opinion> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var opinionToUpdate = await _context.Opinions.FirstOrDefaultAsync(o => o.id == id);

            if (opinionToUpdate == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(opinionToUpdate, (Microsoft.AspNetCore.JsonPatch.JsonPatchError err) => ModelState.AddModelError("JsonPatch", err.ErrorMessage));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Opinion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOpinion(int id)
        {
            var opinion = await _context.Opinions.FindAsync(id);
            if (opinion == null)
            {
                return NotFound();
            }

            _context.Opinions.Remove(opinion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OpinionExists(int id)
        {
            return _context.Opinions.Any(e => e.id == id);
        }
    }
    /*
    private string LimpiarYValidarOpinion(Opinion campo)
    {
        cadena = cadena?.Trim();

        if (campo == Campo.user)
        {
            if (string.IsNullOrEmpty(cadena) || cadena.Length < 3 || cadena.Length > 20)
            {
                return null;
            }
        }
        else if (campo == Campo.Comment)
        {
            if (string.IsNullOrEmpty(cadena) || cadena.Length < 1 || cadena.Length > 500)
            {
                return null;
            }
        }

        return cadena; // Retornamos la cadena limpia y validada
    }
    */
}
