using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class SalidaAlmacenDetalleModels
    {
        private string _id_salidaAlmacenDetalle;

        public string id_salidaAlmacenDetalle
        {
            get { return _id_salidaAlmacenDetalle; }
            set { _id_salidaAlmacenDetalle = value; }
        }
        private string _id_salidaAlmacen;

        public string id_salidaAlmacen
        {
            get { return _id_salidaAlmacen; }
            set { _id_salidaAlmacen = value; }
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
        private decimal _precio;

        public decimal precio
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