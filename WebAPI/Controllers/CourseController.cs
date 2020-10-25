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
    public class CourseController: ControllerBase
    {
        private readonly PowerCampus2Context context;
        public CourseController(PowerCampus2Context _context)
        {
            this.context = _context;
        }

        [HttpGet]
        public IEnumerable<T_course> Get()
        {
            return context.t_course.ToList();
        }
    }
}
