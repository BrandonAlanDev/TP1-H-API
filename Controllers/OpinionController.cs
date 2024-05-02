using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP1_API.Models;

namespace TP1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpinionController : ControllerBase
    {
        private readonly OpinionContext _context;

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
            _context.Opinions.Add(opinion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOpinion", new { id = opinion.id }, opinion);
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
}
