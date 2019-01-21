using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CalendarioFleteModels
    {
        public string Agente { get; set; }
        public string Cliente { get; set; }
        public string Sucursal { get; set; }
        public string LugarOrigen { get; set; }
        public string LugarDestino { get; set; }
        public string Remitente { get; set; }
        public string Destinatario { get; set; }
        public string Estatus { get; set; }
        public string ColorEstatus { get; set; }

        public string Conexion { get; set; }
        public DateTime fechaStart { get; set; }
        public DateTime fechaEnd { get; set; }
        public string start { get; set; }
        public string title { get; set; }
    }
}