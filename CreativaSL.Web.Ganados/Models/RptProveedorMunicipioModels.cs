using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptProveedorMunicipioModels
    {
        public RptProveedorMunicipioModels() {
            _fechaInicio = DateTime.Now;
            _fechaFin = DateTime.Now;
            _listaEstado = new List<CatEstadoModels>();
            _listaMunicipio = new List<CatMunicipioModels>();
            _IDEstado = 0;
            _IDMunicipio = 0;
            _ganadoCompradoHembra = 0;
            _ganadoCompradoMacho = 0;
            _ganadoPactadoHembra = 0;
            _ganadoPactadoMacho = 0;
            _totalComprado = 0;
            _totalPactado = 0;
            //Datos de control
            activo = false;
            user = string.Empty;
            conexion = string.Empty;
            Completado = false;
            opcion = 0;
            resultado = string.Empty;
        }
        private List<RptProveedorMunicipioModels> _listaProveedor;

        public List<RptProveedorMunicipioModels> listaProveedor
        {
            get { return _listaProveedor; }
            set { _listaProveedor = value; }
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
        private List<CatMunicipioModels> _listaMunicipio;

        public List<CatMunicipioModels> listaMunicipio
        {
            get { return _listaMunicipio; }
            set { _listaMunicipio = value; }
        }

        private List<CatEstadoModels> _listaEstado;

        public List<CatEstadoModels> listaEstado
        {
            get { return _listaEstado; }
            set { _listaEstado = value; }
        }
        private int _IDEstado;

        public int IDEstado
        {
            get { return _IDEstado; }
            set { _IDEstado = value; }
        }
        private int _IDMunicipio;

        public int IDMunicipio
        {
            get { return _IDMunicipio; }
            set { _IDMunicipio = value; }
        }
        private int _ganadoPactadoHembra;

        public int ganadoPactadoHembra
        {
            get { return _ganadoPactadoHembra; }
            set { _ganadoPactadoHembra = value; }
        }
        private int _ganadoPactadoMacho;

        public int ganadoPactadoMacho
        {
            get { return _ganadoPactadoMacho; }
            set { _ganadoPactadoMacho = value; }
        }
        private int _ganadoCompradoMacho;

        public int ganadoCompradoMacho
        {
            get { return _ganadoCompradoMacho; }
            set { _ganadoCompradoMacho = value; }
        }
        private int _ganadoCompradoHembra;

        public int ganadoCompradoHembra
        {
            get { return _ganadoCompradoHembra; }
            set { _ganadoCompradoHembra = value; }
        }
        private int _totalPactado;

        public int totalPactado
        {
            get { return _totalPactado; }
            set { _totalPactado = value; }
        }
        private int _totalComprado;

        public int totalComprado
        {
            get { return _totalComprado; }
            set { _totalComprado = value; }
        }
        #region Datos de control
        public bool activo { get; set; }
        public string user { get; set; }
        public string conexion { get; set; }
        public string resultado { get; set; }
        public int Result { get; set; }
        public int opcion { get; set; }
        public bool Completado { get; set; }
        public DateTime fechaProgramada { get;  set; }
        public string Estado { get; internal set; }
        public decimal municipio { get; internal set; }
        #endregion
    }
}