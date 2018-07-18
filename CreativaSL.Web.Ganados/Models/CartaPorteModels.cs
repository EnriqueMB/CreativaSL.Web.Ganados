using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CartaPorteModels
    {
        public string LogoRFC { get; set; }
        public string Folio { get; set; }
        public string NombreCliente { get; set; }
        public string RFCCliente { get; set; }
        public string NombreConductor { get; set; }
        public string Vehiculo { get; set; }
        public string PlacaVehiculo { get; set; }
        public string Kilometros { get; set; }
        public string Remitente { get; set; }
        public string Destinatario { get; set; }
        public string DomicilioRemitente { get; set; }
        public string DomicilioDestinatario { get; set; }
        public string LugarOrigen { get; set; }
        public string LugarDestino { get; set; }
        public DateTime FechaEntrega { get; set; }
        public bool MaterialPeligroso { get; set; }
        public decimal PesoAproximado { get; set; }
        public string ImporteConLetra { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DescuentosDeducciones { get; set; }
        public decimal TotalImpuestosTrasladados { get; set; }
        public decimal TotalImpuestosRetenidos { get; set; }
        public decimal Total { get; set; }
        public string TipoCambio { get; set; }
        public string Moneda { get; set; }
        public string CondicionPago { get; set; }
        public string FormaPago { get; set; }
        public string MetodoPago { get; set; }

        public List<CartaPorteDetallesModels> ListaDetallesCartaPorte { get; set; }

    }
}