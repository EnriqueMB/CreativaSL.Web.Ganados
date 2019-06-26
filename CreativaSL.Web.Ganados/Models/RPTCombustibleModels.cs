using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RPTCombustibleModels
    {
        public RPTCombustibleModels()
        {
            IDSucursal = string.Empty;
            NomSucursal = string.Empty;
            FechaInicio = DateTime.Today;
            FechaFin = DateTime.Today;
            Unidad = string.Empty;
            Fecha = DateTime.Today;
            NomProveedor = string.Empty;
          // NoTicket = string.Empty;
            Litros = 0;
           // KmInicial = 0;
           // KmFinal = 0;
            PrecioLitro = 0;
            //Rendimiento = 0;
            TotalCompra = 0;
            // TipoCombustible = string.Empty;
            _EntregaCombustible = new EntregaCombustibleModels();


            ListaRendimiento = new List<RPTCombustibleModels>();
            Conexion = string.Empty;

            //FECHA HORA | PROVEEDOR | UNIDAD | CHÓFER | LITROS | PRECIOS POR LITROS | IMPORTE | OBSERVACIÓN
        }

        public string IDSucursal { get; set; }
        public string NomSucursal { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Unidad { get; set; }
        public DateTime Fecha { get; set; }
        public string NomProveedor { get; set; }
       // public string NoTicket { get; set; }
        public decimal Litros { get; set; }
        //public int KmInicial { get; set; }
        //public int KmFinal { get; set; }
        public decimal PrecioLitro { get; set; }
       // public decimal Rendimiento { get; set; }
        public decimal TotalCompra { get; set; }
        // public string TipoCombustible { get; set; }
        public List<RPTCombustibleModels> ListaRendimiento { get; set; }
        public string Conexion { get; set; }

        private EntregaCombustibleModels _EntregaCombustible;

        public EntregaCombustibleModels EntregaCombustible
        {
            get { return _EntregaCombustible; }
            set { _EntregaCombustible = value; }
        }
    }
}