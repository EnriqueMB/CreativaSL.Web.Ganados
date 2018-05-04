using System;
using System.Collections.Generic;
using CreativaSL.Web.Ganados.Models;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class CompraAlmacenDetallesViewModels
    {
        public CompraAlmacenDetallesViewModels()
        {
            _IDCompraAlmacen = string.Empty;
            _IDCompraAlmacenDetalle = string.Empty;
            _IDProductoAlmacen = string.Empty;
            _id_unidadProducto = string.Empty;
            _Producto = string.Empty;
            _PrecioUnitario = 0;
            _Cantidad = 0;
            _listUnidadMedida = new List<UnidadesProductosAlmacenModels>();
            _listProducto = new List<CatProductosAlmacenModels>();
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
        private string _id_unidadProducto;

        public string id_unidadProducto
        {
            get { return _id_unidadProducto; }
            set { _id_unidadProducto = value; }
        }
        private int _id_status;
                    
        public int IdStatus
        {
            get { return _id_status; }
            set { _id_status = value; }
        }

        private string _Producto;

        public string Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
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

        private string _IDProductoAlmacen;

        public string IDProductoAlmacen
        {
            get { return _IDProductoAlmacen; }
            set { _IDProductoAlmacen = value; }
        }

        private List<CatProductosAlmacenModels> _listProducto;

        public List<CatProductosAlmacenModels> ListProducto
        {
            get { return _listProducto; }
            set { _listProducto = value; }
        }
        private List<UnidadesProductosAlmacenModels> _listUnidadMedida;

        public List<UnidadesProductosAlmacenModels> ListUnidadMedida
        {
            get { return _listUnidadMedida; }
            set { _listUnidadMedida = value; }
        }
        private string _Conexion;

        public string Conexion
        {
            get { return _Conexion; }
            set { _Conexion = value; }
        }

    }
}