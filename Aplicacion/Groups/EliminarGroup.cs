using Aplicacion.ManejadorError;
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
    public class EliminarGroup
    {
        public class deleteGroup : IRequest
        {
            public int id_group { get; set; }
        }

        public class Manejador : IRequestHandler<deleteGroup>
        {
            private readonly PowerCampus2Context context;

            public Manejador(PowerCampus2Context _context)
            {
                context = _context;
            }

            public async Task<Unit> Handle(deleteGroup request, CancellationToken cancellationToken)
            {
                var group = await context.t_group.FindAsync(request.id_group);
                if (group == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { group = "No se encontró el grupo" });
                }
                context.Remove(group);

                var resultados = await context.SaveChangesAsync();
                if (resultados > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { group = "No se pudo eliminar el grupo" });
            }
        }
    }
}
