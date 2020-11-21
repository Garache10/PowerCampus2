using Aplicacion.ManejadorError;
using Dominio;
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
    public class ConsultaIdGroup
    {
        public class OnlyGroup : IRequest<T_group>
        {
            public int id_group { get; set; }
        }

        public class Manejador : IRequestHandler<OnlyGroup, T_group>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<T_group> Handle(OnlyGroup request, CancellationToken cancellationToken)
            {
                var group = await _context.t_group.FindAsync(request.id_group);
                if (group == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { group = "No se encontró el grupo" });
                }
                return group;
            }
        }
    }
}
