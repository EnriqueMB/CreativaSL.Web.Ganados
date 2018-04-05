using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatProveedorAlmacenModels
    {
        public CatProveedorAlmacenModels()
        {
            _IDProveedorAlmacen = string.Empty;
            _Descripcion = string.Empty;
            _RFC = string.Empty;
        }
        private string _IDProveedorAlmacen;

        public string IDProveedorAlmacen
        {
            get { return _IDProveedorAlmacen; }
            set { _IDProveedorAlmacen = value; }
        }

        private string _Descripcion;
        [Required(ErrorMessage = "Debe ingresar el nombre del proveedor")]
        [Display(Name = "Nombre Proveedor")]
        [StringLength(100, ErrorMessage = "El número de caracteres del campo {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto(ErrorMessage = "Ingrese un dato válido para Nombre del Proveedor")]
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private string _RFC;
        [Required(ErrorMessage = "Debe ingresar el RFC")]
        [Display(Name = "rfc")]
        [RFC(ErrorMessage = "Ingrese un RFC válido")]
        public string RFC
        {
            get { return _RFC; }
            set { _RFC = value; }
        }
        private List<CatProveedorAlmacenModels> _LProveedorA;

        public List<CatProveedorAlmacenModels> LProveedorA
        {
            get { return _LProveedorA; }
            set { _LProveedorA = value; }
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