using Aplicacion.ManejadorError;
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
    public class EliminarInscription
    {
        public class deleteInscription : IRequest
        {
            public int id_inscription { get; set; }
        }

        public class Manejador : IRequestHandler<deleteInscription>
        {
            private readonly PowerCampus2Context context;

            public Manejador(PowerCampus2Context _context)
            {
                context = _context;
            }

            public async Task<Unit> Handle(deleteInscription request, CancellationToken cancellationToken)
            {
                var inscription = await context.t_inscription.FindAsync(request.id_inscription);
                if (inscription == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { inscription = "No se encontró la incripción" });
                }
                context.Remove(inscription);

                var resultados = await context.SaveChangesAsync();
                if (resultados > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { inscription = "No se pudo eliminar la inscripción" });
            }
        }
    }
}
