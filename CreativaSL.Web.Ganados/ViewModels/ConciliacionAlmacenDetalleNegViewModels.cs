using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class ConciliacionAlmacenDetalleNegViewModels
    {
        public ConciliacionAlmacenDetalleNegViewModels()
        {
            _IDConciliacion = string.Empty;
            _IDConciliacionDetalle = string.Empty;
            _IDProductoAlmacen = string.Empty;
            _IDUnidadProducto = string.Empty;
            _Cantidad = 0;
            _Existencia = 0;
            _ListaProductos = new List<CatProductosAlmacenModels>();
            _ListaUnidades = new List<UnidadesProductosAlmacenModels>();
        }

        private string _IDConciliacion;
        [Required(ErrorMessage = "Se requiere identificador de Conciliación de almacén")]
        [Display(Name = "Conciliación de almacén")]
        public string IDConciliacion
        {
            get { return _IDConciliacion; }
            set { _IDConciliacion = value; }
        }

        private string _IDConciliacionDetalle;
        public string IDConciliacionDetalle
        {
            get { return _IDConciliacionDetalle; }
            set { _IDConciliacionDetalle = value; }
        }

        private string _IDProductoAlmacen;
        [Required(ErrorMessage = "Seleccione un producto")]
        [Display(Name = "Producto")]
        public string IDProductoAlmacen
        {
            get { return _IDProductoAlmacen; }
            set { _IDProductoAlmacen = value; }
        }

        private string _IDUnidadProducto;
        [Required(ErrorMessage = "Seleccione una unidad de medida")]
        [Display(Name = "Unidad de medida")]
        public string IDUnidadProducto
        {
            get { return _IDUnidadProducto; }
            set { _IDUnidadProducto = value; }
        }

        private decimal _Cantidad;
        [Required(ErrorMessage = "Ingrese la cantidad")]
        [DecimalMayor0(ErrorMessage = "{0} debe ser mayor a 0")]
        [Display(Name = "Cantidad")]
        public decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        private decimal _Existencia;
        public decimal Existencia
        {
            get { return _Existencia; }
            set { _Existencia = value; }
        }
        

        private List<CatProductosAlmacenModels> _ListaProductos;
        /// <summary>
        /// Lista para llenar combo de productos
        /// </summary>
        public List<CatProductosAlmacenModels> ListaProductos
        {
            get { return _ListaProductos; }
            set { _ListaProductos = value; }
        }

        private List<UnidadesProductosAlmacenModels> _ListaUnidades;
        /// <summary>
        /// Lista para llenar combo de unidades
        /// </summary>
        public List<UnidadesProductosAlmacenModels> ListaUnidades
        {
            get { return _ListaUnidades; }
            set { _ListaUnidades = value; }
        }
    }
}