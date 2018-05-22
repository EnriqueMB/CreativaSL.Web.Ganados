using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatTipoEventoEnvioModels
    {
        private int _IDTipoEventoEnvio;

        public int IDTipoEventoEnvio
        {
            get { return _IDTipoEventoEnvio; }
            set { _IDTipoEventoEnvio = value; }
        }

        private string _Descripcion;

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private bool _MarcarMerma;

        public bool MarcarMerma
        {
            get { return _MarcarMerma; }
            set { _MarcarMerma = value; }
        }
        public bool ParaGanado { get; set; }
        public string Clasificacion { get; set; }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}