using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatSociosModels
    {
        public CatSociosModels()
        {
            _IDSocio = string.Empty;
            _NombreCompleto = string.Empty;
            _Porcentaje = 0;
            _ListaSocios = new List<CatSociosModels>();
            Conexion = string.Empty;
            Usuario = string.Empty;
        }

        private string _IDSocio;

        public string IDSocio
        {
            get { return _IDSocio; }
            set { _IDSocio = value; }
        }

        private string _NombreCompleto;

        public string NombreCompleto
        {
            get { return _NombreCompleto; }
            set { _NombreCompleto = value; }
        }

        private int _Porcentaje;

        public int Procentaje
        {
            get { return _Porcentaje; }
            set { _Porcentaje = value; }
        }

        private List<CatSociosModels> _ListaSocios;

        public List<CatSociosModels> ListaSocios
        {
            get { return _ListaSocios; }
            set { _ListaSocios = value; }
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