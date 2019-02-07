using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ComprobanteFleteModels
    {
        public string LogoEmpresa { get; set; }
        public string Folio { get; set; }
        public string AnnoImpresion { get; set; }
        public string MesImpresion { get; set; }
        public string DiaImpresion { get; set; }
        public string NombreEmpresa { get; set; }
        public string RubroEmpresa { get; set; }
        public string DireccionEmpresa { get; set; }
        public string NombreCliente { get; set; }
        public string RFCCliente { get; set; }
        public string NombreConductor { get; set; }
        public string Vehiculo { get; set; }
        public string PlacaVehiculo { get; set; }
        public string Remitente { get; set; }
        public string Destinatario { get; set; }
        public string DomicilioRemitente { get; set; }
        public string DomicilioDestinatario { get; set; }
        public string LugarOrigen { get; set; }
        public string LugarDestino { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string CondicionPago { get; set; }
        public string MetodoPago { get; set; }
        public string FormaPago { get; set; }
        public decimal Total { get; set; }

        public decimal Subtotal { get; set; }
        public decimal DescuentosDeducciones { get; set; }
        public decimal TotalImpuestosTrasladados { get; set; }
        public decimal TotalImpuestosRetenidos { get; set; }
        
        public string TipoCambio { get; set; }
        public string Moneda { get; set; }

        public List<ComprobanteFleteDetallesModels> ListaDetalles { get; set; }
    }
}