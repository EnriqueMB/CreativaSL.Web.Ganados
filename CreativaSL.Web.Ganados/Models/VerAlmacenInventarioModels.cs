using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class VerAlmacenInventarioModels
    {
        public VerAlmacenInventarioModels()
        {
            _Id_almacen = string.Empty;
            _clave = string.Empty;
            _nombre_producto = string.Empty;
            _unidad_medida = string.Empty;
        }
        private string _Id_almacen;

        public string ID_almacen
        {
            get { return _Id_almacen; }
            set { _Id_almacen = value; }
        }
        private string _clave;

        public string Clave
        {
            get { return _clave; }
            set { _clave = value; }
        }


        private string _nombre_producto;

        public string Nombre_producto
        {
            get { return _nombre_producto; }
            set { _nombre_producto = value; }
        }

        private decimal _Existencia;

        public decimal Existencia
        {
            get { return _Existencia; }
            set { _Existencia = value; }
        }

        public string ExistenciaFormat
        {
            get { return string.Format("{0:F3}", _Existencia); }
        }
        private string _unidad_medida;

        public string Unidad_medida
        {
            get { return _unidad_medida; }
            set { _unidad_medida = value; }
        }
    }
}