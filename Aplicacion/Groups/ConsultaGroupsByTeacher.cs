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

namespace Aplicacion.Groups
{
    public class ConsultaGroupsByTeacher
    {
        public class GroupsByTeacher : IRequest<List<T_group>>
        {
            public string teacher_id { get; set; }
        }

        public class Manejador : IRequestHandler<GroupsByTeacher, List<T_group>>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<List<T_group>> Handle(GroupsByTeacher request, CancellationToken cancellationToken)
            {
                var group = await _context.t_group.Where(e => e.teacher_id == request.teacher_id).ToListAsync();
                if (group == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { group = "No se encontró el grupo" });
                }
                return group;
            }
        }
    }
}
