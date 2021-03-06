﻿using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatProveedorServicioModels
    {
        public CatProveedorServicioModels()
        {
            _id_proveeedorServicio = string.Empty;
            _id_sucursal = string.Empty;
            _nombreRazonSocial = string.Empty;
            _rfc = string.Empty;
            _direccion = string.Empty;
            _telefonCelular = string.Empty;
            _telefonoCasa = string.Empty;
            _correo = string.Empty;
            _fechaIngreso = DateTime.Now;
            _listaProveedoresCombustible = new List<CatProveedorModels>();
            _listaProveedoresServicio = new List<CatProveedorServicioModels>();
            _listaSucursal = new List<CatSucursalesModels>();
            _IDProveedor = string.Empty;
            _nombreSucursal = string.Empty;
            _TodaSucursale = false;
        }
        private string _id_proveeedorServicio;

        public string id_proveedorServicio
        {
            get { return _id_proveeedorServicio; }
            set { _id_proveeedorServicio = value; }
        }
        private string _id_sucursal;

        public string IDSucursal
        {
            get { return _id_sucursal; }
            set { _id_sucursal = value; }
        }
        private string _nombreRazonSocial;
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "nombre")]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s\.\,\-]*$", ErrorMessage = "Solo Letras, Números")]

        public string nombreRazonsocial
        {
            get { return _nombreRazonSocial; }
            set { _nombreRazonSocial = value; }
        }
        private string _rfc;
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "nombre")]
        [StringLength(20, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s]*$", ErrorMessage = "Solo Letras, Números")]

        public string RFC
        {
            get { return _rfc; }
            set { _rfc = value; }
        }
        private string _direccion;
        [Display(Name = "Dirección")]
        [Direccion(ErrorMessage = "Ingrese un datos válido para {0}")]
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }
        private string _telefonCelular;

        public string telefonoCelular
        {
            get { return _telefonCelular; }
            set { _telefonCelular = value; }
        }
        private string _telefonoCasa;

        public string telefonoCasa
        {
            get { return _telefonoCasa; }
            set { _telefonoCasa = value; }
        }
        private string _correo;
        [Display(Name = "Correo")]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        public string correo
        {
            get { return _correo; }
            set { _correo = value; }
        }
        private DateTime _fechaIngreso;

        public DateTime fechaIngreso
        {
            get { return _fechaIngreso; }
            set { _fechaIngreso = value; }
        }
        private string _observaciones;

        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }
        private List<CatSucursalesModels> _listaSucursal;

        public List<CatSucursalesModels> ListaSucursal
        {
            get { return _listaSucursal; }
            set { _listaSucursal = value; }
        }
        private List<CatProveedorModels> _listaProveedoresCombustible;

        public List<CatProveedorModels> listaProveedoresCombustible
        {
            get { return _listaProveedoresCombustible; }
            set { _listaProveedoresCombustible = value; }
        }

        private List<CatProveedorServicioModels> _listaProveedoresServicio;

        public List<CatProveedorServicioModels> listaProveedorServicio
        {
            get { return _listaProveedoresServicio; }
            set { _listaProveedoresServicio = value; }
        }
        private string _IDProveedor;

        public string IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }
        private string _nombreSucursal;

        public string nombreSucursal
        {
            get { return _nombreSucursal; }
            set { _nombreSucursal = value; }
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