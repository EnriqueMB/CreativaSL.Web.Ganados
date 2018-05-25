using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatPeriodoModels
    {
        public CatPeriodoModels()
        {
            _IDPeriodo = 0;
            _Descripcion = string.Empty;
            Usuario = string.Empty;
            Conexion = string.Empty;
        }

        private int _IDPeriodo;

        public int IDPeriodo
        {
            get { return _IDPeriodo; }
            set { _IDPeriodo = value; }
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