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
    public class EliminarHorario
    {
        public class deleteHorario : IRequest
        {
            public int id_horario { get; set; }
        }

        public class Manejador : IRequestHandler<deleteHorario>
        {
            private readonly PowerCampus2Context context;

            public Manejador(PowerCampus2Context _context)
            {
                context = _context;
            }

            public async Task<Unit> Handle(deleteHorario request, CancellationToken cancellationToken)
            {
                var horario = await context.t_horario.FindAsync(request.id_horario);
                if (horario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { group = "No se encontró el horario" });
                }
                context.Remove(horario);

                var resultados = await context.SaveChangesAsync();
                if (resultados > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { group = "No se pudo eliminar el horario" });
            }
        }
    }
}
