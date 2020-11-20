using Aplicacion.ManejadorError;
using Dominio;
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
    public class ConsultaIdCourse
    {
        public class OnlyCourse : IRequest<T_course>
        {
            public int id_course { get; set; }
        }

        public class Manejador : IRequestHandler<OnlyCourse, T_course>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<T_course> Handle(OnlyCourse request, CancellationToken cancellationToken)
            {
                var course = await _context.t_course.FindAsync(request.id_course);
                if (course == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { course = "No se encontró el curso" });
                }
                return course;
            }
        }
    }
}
