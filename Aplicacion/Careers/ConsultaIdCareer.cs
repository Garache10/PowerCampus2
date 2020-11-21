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

namespace Aplicacion.Careers
{
    public class ConsultaIdCareer
    {
        public class OnlyCareer : IRequest<T_career>
        {
            public int id_career { get; set; }
        }

        public class Manejador : IRequestHandler<OnlyCareer, T_career>
        {
            public readonly PowerCampus2Context context;

            public Manejador(PowerCampus2Context _context)
            {
                context = _context;
            }

            public async Task<T_career> Handle(OnlyCareer request, CancellationToken cancellationToken)
            {
                var career = await context.t_career.FindAsync(request.id_career);
                if (career == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { career = "No se encontró la carrera" });
                }
                return career;
            }
        }
    }
}
