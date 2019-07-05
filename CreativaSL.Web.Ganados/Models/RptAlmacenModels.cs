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
            //================
            Fecha = DateTime.Today;
            Hora = string.Empty;
            Entrega = string.Empty;
            Recibe = string.Empty;
            TipoMovimiento = string.Empty;
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

        //Nuevos campos agregados para el reporte entrada y salida de almacen
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Recibe { get; set; } //Nombre del empleado que recibe el producto
        public string Entrega { get; set; } //Nombre de la persona que entrega el producto
        public string TipoMovimiento { get; set; }

     
        //=========================================

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