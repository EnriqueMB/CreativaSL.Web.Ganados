using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatRangoPesoCompraModels
    {
        public CatRangoPesoCompraModels()
        {
            _ListaRangoPeso = new List<CatRangoPesoCompraModels>();
            _ListaProveedor = new List<CatTipoProveedorModels>();
            IDRango = 0;
            _EsMacho = true;
            PesoMinimo = 0;
            Precio = 0;
            PesoMaximo = 0;
        }
        private int _IDRango;

        public int IDRango
        {
            get { return _IDRango; }
            set { _IDRango = value; }
        }

        private bool _EsMacho;

        public bool EsMacho
        {
            get { return _EsMacho; }
            set { _EsMacho = value; }
        }

        public string NombreRango { get; set; }

        private decimal _PesoMinimo;
        [Required(ErrorMessage = "El peso minimo es obligatorio")]
        [Display(Name = "peso minimo")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduzca un número mayor a 0")]
      //  [StringLength(20, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Peso(ErrorMessage = "Solo números enteros")]
        public decimal PesoMinimo
        {
            get { return _PesoMinimo; }
            set { _PesoMinimo = value; }
        }

        private decimal _PesoMaximo;
        [Required(ErrorMessage = "El peso maximo es obligatorio")]
        [Display(Name = "peso maximo")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduzca un número mayor a 0")]
       // [StringLength(20, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Peso(ErrorMessage = "Solo números enteros")]
        public decimal PesoMaximo
        {
            get { return _PesoMaximo; }
            set { _PesoMaximo = value; }
        }

        private decimal _Precio;
        [Required(ErrorMessage = "El precio es obligatorio")]
        [Display(Name = "precio")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduzca un número mayor a 0")]
        [Precio(ErrorMessage = "Solo números y decimales")]
        public decimal Precio
        {
            get { return _Precio; }
            set { _Precio = value; }
        }

        private int _IDTipoProveedor;
        [Required(ErrorMessage = "Tipo Proveedor es obligatorio")]
        [Display(Name = "tipo de proveedor")]
        [CombosInt(ErrorMessage = "Seleccione un tipo de proveedor")]
        public int IDTipoProveedor
        {
            get { return _IDTipoProveedor; }
            set { _IDTipoProveedor = value; }
        }

        private string _TipoProveedor;

        public string TipoProveedor
        {
            get { return _TipoProveedor; }
            set { _TipoProveedor = value; }
        }


        private List<CatRangoPesoCompraModels> _ListaRangoPeso;

        public List<CatRangoPesoCompraModels> ListaRangoPeso
        {
            get { return _ListaRangoPeso; }
            set { _ListaRangoPeso = value; }
        }

        private List<CatTipoProveedorModels> _ListaProveedor;

        public List<CatTipoProveedorModels> ListaProveedor
        {
            get { return _ListaProveedor; }
            set { _ListaProveedor = value; }
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