using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptJaulasXVentaModels
    {
        public RptJaulasXVentaModels()
        {
            _chofer = string.Empty;
            _merma = 0;
            _pesoTotal = 0;
            _machos = 0;
            _hembras = 0;
            _cliente = string.Empty;
            _folio = 0;
            _totalGanados = 0;
            _montoTotal = 0;
            _modelo = string.Empty;
            _fechaFin = DateTime.Today;
            _fechaInicio = DateTime.Today;
            _datosEmpresa = new DatosEmpresaViewModels();
            _listaJaulas = new List<RptJaulasXVentaModels>();
            _noSerie = string.Empty;
            _descripcion = string.Empty;
            _capacidad = string.Empty;
            Resultado = 0;
            Conexion = string.Empty;
            Completado = false;
            Usuario = string.Empty;
        }
        private List<RptJaulasXVentaModels> _listaJaulas;

        public List<RptJaulasXVentaModels> listaJaulas
        {
            get { return _listaJaulas; }
            set { _listaJaulas = value; }
        }
        private string _cliente;

        public string cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }
        private int _machos;

        public int machos
        {
            get { return _machos; }
            set { _machos = value; }
        }
        private int _hembras;

        public int hembras
        {
            get { return _hembras; }
            set { _hembras = value; }
        }
        private decimal _pesoTotal;

        public decimal pesoTotal
        {
            get { return _pesoTotal; }
            set { _pesoTotal = value; }
        }
        private decimal _merma;

        public decimal merma
        {
            get { return _merma; }
            set { _merma = value; }
        }
        private string _color;

        public string color
        {
            get { return _color; }
            set { _color = value; }
        }

        private string _chofer;

        public string chofer
        {
            get { return _chofer; }
            set { _chofer = value; }
        }
        private Int64 _folio;

        public Int64 folio
        {
            get { return _folio; }
            set { _folio = value; }
        }
        private int _totalGanados;

        public int totalGanados
        {
            get { return _totalGanados; }
            set { _totalGanados = value; }
        }

        private string _descripcion;

        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        private Decimal _montoTotal;

        public Decimal montoTotal
        {
            get { return _montoTotal; }
            set { _montoTotal = value; }
        }

        private string _modelo;

        public string modelo
        {
            get { return _modelo; }
            set { _modelo = value; }
        }
        private string _placas;

        public string placas
        {
            get { return _placas; }
            set { _placas = value; }
        }
        private string _noSerie;

        public string noSerie
        {
            get { return _noSerie; }
            set { _noSerie = value; }
        }
        private string _capacidad;
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
        private DatosEmpresaViewModels _datosEmpresa;

        public DatosEmpresaViewModels datosEmpresa
        {
            get { return _datosEmpresa; }
            set { _datosEmpresa = value; }
        }


        public string capacidad
        {
            get { return _capacidad; }
            set { _capacidad = value; }
        }
        #region Datos control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        #endregion
    }
}