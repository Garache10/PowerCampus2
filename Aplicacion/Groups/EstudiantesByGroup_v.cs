using Aplicacion.ManejadorError;
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
    public class EstudiantesByGroup_v
    {
        public class estudiantes_v : IRequest<List<V_estudiantesByGroup>>
        {
            public int id_group { get; set; }
        }

        public class Manejador : IRequestHandler<estudiantes_v, List<V_estudiantesByGroup>>
        {
            private readonly PowerCampus2Context _context;

            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<List<V_estudiantesByGroup>> Handle(estudiantes_v request, CancellationToken cancellationToken)
            {
                var estudiantes = await _context.v_estudiantesByGroup.Where(e => e.id_group == request.id_group).ToListAsync();
                if(estudiantes == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { group = "El grupo no tiene estudiantes" });
                }
                return estudiantes;
            }
        }
    }
}
