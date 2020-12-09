using Aplicacion.Dashboard;
using Dominio.Views;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : IControllerBase_
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<V_Dashboard>> GetDashBoard(string id)
        {
            id = (string)this.RouteData.Values["id"];
            var Data = await Mediator.Send(new ConsultaDashboard.getData { Id = id });
            if(Data == null)
            {
                return NotFound();
            }
            return Data;
        }
    }
}
