using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentosPorCobrarDetalleModels
    {
        public string Id_detalleDoctoCobrar { get; set; }
        public string Id_documentoCobrar { get; set; }
        public string Id_productoServicio { get; set; }
        public int Id_tipoDocumento { get; set; }
        public int Id_tipoConciliacion { get; set; }
        public int Id_conceptoDocumento { get; set; }
        public int Id_conceptoDocumentoDeduccion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public decimal Impuestos { get; set; }

        public string Conexion { get; set; }
        public string Usuario { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }
        public int TipoServicio { get; set; }
        public List<ListaGenerica> ListaAsignar { get; set; }
        public List<CFDI_FormaPagoModels> ListaFormaPagoCFDI { get; set; }
        public List<CFDI_ProductoServicioModels> ListaProductosServiciosCFDI { get; set; }
        public List<CatTipoClasificacionCobroModels> ListaTipoClasificacionCobro { get; set; }

        public List<DocumentosPorCobrarDetalleModels> listaDocumentosDetalle { get; set; }

        public decimal TotalImpuestosRetenidos { get; set; }
        public decimal TotalImpuestosTrasladados { get; set; }

        public string Id_almacen { get; set; }
        public string Id_producto { get; set; }
        public List<CatAlmacenModels> ListaAlmacen { get; set; }
        public List<CatProductosAlmacenModels> ListaProductos { get; set; }

        public decimal Existencia { get; set; }
        public decimal PrecioPromedio { get; set; }

        public string Id_redireccionar { get; set; }
        public string Id_unidadProducto { get; set; }

        public bool Inventario { get; set; }
        public string NombreProducto { get; set; }
        public string NombreAlmacen { get; set; }

        /// <summary>
        /// Es el id del servicio, ya sea del flete, compra o venta
        /// </summary>
        public string  Id_servicio { get; set; }

        public string DescripcionDocumento { get; set; }
        public string nombreConciliacion { get; set; }
        public string nombreClasificacion { get; set; }
        public string nombreRazonSocial { get;  set; }
        public int ganadoTotal { get;  set; }
        public decimal kiloTotal { get;  set; }
        public string Folio { get;  set; }
        public DateTime FechaVenta { get;  set; }
        public object TipoFlete { get;  set; }
        public DateTime FechaEmbarque { get;  set; }
        public DateTime FechaSalida { get;  set; }
        public DateTime FechaTentativaEntrega { get;  set; }
        public DateTime FechaFinalizadoFlete { get;  set; }
        public DateTime FechaLlegada { get;  set; }
        public string NombreVenta { get;  set; }
        public decimal Pendiente { get;  set; }
        public DateTime Fecha { get;  set; }
    }
}
