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
            _NombreSucursal = string.Empty;
            _Observaciones = string.Empty;
            _TelefonoCasa = string.Empty;
            _TelefonoCelular = string.Empty;
            _RazonSocial = string.Empty;
            _Correo = string.Empty;
            _Direccion = string.Empty;
            _FechaIngreso = DateTime.Now;
            _ListaSucursal = new List<CatSucursalesModels>();
            _LProveedorA = new List<CatProveedorAlmacenModels>();
            _TodaSucursale = false;
        }
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
        [Required(ErrorMessage = "Debe ingresar el RFC")]
        [Display(Name = "rfc")]
        [RFC(ErrorMessage = "Ingrese un RFC válido")]
        public string RFC
        {
            get { return _RFC; }
            set { _RFC = value; }
        }

        private string _RazonSocial;
        [Required(ErrorMessage = "Debe ingresar la razón social del proveedor almacén")]
        [Display(Name = "Razón Social")]
        [StringLength(100, ErrorMessage = "El número de caracteres del campo {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto(ErrorMessage = "Ingrese un dato válido para razón social")]
        public string RazonSocial
        {
            get { return _RazonSocial; }
            set { _RazonSocial = value; }
        }

        private string _NombreSucursal;

        public string NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; }
        }

        private string _Correo;
        [Required(ErrorMessage = "Debe ingresar el correo del proveedor almacén")]
        [Display(Name = "Correo")]
        [StringLength(300, ErrorMessage = "El número de caracteres del campo {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Correo(ErrorMessage = "Ingrese un dato válido para correo")]
        public string Correo
        {
            get { return _Correo; }
            set { _Correo = value; }
        }

        private string _TelefonoCasa;
        
        [Display(Name = "Teléfono casa")]
        [StringLength(15, ErrorMessage = "El número de caracteres del campo {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Telefono(ErrorMessage = "Ingrese un dato válido para el teléfono de casa")]
        public string TelefonoCasa
        {
            get { return _TelefonoCasa; }
            set { _TelefonoCasa = value; }
        }

        private string _TelefonoCelular;
       
        [Display(Name = "Teléfono celular")]
        [StringLength(15, ErrorMessage = "El número de caracteres del campo {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Telefono(ErrorMessage = "Ingrese un dato válido para el teléfono celular")]
        public string TelefonoCelular
        {
            get { return _TelefonoCelular; }
            set { _TelefonoCelular = value; }
        }

        private DateTime _FechaIngreso;

        public DateTime FechaIngreso
        {
            get { return _FechaIngreso; }
            set { _FechaIngreso = value; }
        }

        private string _Observaciones;
        [Display(Name = "Teléfono celular")]
        [Texto(ErrorMessage = "Ingrese un dato válido para observaciones")]
        public string Observaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
        }

        private string _Direccion;

        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }

        private string _IDSucursal;
        [Required(ErrorMessage = "Seleccione una sucursal")]
        [Display(Name = "Sucursal")]
        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }


        private List<CatProveedorAlmacenModels> _LProveedorA;

        public List<CatProveedorAlmacenModels> LProveedorA
        {
            get { return _LProveedorA; }
            set { _LProveedorA = value; }
        }

        private List<CatSucursalesModels> _ListaSucursal;

        public List<CatSucursalesModels> ListaSucursal
        {
            get { return _ListaSucursal; }
            set { _ListaSucursal = value; }
        }

        private bool _TodaSucursale;

        public bool TodaSucursale
        {
            get { return _TodaSucursale; }
            set { _TodaSucursale = value; }
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