using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatGeneroModels
    {
        public CatGeneroModels()
        {
            _IDGenero = 0;
            _Descripcion = string.Empty;
            Conexion = string.Empty;
            Usuario = string.Empty;
        }
        private int _IDGenero;

        public int IDGenero
        {
            get { return _IDGenero; }
            set { _IDGenero = value; }
        }

        private string _Descripcion;

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
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