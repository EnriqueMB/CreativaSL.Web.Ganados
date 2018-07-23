using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptEstadoCuentaProveedorModels
    {
        public RptEstadoCuentaProveedorModels()
        {
            _listEstadoCuentaProveedor = new List<RptEstadoCuentaProveedorModels>();
            _datosEmpresa = new DatosEmpresaViewModels();
            _folio = 0;
            _tipoProveedor = string.Empty;
            _nombreProveedor = string.Empty;
            _total = 0;
            _pagos = 0;
            _pendiente = 0;
            _fehcaInicio = DateTime.Today;
            _fechaFin = DateTime.Today;
            Conexion = string.Empty;
        }
        private List<RptEstadoCuentaProveedorModels> _listEstadoCuentaProveedor;

        public List<RptEstadoCuentaProveedorModels> listEstadoCuentaProveedor
        {
            get { return _listEstadoCuentaProveedor; }
            set { _listEstadoCuentaProveedor = value; }
        }
        private DatosEmpresaViewModels _datosEmpresa;

        public DatosEmpresaViewModels datosEmpresa
        {
            get { return _datosEmpresa; }
            set { _datosEmpresa = value; }
        }
        private DateTime _fehcaInicio;

        public DateTime fechaInicio
        {
            get { return _fehcaInicio; }
            set { _fehcaInicio = value; }
        }
        private DateTime _fechaFin;

        public DateTime fechaFin
        {
            get { return _fechaFin; }
            set { _fechaFin = value; }
        }

        private Int64 _folio;

        public Int64 folio
        {
            get { return _folio; }
            set { _folio = value; }
        }
        private string _nombreProveedor;

        public string nombreProveedor
        {
            get { return _nombreProveedor; }
            set { _nombreProveedor = value; }
        }
        private string _tipoProveedor;

        public string tipoProveedor
        {
            get { return _tipoProveedor; }
            set { _tipoProveedor = value; }
        }
        private decimal _total;

        public decimal total
        {
            get { return _total; }
            set { _total = value; }
        }
        private decimal _pendiente;

        public decimal pendiente
        {
            get { return _pendiente; }
            set { _pendiente = value; }
        }
        private decimal _pagos;

        public decimal pagos
        {
            get { return _pagos; }
            set { _pagos = value; }
        }
        #region Datos control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        #endregion
    }
}