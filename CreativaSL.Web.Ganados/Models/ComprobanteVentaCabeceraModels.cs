using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ComprobanteVentaCabeceraModels
    {
        public string LogoEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string RubroEmpresa { get; set; }
        public string TelefonoEmpresa { get; set; }
        public string DireccionEmpresa { get; set; }
        public string Folio { get; set; }
        public string NombreCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public string RFCPCliente { get; set; }
        public string DiaImpresion { get; set; }
        public string MesImpresion { get; set; }
        public string AnnoImpresion { get; set; }
        public int TipoVenta { get; set; }
        public decimal TotalPorCobrarGanado { get; set; }
        public decimal CostoFlete { get; set; }
    }
}