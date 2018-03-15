using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ExistenciaProductoAlmacenModels
    {
        public ExistenciaProductoAlmacenModels() {
            _id_almacen = string.Empty;
            _id_producto = string.Empty;
            _id_unidadProducto = string.Empty;
            _existencia = 0;
            //Datos de control
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Usuario = string.Empty;
            Opcion = 0;
        }
        private string _id_almacen;

        public string id_almacen
        {
            get { return _id_almacen; }
            set { _id_almacen = value; }
        }
        private string _id_producto;

        public string id_producto
        {
            get { return _id_producto; }
            set { _id_producto = value; }
        }
        private string _id_unidadProducto;

        public string id_unidadProducto
        {
            get { return _id_unidadProducto; }
            set { _id_unidadProducto = value; }
        }
        private decimal _existencia;

        public decimal existencia
        {
            get { return _existencia; }
            set { _existencia = value; }
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