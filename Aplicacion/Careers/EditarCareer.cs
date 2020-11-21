using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Careers
{
    public class EditarCareer
    {
        public class editCareer : IRequest
        {
            public int id_career { get; set; }
            public string career { get; set; }
        }

        public class Manejador : IRequestHandler<editCareer>
        {
            public readonly PowerCampus2Context _context;

            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(editCareer request, CancellationToken cancellationToken)
            {
                var career= await _context.t_career.FindAsync(request.id_career);

                if (career == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { career = "No se encontró la carrera" });
                }

                career.career = request.career ?? career.career;

                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { career = "No se pudo editar la carrera" });
            }
        }
    }
}
