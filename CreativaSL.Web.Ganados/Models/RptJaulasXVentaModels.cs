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
            _folio = 0;
            _modelo = string.Empty;
            _fechaFin = DateTime.Today;
            _fechaInicio = DateTime.Today;
            _datosEmpresa = new DatosEmpresaViewModels();
            _noSerie = string.Empty;
            _descripcion = string.Empty;
            _capacidad = string.Empty;
            Resultado = 0;
            Conexion = string.Empty;
            Completado = false;
            Usuario = string.Empty;
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
        private string _descripcion;

        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
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