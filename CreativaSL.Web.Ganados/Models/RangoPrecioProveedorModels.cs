using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            _PesoMaximo = 0;
            _PesoMinimo = 0;
            _Precio = 0;
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

        private decimal _PesoMinimo;

        public decimal PesoMinimo
        {
            get { return _PesoMinimo; }
            set { _PesoMinimo = value; }
        }

        private decimal _PesoMaximo;

        public decimal PesoMaximo
        {
            get { return _PesoMaximo; }
            set { _PesoMaximo = value; }
        }

        private bool _EsMacho;

        public bool EsMacho
        {
            get { return _EsMacho; }
            set { _EsMacho = value; }
        }


        private decimal _Precio;
        [Required(ErrorMessage = "El precio es obligatorio")]
        [Display(Name = "precio")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduzca un número mayor a 0")]
        [Precio(ErrorMessage = "Ingrese un dato válido para precio")]
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