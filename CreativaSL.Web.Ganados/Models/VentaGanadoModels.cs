using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class VentaGanadosModels
    {
        private string _id_Ganados;

        public string id_Ganados
        {
            get { return _id_Ganados; }
            set { _id_Ganados = value; }
        }
        private string _id_venta;

        public string id_venta
        {
            get { return _id_venta; }
            set { _id_venta = value; }
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