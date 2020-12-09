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

namespace Aplicacion.Dashboard
{
    public class ConsultaDashboard
    {
        public class getData : IRequest<V_Dashboard>
        {
            public string Id { get; set; }
        }

        public class Manejador : IRequestHandler<getData, V_Dashboard>
        {
            private readonly PowerCampus2Context _context;

            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<V_Dashboard> Handle(getData request, CancellationToken cancellationToken)
            {
                var Data = await _context.v_Dashboard.Where(e => e.Id == request.Id).FirstOrDefaultAsync();
                if(Data == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Dashboard = "No hay datos que mostrar" });
                }
                return Data;
            }
        }
    }
}
