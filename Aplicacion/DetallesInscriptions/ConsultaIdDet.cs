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

namespace Aplicacion.DetallesInscriptions
{
    public class ConsultaIdDet
    {
        public class OnlyDet : IRequest<T_det_inscription>
        {
            public int id_det_inscription { get; set; }
        }

        public class Manejador : IRequestHandler<OnlyDet, T_det_inscription>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<T_det_inscription> Handle(OnlyDet request, CancellationToken cancellationToken)
            {
                var det = await _context.t_det_inscription.FindAsync(request.id_det_inscription);
                if (det == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { detalle = "No se encontró el detalle" });
                }
                return det;
            }
        }
    }
}
