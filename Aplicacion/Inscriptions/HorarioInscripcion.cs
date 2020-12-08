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

namespace Aplicacion.Inscriptions
{
    public class HorarioInscripcion
    {
        public class horarioPorInscripcion : IRequest<List<V_HorarioInscripcion>>
        {
            public int id_inscription { get; set; }
        }

        public class Manejador : IRequestHandler<horarioPorInscripcion, List<V_HorarioInscripcion>>
        {
            private readonly PowerCampus2Context _context;

            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<List<V_HorarioInscripcion>> Handle(horarioPorInscripcion request, CancellationToken cancellationToken)
            {
                var horarios = await _context.v_HorarioInscripcion.Where(e => e.id_inscription == request.id_inscription).ToListAsync();
                if(horarios == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { inscription = "Esta inscripcion no tiene horarios registrados" });
                }
                return horarios;
            }
        }
    }
}
