using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RespuestaAjax
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }
        public string Href { get; set; }
        public string Function { get; set; }

        public RespuestaAjax()
        {
            Success = false;
            Mensaje = string.Empty;
            Href = string.Empty;
            Function = string.Empty;
        }
    }
}