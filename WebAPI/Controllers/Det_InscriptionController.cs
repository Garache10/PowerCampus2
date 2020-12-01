using Aplicacion.DetallesInscriptions;
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
    public class Det_InscriptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public Det_InscriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Obtener todos los registros
        public async Task<ActionResult<IEnumerable<T_det_inscription>>> GetDetInscription()
        {
            return await _mediator.Send(new ConsultaDet.ListaDetInscription());
        }

        //Obtener por id_det_inscription
        [HttpGet("{id_det_inscription}")]
        public async Task<ActionResult<T_det_inscription>> GetDetInscription(int id_det_inscription)
        {
            var det = await _mediator.Send(new ConsultaIdDet.OnlyDet { id_det_inscription = id_det_inscription });
            if (det == null)
            {
                return NotFound();
            }
            return det;
        }

        [HttpGet("classes/{inscription_id}")]
        public async Task<ActionResult<IEnumerable<T_det_inscription>>> GetDetByInscription(int inscription_id)
        {
            var classes = await _mediator.Send(new ConsultaDetailsByInscription.DetailsByInscription { inscription_id = inscription_id });
            if (classes == null)
            {
                return NotFound();
            }
            return classes;
        }

        //Insertar un detalle
        [HttpPost]
        public async Task<ActionResult<Unit>> PostDet(AgregarDet.newDet data)
        {
            return await _mediator.Send(data);
        }

        //Modificar un detalle por id_det_inscription
        [HttpPut("{id_det_inscription}")]
        public async Task<ActionResult<Unit>> PutDet(int id_det_inscription, EditarDet.editDet data)
        {
            data.id_det_inscription = id_det_inscription;
            return await _mediator.Send(data);
        }

        //Borrar un detalle por id_det_inscription
        [HttpDelete("{id_det_inscription}")]
        public async Task<ActionResult<Unit>> DeleteDet(int id_det_inscription)
        {
            return await _mediator.Send(new EliminarDet.deleteDet { id_det_inscription = id_det_inscription });
        }
    }
}
