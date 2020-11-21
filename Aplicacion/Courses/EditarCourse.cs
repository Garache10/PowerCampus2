using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Courses
{
    public class EditarCourse
    {
        public class editCourse : IRequest
        {
            public int id_course { get; set; }
            public string course { get; set; }
            public DateTime time_schedule { get; set; }
            public int career_id { get; set; }
            public int teacher_id { get; set; }
        }

        public class Manejador : IRequestHandler<editCourse>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(editCourse request, CancellationToken cancellationToken)
            {
                var course = await _context.t_course.FindAsync(request.id_course);

                if (course == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { course = "No se encontró el curso" });
                }

                course.course = request.course ?? course.course;
                /*course.time_schedule = request.time_schedule ?? course.time_schedule;
                course.career_id = request.career_id ?? course.career_id;
                course.teacher_id = request.teacher_id ?? course.teacher_id;*/

                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { course = "No se pudo editar el curso" });
            }
        }
    }
}
