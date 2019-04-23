using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ComprobanteGanadoModels
    {
        public string RangoPeso { get; set; }
        public int Cabezas { get; set; }
        public string TipoGanado { get; set; }
        public decimal Kilos { get; set; }
        public decimal PesoPromedio { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }
    }
}