using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
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
    public class AgregarCourse
    {
        public class newCourse : IRequest
        {
            public string course { get; set; }
            public DateTime time_schedule { get; set; }
            public int career_id { get; set; }
            public Guid teacher_id { get; set; }
        }

        public class validar : AbstractValidator<newCourse>
        {
            public validar()
            {
                RuleFor(x => x.course).NotEmpty();
                RuleFor(x => x.career_id).NotEmpty();
                //RuleFor(x => x.teacher_id).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<newCourse>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(newCourse request, CancellationToken cancellationToken)
            {
                var course = new T_course
                {
                    course = request.course,
                    time_schedule = request.time_schedule,
                    career_id = request.career_id,
                    teacher_id = request.teacher_id
                };

                _context.t_course.Add(course);
                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { course = "No se pudo registrar el curso" });
            }
        }
    }
}
