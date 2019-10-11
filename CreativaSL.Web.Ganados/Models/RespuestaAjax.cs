using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RespuestaAjax
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }
        public string MensajeErrorSQL { get; set; }
        public string Href { get; set; }
        public string Function { get; set; }
        public IEnumerable<ErrorModelState> Errores { get; set; }
        public IEnumerable Err { get; set; }

        public RespuestaAjax()
        {
            Success = false;
            Mensaje = string.Empty;
            MensajeErrorSQL = string.Empty;
            Href = string.Empty;
            Function = string.Empty;
            Errores = null;
            Err = null;
        }
    }
}