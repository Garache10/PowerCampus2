using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Groups
{
    public class ConsultaGroup
    {
        public class ListaGroups : IRequest<List<T_group>> { }

        public class Manejador : IRequestHandler<ListaGroups, List<T_group>>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<List<T_group>> Handle(ListaGroups request, CancellationToken cancellationToken)
            {
                var groups = await _context.t_group.ToListAsync();
                return groups;
            }
        }
    }
}
