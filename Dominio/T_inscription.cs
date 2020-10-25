using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class T_inscription
    {
        public int id_inscription { get; set; }
        public DateTime inscription_day { get; set; }
        public int user_id { get; set; }
        public int status { get; set; }
    }
}
