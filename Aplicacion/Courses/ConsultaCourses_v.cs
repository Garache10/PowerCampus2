using Dominio.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Courses
{
    public class ConsultaCourses_v
    {
        public class ListaCursos_v : IRequest<List<V_cursos>> { }

        public class Manejador : IRequestHandler<ListaCursos_v, List<V_cursos>>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<List<V_cursos>> Handle(ListaCursos_v request, CancellationToken cancellationToken)
            {
                var courses = await _context.v_cursos.ToListAsync();
                return courses;
            }
        }
    }
}
