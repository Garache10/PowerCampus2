using Aplicacion.Careers;
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
    public class CareerController: ControllerBase
    {
        private readonly IMediator _mediator;
        public CareerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Obtener todos los registros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<T_career>>> GetCareer()
        {
            return await _mediator.Send(new ConsultaCareer.ListaCareers());
        }

        //Obtener por id_career
        [HttpGet("{id_career}")]
        public async Task<ActionResult<T_career>> GetCareer(int id_career)
        {
            var career = await _mediator.Send(new ConsultaIdCareer.OnlyCareer{ id_career = id_career});

            if (career == null)
            {
                return NotFound();
            }

            return career;
        }

        //Insertar una carrera
        [HttpPost]
        public async Task<ActionResult<Unit>> PostCareer(AgregarCareer.newCareer data)
        {
            return await _mediator.Send(data);
        }

        //Modificar una carrera
        [HttpPut("{id_career}")]
        public async Task<ActionResult<Unit>> PutCareer(int id_career, EditarCareer.editCareer data)
        {
            data.id_career = id_career;
            return await _mediator.Send(data);
        }

        //Borrar una carrera
        [HttpDelete("{id_career}")]
        public async Task<ActionResult<Unit>> DeleteCareer(int id_career)
        {
            return await _mediator.Send(new EliminarCareer.deleteCareer { id_career = id_career });
        }
    }
}
