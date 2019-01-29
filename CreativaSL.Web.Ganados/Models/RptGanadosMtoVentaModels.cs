using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptGanadosMtoVentaModels
    {
        public RptGanadosMtoVentaModels()
        {
            _listaGanadosMtoVenta = new List<RptGanadosMtoVentaModels>();
            _ListaTotalGanado = new List<RptGanadosMtoVentaModels>();
            _cliente = string.Empty;
            _numArete = string.Empty;
            _folio = 0;
            _fechaHoraVenta = DateTime.Today;
            _fechaInicio = DateTime.Today;
            _fechaFin = DateTime.Today;
            _montoTotal = 0;
            _genero = string.Empty;
            Conexion = string.Empty;
            _datosEmpresa = new DatosEmpresaViewModels();
            _IdSucursal = string.Empty;
            _NombreSucursal = string.Empty;
            _FolioVC = string.Empty;
            _KGCabeza = 0;
            _TipoEvento = string.Empty;
        }

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

        private string _FolioVC;

        public string FolioVC
        {
            get { return _FolioVC; }
            set { _FolioVC = value; }
        }

        private int _KGCabeza;

        public int KGCabeza
        {
            get { return _KGCabeza; }
            set { _KGCabeza = value; }
        }

        private string _TipoEvento;

        public string TipoEvento
        {
            get { return _TipoEvento; }
            set { _TipoEvento = value; }
        }
        
        private List<RptGanadosMtoVentaModels> _listaGanadosMtoVenta;

        public List<RptGanadosMtoVentaModels> listaGanadosMtoVenta
        {
            get { return _listaGanadosMtoVenta; }
            set { _listaGanadosMtoVenta = value; }
        }

        private List<RptGanadosMtoVentaModels> _ListaTotalGanado;

        public List<RptGanadosMtoVentaModels> ListaTotalGanado
        {
            get { return _ListaTotalGanado; }
            set { _ListaTotalGanado = value; }
        }
        
        private string _cliente;

        public string cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }
        private string _numArete;

        public string numArete
        {
            get { return _numArete; }
            set { _numArete = value; }
        }
        private string _genero;

        public string genero
        {
            get { return _genero; }
            set { _genero = value; }
        }
        private Int64 _folio;

        public Int64 folio
        {
            get { return _folio; }
            set { _folio = value; }
        }

        private DateTime _fechaInicio;

        public DateTime fechaInicio
        {
            get { return _fechaInicio; }
            set { _fechaInicio = value; }
        }
        private DateTime _fechaFin;

        public DateTime fechaFin
        {
            get { return _fechaFin; }
            set { _fechaFin = value; }
        }
        private DateTime _fechaHoraVenta;

        public DateTime fechaHoraVenta
        {
            get { return _fechaHoraVenta; }
            set { _fechaHoraVenta = value; }
        }
        private Decimal _montoTotal;

        public Decimal montoTotal
        {
            get { return _montoTotal; }
            set { _montoTotal = value; }
        }
        private DatosEmpresaViewModels  _datosEmpresa;

        public DatosEmpresaViewModels datosEmpresa
        {
            get { return _datosEmpresa; }
            set { _datosEmpresa = value; }
        }
      
        #region Datos control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        #endregion
    }
}