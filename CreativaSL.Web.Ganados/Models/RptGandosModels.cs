using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptGandosModels
    {
        public RptGandosModels()
        {
            _noArete = string.Empty;
            _montoTotal = string.Empty;
            _genero = string.Empty;
            _folio = string.Empty;
            _fechahoraVenta = string.Empty;
            _fechaInicio = DateTime.Today;
            _fechaFin = DateTime.Today;
            _listaGanadosVendidos = new List<RptGandosModels>();
            _datosEmpresa = new DatosEmpresaViewModels();
            Conexion = string.Empty;
            Completado = false;
            Resultado = 0;
            Usuario = string.Empty;
        }
        private DateTime _fechaHoraTerminada;

        public DateTime fechaHoraTerminada
        {
            get { return _fechaHoraTerminada; }
            set { _fechaHoraTerminada = value; }
        }
        private DateTime _fechaHoraProgramada;

        public DateTime fechaHoraProgramada
        {
            get { return _fechaHoraProgramada; }
            set { _fechaHoraProgramada = value; }
        }

        private string _montoTotalGanados;

        public string montoTotalGanados
        {
            get { return _montoTotalGanados; }
            set { _montoTotalGanados = value; }
        }

        private string _montoTotal;

        public string MontoTotal
        {
            get { return _montoTotal; }
            set { _montoTotal = value; }
        }

        private string _fechahoraVenta;

        public string FechahoraVenta
        {
            get { return _fechahoraVenta; }
            set { _fechahoraVenta = value; }
        }

        private string _folio;

        public string Folio
        {
            get { return _folio; }
            set { _folio = value; }
        }

        private string _genero;

        public string Genero
        {
            get { return _genero; }
            set { _genero = value; }
        }

        private string _noArete;

        public string NoArete
        {
            get { return _noArete; }
            set { _noArete = value; }
        }

        private int _GanadosMachos;

        public int GanadosMachos
        {
            get { return _GanadosMachos; }
            set { _GanadosMachos = value; }
        }
        private int _GandosHembras;

        public int GanadosHembras
        {
            get { return _GandosHembras; }
            set { _GandosHembras = value; }
        }
        private int _GanadosTotal;

        public int GanadosTotal
        {
            get { return _GanadosTotal; }
            set { _GanadosTotal = value; }
        }
        private DateTime _fechaInicio;

        public DateTime FechaInicio
        {
            get { return _fechaInicio; }
            set { _fechaInicio = value; }
        }
        private List<RptGandosModels> _listaGanadosVendidos;

        public List<RptGandosModels> listaGanadosVendidos
        {
            get { return _listaGanadosVendidos; }
            set { _listaGanadosVendidos = value; }
        }

        private DatosEmpresaViewModels _datosEmpresa;

        public DatosEmpresaViewModels datosEmpresa
        {
            get { return _datosEmpresa; }
            set { _datosEmpresa = value; }
        }
        
        private DateTime _fechaFin;

        public DateTime FechaFin
        {
            get { return _fechaFin; }
            set { _fechaFin = value; }
        }
        #region Datos control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        #endregion
    }
}