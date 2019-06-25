using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ReporteTipoFleteModels
    {
        public ReporteTipoFleteModels()
        {
            _FechaHora = DateTime.Now;
            _FechaInicio = DateTime.Today;
            _FechaFin = DateTime.Today;
            _Folio = 0;
            _Importe = 0;
            _LugarOrigen = string.Empty;
            _LugarDestino = string.Empty;
            _ListaReporteFleteModels = new List<ReporteTipoFleteModels>();
            _datosEmpresa = new DatosEmpresaViewModels();
            _ListaCmbSucursal = new List<CatSucursalesModels>();
            _ListaTipoFlete = new List<CmbTipoFleteModels>();
            _id_tipoFlete = 0;
            _TipoFlete = string.Empty;

            Conexion = string.Empty;
            Resultado = 0;
            Usuario = string.Empty;
            Completado = false;

        }

        private List<CmbTipoFleteModels> _ListaTipoFlete;

        public List<CmbTipoFleteModels> ListaTipoFlete
        {
            get { return _ListaTipoFlete; }
            set { _ListaTipoFlete = value; }
        }
        private int _id_tipoFlete;

        public int id_tipoFlete
        {
            get { return _id_tipoFlete; }
            set { _id_tipoFlete = value; }
        }


        private List<ReporteTipoFleteModels> _ListaReporteFleteModels;

        public List<ReporteTipoFleteModels> ListaReporteFlete
        {
            get { return _ListaReporteFleteModels; }
            set { _ListaReporteFleteModels = value; }
        }

        private DatosEmpresaViewModels _datosEmpresa;

        public DatosEmpresaViewModels datosEmpresa
        {
            get { return _datosEmpresa; }
            set { _datosEmpresa = value; }
        }

        private string _id_sucursal;

        public string id_sucursal
        {
            get { return _id_sucursal; }
            set { _id_sucursal = value; }
        }
        
        private List<CatSucursalesModels> _ListaCmbSucursal;
        /// <summary>
        /// Lista para llenar combo de sucursal
        /// </summary>
        public List<CatSucursalesModels> ListaCmbSucursal
        {
            get { return _ListaCmbSucursal; }
            set { _ListaCmbSucursal = value; }
        }


        private DateTime _FechaHora;

        public DateTime FechaHora
        {
            get { return _FechaHora; }
            set { _FechaHora = value; }
        }

        public string FechaInicioFormat
        {
            get { return _FechaInicio.ToShortDateString(); }
        }
        public string FechaFinFormat
        {
            get { return _FechaFin.ToShortDateString(); }
        }
        public string FechaFormat
        {
            get { return _FechaHora.ToShortDateString(); }
        }

        private string _TipoFlete;

        public string TipoFlete
        {
            get { return _TipoFlete; }
            set { _TipoFlete = value; }
        }
        private string _NombreTipoFlete;

        public string NombreTipoFlete
        {
            get { return _NombreTipoFlete; }
            set { _NombreTipoFlete = value; }
        }

        private Int64 _Folio;

        public Int64 Folio
        {
            get { return _Folio; }
            set { _Folio = value; }
        }
        private string _Proveedor;

        public string Proveedor
        {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }
        private string _LugarOrigen;

        public string LugarOrigen
        {
            get { return _LugarOrigen; }
            set { _LugarOrigen = value; }
        }
        private string _LugarDestino;

        public string LugarDestino
        {
            get { return _LugarDestino; }
            set { _LugarDestino = value; }
        }
        private string _Chofer;

        public string Chofer
        {
            get { return _Chofer; }
            set { _Chofer = value; }
        }
        private string _Unidad;

        public string Unidad
        {
            get { return _Unidad; }
            set { _Unidad = value; }
        }
        private string _Observacion;

        public string Observacion
        {
            get { return _Observacion; }
            set { _Observacion = value; }
        }
        private Decimal _Importe;

        public Decimal Importe
        {
            get { return _Importe; }
            set { _Importe = value; }
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




        #region Datos control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        #endregion



    }
}