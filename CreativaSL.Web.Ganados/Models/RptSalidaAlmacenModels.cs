using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptSalidaAlmacenModels
    {
        public RptSalidaAlmacenModels()
        {
            IDSucursal = string.Empty;
            NomAlmacen = string.Empty;
            NomSucursal = string.Empty;
            Folio = string.Empty;
            FechaSalida = DateTime.Today;
            FechaInicio = DateTime.Today;
            FechaFin = DateTime.Today;
            Producto = string.Empty;
            Cantidad = 0;
            UnidadMedida = string.Empty;
            Empleado = string.Empty;
            Conexion = string.Empty;
            ListaSalidaA = new List<RptSalidaAlmacenModels>();
            DatosEmpresa = new DatosEmpresaViewModels();
        }
        public string IDSucursal { get; set; }
        public string NomSucursal { get; set; }
        public string Folio { get; set; }
        public DateTime FechaSalida { get; set; }
        public string NomAlmacen { get; set; }
        public string Producto { get; set; }
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public string Empleado { get; set; }

        public string Conexion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        private List<RptSalidaAlmacenModels> _ListaSalidaA;

        public List<RptSalidaAlmacenModels> ListaSalidaA
        {
            get { return _ListaSalidaA; }
            set { _ListaSalidaA = value; }
        }
        private DatosEmpresaViewModels _DatosEmpresa;

        public DatosEmpresaViewModels DatosEmpresa
        {
            get { return _DatosEmpresa; }
            set { _DatosEmpresa = value; }
        }
    }
}