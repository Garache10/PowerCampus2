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
    public class UserController : ControllerBase
    {
        private readonly PowerCampus2Context context;
        public UserController(PowerCampus2Context _context)
        {
            this.context = _context;
        }

        //Obtener todos los registros
        public async Task<ActionResult<IEnumerable<T_user>>> GetUser()
        {
            return await context.t_user.ToListAsync();
        }

        //Obtener por id_user
        [HttpGet("{id_user}")]
        public async Task<ActionResult<T_user>> GetUser(int id_user)
        {
            var user = await context.t_user.FindAsync(id_user);

            if (user == null)
            {
                return NotFound();
            }

            //Código para obtener el rol del usuario
            var role = await context.t_role.FindAsync(user.role_id);
            System.Diagnostics.Debug.WriteLine("ROLE: ", role.role);

            return user;
        }

        //Insertar un usuario
        [HttpPost]
        public async Task<ActionResult<T_user>> PostUser(T_user user)
        {
            context.t_user.Add(user);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.id_user }, user);
        }

        //Modificar un usuario
        [HttpPut("{id_user}")]
        public async Task<IActionResult> PutUser(int id_user, T_user user)
        {
            if (id_user != user.id_user)
            {
                return BadRequest();
            }

            context.Entry(user).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id_user))
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
        [HttpDelete("{id_user}")]
        public async Task<ActionResult<T_user>> DeleteUser(int id_user)
        {
            var user = await context.t_user.FindAsync(id_user);
            if (user == null)
            {
                return NotFound();
            }

            context.t_user.Remove(user);
            await context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id_user)
        {
            return context.t_user.Any(e => e.id_user == id_user);
        }
    }
}
