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
    public class EditarGroup
    {
        public class editGroup : IRequest
        {
            public int id_group { get; set; }
            public int course_id { get; set; }
            public int quota { get; set; }
            public string teacher_id { get; set; }
        }

        public class Manejador : IRequestHandler<editGroup>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(editGroup request, CancellationToken cancellationToken)
            {
                var group = await _context.t_group.FindAsync(request.id_group);

                if (group == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { group = "No se encontró el grupo" });
                }

                group.course_id = request.course_id; //?? group.course_id;
                group.quota = request.quota; //?? group.quota;
                group.teacher_id = request.teacher_id ?? group.teacher_id;

                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { group = "No se pudo editar el grupo" });
            }
        }
    }
}
