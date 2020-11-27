using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Users
{
    public class ConsultaIdUser
    {
        public class OnlyUser : IRequest<T_user>
        {
            public string id { get; set; }
        }

        public class Manejador : IRequestHandler<OnlyUser, T_user>
        {
            private readonly UserManager<T_user> _userManager;
            public Manejador(UserManager<T_user> userManager)
            {
                _userManager = userManager;
            }

            public async Task<T_user> Handle(OnlyUser request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.id);
                if (user == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { user = "No se encontró el usuario" });
                }
                return user;
            }
        }
    }
}
