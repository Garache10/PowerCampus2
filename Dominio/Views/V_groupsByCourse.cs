using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Views
{
    public class V_groupsByCourse
    {
        public int id_group { get; set; }
        public int course_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int quota { get; set; }
    }
}
