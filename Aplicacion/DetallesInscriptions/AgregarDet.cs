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

namespace Aplicacion.DetallesInscriptions
{
    public class AgregarDet
    {
        public class newDet : IRequest
        {
            public int inscription_id { get; set; }
            public int course_id { get; set; }
        }

        public class validar : AbstractValidator<newDet>
        {
            public validar()
            {
                RuleFor(x => x.course_id).NotEmpty();
                RuleFor(x => x.inscription_id).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<newDet>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(newDet request, CancellationToken cancellationToken)
            {
                var det = new T_det_inscription
                {
                    course_id = request.course_id,
                    inscription_id = request.inscription_id
                };

                _context.t_det_inscription.Add(det);
                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { Detalles = "No se pudo registrar el detalle" });
            }
        }
    }
}
