using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class EntradaAlmacenDetalleModels
    {

        public EntradaAlmacenDetalleModels()
        {
            _CompraDetalle = new CompraAlmacenDetalleModels();
            _ProductoAlmacen = new CatProductosAlmacenModels();
            _UnidadMedida = new CatUnidadMedidaModels();
        }

        private string _IDEntradaAlmacenDetalle;

        public string IDEntradaAlmacenDetalle
        {
            get { return _IDEntradaAlmacenDetalle; }
            set { _IDEntradaAlmacenDetalle = value; }
        }

        private string _IDEntradaAlmacen;

        public string IDEntradaAlmacen
        {
            get { return _IDEntradaAlmacen; }
            set { _IDEntradaAlmacen = value; }
        }

        private string _IDCompraDetalle;

        public string IDCompraDetalle
        {
            get { return _IDCompraDetalle; }
            set { _IDCompraDetalle = value; }
        }

        private string _IDUnidadProducto;

        public string IDUnidadProducto
        {
            get { return _IDUnidadProducto; }
            set { _IDUnidadProducto = value; }
        }

        private string _IDProducto;

        public string IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
        }

        private decimal _Cantidad;

        public decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }
        private decimal _Precio;

        public decimal Precio
        {
            get { return _Precio; }
            set { _Precio = value; }
        }

        private CompraAlmacenDetalleModels _CompraDetalle;

        public CompraAlmacenDetalleModels CompraDetalle
        {
            get { return _CompraDetalle; }
            set { _CompraDetalle = value; }
        }

        private CatProductosAlmacenModels _ProductoAlmacen;

        public CatProductosAlmacenModels ProductoAlmacen
        {
            get { return _ProductoAlmacen; }
            set { _ProductoAlmacen = value; }
        }

        private CatUnidadMedidaModels _UnidadMedida;

        public CatUnidadMedidaModels UnidadMedida
        {
            get { return _UnidadMedida; }
            set { _UnidadMedida = value; }
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