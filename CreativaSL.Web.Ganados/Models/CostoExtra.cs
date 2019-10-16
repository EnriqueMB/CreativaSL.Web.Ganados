using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CostoExtra
    {
        public string Id { get; set; }
        public string IdVenta { get; set; }
        public string NombreProducto { get; set; }
        public string NombreUnidadMedida { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public string Observacion { get; set; }
    }
}