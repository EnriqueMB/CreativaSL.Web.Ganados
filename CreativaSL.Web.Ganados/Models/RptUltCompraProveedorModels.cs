using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptUltCompraProveedorModels
    {
        public RptUltCompraProveedorModels()
        {
            _IDCompra = string.Empty;
            _IDProveedor = string.Empty;
            _NombreProveedor = string.Empty;
            _FechaSiguienteCompra = DateTime.Now;
            _ListaSiguientesCompras = new List<RptUltCompraProveedorModels>();
            _fechaStart = DateTime.Today;
            _fechaEnd = DateTime.Today;
            Conexion = string.Empty;
            Usuario = string.Empty;
        }

        private string _IDProveedor;

        public string IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }

        private string _IDCompra;

        public string IDCompra
        {
            get { return _IDCompra; }
            set { _IDCompra = value; }
        }

        private string _NombreProveedor;

        public string NombreProveedor
        {
            get { return _NombreProveedor; }
            set { _NombreProveedor = value; }
        }

        private DateTime _FechaSiguienteCompra;

        public DateTime FechaSiguienteCompra
        {
            get { return _FechaSiguienteCompra; }
            set { _FechaSiguienteCompra = value; }
        }

        private List<RptUltCompraProveedorModels> _ListaSiguientesCompras;

        public List<RptUltCompraProveedorModels> ListaSiguienteCompras
        {
            get { return _ListaSiguientesCompras; }
            set { _ListaSiguientesCompras = value; }
        }

        private DateTime _fechaStart;

        public DateTime fechaStart
        {
            get { return _fechaStart; }
            set { _fechaStart = value; }
        }
        private DateTime _fechaEnd;

        public DateTime fechaEnd
        {
            get { return _fechaEnd; }
            set { _fechaEnd = value; }
        }

        private int _GanadosPactadoMachos;

        public int GanadosPactadoMachos
        {
            get { return _GanadosPactadoMachos; }
            set { _GanadosPactadoMachos = value; }
        }
        private int _GanadosPactadoHembras;

        public int GanadosPactadoHembras
        {
            get { return _GanadosPactadoHembras; }
            set { _GanadosPactadoHembras = value; }
        }

        private string _start;

        public string start
        {
            get { return _start; }
            set { _start = value; }
        }

        private int _FolioCompra;

        public int FolioCompra
        {
            get { return _FolioCompra; }
            set { _FolioCompra = value; }
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