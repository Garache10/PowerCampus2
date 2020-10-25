using Dominio;
using Microsoft.AspNetCore.Mvc;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class RoleController:ControllerBase
    {
        private readonly PowerCampus2Context context;
        public RoleController(PowerCampus2Context _context)
        {
            this.context = _context;
        }

        [HttpGet]
        public IEnumerable<T_role> Get()
        {
            return context.t_role.ToList();
        }
    }
}
