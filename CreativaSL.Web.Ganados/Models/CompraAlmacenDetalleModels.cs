using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CreativaSL.Web.Ganados.Models;

namespace CreativaSL.Web.Ganados.Models
{
    public class CompraAlmacenDetalleModels
    {
        public CompraAlmacenDetalleModels()
        {
            _IDCompraAlmacen = string.Empty;
            _IDCompraAlmacenDetalle = string.Empty;
            _IDProductoAlmacen = string.Empty;
            _IDUnidadProducto = string.Empty;
            _Producto = new CatProductosAlmacenModels();
            _PrecioUnitario = 0;
            _Cantidad = 0;
            _SubTotal = 0;
            _UnidadMedida = new CatUnidadMedidaModels();

        }
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
        private CatProductosAlmacenModels _Producto;

        public CatProductosAlmacenModels Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
        }
        private CatUnidadMedidaModels _UnidadMedida;

        public CatUnidadMedidaModels UnidadMedida
        {
            get { return _UnidadMedida; }
            set { _UnidadMedida = value; }
        }
        private int _id_status;

        public int id_status
        {
            get { return _id_status; }
            set { _id_status = value; }
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

        private decimal _CantidadAsignada;

        public decimal CantidadAsignada
        {
            get { return _CantidadAsignada; }
            set { _CantidadAsignada = value; }
        }
        private List<CompraAlmacenDetalleModels> _listaCompraDetalle;

        public List<CompraAlmacenDetalleModels> ListCompraDetalle
        {
            get { return _listaCompraDetalle; }
            set { _listaCompraDetalle = value; }
        }
        private List<CatProductosAlmacenModels> _listProducto;

        public List<CatProductosAlmacenModels> ListProducto
        {
            get { return _listProducto; }
            set { _listProducto = value; }
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