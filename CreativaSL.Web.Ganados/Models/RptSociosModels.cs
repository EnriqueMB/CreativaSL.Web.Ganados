using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptSociosModels
    {
        public RptSociosModels()
        {
            _NombreCompleto = string.Empty;
            _Procentaje = 0;
            _ListaSocio = new List<RptSociosModels>();
            _Total = 0;
            _DatosEmpresa = new DatosEmpresaViewModels();
            _Conexion = string.Empty;
            _IdSucursal = string.Empty;
        }
        private string _IdSucursal;

        public string IdSucursal
        {
            get { return _IdSucursal; }
            set { _IdSucursal = value; }
        }


        private string _NombreCompleto;

        public string NombreCompleto
        {
            get { return _NombreCompleto; }
            set { _NombreCompleto = value; }
        }

        private int _Procentaje;

        public int Porcentaje
        {
            get { return _Procentaje; }
            set { _Procentaje = value; }
        }

        private decimal _Total;

        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        private List<RptSociosModels> _ListaSocio;

        public List<RptSociosModels> ListaSocios
        {
            get { return _ListaSocio; }
            set { _ListaSocio = value; }
        }

        private DatosEmpresaViewModels _DatosEmpresa;

        public DatosEmpresaViewModels DatosEmpresa
        {
            get { return _DatosEmpresa; }
            set { _DatosEmpresa = value; }
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
    }
}