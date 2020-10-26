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
    public class CareerController: ControllerBase
    {
        private readonly PowerCampus2Context context;
        public CareerController(PowerCampus2Context _context)
        {
            this.context = _context;
        }

        //Obtener todos los registros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<T_career>>> GetCareer()
        {
            return await context.t_career.ToListAsync();
        }

        //Obtener por id_career
        [HttpGet("{id_career}")]
        public async Task<ActionResult<T_career>> GetCareer(int id_career)
        {
            var career = await context.t_career.FindAsync(id_career);

            if (career == null)
            {
                return NotFound();
            }

            return career;
        }

        //Insertar una carrera
        [HttpPost]
        public async Task<ActionResult<T_career>> PostCareer(T_career car)
        {
            context.t_career.Add(car);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCareer", new { id = car.id_career }, car);
        }

        //Modificar una carrera
        [HttpPut("{id_career}")]
        public async Task<IActionResult> PutCareer(int id_career, T_career car)
        {
            if (id_career != car.id_career)
            {
                return BadRequest();
            }

            context.Entry(car).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CareerExists(id_career))
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

        //Borrar una carrera
        [HttpDelete("{id_career}")]
        public async Task<ActionResult<T_career>> DeleteCareer(int id_career)
        {
            var career = await context.t_career.FindAsync(id_career);
            if (career == null)
            {
                return NotFound();
            }

            context.t_career.Remove(career);
            await context.SaveChangesAsync();

            return career;
        }

        private bool CareerExists(int id_career)
        {
            return context.t_career.Any(e => e.id_career == id_career);
        }
    }
}
