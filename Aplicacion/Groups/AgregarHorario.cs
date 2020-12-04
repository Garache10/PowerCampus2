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

namespace Aplicacion.Groups
{
    public class AgregarHorario
    {
        public class newHorario : IRequest
        {
            public string horarioInicio { get; set; }
            public string horarioFin { get; set; }
            public string day { get; set; }
            public int group_id { get; set; }
        }

        public class validar : AbstractValidator<newHorario>
        {
            public validar()
            {
                RuleFor(x => x.horarioInicio).NotEmpty();
                RuleFor(x => x.horarioFin).NotEmpty();
                RuleFor(x => x.day).NotEmpty();
                RuleFor(x => x.group_id).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<newHorario>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(newHorario request, CancellationToken cancellationToken)
            {
                var horario = new T_horario
                {
                    horarioInicio = request.horarioInicio,
                    horarioFin = request.horarioFin,
                    day = request.day,
                    group_id = request.group_id
                };

                _context.t_horario.Add(horario);
                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { group = "No se pudo guardar el horario" });
            }
        }
    }
}
