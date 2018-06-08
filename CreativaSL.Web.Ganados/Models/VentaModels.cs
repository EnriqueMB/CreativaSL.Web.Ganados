using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Areas.Admin.Models.Venta
{
    public class VentaModels
    {
        public string Folio{ get; set; }
        public string Id_sucursal { get; set; }

        private string _id_venta, _id_cliente, _id_flete, _documentoXcobrar, _id_recepcion, 
            _guiaTransito, _certZooSanitario, _certTuberculosis, _certBrucelosis, _numFactura;
        private decimal _montoTotal, _pesoTotalEnviado;
        private DateTime _fechaHoraVenta, _fechaHoraEmbarque, _fechaHoraSalida;
        private DataTable _tablaVenta;


        public string id_venta
        {
            get { return _id_venta; }
            set { _id_venta = value; }
        }
        public string id_cliente
        {
            get { return _id_cliente; }
            set { _id_cliente = value; }
        }
        public string id_flete
        {
            get { return _id_flete; }
            set { _id_flete = value; }
        }
        public string documentoXcobrar
        {
            get { return _documentoXcobrar; }
            set { _documentoXcobrar = value; }
        }
        public string id_recepcion
        {
            get { return _id_recepcion; }
            set { _id_recepcion = value; }
        }
        public decimal montoTotal
        {
            get { return _montoTotal; }
            set { _montoTotal = value; }
        }
        public DateTime fechaHoraVenta
        {
            get { return _fechaHoraVenta; }
            set { _fechaHoraVenta = value; }
        }
        public DateTime fechaHoraEmbarque
        {
            get { return _fechaHoraEmbarque; }
            set { _fechaHoraEmbarque = value; }
        }
        public DateTime fechaHoraSalida
        {
            get { return _fechaHoraSalida; }
            set { _fechaHoraSalida = value; }
        }
        public string guiaTransito
        {
            get { return _guiaTransito; }
            set { _guiaTransito = value; }
        }
        public decimal pesoTotalEnviado
        {
            get { return _pesoTotalEnviado; }
            set { _pesoTotalEnviado = value; }
        }
        public string certZooSanitario
        {
            get { return _certZooSanitario; }
            set { _certZooSanitario = value; }
        }
        public string certTuberculosis
        {
            get { return _certTuberculosis; }
            set { _certTuberculosis = value; }
        }
        public string certBrucelosis
        {
            get { return _certBrucelosis; }
            set { _certBrucelosis = value; }
        }
        public string numFactura
        {
            get { return _numFactura; }
            set { _numFactura = value; }
        }

        public DataTable tablaVenta
        {
            get { return _tablaVenta; }
            set { _tablaVenta = value; }
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