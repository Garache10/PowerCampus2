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

namespace Aplicacion.Users
{
    public class ConsultaId
    {
        public class OnlyUser: IRequest<T_user>
        {
            public int id_user { get; set; }
        }

        public class Manejador: IRequestHandler<OnlyUser, T_user>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<T_user> Handle(OnlyUser request, CancellationToken cancellationToken)
            {
                var user = await _context.t_user.FindAsync(request.id_user);
                if(user == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { user = "No se encontró el usuario" });
                }
                return user;
            }
        }

    }
}
