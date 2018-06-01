using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptSalidasModels
    {
        public RptSalidasModels()
        {
            
            _DatosEmpresa = new DatosEmpresaViewModels();
            _fechaFin = DateTime.Now;
            _fechaInicio = DateTime.Now;
            _fecha = DateTime.Now;
            _subtotal = 0;
            _concepto = 0;
            //Datos de control
            activo = false;
            user = string.Empty;
            conexion = string.Empty;
            Completado = false;
            opcion = 0;
            resultado = string.Empty;
            _TablaDatos = new DataTable();
        }
        private List<RptSalidasModels> _listaSalidas;

        public List<RptSalidasModels> listaSalidas
        {
            get { return _listaSalidas; }
            set { _listaSalidas = value; }
        }
        private int _concepto;

        public int concepto
        {
            get { return _concepto; }
            set { _concepto = value; }
        }
        private DateTime _fecha;

        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        private decimal _subtotal;

        public decimal subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; }
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

        private DatosEmpresaViewModels _DatosEmpresa;

        public DatosEmpresaViewModels DatosEmpresa
        {
            get { return _DatosEmpresa; }
            set { _DatosEmpresa = value; }
        }

        private DataTable _TablaDatos;

        public DataTable TablaDatos
        {
            get { return _TablaDatos; }
            set { _TablaDatos = value; }
        }



        #region Datos de control
        public bool activo { get; set; }
        public string user { get; set; }
        public string conexion { get; set; }
        public string resultado { get; set; }
        public int Result { get; set; }
        public int opcion { get; set; }
        public bool Completado { get; set; }
        public DateTime fechaProgramada { get; set; }
        public string Estado { get; set; }
        public string municipio { get; set; }
        #endregion

    }
}