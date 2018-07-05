using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptCorralesModels
    {

        public RptCorralesModels()
        {
            _Corral = string.Empty;
            _ListaCorrales = new List<RptCorralesModels>();
            _DatosEmpresa = new DatosEmpresaViewModels();
            _Conexion = string.Empty;
            _FechaInicio = DateTime.Today;
            _FechaFin = DateTime.Today;
            _NumeroSerie = string.Empty;
            _NumeroGrande = string.Empty;
        }

        private string _Corral;

        public string Corral
        {
            get { return _Corral; }
            set { _Corral = value; }
        }

        private int _NumeroFilas;

        public int NumeroFilas
        {
            get { return _NumeroFilas; }
            set { _NumeroFilas = value; }
        }


        private string _NumeroSerie;

        public string NumeroSerie
        {
            get { return _NumeroSerie; }
            set { _NumeroSerie = value; }
        }

        private string _NumeroGrande;

        public string NumeroGrande
        {
            get { return _NumeroGrande; }
            set { _NumeroGrande = value; }
        }

        private decimal _PesoInicial;

        public decimal PesoInicial
        {
            get { return _PesoInicial; }
            set { _PesoInicial = value; }
        }

        private decimal _Repeso;

        public decimal Repeso
        {
            get { return _Repeso; }
            set { _Repeso = value; }
        }

        private decimal _PesoPagado;

        public decimal PesoPagado
        {
            get { return _PesoPagado; }
            set { _PesoPagado = value; }
        }

        private decimal _DiferenciaKG;

        public decimal DiferenciaKG
        {
            get { return _DiferenciaKG; }
            set { _DiferenciaKG = value; }
        }

        private decimal _Merma;

        public decimal Merma
        {
            get { return _Merma; }
            set { _Merma = value; }
        }

        private List<RptCorralesModels> _ListaCorrales;

        public List<RptCorralesModels> ListaCorrales
        {
            get { return _ListaCorrales; }
            set { _ListaCorrales = value; }
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