﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using CreativaSL.Web.Ganados.ViewModels;

namespace CreativaSL.Web.Ganados.Models
{
    public class NominaModels
    {

        public NominaModels()
        {
            _IDNomina = string.Empty;
            _IDSucursal = string.Empty;
            _ClaveNomina = string.Empty;
            _FechaInicio = DateTime.Today;
            _FechaFin = DateTime.Today;
            _ListaNomina = new List<NominaModels>();
            Usuario = string.Empty;
            Conexion = string.Empty;
            _CountEmpleado = 0;
            _EsBusqueda = false;
            _BandBusqFechas = false;
            _NombreEmpleado = string.Empty;
            _Puesto = string.Empty;
            _CatedoriaPuesto = string.Empty;
            _Sueldo = 0;
            _Percepciones = 0;
            _Deducciones = 0;
            _ListaSucursales = new List<CatSucursalesModels>();
            _EsBusqueda = false;
            _BandBusqFechas = false;
            _BandIDSucursal = false;
            _NombreSucursal = string.Empty;
            _ListaEmpleados = new List<EmpleadoNominaViewModels>();
           // _TablaEmpleadoNomina = new DataTable();
        }
        private bool _AbrirCaja;
        /// <summary>
        /// Ver si el Empleado Abre caja 
        /// </summary>
        public bool AbrirCaja
        {
            get { return _AbrirCaja; }
            set { _AbrirCaja = value; }
        }
        private string _IDNomina;

        public string IDNomina
        {
            get { return _IDNomina; }
            set { _IDNomina = value; }
        }

        private string _IDSucursal;
        [Required(ErrorMessage = "Seleccione una sucursal")]
        [Display(Name = "Sucursal")]
        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private string _ClaveNomina;

        public string ClaveNomina
        {
            get { return _ClaveNomina; }
            set { _ClaveNomina = value; }
        }

        private DateTime _FechaInicio;
        [Required(ErrorMessage = "Debe seleccionar una fecha de inicio del periodo de nómina")]
        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio
        {
            get { return _FechaInicio; }
            set { _FechaInicio = value; }
        }

        private DateTime _FechaFin;
        [Required(ErrorMessage = "Debe seleccionar una fecha de fin del periodo de nómina")]
        [Display(Name = "Fecha de fin")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin
        {
            get { return _FechaFin; }
            set { _FechaFin = value; }
        }

        private List<EmpleadoNominaViewModels> _ListaEmpleados;

        public List<EmpleadoNominaViewModels> ListaEmpleados
        {
            get { return _ListaEmpleados; }
            set { _ListaEmpleados = value; }
        }


        private List<NominaModels> _ListaNomina;

        public List<NominaModels> ListaNomina
        {
            get { return _ListaNomina; }
            set { _ListaNomina = value; }
        }

        private int _CountEmpleado;

        public int CountEmpleado
        {
            get { return _CountEmpleado; }
            set { _CountEmpleado = value; }
        }

        private bool _EsBusqueda;

        public bool EsBusqueda
        {
            get { return _EsBusqueda; }
            set { _EsBusqueda = value; }
        }

        private bool _BandBusqFechas;

        public bool BandBusqFechas
        {
            get { return _BandBusqFechas; }
            set { _BandBusqFechas = value; }
        }
        private bool _BandIDSucursal;

        public bool BandIDSucursal
        {
            get { return _BandIDSucursal; }
            set { _BandIDSucursal = value; }
        }

        private bool _BandBusqClave;

        public bool BandBusqClave
        {
            get { return _BandBusqClave; }
            set { _BandBusqClave = value; }
        }

        private List<CatSucursalesModels> _ListaSucursales;

        public List<CatSucursalesModels> ListaSucursales
        {
            get { return _ListaSucursales; }
            set { _ListaSucursales = value; }
        }

        private string _NombreSucursal;

        public string NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; }
        }
        
        private DataTable _TablaEmpleadoNomina;
       
        public DataTable TablaEmpladoNomina
        {
            get { return _TablaEmpleadoNomina; }
            set { _TablaEmpleadoNomina = value; }
        }

        private string _IDConcepto;

        public string IDConcepto
        {
            get { return _IDConcepto; }
            set { _IDConcepto = value; }
        }

        private bool _EsFijo;

        public bool EsFijo
        {
            get { return _EsFijo; }
            set { _EsFijo = value; }
        }


        #region Empleado para la nomina
        /// <summary>
        /// Para Listar los empleado que van a registrarse en la nomina
        /// </summary>

        private string _IDEmpleado;

        public string IDEmpleado
        {
            get { return _IDEmpleado; }
            set { _IDEmpleado = value; }
        }

        private string _CodigoUsuario;

        public string CodigoUsuario
        {
            get { return _CodigoUsuario; }
            set { _CodigoUsuario = value; }
        }

        private string _NombreEmpleado;

        public string NombreEmpleado
        {
            get { return _NombreEmpleado; }
            set { _NombreEmpleado = value; }
        }

        private string _Puesto;

        public string Puesto
        {
            get { return _Puesto; }
            set { _Puesto = value; }
        }

        private string _CatedoriaPuesto;

        public string CategoriaPuesto
        {
            get { return _CatedoriaPuesto; }
            set { _CatedoriaPuesto = value; }
        }

        private decimal _Sueldo;

        public decimal Sueldo
        {
            get { return _Sueldo; }
            set { _Sueldo = value; }
        }

        private decimal _Percepciones;

        public decimal Percepciones
        {
            get { return _Percepciones; }
            set { _Percepciones = value; }
        }

        private decimal _Deducciones;

        public decimal Deducciones
        {
            get { return _Deducciones; }
            set { _Deducciones = value; }
        }
        
        #endregion

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}