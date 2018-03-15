using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatProductosModels
    {
        public CatProductosModels() {
            _IDProducto = string.Empty;
            _Clave = string.Empty;
            _Descripcion = string.Empty;
            _Clave_cfdi = string.Empty;

            //Datos control
            Conexion = string.Empty;
            Resultado = 0;
            Opcion = 0;
            Completado = false;
            Usuario = string.Empty;

        }
        /// <summary>
        /// LISTA DE PRODUCTOS
        /// </summary>
        /// 
        private List<CatProductosModels> _listaProductos;

        public List<CatProductosModels> listaProductos
        {
            get { return _listaProductos; }
            set { _listaProductos = value; }
        }

        private string _IDProducto;

        public string IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
        }

        private string _Clave;
        [Required(ErrorMessage = "La Clave es obligatoria")]
        [Display(Name = "Clave")]
        [StringLength(20, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s\-]*$", ErrorMessage = "Solo Letras, Números y '-'")]
        public string Clave
        {
            get { return _Clave; }
            set { _Clave = value; }
        }

        private string _Descripcion;
        [Required(ErrorMessage = "La Clave es obligatoria")]
        [Display(Name = "Clave")]
        [StringLength(140, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s\-\.\,\(\)]*$", ErrorMessage = "Solo Letras, Números y '-'")]
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private string _Clave_cfdi;
        [Required(ErrorMessage = "La Clave CFDI es obligatoria")]
        [Display(Name = "Clave CFDI")]
        [StringLength(10, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s\-]*$", ErrorMessage = "Solo Letras, Números y '-'")]

        public string Clave_cfdi
        {
            get { return _Clave_cfdi; }
            set { _Clave_cfdi = value; }
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