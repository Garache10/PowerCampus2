using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Users
{
    public class Consulta
    {
        public class ListaUsers : IRequest<List<T_user>> { }
        public class Manejador: IRequestHandler<ListaUsers, List<T_user>>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<List<T_user>> Handle(ListaUsers request, CancellationToken cancellationToken)
            {
                var users = await _context.t_user.ToListAsync();
                return users;
            }
        }
    }
}
