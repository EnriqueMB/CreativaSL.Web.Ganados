using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptEntadaAlmacenModels
    {
        public RptEntadaAlmacenModels()
        {
            IDSucursal = string.Empty;
            NombreAlmacen = string.Empty;
            NombreSucursal = string.Empty;
            Folio = string.Empty;
            FechaEntrega = DateTime.Today;
            Producto = string.Empty;
            CantidadPro = 0;
            NumFactura = string.Empty;
            CantidadEntrega = 0;
            UnidadMedida = string.Empty;
            Conexion = string.Empty;
            _ListaEntradaA = new List<RptEntadaAlmacenModels>();
            _DatosEmpresa = new DatosEmpresaViewModels();
            FechaInicio = DateTime.Today;
            FechaFin = DateTime.Today;
        }

        public string IDSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string Folio { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string NombreAlmacen { get; set; }
        public string Producto { get; set; }
        public decimal CantidadPro { get; set; }
        public string NumFactura { get; set; }
        public decimal CantidadEntrega { get; set; }
        public string UnidadMedida { get; set; }
        public string Conexion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        private List<RptEntadaAlmacenModels> _ListaEntradaA;

        public List<RptEntadaAlmacenModels> ListaEntradaA
        {
            get { return _ListaEntradaA; }
            set { _ListaEntradaA = value; }
        }
        private DatosEmpresaViewModels _DatosEmpresa;

        public DatosEmpresaViewModels DatosEmpresa
        {
            get { return _DatosEmpresa; }
            set { _DatosEmpresa = value; }
        }
    }
}