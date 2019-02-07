using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ComprobanteFleteDetallesModels
    {
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public string DescripcionProductoServicio { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal ImpuestosTrasladados { get; set; }
        public decimal ImpuestosRetenidos { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}