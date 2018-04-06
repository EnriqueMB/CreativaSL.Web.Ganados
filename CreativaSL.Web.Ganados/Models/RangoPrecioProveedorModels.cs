using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RangoPrecioProveedorModels
    {

        public RangoPrecioProveedorModels()
        {
            _IDProveedor = string.Empty;
            _IDRango = 0;
            _Precio = 0;
            _ListaRangoPrecioProveedor = new List<RangoPrecioProveedorModels>();
        }

        private string _IDProveedor;

        public string IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }

        private int _IDRango;

        public int IDRango
        {
            get { return _IDRango; }
            set { _IDRango = value; }
        }

        private decimal _Precio;

        public decimal Precio
        {
            get { return _Precio; }
            set { _Precio = value; }
        }

        private List<RangoPrecioProveedorModels> _ListaRangoPrecioProveedor;

        public List<RangoPrecioProveedorModels> ListaRangoPrecioProveedor
        {
            get { return _ListaRangoPrecioProveedor; }
            set { _ListaRangoPrecioProveedor = value; }
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