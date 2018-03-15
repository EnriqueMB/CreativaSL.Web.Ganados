using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatProductosAlmacenModels
    {
        private string _IDProductoAlmacen;

        public string IDProductoAlmacen
        {
            get { return _IDProductoAlmacen; }
            set { _IDProductoAlmacen = value; }
        }

        private int _IDTipoCodigo;

        public int IDTipoCodigo
        {
            get { return _IDTipoCodigo; }
            set { _IDTipoCodigo = value; }
        }

        private string _Clave;

        public string Clave
        {
            get { return _Clave; }
            set { _Clave = value; }
        }

        private string _Nombre;

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private string _Descripcion;

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private decimal _UltimoCosto;

        public decimal UltimoCosto
        {
            get { return _UltimoCosto; }
            set { _UltimoCosto = value; }
        }

        private bool _Almacen;

        public bool Almacen
        {
            get { return _Almacen; }
            set { _Almacen = value; }
        }

        private int _IDUnidadMedida;

        public int IDUnidadMedida
        {
            get { return _IDUnidadMedida; }
            set { _IDUnidadMedida = value; }
        }

        private string _Imagen;

        public string Imagen
        {
            get { return _Imagen; }
            set { _Imagen = value; }
        }

        private string _Extension;

        public string Extension
        {
            get { return _Extension; }
            set { _Extension = value; }
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