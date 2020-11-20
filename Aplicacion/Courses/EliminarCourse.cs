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
    public class EliminarCourse
    {
        public class deleteCourse : IRequest
        {
            public int id_course { get; set; }
        }

        public class Manejador : IRequestHandler<deleteCourse>
        {
            private readonly PowerCampus2Context context;

            public Manejador(PowerCampus2Context _context)
            {
                context = _context;
            }

            public async Task<Unit> Handle(deleteCourse request, CancellationToken cancellationToken)
            {
                var course = await context.t_course.FindAsync(request.id_course);
                if (course == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { course = "No se encontró el curso" });
                }
                context.Remove(course);

                var resultados = await context.SaveChangesAsync();
                if (resultados > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { curso = "No se pudo eliminar el curso" });
            }
        }
    }
}
