using Aplicacion.Inscriptions;
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
    public class InscriptionController: ControllerBase
    {
        private readonly IMediator _mediator;
        public InscriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Obtener todos los registros
        public async Task<ActionResult<IEnumerable<T_inscription>>> GetInscription()
        {
            return await _mediator.Send(new ConsultaInscription.ListarInscriptions());
        }

        //Obtener por id_inscription
        [HttpGet("{id_inscription}")]
        public async Task<ActionResult<T_inscription>> GetInscription(int id_inscription)
        {
            var inscription = await _mediator.Send(new ConsultaIdInscription.OnlyInscription { id_inscription = id_inscription });

            if (inscription == null)
            {
                return NotFound();
            }

            return inscription;
        }

        //Insertar un usuario
        [HttpPost]
        public async Task<ActionResult<Unit>> PostInscription(AgregarInscription.newInscription data)
        {
            return await _mediator.Send(data);
        }

        //Modificar un usuario
        [HttpPut("{id_inscription}")]
        public async Task<ActionResult<Unit>> PutInscription(int id_inscription, EditarInscription.editInscription data)
        {
            data.id_inscription = id_inscription;
            return await _mediator.Send(data);
        }

        //Borrar un usuario
        [HttpDelete("{id_inscription}")]
        public async Task<ActionResult<Unit>> DeleteInscription(int id_inscription)
        {
            return await _mediator.Send(new EliminarInscription.deleteInscription { id_inscription = id_inscription });
        }
    }
}
