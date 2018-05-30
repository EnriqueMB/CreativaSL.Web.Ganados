using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentosPorCobrarDetallePagosModels
    {
        public string Id_documentoPorCobrarDetallePagos { get; set; }
        public string Id_documentoPorCobrar { get; set; }
        public int Id_formaPago { get; set; }
        public decimal Monto { get; set; }
        public string Observacion { get; set; }
        public DateTime fecha { get; set; }

        public DocumentosPorCobrarDetallePagosBancarizadoModels DocumentoPorCobrarDetallePagosBancarizado;

        public List<CFDI_FormaPagoModels> ListaFormaPagos;
        public List<ListaGenerica> ListaAsignar { get; set; }

        public string Conexion { get; set; }
        public string Usuario { get; set; }

    }
}