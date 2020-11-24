using Aplicacion.Login;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : IControllerBase_
    {
        //Logeo de usuario
        [HttpPost]
        public async Task<ActionResult<DataUsuarioFront>> Login_(Log_In.Logeo parametros)
        {
            return await Mediator.Send(parametros);
        }
    }
}
