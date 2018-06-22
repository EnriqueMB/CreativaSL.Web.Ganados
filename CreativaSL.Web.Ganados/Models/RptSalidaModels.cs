using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptSalidaModels
    {
        public RptSalidaModels()
        {
            _Categoria = string.Empty;
            _SubCategoria = string.Empty;
            _Fecha = DateTime.Today;
            _FechaInicio = DateTime.Today;
            _FechaFin = DateTime.Today;
            _Conexion = string.Empty;
            _ListaSalidas = new List<RptSalidaModels>();
            _DatosEmpresa = new DatosEmpresaViewModels();
        }
        private string _Categoria;

        public string Categoria
        {
            get { return _Categoria; }
            set { _Categoria = value; }
        }

        private string _SubCategoria;

        public string SubCategoria
        {
            get { return _SubCategoria; }
            set { _SubCategoria = value; }
        }

        private DateTime _Fecha;

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private decimal _Total;

        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
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

        private List<RptSalidaModels> _ListaSalidas;

        public List<RptSalidaModels> ListaSalidas
        {
            get { return _ListaSalidas; }
            set { _ListaSalidas = value; }
        }

        private DatosEmpresaViewModels _DatosEmpresa;

        public DatosEmpresaViewModels DatosEmpresa
        {
            get { return _DatosEmpresa; }
            set { _DatosEmpresa = value; }
        }
    }
}