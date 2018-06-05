using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentosPorCobrarDetalleImpuestoModels
    {
        public string Id_documentoCobrarDetalleImpuesto { get; set; }
        public string Id_detalleDoctoCobrar { get; set; }
        public int Id_tipoImpuesto { get; set; }
        public int Id_tipoFactor { get; set; }
        public int Id_impuesto { get; set; }

        public decimal Base { get; set; }
        public decimal TasaCuota { get; set; }
        public decimal Importe { get; set; }

        public CFDI_ImpuestoModels Impuesto { get; set; }
        public CFDI_TipoImpuestoModels TipoImpuesto { get; set; }
        public CFDI_TipoFactorModels TipoFactor { get; set; }

        public List<CFDI_ImpuestoModels> ListaImpuesto { get; set; }
        public List<CFDI_TipoImpuestoModels> ListaTipoImpuesto { get; set; }
        public List<CFDI_TipoFactorModels> ListaTipoFactor { get; set; }

        public string Conexion { get; set; }
        public string Usuario { get; set; }
    }
}