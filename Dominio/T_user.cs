using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class T_user: IdentityUser
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int role { get; set; }
    }
}
