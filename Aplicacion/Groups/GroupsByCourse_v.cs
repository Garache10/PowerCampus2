using Aplicacion.ManejadorError;
using Dominio;
using Dominio.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Groups
{
    public class GroupsByCourse_v
    {
        public class GrupoPorCursoV : IRequest<List<V_groupsByCourse>>
        {
            public int course_id { get; set; }
        }

        public class Manejador : IRequestHandler<GrupoPorCursoV, List<V_groupsByCourse>>
        {
            private readonly PowerCampus2Context _context;

            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<List<V_groupsByCourse>> Handle(GrupoPorCursoV request, CancellationToken cancellationToken)
            {
                var group = await _context.v_groupsByCourse.Where(e => e.course_id == request.course_id).ToListAsync();
                if (group == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { group = "No se encontró el grupo" });
                }
                return group;
            }
        }
    }
}
