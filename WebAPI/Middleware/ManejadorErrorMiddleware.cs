using Aplicacion.ManejadorError;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Middleware
{
    public class ManejadorErrorMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ManejadorErrorMiddleware> logger;

        public ManejadorErrorMiddleware(RequestDelegate _next, ILogger<ManejadorErrorMiddleware> _logger)
        {
            next = _next;
            logger = _logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await ManejadorExcepcionAsync(context, e, logger);
            }
        }

        private async Task ManejadorExcepcionAsync(HttpContext context, Exception e, ILogger<ManejadorErrorMiddleware> logger)
        {
            object errores = null;
            switch (e)
            {
                case ManejadorExcepcion me:
                    logger.LogError(e, "Manejador Error");
                    errores = me.Errores;
                    context.Response.StatusCode = (int)me.codigo;
                    break;

                case Exception ex:
                    logger.LogError(e, "Error de servidor");
                    errores = string.IsNullOrWhiteSpace(ex.Message) ? "Error" : ex.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            if(errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new { errores });
                await context.Response.WriteAsync(resultados);
            }
        }
    }
}
