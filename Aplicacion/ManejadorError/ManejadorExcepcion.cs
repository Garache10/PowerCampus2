using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ManejadorError
{
    public class ManejadorExcepcion : Exception
    {
        public HttpStatusCode codigo { get; }  
        public object Errores { get; }

        public ManejadorExcepcion(HttpStatusCode _codigo, object _errores = null)
        {
            codigo = _codigo;
            Errores = _errores;
        }
    }
}
