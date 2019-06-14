using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class VentaEnvioCorreo
    {
        public VentaEnvioCorreo()
        {
            IdVenta = string.Empty;
            ProveedorVenta = string.Empty;
            FechaEmbarque = DateTime.Today;
            LugarDestino = string.Empty;
            NombreChofer = string.Empty;
            TelefonoMovil = string.Empty;
            Modelo = string.Empty;
            Color = string.Empty;
            Placas = string.Empty;
            GPS = string.Empty;
            PlacasJaulas = string.Empty;
            ColorJaula = string.Empty;
            Marca = string.Empty;
            ChoferAuxiliar = string.Empty;
            Conexion = string.Empty;
            HoraSalida = string.Empty;
        }

        public string IdVenta { get; set; }
        public string ProveedorVenta { get; set; }
        public DateTime FechaEmbarque { get; set; }
        public int CabezaHembras { get; set; }
        public decimal PesoHembra { get; set; }
        public int CabezaMachos { get; set; }
        public decimal PesoMachos { get; set; }
        public decimal TotalGeneral { get; set; }
        public int TotalCabezas { get; set; }
        public string LugarDestino { get; set; }
        public string NombreChofer { get; set; }
        public string TelefonoMovil { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string Placas { get; set; }
        public string GPS { get; set; }
        public string PlacasJaulas { get; set; }
        public string ColorJaula { get; set; }
        public string Marca { get; set; }
        public string ChoferAuxiliar { get; set; }
        public string Conexion { get; set; }
        public string HoraSalida { get; set; }

        public string Asunto { get; set; }
        public string Mensaje { get; set; }

        public List<VentaEnvioCorreo> VentaCorreo {get; set;}
        public List<VentaEnvioCorreo> VentaCorreoDetalle { get; set; }
        public string Correo { get; set; }
        public string Tipo { get; set; }
        public string NombreCliente { get; set; }
    }
}