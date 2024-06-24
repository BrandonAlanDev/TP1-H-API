using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TP1_API.DB;
using TP1_API.Models;

namespace TP1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpinionController : ControllerBase
    {
        private readonly Conexion _conexion;

        public OpinionController()
        {
            _conexion = new Conexion();
        }

        // GET: api/Opinion
        [HttpGet]
        public ActionResult<IEnumerable<Opinion>> GetOpinions()
        {
            var opinions = _conexion.GetOpinions();
            return Ok(opinions);
        }

        // GET: api/Opinion/user/{nombreUsuario}
        [HttpGet("user/{nombreUsuario}")]
        public ActionResult<IEnumerable<Opinion>> GetOpinionsByUser(string nombreUsuario)
        {
            var opinions = _conexion.GetOpinionsByUser(nombreUsuario);
            if (opinions == null)
            {
                return NotFound();
            }

            return Ok(opinions);
        }

        // DELETE: api/Opinion
        [HttpDelete]
        public IActionResult DeleteOpinion([FromBody] int idComentario)
        {
            _conexion.DeleteOpinion(idComentario);
            return NoContent();
        }

        // POST: api/Opinion
        [HttpPost]
        public IActionResult PostOpinion([FromBody] Opinion opinion)
        {
            if (opinion == null)
            {
                return BadRequest();
            }

            // Establecer visible en true (1) por defecto
            opinion.visible = true;
            _conexion.InsertOpinion(opinion);

            return CreatedAtAction(nameof(GetOpinions), new { id = opinion.id_comentario }, opinion);
        }
    }
}