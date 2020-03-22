using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptProvedorVendioMasModels
    {
       public RptProvedorVendioMasModels()
        {
            _IDProveedor = string.Empty;
            _NombreCompra = string.Empty;
            _NombreProveedor = string.Empty;
            _ListaProveedorVendioMas = new List<RptProvedorVendioMasModels>();
            _FechaInicio = DateTime.Today;
            _FechaFin = DateTime.Today;
            _Conexion = string.Empty;
            _DatosEmpresa = new DatosEmpresaViewModels();
            _IdSucursal = string.Empty;
        }

       public decimal ImporteGanadoMachos { get; set; }
       public decimal ImporteGanadoHembras { get; set; }
       public decimal ImporteGanadoTotal { get; set; }

        private string _IdSucursal;

        public string IdSucursal
        {
            get { return _IdSucursal; }
            set { _IdSucursal = value; }
        }
        private string _NombreSucursal;

        public string NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; }
        }

        private int _KilosMachos;

        public int KilosMachos
        {
            get { return _KilosMachos; }
            set { _KilosMachos = value; }
        }

        private int _KilosHembra;

        public int KilosHembra
        {
            get { return _KilosHembra; }
            set { _KilosHembra = value; }
        }

        private int _TotalKilos;

        public int TotalKilos
        {
            get { return _TotalKilos; }
            set { _TotalKilos = value; }
        }


        private string _IDProveedor;

        public string IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }

        private string _NombreProveedor;

        public string NombreProveedor
        {
            get { return _NombreProveedor; }
            set { _NombreProveedor = value; }
        }

        private string _NombreCompra;

        public string NombreCompra
        {
            get { return _NombreCompra; }
            set { _NombreCompra = value; }
        }

        private int _GanadoCompradoMachos;

        public int GanadoCompradoMachos
        {
            get { return _GanadoCompradoMachos; }
            set { _GanadoCompradoMachos = value; }
        }

        private int _GanadoCompradoHembra;

        public int GanadoCompradoHembra
        {
            get { return _GanadoCompradoHembra; }
            set { _GanadoCompradoHembra = value; }
        }

        private int _TotalGando;

        public int TotalGanado
        {
            get { return _TotalGando; }
            set { _TotalGando = value; }
        }

        private decimal _PrecioGanado;

        public decimal PrecioGanado
        {
            get { return _PrecioGanado; }
            set { _PrecioGanado = value; }
        }

        private List<RptProvedorVendioMasModels> _ListaProveedorVendioMas;

        public List<RptProvedorVendioMasModels> ListaProveedorVendioMas
        {
            get { return _ListaProveedorVendioMas; }
            set { _ListaProveedorVendioMas = value; }
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

        private string _Conexion;

        public string Conexion
        {
            get { return _Conexion; }
            set { _Conexion = value; }
        }

        private DatosEmpresaViewModels _DatosEmpresa;

        public DatosEmpresaViewModels DatosEmpresa
        {
            get { return _DatosEmpresa; }
            set { _DatosEmpresa = value; }
        }

    }
}