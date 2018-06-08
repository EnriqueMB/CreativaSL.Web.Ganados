using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ConciliacionAlmacenDetalleModel
    {

        public ConciliacionAlmacenDetalleModel()
        {
            _IDConciliacionAlmacenDetalle = string.Empty;
            _IDConciliacionAlmacen = string.Empty;
            _UnidadMedida = new UnidadesProductosAlmacenModels();
            _Producto = new CatProductosAlmacenModels();
            _Cantidad = 0;
            _Precio = 0;
            NuevoRegistro = false;
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Usuario = string.Empty;
            Opcion = 0;
        }

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

        private UnidadesProductosAlmacenModels _UnidadMedida;
        public UnidadesProductosAlmacenModels UnidadMedida
        {
            get { return _UnidadMedida; }
            set { _UnidadMedida = value; }
        }

        private CatProductosAlmacenModels _Producto;
        public CatProductosAlmacenModels Producto
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

        private decimal _Precio;
        public decimal Precio
        {
            get { return _Precio; }
            set { _Precio = value; }
        }

        #region Datos De Control
        public bool NuevoRegistro { get; set; }
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}