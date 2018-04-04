using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatTipoCombustibleModels
    {
        public CatTipoCombustibleModels()
        {
            _IDTipoCombustible = 0;
            _Descripcion = string.Empty;
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Usuario = string.Empty;
            Opcion = 0;
        }
        private int _IDTipoCombustible;
        /// <summary>
        /// Identificador del tipo de combustible
        /// </summary>
        public int IDTipoCombustible
        {
            get { return _IDTipoCombustible; }
            set { _IDTipoCombustible = value; }
        }

        private string _Descripcion;
        /// <summary>
        /// Texto descriptivo del tipo de combustible
        /// </summary>
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