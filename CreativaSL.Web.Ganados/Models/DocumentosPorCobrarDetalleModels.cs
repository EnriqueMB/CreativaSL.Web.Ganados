using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentosPorCobrarDetalleModels
    {
        public string Id_detalleDoctoCobrar { get; set; }
        public string Id_documentoCobrar { get; set; }
        public string Id_productoServicio { get; set; }
        public int Id_tipoConciliacion { get; set; }
        public int Id_conceptoDocumento { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }


    }
}