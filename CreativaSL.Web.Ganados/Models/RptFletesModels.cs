using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptFletesModels
    {
        public RptFletesModels()
        {
            _listaFletes = new List<RptFletesModels>();
            _cliente = string.Empty;
            _chofer = string.Empty;
            _vehiculo = string.Empty;
            _modelo = string.Empty;
            _placas = string.Empty;
            _noSerie = string.Empty;
            _folio = 0;
            _empresaDestino = string.Empty;
            _empresaOrigen = string.Empty;
            _lugarOrigen = string.Empty;
            _lugarDestino = string.Empty;
            _datosEmpresa = new DatosEmpresaViewModels();
            _fechaFin = DateTime.Today;
            _fechaInicio = DateTime.Today;
            Conexion = string.Empty;
            _impuestoRetenido = 0;
            _impuestoTrasladado = 0;
            _total = 0;
        }
        private List<RptFletesModels> _listaFletes;

        public List<RptFletesModels> listaFletes
        {
            get { return _listaFletes; }
            set { _listaFletes = value; }
        }
        private string _cliente;

        public string cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }
        private string _chofer;

        public string chofer
        {
            get { return _chofer; }
            set { _chofer = value; }
        }
        private string _vehiculo;

        public string vehiculo
        {
            get { return _vehiculo; }
            set { _vehiculo = value; }
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
        private Int64 _folio;

        public Int64 folio
        {
            get { return _folio; }
            set { _folio = value; }
        }
        private string _empresaOrigen;

        public string empresaOrigen
        {
            get { return _empresaOrigen; }
            set { _empresaOrigen = value; }
        }
        private string _empresaDestino;

        public string empresaDestino
        {
            get { return _empresaDestino; }
            set { _empresaDestino = value; }
        }
        private string _lugarOrigen;

        public string lugarOrigen
        {
            get { return _lugarOrigen; }
            set { _lugarOrigen = value; }
        }
        private string _lugarDestino;

        public string lugarDestino
        {
            get { return _lugarDestino; }
            set { _lugarDestino = value; }
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
        private DatosEmpresaViewModels _datosEmpresa;

        public DatosEmpresaViewModels datosEmpresa
        {
            get { return _datosEmpresa; }
            set { _datosEmpresa = value; }
        }
        private Decimal _impuestoTrasladado;

        public Decimal impuestoTrasladado
        {
            get { return _impuestoTrasladado; }
            set { _impuestoTrasladado = value; }
        }
        private Decimal _impuestoRetenido;

        public Decimal impuestoRetenido
        {
            get { return _impuestoRetenido; }
            set { _impuestoRetenido = value; }
        }
        private Decimal _total;

        public Decimal total
        {
            get { return _total; }
            set { _total = value; }
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