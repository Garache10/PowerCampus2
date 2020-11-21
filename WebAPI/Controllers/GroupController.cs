using Aplicacion.Groups;
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
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Obtener todos los registros
        public async Task<ActionResult<IEnumerable<T_group>>> GetGroup()
        {
            return await _mediator.Send(new ConsultaGroup.ListaGroups());
        }

        //Obtener por id_group
        [HttpGet("{id_group}")]
        public async Task<ActionResult<T_group>> GetGroup(int id_group)
        {
            var group = await _mediator.Send(new ConsultaIdGroup.OnlyGroup { id_group = id_group });

            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

        //Insertar un grupo
        [HttpPost]
        public async Task<ActionResult<Unit>> PostGroup(AgregarGroup.newGroup data)
        {
            return await _mediator.Send(data);
        }

        //Modificar un grupo
        [HttpPut("{id_group}")]
        public async Task<ActionResult<Unit>> PutGroup(int id_group, EditarGroup.editGroup data)
        {
            data.id_group = id_group;
            return await _mediator.Send(data);
        }

        //Borrar un grupo
        [HttpDelete("{id_group}")]
        public async Task<ActionResult<Unit>> DeleteGroup(int id_group)
        {
            return await _mediator.Send(new EliminarGroup.deleteGroup { id_group = id_group });
        }
    }
}
