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

namespace Aplicacion.DetallesInscriptions
{
    public class ConsultaDetailsByInscription
    {
        public class DetailsByInscription : IRequest<List<T_det_inscription>>
        {
            public int inscription_id { get; set; }
        }

        public class Manejador : IRequestHandler<DetailsByInscription, List<T_det_inscription>>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<List<T_det_inscription>> Handle(DetailsByInscription request, CancellationToken cancellationToken)
            {
                var classes = await _context.t_det_inscription.Where(e => e.inscription_id == request.inscription_id).ToListAsync();
                if(classes == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Details = "No se encontraron clases" });
                }
                return classes;
            }
        }
    }
}
