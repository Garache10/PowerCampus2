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
        public async Task<ActionResult<T_user>> GetUser(string id_user)
        {
            var user = await Mediator.Send(new ConsultaIdUser.OnlyUser { id = id_user });

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
            data.Id = id_user;
            return await Mediator.Send(data);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Unit>> DeleteUser(string id_user)
        {
            return await Mediator.Send(new EliminarUser.deleteUser { Id = id_user });
        }
    }
}
