using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatRemolqueModels
    {
        public CatRemolqueModels() {
            _IDRemolque = string.Empty;
            _color = string.Empty;
            _listaRemolque = new List<CatRemolqueModels>();
            _placa = string.Empty;
            _nombreSucursal = string.Empty;
            _capacidad = string.Empty;
            _Estatus = true;
            //datos de control
            Conexion = string.Empty;
            Resultado = 0;
            Opcion = 0;
            Completado = false;
            Usuario = string.Empty;
        }
        private List<CatSucursalesModels> _listaSucursales;

        public List<CatSucursalesModels> listaSucursales
        {
            get { return _listaSucursales; }
            set { _listaSucursales = value; }
        }

        private bool _Estatus;

        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        private string _IDSucursal;

        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }
        private string _capacidad;

        public string capacidad
        {
            get { return _capacidad; }
            set { _capacidad = value; }
        }

        private string _nombreSucursal;

        public string nombreSucursal
        {
            get { return _nombreSucursal; }
            set { _nombreSucursal = value; }
        }

        private List<CatRemolqueModels> _listaRemolque;

        public List<CatRemolqueModels> listaRemolque
        {
            get { return _listaRemolque; }
            set { _listaRemolque = value; }
        }

        private string _IDRemolque;

        public string IDRemolque
        {
            get { return _IDRemolque; }
            set { _IDRemolque = value; }
        }
        private string _placa;
        [Required(ErrorMessage = "La placa es obligatoria")]
       
        [Display(Name = "Placas")]
        [StringLength(10, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Placas(ErrorMessage = "Solo Letras, números y guiones ")]
        public string placa
        {
            get { return _placa; }
            set { _placa = value; }
        }
        private string _color;
        [Required(ErrorMessage = "El color es obligatorio")]
        [Display(Name = "Color")]
        [Texto(ErrorMessage = "Solo Letras y números")]
        public string color
        {
            get { return _color; }
            set { _color = value; }
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