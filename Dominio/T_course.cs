using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class T_course
    {
        public int id_course { get; set; }
        public string course { get; set; }
        public DateTime time_schedule { get; set; }
        public int career_id { get; set; }
        public Guid? teacher_id { get; set; }
    }
}
