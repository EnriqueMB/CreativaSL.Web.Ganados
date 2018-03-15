using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ConciliacionAlmacenDetalleModel
    {
        private string _IDConciliacionAlmacenDetalle;

        public string IDConciliacionAlmacenDetalle
        {
            get { return _IDConciliacionAlmacenDetalle; }
            set { _IDConciliacionAlmacenDetalle = value; }
        }

        private string _IDConciliacionAlmacen;

        public string IDConciliacionAlmacen
        {
            get { return _IDConciliacionAlmacen; }
            set { _IDConciliacionAlmacen = value; }
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

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}