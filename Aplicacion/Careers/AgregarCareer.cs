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

namespace Aplicacion.Careers
{
    public class AgregarCareer
    {
        public class newCareer: IRequest
        {
            public string career { get; set; }
        }

        public class validar : AbstractValidator<newCareer>
        {
            public validar()
            {
                RuleFor(x => x.career).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<newCareer>
        {
            public readonly PowerCampus2Context context;

            public Manejador(PowerCampus2Context _context)
            {
                context = _context;
            }

            public async Task<Unit> Handle(newCareer request, CancellationToken cancellationToken)
            {
                var Career = new T_career
                {
                    career = request.career
                };

                context.t_career.Add(Career);
                var valor = await context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { career = "No se pudo registrar la carrera" });
            }
        }
    }
}
