using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RPTCombustibleModels
    {
        public RPTCombustibleModels()
        {
            _Fecha = DateTime.Today; //FECHA HORA | PROVEEDOR | UNIDAD | CHÓFER | LITROS | PRECIOS POR LITROS | IMPORTE | OBSERVACIÓN
            _Proveedor = string.Empty;
            _Unidad = string.Empty;
            _Chofer = string.Empty;
            _Litros = 0;
            _PrecioXLitros = 0;
            _Importe = 0;
            _Observacion = string.Empty;
            _datosEmpresa = new DatosEmpresaViewModels();//el nombre
            _FechaInic = DateTime.Today;
            _IdSucursal = string.Empty;
            _ListaEntradas = new List<RPTCombustibleModels>();
            _Conexion = string.Empty;
            _IdSucursal = string.Empty;
            _NombreSucursal = string.Empty;
            _ListaCmbSucursal = new List<CatSucursalesModels>();

        }
        private DateTime _Fecha;
        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }
        

        private string _Proveedor;
        public string Proveedor
        {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }


        private string _Unidad;

        public string Unidad
        {
            get { return _Unidad; }
            set { _Unidad = value; }
        }

        private string _Chofer;

        public string Chofer
        {
            get { return _Chofer; }
            set { _Chofer = value; }
        }

        private decimal _Litros;

        public decimal Litros
        {
            get { return _Litros; }
            set { _Litros = value; }
        }

        private decimal _PrecioXLitros;
        

        public decimal PrecioXLitros
        {
            get { return _PrecioXLitros; }
            set { _PrecioXLitros = value; }
        }

        private decimal _Importe;

        public decimal Importe
        {
            get { return _Importe; }
            set { _Importe = value; }
        }

        private string _Observacion;

        public string Observacion
        {
            get { return _Observacion; }
            set { _Observacion = value; }
        }

        private string _NombreSucursal;

        public string NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; }
        }
        private DatosEmpresaViewModels _datosEmpresa;

        public DatosEmpresaViewModels datosEmpresa
        {
            get { return _datosEmpresa; }
            set { _datosEmpresa = value; }
        }

        private DateTime _FechaInic;

        public DateTime FechaInic
        {
            get { return _FechaInic; }
            set { _FechaInic = value; }
        }

        private DateTime _FechaFin;

        public DateTime FechaFin
        {
            get { return _FechaFin; }
            set { _FechaFin = value; }
        }


        private string _IdSucursal;

        public string IdSucursal
        {
            get { return _IdSucursal; }
            set { _IdSucursal = value; }
        }

        private List<RPTCombustibleModels> _ListaEntradas;

        public List<RPTCombustibleModels> ListaEntradas
        {
            get { return _ListaEntradas; }
            set { _ListaEntradas = value; }
        }


        private string _Conexion;

        public string Conexion
        {
            get { return _Conexion; }
            set { _Conexion = value; }
        }

        private List<CatSucursalesModels> _ListaCmbSucursal;
        /// <summary>
        /// Lista para llenar combo de sucursal
        /// </summary>
        public List<CatSucursalesModels> ListaCmbSucursal
        {
            get { return _ListaCmbSucursal; }
            set { _ListaCmbSucursal = value; }
        }

        //FECHA HORA | PROVEEDOR | UNIDAD | CHÓFER | LITROS | PRECIOS POR LITROS | IMPORTE | OBSERVACIÓN

    }
}