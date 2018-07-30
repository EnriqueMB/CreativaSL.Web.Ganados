using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentoPorPagarDetalleModels
    {
        private string _IDDetalleDoctoPagar;

        public string IDDetalleDoctoPagar
        {
            get { return _IDDetalleDoctoPagar; }
            set { _IDDetalleDoctoPagar = value; }
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
        private decimal _cantidad;

        public decimal cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        private string _nombreRazonSocial;

        public string nombreRazonSocial
        {
            get { return _nombreRazonSocial; }
            set { _nombreRazonSocial = value; }
        }

        private string _IDTipoConciliacion;

        public string IDTipoConciliacion
        {
            get { return _IDTipoConciliacion; }
            set { _IDTipoConciliacion = value; }
        }

        private string _IDConceptoDocumento;

        public string IDConceptoDocumento
        {
            get { return _IDConceptoDocumento; }
            set { _IDConceptoDocumento = value; }
        }

        private decimal _PrecioUnitario;

        public decimal PrecioUnitario
        {
            get { return _PrecioUnitario; }
            set { _PrecioUnitario = value; }
        }
        private int _ganadoTotal;

        public int ganadoTotal
        {
            get { return _ganadoTotal; }
            set { _ganadoTotal = value; }
        }
        private decimal _kiloTotal;

        public decimal kiloTotal
        {
            get { return _kiloTotal; }
            set { _kiloTotal = value; }
        }

        private decimal _SubTotal;

        public decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; }
        }
        private DateTime _fecha;

        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        private string _numeroFacturaNota;

        public string numeroFacturaNota
        {
            get { return _numeroFacturaNota; }
            set { _numeroFacturaNota = value; }
        }
        private string _descripcionProveedor;

        public string descripcionProveedor
        {
            get { return _descripcionProveedor; }
            set { _descripcionProveedor = value; }
        }
        private string _RFCProveedor;

        public string RFCProveedor
        {
            get { return _RFCProveedor; }
            set { _RFCProveedor = value; }
        }
        private decimal _precioUnitario;

        public decimal precioUnitario
        {
            get { return _precioUnitario; }
            set { _precioUnitario = value; }
        }
        private string _tipoCombustible;

        public string tipoCombustible
        {
            get { return _tipoCombustible; }
            set { _tipoCombustible = value; }
        }
        private decimal _litros;

        public decimal litros
        {
            get { return _litros; }
            set { _litros = value; }
        }
        private string _numTicket;

        public string numTicket
        {
            get { return _numTicket; }
            set { _numTicket = value; }
        }
        private string _nombreConciliacion;

        public string nombreConciliacion
        {
            get { return _nombreConciliacion; }
            set { _nombreConciliacion = value; }
        }
        private string _nombreClasificacion;

        public string nombreClasificacion
        {
            get { return _nombreClasificacion; }
            set { _nombreClasificacion = value; }
        }
        private string _tipoServicioVehiculo;

        public string tipoServicioVehiculo
        {
            get { return _tipoServicioVehiculo; }
            set { _tipoServicioVehiculo = value; }
        }
        private List<DocumentoPorPagarDetalleModels> _listaDocumentosDetalle;

        public List<DocumentoPorPagarDetalleModels> listaDocumentosDetalle
        {
            get { return _listaDocumentosDetalle; }
            set { _listaDocumentosDetalle = value; }
        }

        public RespuestaAjax RespuestaAjax { get; set; }
        public string Id_servicio { get; set; }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}