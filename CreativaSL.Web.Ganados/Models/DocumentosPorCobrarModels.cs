using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentosPorCobrarModels
    {

        public DocumentosPorCobrarModels()
        {
            _IDSucursal = string.Empty;
            Id_tipoDocumento = 0 ;
        }

        public string Id_documentoCobrar { get; set; }
        public int Id_tipoDocumento { get; set; }
        public int id_tipoConciliacion { get; set; }
        public string Id_sucursal { get; set; }
        public string Id_metodoPago { get; set; }
        public DateTime Fecha { get; set; }
        public bool EsSistema { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }
        public decimal Cambio { get; set; }
        public decimal Pagos { get; set; }
        public decimal Pendiente { get; set; }
        public decimal Subtotal { get; set; }
        public string NombreSucursal { get; set; }
        public string Conexion { get; set; }    
        public string Usuario { get; set; }
        public long Folio { get; set; }

        public int Opcion { get; set; }
        public bool Completado { get; set; }

        public DocumentosPorCobrarModels DocumentoPorCobraFlete;

        private string _IDSucursal;
        public string Id_sucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private List<DocumentosPorCobrarModels> _ListaDocumentos;
        public List<DocumentosPorCobrarModels> ListaDocumentos
        {
            get { return _ListaDocumentos; }
            set { _ListaDocumentos = value; }
        }

        private List<CatSucursalesModels> _listaSucursal;
        public List<CatSucursalesModels> ListaSucursal
        {
            get { return _listaSucursal; }
            set { _listaSucursal = value; }
        }

        private List<CatTipoClasificacionCobroModels> _ListaCDocumento;
        public List<CatTipoClasificacionCobroModels> ListaCDocumento
        {
            get { return _ListaCDocumento; }
            set { _ListaCDocumento = value; }
        }

        private List<CFDI_MetodoPagoModels> _ListaMetodoPago;
        public List<CFDI_MetodoPagoModels> ListaMetodoPago
        {
            get { return _ListaMetodoPago; }
            set { _ListaMetodoPago = value; }
        }

    }
}
