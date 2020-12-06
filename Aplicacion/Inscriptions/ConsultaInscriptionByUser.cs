using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Inscriptions
{
    public class ConsultaInscriptionByUser
    {
        public class insByUser : IRequest<T_inscription>
        {
            public string user_id { get; set; }
        }

        public class Manejador : IRequestHandler<insByUser, T_inscription>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<T_inscription> Handle(insByUser request, CancellationToken cancellationToken)
            {
                var inscription = await _context.t_inscription.Where(e => e.user_id == request.user_id).FirstOrDefaultAsync();
                return inscription;
            }
        }
    }
}
