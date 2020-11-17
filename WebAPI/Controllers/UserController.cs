using Aplicacion.Users;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Obtener todos los registros
        public async Task<ActionResult<IEnumerable<T_user>>> GetUser()
        {
            return await _mediator.Send(new Consulta.ListaUsers());
        }

        //Obtener por id_user
        [HttpGet("{id_user}")]
        public async Task<ActionResult<T_user>> GetUser(int id_user)
        {
            var user = await _mediator.Send(new ConsultaId.OnlyUser { id_user = id_user });

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        //Insertar un usuario
        [HttpPost]
        public async Task<ActionResult<Unit>> PostUser(Agregar.newUser data)
        {
            return await _mediator.Send(data);
        }

        //Modificar un usuario
        [HttpPut("{id_user}")]
        public async Task<ActionResult<Unit>> PutUser(int id_user, Editar.editUser data)
        {
            data.id_user = id_user;
            return await _mediator.Send(data);
        }

        //Borrar un usuario
        [HttpDelete("{id_user}")]
        public async Task<ActionResult<Unit>> DeleteUser(int id_user)
        {
            return await _mediator.Send(new Eliminar.deleteUser { id_user = id_user });
        }

        /*
        private bool UserExists(int id_user)
        {
            return _mediator.t_user.Any(e => e.id_user == id_user);
        }*/
    }
}
