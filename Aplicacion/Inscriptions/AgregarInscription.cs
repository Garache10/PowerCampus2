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

namespace Aplicacion.Inscriptions
{
    public class AgregarInscription
    {
        public class newInscription : IRequest
        {
            public DateTime inscription_day { get; set; }
            public string user_id { get; set; }
            public int status { get; set; }
        }

        public class validar : AbstractValidator<newInscription>
        {
            public validar()
            {
                //RuleFor(x => x.inscription_day).NotEmpty();
                RuleFor(x => x.user_id).NotEmpty();
                RuleFor(x => x.status).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<newInscription>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(newInscription request, CancellationToken cancellationToken)
            {
                var inscription = new T_inscription
                {
                    inscription_day = request.inscription_day,
                    user_id = request.user_id,
                    status = request.status
                };

                _context.t_inscription.Add(inscription);
                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { inscription = "No se pudo registrar la inscripción" });
            }
        }
    }
}
