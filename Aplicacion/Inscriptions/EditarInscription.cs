using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Inscriptions
{
    public class EditarInscription
    {
        public class editInscription : IRequest
        {
            public int id_inscription { get; set; }
            public DateTime inscription_day { get; set; }
            public string user_id { get; set; }
            public int status { get; set; }
        }

        public class Manejador : IRequestHandler<editInscription>
        {
            private readonly PowerCampus2Context _context;
            public Manejador(PowerCampus2Context context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(editInscription request, CancellationToken cancellationToken)
            {
                var inscription = await _context.t_inscription.FindAsync(request.id_inscription);

                if (inscription == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { user = "No se encontró la inscripción" });
                }

                inscription.inscription_day = request.inscription_day; //?? inscription.inscription_day;
                inscription.user_id = request.user_id ?? inscription.user_id;
                inscription.status = request.status; //?? inscription.status;

                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { inscription = "No se pudo editar la inscripción" });
            }
        }
    }
}
