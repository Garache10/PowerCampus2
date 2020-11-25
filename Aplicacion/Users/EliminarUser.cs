using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Users
{
    public class EliminarUser
    {
        public class deleteUser : IRequest
        {
            public string Id { get; set; }
        }

        public class Manejador : IRequestHandler<deleteUser>
        {
            private readonly UserManager<T_user> _userManager;

            public Manejador(UserManager<T_user> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(deleteUser request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.Id);
                if (user == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { user = "No se encontró el usuario" });
                }

                var resultados = await _userManager.DeleteAsync(user);
                if (resultados.Succeeded)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { user = "No se pudo eliminar el usuario" });
            }
        }
    }
}
