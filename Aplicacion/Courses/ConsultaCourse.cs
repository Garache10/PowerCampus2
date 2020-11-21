using Dominio;
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
    public class ConsultaCourse
    {
        public class ListaCourses : IRequest<List<T_course>> { }
        public class Manejador : IRequestHandler<ListaCourses, List<T_course>>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<List<T_course>> Handle(ListaCourses request, CancellationToken cancellationToken)
            {
                var courses = await _context.t_course.ToListAsync();
                return courses;
            }
        }
    }
}
