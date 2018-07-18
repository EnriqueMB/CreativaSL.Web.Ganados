using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class TransaccionesFleteModels
    {
        public string Id_flete { get; set; }
        public DocumentosPorCobrarModels DocumentosPorCobrar { get; set; }
        public string Conexion { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }
        public decimal Subtotal { get; set; }
    }
}