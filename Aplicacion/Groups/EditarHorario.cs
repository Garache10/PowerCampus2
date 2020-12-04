using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Groups
{
    public class EditarHorario
    {
        public class editHorario : IRequest
        {
            public int id_horario { get; set; }
            public string horarioInicio { get; set; }
            public string horarioFin { get; set; }
            public string day { get; set; }
        }

        public class Manejador : IRequestHandler<editHorario>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(editHorario request, CancellationToken cancellationToken)
            {
                var horario = await _context.t_horario.FindAsync(request.id_horario);

                if (horario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { group = "No se encontró el horario" });
                }

                horario.horarioInicio = request.horarioInicio ?? horario.horarioInicio;
                horario.horarioFin = request.horarioFin ?? horario.horarioFin;
                horario.day = request.day ?? horario.day;

                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { group = "No se pudo editar el horario" });
            }
        }
    }
}
