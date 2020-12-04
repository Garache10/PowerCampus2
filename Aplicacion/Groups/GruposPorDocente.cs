using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Dominio.Views;

namespace Aplicacion.Groups
{
    public class GruposPorDocente
    {
        public class GroupsForTeacher : IRequest<List<V_groupsForTeacher>>
        {
            public string teacher_id { get; set; }
        }

        public class Manejador : IRequestHandler<GroupsForTeacher, List<V_groupsForTeacher>>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<List<V_groupsForTeacher>> Handle(GroupsForTeacher request, CancellationToken cancellationToken)
            {
                var group = await _context.v_groupsForTeacher.Where(e => e.teacher_id == request.teacher_id).ToListAsync();
                if (group == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { group = "No se encontró el grupo" });
                }
                return group;
            }
        }
    }
}
