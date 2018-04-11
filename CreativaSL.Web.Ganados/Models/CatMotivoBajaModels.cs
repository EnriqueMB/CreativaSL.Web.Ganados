using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatMotivoBajaModels
    {
        public CatMotivoBajaModels()
        {
            _IDMotivoBaja = 0;
            _Descripcion = string.Empty;
            _ListaMotivoBaja = new List<CatMotivoBajaModels>();
            Conexion = string.Empty;
            Usuario = string.Empty;
        }
        private int _IDMotivoBaja;
        /// <summary>
        /// Identificador que le pertenece al motivo de la baja
        /// </summary>
        public int IDMotivoBaja
        {
            get { return _IDMotivoBaja; }
            set { _IDMotivoBaja = value; }
        }

        private string _Descripcion;
        /// <summary>
        /// Descripcion del motivo de la baja
        /// </summary>
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        private List<CatMotivoBajaModels> _ListaMotivoBaja;

        public List<CatMotivoBajaModels> ListaMotivoBaja
        {
            get { return _ListaMotivoBaja; }
            set { _ListaMotivoBaja = value; }
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