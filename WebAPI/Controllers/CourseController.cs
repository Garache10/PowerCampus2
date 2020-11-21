using Aplicacion.Courses;
using Dominio;
using MediatR;
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
        private readonly IMediator _mediator;
        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Obtener todos los registros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<T_course>>> GetCourse()
        {
            return await _mediator.Send(new ConsultaCourse.ListaCourses());
        }

        //Obtener por id_course
        [HttpGet("{id_course}")]
        public async Task<ActionResult<T_course>> GetCourse(int id_course)
        {
            var course = await _mediator.Send(new ConsultaIdCourse.OnlyCourse { id_course = id_course });

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        //Insertar una carrera
        [HttpPost]
        public async Task<ActionResult<Unit>> PostCourse(AgregarCourse.newCourse data)
        {
            return await _mediator.Send(data);
        }

        //Modificar una carrera
        [HttpPut("{id_course}")]
        public async Task<ActionResult<Unit>> PutCourse(int id_course, EditarCourse.editCourse data)
        {
            data.id_course = id_course;
            return await _mediator.Send(data);
        }

        //Borrar una carrera
        [HttpDelete("{id_course}")]
        public async Task<ActionResult<Unit>> DeleteCourse(int id_course)
        {
            return await _mediator.Send(new EliminarCourse.deleteCourse { id_course = id_course });
        }
    }
}
