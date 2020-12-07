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
    public class ClasesEstudianteInscripcion
    {
        public class ListaClases : IRequest<List<V_InscripcionEstudianteClases>>
        {
            public int inscription_id { get; set; }
        }

        public class Manejador : IRequestHandler<ListaClases, List<V_InscripcionEstudianteClases>>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<List<V_InscripcionEstudianteClases>> Handle(ListaClases request, CancellationToken cancellationToken)
            {
                var Clases = await _context.v_InscripcionEstudianteClases.Where(e => e.inscription_id == request.inscription_id).ToListAsync();
                if(Clases == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { inscription = "Aún no tiene inscripciones" });
                }
                return Clases;
            }
        }
    }
}
