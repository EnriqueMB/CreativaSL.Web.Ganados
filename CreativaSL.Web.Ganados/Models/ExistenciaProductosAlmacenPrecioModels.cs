using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ExistenciaProductosAlmacenPrecioModels
    {
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
        private decimal _cantidad;

        public decimal cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        private string _precio;

        public string precio
        {
            get { return _precio; }
            set { _precio = value; }
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