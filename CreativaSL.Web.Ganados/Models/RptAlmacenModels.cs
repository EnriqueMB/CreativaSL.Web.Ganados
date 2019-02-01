using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptAlmacenModels
    {
        public RptAlmacenModels()
        {
            IDSucursal = string.Empty;
            NomSucursal = string.Empty;
            NomAlmacen = string.Empty;
            ClaveProducto = string.Empty;
            NomProducto = string.Empty;
            Existencia = 0;
            NomUnidadMedida = string.Empty;
            Precio = 0;
            Total = 0;
            Conexion = string.Empty;
            _ListaAlmacen = new List<RptAlmacenModels>();
            _DatosEmpresa = new DatosEmpresaViewModels();
            FechaFin = DateTime.Today;
            FechaInicio = DateTime.Today;
        }
        public string IDSucursal { get; set; }
        public string NomSucursal { get; set; }
        public string NomAlmacen { get; set; }
        public string ClaveProducto { get; set; }
        public string NomProducto { get; set; }
        public decimal Existencia { get; set; }
        public string NomUnidadMedida { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
        public string Conexion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        
        private List<RptAlmacenModels> _ListaAlmacen;

        public List<RptAlmacenModels> ListaAlmacen
        {
            get { return _ListaAlmacen; }
            set { _ListaAlmacen = value; }
        }

        private DatosEmpresaViewModels _DatosEmpresa;

        public DatosEmpresaViewModels DatosEmpresa
        {
            get { return _DatosEmpresa; }
            set { _DatosEmpresa = value; }
        }

    }
}