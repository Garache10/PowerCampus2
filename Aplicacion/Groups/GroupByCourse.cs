using Aplicacion.ManejadorError;
using Dominio;
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
    public class GroupByCourse
    {
        public class GrupoPorCurso : IRequest<List<T_group>>
        {
            public int course_id { get; set; }
        }

        public class Manejador : IRequestHandler<GrupoPorCurso, List<T_group>>
        {
            private readonly PowerCampus2Context _context;

            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<List<T_group>> Handle(GrupoPorCurso request, CancellationToken cancellationToken)
            {
                var group = await _context.t_group.Where(e => e.course_id == request.course_id).ToListAsync();
                if (group == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { group = "No se encontró el grupo" });
                }
                return group;
            }
        }
    }
}
