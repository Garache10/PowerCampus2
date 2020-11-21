using Aplicacion.Login;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Logeo de usuario
        [HttpGet("{username}/{password}")]
        public async Task<ActionResult<T_user>> LogUser(string username, string password)
        {
            var log = await _mediator.Send(new Log_In.Logeo { username = username, password = password });
            if (log == null)
            {
                return NotFound();
            }
            return log;
        }
    }
}
