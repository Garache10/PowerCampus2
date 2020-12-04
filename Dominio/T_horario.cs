using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class T_horario
    {
        public int id_horario { get; set; }
        public string horarioInicio { get; set; }
        public string horarioFin { get; set; }
        public string day { get; set; }
        public int group_id { get; set; }
    }
}
