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
    public class Editar
    {
        public class editUser : IRequest
        {
            public int id_user { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            //public int role_id { get; set; }
        }

        public class Manejador : IRequestHandler<editUser>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(editUser request, CancellationToken cancellationToken)
            {
                var user = await _context.t_user.FindAsync(request.id_user);

                if(user == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { user = "No se encontró el usuario" });
                }

                user.username = request.username ?? user.username;
                user.password = request.password ?? user.password;
                user.firstname = request.firstname ?? user.firstname;
                user.lastname = request.lastname ?? user.lastname;
                user.email = request.email ?? user.email;
                //user.role_id = request.role_id ?? user.role_id;

                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { user = "No se pudo editar el usuario" });
            }
        }
    }
}
