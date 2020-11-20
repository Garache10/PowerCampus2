using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Careers
{
    public class ConsultaCareer
    {
        public class ListaCareers : IRequest<List<T_career>> { }
        public class Manejador : IRequestHandler<ListaCareers, List<T_career>>
        {
            private readonly PowerCampus2Context context;

            public Manejador(PowerCampus2Context _context)
            {
                context = _context;
            }

            public async Task<List<T_career>> Handle(ListaCareers request, CancellationToken cancellationToken)
            {
                var careers = await context.t_career.ToListAsync();
                return careers;
            }
        }

    }
}
