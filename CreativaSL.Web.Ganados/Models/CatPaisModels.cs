using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatPaisModels
    {
        public CatPaisModels() {
            _id_pais = string.Empty;
            _descripcion = string.Empty;

        }
       

        private string   _id_pais;

        public string id_pais
        {
            get { return _id_pais; }
            set { _id_pais = value; }
        }
        private string _descripcion;

        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        #region Datos de control
        public bool activo { get; set; }
        public string user { get; set; }
        public string conexion { get; set; }
        public int opcion { get; set; }
        #endregion
    }
}