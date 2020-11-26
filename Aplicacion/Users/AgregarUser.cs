using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
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
    public class AgregarUser
    {
        public class newUser : IRequest
        {
            public string username { get; set; }
            public string password { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            public int role { get; set; }
        }

        public class validar : AbstractValidator<newUser>
        {
            public validar()
            {
                RuleFor(x => x.username).NotEmpty();
                RuleFor(x => x.password).NotEmpty();
                RuleFor(x => x.email).NotEmpty();
                //RuleFor(x => x.role).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<newUser>
        {
            private readonly UserManager<T_user> _userManager;
            public Manejador(UserManager<T_user> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(newUser request, CancellationToken cancellationToken)
            {
                var pass = request.password;
                var user = new T_user
                {                   
                    firstname = request.firstname,
                    lastname = request.lastname,
                    UserName = request.username,
                    Email = request.email
                };
                var valor = await _userManager.CreateAsync(user, pass);

                if (valor.Succeeded)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { user = "No se pudo registrar el usuario" });
            }
        }
    }
}
