using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class FleteDetallesModels
    {
        public string Id_flete { get; set; }
        public string Id_documentoPorCobrar { get; set; }
        public string FleteFolio { get; set; }
        public string FleteSucursal { get; set; }
        public string FleteFecha { get; set; }
        public string FleteObservacion { get; set; }
        public string FleteLineaFletera { get; set; }
        public decimal FletePrecio { get; set; }
        public string FleteChofer { get; set; }
        public string FleteVehiculo { get; set; }

        public string ClienteNombreRazonSocialCompleto { get; set; }
        public string ClienteRegimenFiscal { get; set; }
        public string ClienteDireccion { get; set; }

        public string FleteLugarOrigen { get; set; }
        public string FleteDireccionOrigen { get; set; }

        public string FleteLugarDestino { get; set; }
        public string FleteDireccionDestino { get; set; }

        public decimal DocumentosPrecioDocumentacion { get; set; }
        public string  DocumentosTipoSalidaDocumentacion { get; set; }

        public string Conexion { get; set; }

        public decimal TotalPercepciones { get; set; }
        public decimal TotalDeducciones { get; set; }
        public decimal Total { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Subtotal { get; set; }

        public decimal Pendiente { get; set; }
        public decimal Cambio { get; set; }
        public decimal Pagos { get; set; }
    }
}