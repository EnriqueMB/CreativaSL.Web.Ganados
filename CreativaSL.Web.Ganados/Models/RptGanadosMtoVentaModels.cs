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
            _totalGanados = 0;
            _totalHembras = 0;
            _totalMachos = 0;
            _montoTotal = 0;
            _genero = string.Empty;
            Conexion = string.Empty;
            _datosEmpresa = new DatosEmpresaViewModels();
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
        private int _totalMachos;

        public int totalMachos
        {
            get { return _totalMachos; }
            set { _totalMachos = value; }
        }
        private int _totalHembras;

        public int totalHembras
        {
            get { return _totalHembras; }
            set { _totalHembras = value; }
        }
        private int _totalGanados;

        public int totalGanados
        {
            get { return _totalGanados; }
            set { _totalGanados = value; }
        }


        #region Datos control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        #endregion
    }
}