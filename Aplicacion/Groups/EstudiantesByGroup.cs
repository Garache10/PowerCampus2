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
    public class EstudiantesByGroup
    {
        public class ListEstudiantesByGroup : IRequest<List<T_det_inscription>>
        {
            public int group_id { get; set; }
        }

        public class Manejador : IRequestHandler<ListEstudiantesByGroup, List<T_det_inscription>>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }
            public async Task<List<T_det_inscription>> Handle(ListEstudiantesByGroup request, CancellationToken cancellationToken)
            {
                var estudiantes = await _context.t_det_inscription.Where(e => e.group_id == request.group_id).ToListAsync();
                if (estudiantes == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { group = "Sin estudiantes" });
                }
                return estudiantes;
            }
        }
    }
}
