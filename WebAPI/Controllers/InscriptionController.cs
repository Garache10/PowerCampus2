using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscriptionController: ControllerBase
    {
        private readonly PowerCampus2Context context;
        public InscriptionController(PowerCampus2Context _context)
        {
            this.context = _context;
        }

        //Obtener todos los registros
        public async Task<ActionResult<IEnumerable<T_inscription>>> GetInscription()
        {
            return await context.t_inscription.ToListAsync();
        }

        //Obtener por id_inscription
        [HttpGet("{id_inscription}")]
        public async Task<ActionResult<T_inscription>> GetInscription(int id_inscription)
        {
            var inscription = await context.t_inscription.FindAsync(id_inscription);

            if (inscription == null)
            {
                return NotFound();
            }

            return inscription;
        }

        //Insertar un usuario
        [HttpPost]
        public async Task<ActionResult<T_inscription>> PostInscription(T_inscription inscription)
        {
            context.t_inscription.Add(inscription);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetInscription", new { id = inscription.id_inscription }, inscription);
        }

        //Modificar un usuario
        [HttpPut("{id_inscription}")]
        public async Task<IActionResult> PutInscription(int id_inscription, T_inscription inscription)
        {
            if (id_inscription != inscription.id_inscription)
            {
                return BadRequest();
            }

            context.Entry(inscription).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscriptionExists(id_inscription))
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

        //Borrar un usuario
        [HttpDelete("{id_inscription}")]
        public async Task<ActionResult<T_inscription>> DeleteInscription(int id_inscription)
        {
            var inscription = await context.t_inscription.FindAsync(id_inscription);
            if (inscription == null)
            {
                return NotFound();
            }

            context.t_inscription.Remove(inscription);
            await context.SaveChangesAsync();

            return inscription;
        }

        private bool InscriptionExists(int id_inscription)
        {
            return context.t_inscription.Any(e => e.id_inscription == id_inscription);
        }
    }
}
