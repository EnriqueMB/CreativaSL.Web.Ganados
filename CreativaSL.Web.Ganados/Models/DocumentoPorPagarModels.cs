using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentoPorPagarModels
    {

        public DocumentoPorPagarModels()
        {
            _IDDocumentoPagar = string.Empty;
            Conexion = string.Empty;
            Usuario = string.Empty;
            _NombreSucursal = string.Empty;
            _IDSucursal = string.Empty;
            _EstatusNombre = string.Empty;
            _ListaDocumentos = new List<DocumentoPorPagarModels>();
        }
        private List<DocumentoPorPagarDetalleModels> _listaDocumentoPorPagarDetalle;

        public List<DocumentoPorPagarDetalleModels> listaDocumentoPorPagarDetalle
        {
            get { return _listaDocumentoPorPagarDetalle; }
            set { _listaDocumentoPorPagarDetalle = value; }
        }


        private string _IDDocumentoPagar;

        public string IDDocumentoPagar
        {
            get { return _IDDocumentoPagar; }
            set { _IDDocumentoPagar = value; }
        }
        private int _IDTipoDocumento;

        public int IDTipoDocumento
        {
            get { return _IDTipoDocumento; }
            set { _IDTipoDocumento = value; }
        }

        private string _IDSucursal;

        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private string _NombreSucursal;

        public string NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; }
        }

        private string _EstatusNombre;

        public string EstatusNombre
        {
            get { return _EstatusNombre; }
            set { _EstatusNombre = value; }
        }

        private DateTime _Fecha;

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private int _IDEstatus;

        public int IDEstatus
        {
            get { return _IDEstatus; }
            set { _IDEstatus = value; }
        }

        private bool _EsSistema;

        public bool EsSistema
        {
            get { return _EsSistema; }
            set { _EsSistema = value; }
        }

        private decimal _Total;

        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        private decimal _Pagos;

        public decimal Pagos
        {
            get { return _Pagos; }
            set { _Pagos = value; }
        }

        private decimal _Pendientes;

        public decimal Pendientes
        {
            get { return _Pendientes; }
            set { _Pendientes = value; }
        }

        private List<DocumentoPorPagarModels> _ListaDocumentos;

        public List<DocumentoPorPagarModels> ListaDocumentos
        {
            get { return _ListaDocumentos; }
            set { _ListaDocumentos = value; }
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