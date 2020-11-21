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
    public class AgregarGroup
    {
        public class newGroup : IRequest
        {
            public int course_id { get; set; }
            public int quota { get; set; }
        }

        public class validar : AbstractValidator<newGroup>
        {
            public validar()
            {
                RuleFor(x => x.course_id).NotEmpty();
                RuleFor(x => x.quota).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<newGroup>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(newGroup request, CancellationToken cancellationToken)
            {
                var group = new T_group
                {
                    course_id = request.course_id,
                    quota = request.quota
                };

                _context.t_group.Add(group);
                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { group = "No se pudo registrar el grupo" });
            }
        }
    }
}
