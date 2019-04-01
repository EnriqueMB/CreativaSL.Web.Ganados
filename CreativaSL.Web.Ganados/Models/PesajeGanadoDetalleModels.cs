using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class PesajeGanadoDetalleModels
    {
        public int Id { get; set; }
        public string Id_documentoCobrar { get; set; }
        public string Folio { get; set; }
        public string NombreSucursal { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaFinalizacion { get; set; }

        public string NombreCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public string DireccionTelefono { get; set; }

        public decimal MontoTotalKilo { get; set; }
        public decimal MontoTotalPorCobrar { get; set; }

        public decimal TotalPercepciones { get; set; }
        public decimal TotalDeducciones { get; set; }
        public decimal Total { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Pagos { get; set; }
    }
}