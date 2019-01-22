using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptProveedorMermaAltaModels
    {
        public RptProveedorMermaAltaModels() {
            _nombreProveedor = string.Empty;
            _merma = 0;
            _mermaTotal = 0;
            _listaRptProveedorMerma = new List<RptProveedorMermaAltaModels>();
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Opcion = 0;
            Usuario = string.Empty;
            _DatosEmpresa = new DatosEmpresaViewModels();
            _FechaFin = DateTime.Today;
            _FechaInicio = DateTime.Today;
            _NombreCompra = string.Empty;
            _IDSucursalActual = string.Empty;
            _ListaCmbSucursal = new List<CatSucursalesModels>();
            _NombreSucursal = string.Empty;
        }
        private string _NombreSucursal;

        public string NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; }
        }


        private string _IDProveedor;

        public string IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }

        private decimal _toleranciaCompra;

        public decimal toleranciaCompra
        {
            get { return _toleranciaCompra; }
            set { _toleranciaCompra = value; }
        }

        private string _nombreProveedor;

        public string nombreProveedor
        {
            get { return _nombreProveedor; }
            set { _nombreProveedor = value; }
        }
        private int _merma;

        public int merma
        {
            get { return _merma; }
            set { _merma = value; }
        }
        private decimal _mermaTotal;

        public decimal mermaTotal
        {
            get { return _mermaTotal; }
            set { _mermaTotal = value; }
        }
        private List<RptProveedorMermaAltaModels> _listaProveedores;

        public List<RptProveedorMermaAltaModels> listaProveedores
        {
            get { return _listaProveedores; }
            set { _listaProveedores = value; }
        }

        private List<RptProveedorMermaAltaModels> _listaRptProveedorMerma;

        public List<RptProveedorMermaAltaModels> listaRptProveedorMerma
        {
            get { return _listaRptProveedorMerma; }
            set { _listaRptProveedorMerma = value; }
        }
        private DatosEmpresaViewModels _DatosEmpresa;

        public DatosEmpresaViewModels DatosEmpresa
        {
            get { return _DatosEmpresa; }
            set { _DatosEmpresa = value; }
        }

        private string _NombreCompra;

        public string NombreCompra
        {
            get { return _NombreCompra; }
            set { _NombreCompra = value; }
        }

        private int _GanadoMacho;

        public int GanadoMacho
        {
            get { return _GanadoMacho; }
            set { _GanadoMacho = value; }
        }

        private int _GanadoHembra;

        public int GanadoHembra
        {
            get { return _GanadoHembra; }
            set { _GanadoHembra = value; }
        }

        private int _TotalGanadoComprado;

        public int TalGanadoComprado
        {
            get { return _TotalGanadoComprado; }
            set { _TotalGanadoComprado = value; }
        }

        private DateTime _FechaInicio;

        public DateTime FechaInicio
        {
            get { return _FechaInicio; }
            set { _FechaInicio = value; }
        }

        private DateTime _FechaFin;

        public DateTime FechaFin
        {
            get { return _FechaFin; }
            set { _FechaFin = value; }
        }

        private string _IDSucursalActual;
        /// <summary>
        /// Identificacor de la sucursal actual del Empleado
        /// </summary>s
        //[Required(ErrorMessage = "Seleccione una sucursal")]
        //[Display(Name = "Sucursal")]
        public string IDSucursalActual
        {
            get { return _IDSucursalActual; }
            set { _IDSucursalActual = value; }
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

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}