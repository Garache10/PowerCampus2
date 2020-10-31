using Dominio;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Users
{
    public class Agregar
    {
        public class newUser: IRequest
        {
            public string username { get; set; }
            public string password { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            public int role_id { get; set; }
        }

        public class Manejador: IRequestHandler<newUser>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(newUser request, CancellationToken cancellationToken)
            {
                var user = new T_user
                {
                    username = request.username,
                    password = request.password,
                    firstname = request.firstname,
                    lastname = request.lastname,
                    email = request.email,
                    role_id = request.role_id
                };

                _context.t_user.Add(user);
                var valor = await _context.SaveChangesAsync();
                if(valor > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("cannot add new user");
            }
        }
    }
}
