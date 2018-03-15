using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatMunicipioModels
    {
        public CatMunicipioModels() {
            _id_estado = 0;
            _descripcion = string.Empty;
            _codigoMunicipio = string.Empty;
            _id_municipio = 0;
            //Datos de control
            conexion = string.Empty;
            activo = false;
            user = string.Empty;
            opcion = 0;
        }
        private int _id_municipio;

        public int id_municipio
        {
            get { return _id_municipio; }
            set { _id_municipio = value; }
        }

        private int _id_estado;

        public int id_estado
        {
            get { return _id_estado; }
            set { _id_estado = value; }
        }
        private string _codigoMunicipio;

        public string codigoMunicipio
        {
            get { return _codigoMunicipio; }
            set { _codigoMunicipio = value; }
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