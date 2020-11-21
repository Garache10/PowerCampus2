using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Users
{
    public class Eliminar
    {
        public class deleteUser : IRequest
        {
            public int id_user { get; set; }
        }

        public class Manejador : IRequestHandler<deleteUser>
        {
            private readonly PowerCampus2Context context;

            public Manejador(PowerCampus2Context _context)
            {
                context = _context;
            }

            public async Task<Unit> Handle(deleteUser request, CancellationToken cancellationToken)
            {
                var user = await context.t_user.FindAsync(request.id_user);
                if(user == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { user = "No se encontró el usuario" });
                }
                context.Remove(user);

                var resultados = await context.SaveChangesAsync();
                if(resultados > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { user = "No se pudo eliminar el usuario" });
            }
        }
    }
}
