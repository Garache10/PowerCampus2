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
    public class HorarioByGroup
    {
        public class horarioPorGrupo : IRequest<List<T_horario>>
        {
            public int id_group { get; set; }
        }

        public class Manejador : IRequestHandler<horarioPorGrupo, List<T_horario>>
        {
            private readonly PowerCampus2Context _context;

            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<List<T_horario>> Handle(horarioPorGrupo request, CancellationToken cancellationToken)
            {
                var group = await _context.t_horario.Where(e => e.group_id == request.id_group).ToListAsync();
                if (group == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { group = "No tiene horario" });
                }
                return group;
            }
        }
    }
}
