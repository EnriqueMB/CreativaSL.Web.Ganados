using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class FierroCompraModels
    {
        private string _id_compraFierro;

        public string id_compraFierro
        {
            get { return _id_compraFierro; }
            set { _id_compraFierro = value; }
        }
        private string _id_compra;

        public string id_compra
        {
            get { return _id_compra; }
            set { _id_compra = value; }
        }
        private string _id_fierro;

        public string id_fierro
        {
            get { return _id_fierro; }
            set { _id_fierro = value; }
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