using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Views
{
    public class V_HorarioInscripcion
    {
        public string day { get; set; }
        public string horarioInicio { get; set; }
        public string horarioFin { get; set;}
        public int id_group { get; set; }
        public int id_inscription { get; set; }
        public string user_id { get; set; }
    }
}
