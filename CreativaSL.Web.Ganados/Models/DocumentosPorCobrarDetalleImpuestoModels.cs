using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentosPorCobrarDetalleImpuestoModels
    {
        public string id_documentoCobrarDetalleImpuesto { get; set; }
        public string id_documentoCobrar { get; set; }
        public string tipoImpuesto { get; set; }
        public decimal base_impuesto { get; set; }
        public string impuesto { get; set; }
        public string tipoFactor { get; set; }
        public string tasaCuota { get; set; }
        public string importe { get; set; }


    }
}