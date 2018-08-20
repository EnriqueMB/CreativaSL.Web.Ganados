using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class VentaDetalleModels
    {
        public string Id_venta { get; set; }
        public string VentaFolio { get; set; }
        public string VentaSucursal { get; set; }
        public string VentaFecha { get; set; }
        public string VentaMerma { get; set; }
        public string VentaObservacion { get; set; }

        public string ClienteNombreRazonSocialCompleto { get; set; }
        public string ClienteRegimenFiscal  { get; set; }
        public string ClienteDireccion { get; set; }
        public string ClientePSG { get; set; }

        public string EmpresaRazonSocial { get; set; }
        public string EmpresaDireccion { get; set; }
        public string EmpresaRFC { get; set; }
        public string EmpresaPSG { get; set; }

        public string FleteLineaFletera { get; set; }
        public string FleteLugarOrigen { get; set; }
        public string FleteLugarDestino { get; set; }
        public string FleteNombreChofer { get; set; }
        public decimal FletePrecio { get; set; }

        public decimal DocumentosPrecioDocumentacion  { get; set; }
        public string DocumentosTipoSalidaDocumentacion { get; set; }

        public string Conexion { get; set; }
        public decimal TotalPercepciones { get; set; }
        public decimal TotalDeducciones { get; set; }
        public decimal Total { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Subtotal { get; set; }

        public decimal Pendiente { get; set; }
        public decimal Cambio { get; set; }
        public decimal Pagos { get; set; }
        public string Id_documentoPorCobrar { get; set; }
    }
}