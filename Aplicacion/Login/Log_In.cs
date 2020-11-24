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

namespace Aplicacion.Login
{
    public class Log_In
    {
        public class Logeo : IRequest<T_user>
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Logeo>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.username).NotEmpty();
                RuleFor(x => x.password).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Logeo, T_user>
        {
            private readonly UserManager<T_user> _userManager;
            private readonly SignInManager<T_user> _signInManager;

            public Manejador(UserManager<T_user> userManager, SignInManager<T_user> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            public async Task<T_user> Handle(Logeo request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByNameAsync(request.username);
                if (usuario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
                }

                var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.password, false);
                if (resultado.Succeeded)
                {
                    return new T_user
                    {

                        Id = usuario.Id,
                        UserName = usuario.UserName,
                        firstname = usuario.firstname,
                        lastname = usuario.lastname,
                        Email = usuario.Email
                    };
                }

                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
            }            
        }
    }
}
