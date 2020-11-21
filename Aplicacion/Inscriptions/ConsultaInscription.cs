using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Inscriptions
{
    public class ConsultaInscription
    {
        public class ListarInscriptions : IRequest<List<T_inscription>> { }

        public class Manejador : IRequestHandler<ListarInscriptions, List<T_inscription>>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<List<T_inscription>> Handle(ListarInscriptions request, CancellationToken cancellationToken)
            {
                var inscriptions = await _context.t_inscription.ToListAsync();
                return inscriptions;
            }
        }
    }
}
