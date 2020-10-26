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
    public class GroupController: ControllerBase
    {
        private readonly PowerCampus2Context context;
        public GroupController(PowerCampus2Context _context)
        {
            this.context = _context;
        }

        //Obtener todos los registros
        public async Task<ActionResult<IEnumerable<T_group>>> GetGroup()
        {
            return await context.t_group.ToListAsync();
        }

        //Obtener por id_group
        [HttpGet("{id_group}")]
        public async Task<ActionResult<T_group>> GetGroup(int id_group)
        {
            var group = await context.t_group.FindAsync(id_group);

            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

        //Insertar un grupo
        [HttpPost]
        public async Task<ActionResult<T_group>> PostGroup(T_group group)
        {
            context.t_group.Add(group);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = group.id_group }, group);
        }

        //Modificar un grupo
        [HttpPut("{id_group}")]
        public async Task<IActionResult> PutGroup(int id_group, T_group group)
        {
            if (id_group != group.id_group)
            {
                return BadRequest();
            }

            context.Entry(group).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id_group))
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

        //Borrar un grupo
        [HttpDelete("{id_group}")]
        public async Task<ActionResult<T_group>> DeleteGroup(int id_group)
        {
            var group = await context.t_group.FindAsync(id_group);
            if (group == null)
            {
                return NotFound();
            }

            context.t_group.Remove(group);
            await context.SaveChangesAsync();

            return group;
        }

        private bool GroupExists(int id_group)
        {
            return context.t_group.Any(e => e.id_group == id_group);
        }
    }
}
