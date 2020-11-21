using Aplicacion.ManejadorError;
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
    public class EliminarDet
    {
        public class deleteDet : IRequest
        {
            public int id_det_inscription { get; set; }
        }

        public class Manejador : IRequestHandler<deleteDet>
        {
            private readonly PowerCampus2Context context;

            public Manejador(PowerCampus2Context _context)
            {
                context = _context;
            }

            public async Task<Unit> Handle(deleteDet request, CancellationToken cancellationToken)
            {
                var det = await context.t_det_inscription.FindAsync(request.id_det_inscription);
                if (det == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { detalles = "No se encontró el detalle" });
                }
                context.Remove(det);

                var resultados = await context.SaveChangesAsync();
                if (resultados > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { user = "No se pudo eliminar el detalle" });
            }
        }
    }
}
