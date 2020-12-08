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
    public class EditarDet
    {
        public class editDet : IRequest
        {
            public int id_det_inscription { get; set; }
            public int inscription_id { get; set; }
            public int group_id { get; set; }
            public int nota { get; set; }
        }

        public class Manejador : IRequestHandler<editDet>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(editDet request, CancellationToken cancellationToken)
            {
                var det = await _context.t_det_inscription.FindAsync(request.id_det_inscription);

                if (det == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { detalles = "No se encontró el detalle" });
                }

                det.group_id = request.group_id;
                det.inscription_id = request.inscription_id;
                det.nota = request.nota;

                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { detalles = "No se pudo editar el detalle" });
            }
        }
    }
}
