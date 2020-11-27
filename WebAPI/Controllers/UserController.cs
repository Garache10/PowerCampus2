using Aplicacion.Users;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class UserController : IControllerBase_
    {
        public async Task<ActionResult<IEnumerable<T_user>>> GetUser()
        {
            return await Mediator.Send(new ConsultaUser.ListaUsers());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<T_user>> GetUser(string Id)
        {
            Id = (string)this.RouteData.Values["Id"];
            var user = await Mediator.Send(new ConsultaIdUser.OnlyUser { id = Id });

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> PostUser(AgregarUser.newUser data)
        {
            return await Mediator.Send(data);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Unit>> PutUser(string id_user, EditarUser.editUser data)
        {
            id_user = (string)this.RouteData.Values["Id"];
            data.Id = id_user;
            return await Mediator.Send(data);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Unit>> DeleteUser(string id_user)
        {
            id_user = (string)this.RouteData.Values["Id"];
            return await Mediator.Send(new EliminarUser.deleteUser { Id = id_user });
        }
    }
}
