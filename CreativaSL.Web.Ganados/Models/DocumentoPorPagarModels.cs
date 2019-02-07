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
            _numFactura = string.Empty;
            Conexion = string.Empty;
            Usuario = string.Empty;
            _NombreSucursal = string.Empty;
            _IDSucursal = string.Empty;
            _EstatusNombre = string.Empty;
            _ListaDocumentos = new List<DocumentoPorPagarModels>();
        }

        public string TotalString { get; set; }
        public string PagadoString { get; set; }
        public string PendienteString { get; set; }
        public string NombreRazonSocial { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal TotalDeducciones { get; set; }
        public decimal TotalPercepciones { get; set; }

        public decimal Impuestos { get; set; }
        //public decimal Subtotal { get; set; }
        public decimal Cambio { get; set; }

        private List<DocumentoPorPagarDetalleModels> _listaDocumentoPorPagarDetalle;

        public List<DocumentoPorPagarDetalleModels> listaDocumentoPorPagarDetalle
        {
            get { return _listaDocumentoPorPagarDetalle; }
            set { _listaDocumentoPorPagarDetalle = value; }
        }
        private List<CatTipoProveedorModels> _LisTipoProveedor;

        public List<CatTipoProveedorModels> LisTipoProveedor
        {
            get { return _LisTipoProveedor; }
            set { _LisTipoProveedor = value; }
        }

        private List<CatProveedorModels> _LisProveedor;

        public List<CatProveedorModels> LisProveedor
        {
            get { return _LisProveedor; }
            set { _LisProveedor = value; }
        }

        private string _IDDocumentoPagar;

        public string IDDocumentoPagar
        {
            get { return _IDDocumentoPagar; }
            set { _IDDocumentoPagar = value; }
        }
        private int _IDTProveedor;

        public int IDTProveedor
        {
            get { return _IDTProveedor; }
            set { _IDTProveedor = value; }
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
        private string _IDProveedor;

        public string IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }
        private string _numFactura;

        public string numFactura
        {
            get { return _numFactura; }
            set { _numFactura = value; }
        }

        private string _NombreSucursal;

        public string NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; }
        }
        private List<CatSucursalesModels> _listaSucursal;

        public List<CatSucursalesModels> ListaSucursal
        {
            get { return _listaSucursal; }
            set { _listaSucursal = value; }
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

        private List<CatTipoConciliacionModels> _ListaConciliacion;

        public List<CatTipoConciliacionModels> ListaConciliacion
        {
            get { return _ListaConciliacion; }
            set { _ListaConciliacion = value; }

        }
        private List<CatEstatusDocumentoModels> _ListaEstatus;

        public List<CatEstatusDocumentoModels> ListaEstatus
        {
            get { return _ListaEstatus; }
            set { _ListaEstatus = value; }

        }
        //ready
        private List<CatTipoDocumentoModels> _ListaCDocumento;

        public List<CatTipoDocumentoModels> ListaCDocumento
        {
            get { return _ListaCDocumento; }
            set { _ListaCDocumento = value; }

        }
        //[spCSLDB_DocumentoPorPagar_AC]
        #region DetalleDocumentos
        private string _id_detalleDoc;

        public string id_detalleDoc
        {
            get { return _id_detalleDoc; }
            set { _id_detalleDoc = value; }
        }
        private int _id_tipoConc;

        public int id_tipoConc
        {
            get { return _id_tipoConc; }
            set { _id_tipoConc = value; }
        }
        private int _id_concepto;

        public int id_concepto
        {
            get { return _id_concepto; }
            set { _id_concepto = value; }
        }
        private decimal _cantidad;

        public decimal cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        private decimal _precio;

        public decimal precio
        {
            get { return _precio; }
            set { _precio = value; }
        }
        private decimal _subtotal;

        public decimal subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; }
        }

        #endregion
        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}