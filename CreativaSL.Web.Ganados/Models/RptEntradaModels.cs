using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptEntradaModels
    {

        public RptEntradaModels()
        {
            _NombreCliente = string.Empty;
            _Fecha = DateTime.Today;
            _Total = 0;
            _Descripcion = string.Empty;
            _Merma = 0;
            _Folio = 0;
            _Observaciones = string.Empty;
            _ListaEntradas = new List<RptEntradaModels>();
            _DatosEmpresa = new DatosEmpresaViewModels();
        }

        private string _NombreCliente;

        public string NombreCliente
        {
            get { return _NombreCliente; }
            set { _NombreCliente = value; }
        }

        private DateTime _Fecha;

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private int _Total;

        public int Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        private string _Descripcion;

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private decimal _Merma;

        public decimal Merma
        {
            get { return _Merma; }
            set { _Merma = value; }
        }

        private Int64 _Folio;

        public Int64 Folio
        {
            get { return _Folio; }
            set { _Folio = value; }
        }

        private string _Observaciones;

        public string Observaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
        }

        private decimal _ImporteGanado;

        public decimal ImporteGanado
        {
            get { return _ImporteGanado; }
            set { _ImporteGanado = value; }
        }

        private decimal _CostoFlete;

        public decimal CostoFlete
        {
            get { return _CostoFlete; }
            set { _CostoFlete = value; }
        }

        private string _AplicaCobro;

        public string AplicaCobro
        {
            get { return _AplicaCobro; }
            set { _AplicaCobro = value; }
        }

        private string _Conexion;

        public string Conexion
        {
            get { return _Conexion; }
            set { _Conexion = value; }
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

        private List<RptEntradaModels> _ListaEntradas;

        public List<RptEntradaModels> ListaEntradas
        {
            get { return _ListaEntradas; }
            set { _ListaEntradas = value; }
        }

        private DatosEmpresaViewModels _DatosEmpresa;

        public DatosEmpresaViewModels DatosEmpresa
        {
            get { return _DatosEmpresa; }
            set { _DatosEmpresa = value; }
        }
    }
}