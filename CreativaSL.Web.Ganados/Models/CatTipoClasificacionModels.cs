using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatTipoClasificacionModels
    {
        private int _IDTipoClasificacionGasto;

        public int IDTipoClasificacionGasto
        {
            get { return _IDTipoClasificacionGasto; }
            set { _IDTipoClasificacionGasto = value; }
        }

        private string _Descripcion;

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private bool _Sistema;

        public bool Sistema
        {
            get { return _Sistema; }
            set { _Sistema = value; }
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