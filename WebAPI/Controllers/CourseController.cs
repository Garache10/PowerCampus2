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
    public class CourseController: ControllerBase
    {
        private readonly PowerCampus2Context context;
        public CourseController(PowerCampus2Context _context)
        {
            this.context = _context;
        }

        //Obtener todos los registros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<T_course>>> GetCourse()
        {
            return await context.t_course.ToListAsync();
        }

        //Obtener por id_course
        [HttpGet("{id_course}")]
        public async Task<ActionResult<T_course>> GetCourse(int id_course)
        {
            var course = await context.t_course.FindAsync(id_course);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        //Insertar una carrera
        [HttpPost]
        public async Task<ActionResult<T_course>> PostCourse(T_course car)
        {
            context.t_course.Add(car);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = car.id_course }, car);
        }

        //Modificar una carrera
        [HttpPut("{id_course}")]
        public async Task<IActionResult> PutCourse(int id_course, T_course car)
        {
            if (id_course != car.id_course)
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
                if (!CourseExists(id_course))
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
        [HttpDelete("{id_course}")]
        public async Task<ActionResult<T_course>> DeleteCourse(int id_course)
        {
            var course = await context.t_course.FindAsync(id_course);
            if (course == null)
            {
                return NotFound();
            }

            context.t_course.Remove(course);
            await context.SaveChangesAsync();

            return course;
        }

        private bool CourseExists(int id_course)
        {
            return context.t_course.Any(e => e.id_course == id_course);
        }
    }
}
