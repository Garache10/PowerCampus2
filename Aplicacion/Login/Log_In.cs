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

namespace Aplicacion.Login
{
    public class Log_In
    {
        public class Logeo : IRequest<T_user>
        {
            public int id_user { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            public int role_id { get; set; }
        }

        public class Manejador : IRequestHandler<Logeo, T_user>
        {
            private readonly PowerCampus2Context context;

            public Manejador(PowerCampus2Context _context)
            {
                context = _context;
            }

            public async Task<T_user> Handle(Logeo request, CancellationToken cancellationToken)
            {
                var user = await context.t_user.FindAsync(request.username);
                var pass = request.password;
                if (user == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { login = "Credenciales incorrectas" });
                }
                else if (user.password != pass)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { user = "Credenciales incorrectas" });
                }
                return user;
            }
        }
    }
}
