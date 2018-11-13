using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ComprobanteCompraDetallesModels
    {
        public ComprobanteCompraDetallesModels()
        {
            Cantidad = 0;
            Genero = string.Empty;
            TotalKilos = 0;
            PrecioPorKilo = 0;
            Subtotal = 0;
        }
        public decimal Cantidad { get; set; }
        public string  Genero { get; set; }
        public decimal TotalKilos { get; set; }
        public decimal PrecioPorKilo { get; set; }
        public decimal Subtotal { get; set; }
    }
}