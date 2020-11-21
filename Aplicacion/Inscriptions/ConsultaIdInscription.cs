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

namespace Aplicacion.Inscriptions
{
    public class ConsultaIdInscription
    {
        public class OnlyInscription : IRequest<T_inscription>
        {
            public int id_inscription { get; set; }
        }

        public class Manejador : IRequestHandler<OnlyInscription, T_inscription>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<T_inscription> Handle(OnlyInscription request, CancellationToken cancellationToken)
            {
                var inscription = await _context.t_inscription.FindAsync(request.id_inscription);
                if (inscription == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { inscription = "No se encontró la inscripción" });
                }
                return inscription;
            }
        }
    }
}
