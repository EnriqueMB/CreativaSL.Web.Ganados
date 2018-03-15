using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RangoPrecioProveedorModels
    {
        private string _id_proveedor;

        public string id_proveedor
        {
            get { return _id_proveedor; }
            set { _id_proveedor = value; }
        }
        private int _id_rango;

        public int id_rango
        {
            get { return _id_rango; }
            set { _id_rango = value; }
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