using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatEstadoModels
    {
        public CatEstadoModels() {
            _id_estado = 0;
            _descripcion = string.Empty;
            _codigoEstado = string.Empty;
            //Datos de control
            conexion = string.Empty;
            activo = false;
            user = string.Empty;
            opcion = 0;

        }
        private string _codigoEstado;

        public string codigoEstado
        {
            get { return _codigoEstado; }
            set { _codigoEstado = value; }
        }
        private int _id_estado;

        public int id_estado
        {
            get { return _id_estado; }
            set { _id_estado = value; }
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