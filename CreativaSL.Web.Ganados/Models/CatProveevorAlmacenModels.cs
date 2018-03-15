using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatProveevorAlmacenModels
    {
        private string _IDProveedorAlmacen;

        public string IDProveedorAlmacen
        {
            get { return _IDProveedorAlmacen; }
            set { _IDProveedorAlmacen = value; }
        }

        private string _Descripcion;

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private string _RFC;

        public string RFC
        {
            get { return _RFC; }
            set { _RFC = value; }
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