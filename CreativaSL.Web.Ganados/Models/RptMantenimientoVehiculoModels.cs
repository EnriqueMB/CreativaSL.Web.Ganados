using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptMantenimientoVehiculoModels
    {
        public RptMantenimientoVehiculoModels()
        {
            FechaInicio = DateTime.Today;
            FechaFin = DateTime.Today;
            IDSucursal = string.Empty;
            NomSucursal = string.Empty;
            Vehiculo = string.Empty;
            Servicio = string.Empty;
            NomProveedor = string.Empty;
            Total = 0;
            Conexion = string.Empty;
            Fecha = DateTime.Today;
            _datosEmpresa = new DatosEmpresaViewModels();
            _ListaMantemiento = new List<RptMantenimientoVehiculoModels>();
        }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string IDSucursal { get; set; }
        public string NomSucursal { get; set; }
        public string Vehiculo { get; set; }
        public string Servicio { get; set; }
        public string NomProveedor { get; set; }
        public decimal Total { get; set; }
        public string Conexion { get; set; }
        public DateTime Fecha { get; set; }

        private List<RptMantenimientoVehiculoModels> _ListaMantemiento;

        public List<RptMantenimientoVehiculoModels> ListaMantemiento
        {
            get { return _ListaMantemiento; }
            set { _ListaMantemiento = value; }
        }

        private DatosEmpresaViewModels _datosEmpresa;

        public DatosEmpresaViewModels datosEmpresa
        {
            get { return _datosEmpresa; }
            set { _datosEmpresa = value; }
        }
    }
}