using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.DetallesInscriptions
{
    public class ConsultaDet
    {
        public class ListaDetInscription : IRequest<List<T_det_inscription>> { }

        public class Manejador : IRequestHandler<ListaDetInscription, List<T_det_inscription>>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<List<T_det_inscription>> Handle(ListaDetInscription request, CancellationToken cancellationToken)
            {
                var detalles = await _context.t_det_inscription.ToListAsync();
                return detalles;
            }
        }
    }
}
