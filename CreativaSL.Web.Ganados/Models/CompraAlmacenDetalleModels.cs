using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CompraAlmacenDetalleModels
    {
        private string _IDCompraAlmacenDetalle;

        public string IDCompraAlmacenDetalle
        {
            get { return _IDCompraAlmacenDetalle; }
            set { _IDCompraAlmacenDetalle = value; }
        }

        private string _IDCompraAlmacen;

        public string IDCompraAlmacen
        {
            get { return _IDCompraAlmacen; }
            set { _IDCompraAlmacen = value; }
        }

        private string _IDProductoAlmacen;

        public string IDProductoAlmacen
        {
            get { return _IDProductoAlmacen; }
            set { _IDProductoAlmacen = value; }
        }

        private string _IDUnidadProducto;

        public string IDUnidadProducto
        {
            get { return _IDUnidadProducto; }
            set { _IDUnidadProducto = value; }
        }

        private decimal _Cantidad;

        public decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        private decimal _PrecioUnitario;

        public decimal PrecioUnitario
        {
            get { return _PrecioUnitario; }
            set { _PrecioUnitario = value; }
        }

        private decimal _SubTotal;

        public decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; }
        }

        private string _CantidadAsignada;

        public string CantidadAsignada
        {
            get { return _CantidadAsignada; }
            set { _CantidadAsignada = value; }
        }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}