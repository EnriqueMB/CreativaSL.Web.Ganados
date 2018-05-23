using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentosPorCobrarModels
    {
        public string id_documentoCobrar { get; set; }
        public string id_sucursal { get; set; }
        public string id_metodoPago { get; set; }
        public DateTime fecha { get; set; }
        public bool esSistema { get; set; }
        public decimal impuesto_retenido { get; set; }
        public decimal impuesto_trasladado { get; set; }
        public decimal impuestos { get; set; }
        public decimal total { get; set; }
        public decimal cambio { get; set; }
        public decimal pagos { get; set; }
        public decimal pendiente { get; set; }


    }
}