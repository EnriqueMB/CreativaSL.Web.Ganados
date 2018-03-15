using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ProveedorLugarModels
    {
        private string _id_proveedorLugar;

        public string id_proveedor_Lugar
        {
            get { return _id_proveedorLugar; }
            set { _id_proveedorLugar = value; }
        }
        private string _id_proveedor;

        public string id_proveedor
        {
            get { return _id_proveedor; }
            set { _id_proveedor = value; }
        }
        private string _id_lugar;

        public string id_lugar
        {
            get { return _id_lugar; }
            set { _id_lugar = value; }
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