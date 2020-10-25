using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class T_user
    {
        public int id_user { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public int role_id { get; set; }
        public T_role t_role { get; set; }
    }
}
