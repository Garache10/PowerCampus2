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
    public class EditarUser
    {
        public class editUser : IRequest
        {
            public string Id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            public int role { get; set; }
        }

        public class Manejador : IRequestHandler<editUser>
        {
            private readonly UserManager<T_user> _userManager;
            public Manejador(UserManager<T_user> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(editUser request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.Id);

                if (user == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { user = "No se encontró el usuario" });
                }

                user.UserName = request.username ?? user.UserName;
                user.PasswordHash = request.password ?? user.PasswordHash;
                user.firstname = request.firstname ?? user.firstname;
                user.lastname = request.lastname ?? user.lastname;
                user.Email = request.email ?? user.Email;
                user.role = request.role; //?? user.role_id;

                var valor = await _userManager.UpdateAsync(user);
                if (valor.Succeeded)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { user = "No se pudo editar el usuario" });
            }
        }
    }
}
