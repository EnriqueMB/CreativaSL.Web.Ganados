using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentoXPagar_Datos
    {
        public List<DocumentoPorPagarDetalleModels> ObtenerDetalleListaDocumentosPagar(DocumentoPorPagarDetalleModels datos)
        {
            try
            {
                List<DocumentoPorPagarDetalleModels> Lista = new List<DocumentoPorPagarDetalleModels>();
                DocumentoPorPagarDetalleModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Documento_get_DocumentoXPagarDetalle", datos.IDDocumentoPagar, datos.IDTipoDocumento);
                while (dr.Read())
                {
                    Item = new DocumentoPorPagarDetalleModels();
                    Item.IDTipoDocumento = !dr.IsDBNull(dr.GetOrdinal("IDTipo")) ? dr.GetInt32(dr.GetOrdinal("IDTipo")) : 0;
                    Item.IDDocumentoPagar = !dr.IsDBNull(dr.GetOrdinal("IDDocumentoPagar")) ? dr.GetString(dr.GetOrdinal("IDDocumentoPagar")) : string.Empty;
                    Item.nombreConciliacion = !dr.IsDBNull(dr.GetOrdinal("NombreConciliacion")) ? dr.GetString(dr.GetOrdinal("NombreConciliacion")) : string.Empty;
                    Item.nombreClasificacion = !dr.IsDBNull(dr.GetOrdinal("NombreClasificacion")) ? dr.GetString(dr.GetOrdinal("NombreClasificacion")) : string.Empty;
                    Item.cantidad = !dr.IsDBNull(dr.GetOrdinal("Cantidad")) ? dr.GetDecimal(dr.GetOrdinal("Cantidad")) : 0;
                    Item.PrecioUnitario= !dr.IsDBNull(dr.GetOrdinal("PrecioUnitario")) ? dr.GetDecimal(dr.GetOrdinal("PrecioUnitario")) : 0;
                    Item.SubTotal= !dr.IsDBNull(dr.GetOrdinal("Subtotal")) ? dr.GetDecimal(dr.GetOrdinal("Subtotal")) : 0;
                    if (Item.IDTipoDocumento == 1) {
                        Item.nombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("NombreORazonSocial")) ? dr.GetString(dr.GetOrdinal("NombreORazonSocial")) : string.Empty;
                        Item.ganadoTotal= !dr.IsDBNull(dr.GetOrdinal("GanadoTotalComprado")) ? dr.GetInt32(dr.GetOrdinal("GanadoTotalComprado")) : 0;
                        Item.kiloTotal = !dr.IsDBNull(dr.GetOrdinal("KilosTotal")) ? dr.GetDecimal(dr.GetOrdinal("KilosTotal")) : 0;
                    }
                    if (Item.IDTipoDocumento == 2)
                    {
                        Item.fecha = !dr.IsDBNull(dr.GetOrdinal("FechaCompra")) ? dr.GetDateTime(dr.GetOrdinal("FechaCompra")) : DateTime.Now;
                        Item.numeroFacturaNota = !dr.IsDBNull(dr.GetOrdinal("NumeroFacturaNota")) ? dr.GetString(dr.GetOrdinal("NumeroFacturaNota")) : string.Empty;
                        Item.descripcionProveedor = !dr.IsDBNull(dr.GetOrdinal("DescripcionProveedor")) ? dr.GetString(dr.GetOrdinal("DescripcionProveedor")) : string.Empty;
                        Item.RFCProveedor = !dr.IsDBNull(dr.GetOrdinal("RFCProveedor")) ? dr.GetString(dr.GetOrdinal("RFCProveedor")) : string.Empty;
                    }
                    if (Item.IDTipoDocumento == 3)
                    {
                        Item.fecha = !dr.IsDBNull(dr.GetOrdinal("FechaEntrega")) ? dr.GetDateTime(dr.GetOrdinal("FechaEntrega")) : DateTime.Now;
                        Item.tipoCombustible = !dr.IsDBNull(dr.GetOrdinal("TipoCombustible")) ? dr.GetString(dr.GetOrdinal("TipoCombustible")) : string.Empty;
                        Item.litros = !dr.IsDBNull(dr.GetOrdinal("Litros")) ? dr.GetDecimal(dr.GetOrdinal("Litros")) : 0;
                        Item.numTicket = !dr.IsDBNull(dr.GetOrdinal("NumTicket")) ? dr.GetString(dr.GetOrdinal("NumTicket")) : string.Empty;
                    }
                    if (Item.IDTipoDocumento == 4)
                    {
                        Item.fecha = !dr.IsDBNull(dr.GetOrdinal("FechaServicio")) ? dr.GetDateTime(dr.GetOrdinal("FechaServicio")) : DateTime.Now;
                        Item.tipoServicioVehiculo = !dr.IsDBNull(dr.GetOrdinal("TipoServicioVehiculo")) ? dr.GetString(dr.GetOrdinal("TipoServicioVehiculo")) : string.Empty;
                        
                    }


                    Lista.Add(Item);
                }
                return Lista;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DocumentoPorPagarModels> ObtenerListaDocumentosPagar(DocumentoPorPagarModels datos)
        {
            try
            {
                List<DocumentoPorPagarModels> Lista = new List<DocumentoPorPagarModels>();
                DocumentoPorPagarModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Documento_get_DocumentoXPagar");
                while (dr.Read())
                {
                    Item = new DocumentoPorPagarModels();
                    Item.IDTipoDocumento = !dr.IsDBNull(dr.GetOrdinal("IDTipo")) ? dr.GetInt32(dr.GetOrdinal("IDTipo")) : 0;
                    Item.IDDocumentoPagar = !dr.IsDBNull(dr.GetOrdinal("IDDocuemntoPagar")) ? dr.GetString(dr.GetOrdinal("IDDocuemntoPagar")) : string.Empty;
                    Item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    Item.Fecha = !dr.IsDBNull(dr.GetOrdinal("Fecha")) ? dr.GetDateTime(dr.GetOrdinal("Fecha")) : DateTime.Today;
                    Item.IDEstatus = !dr.IsDBNull(dr.GetOrdinal("IDEstatus")) ? dr.GetInt16(dr.GetOrdinal("IDEstatus")) : 1;
                    Item.EsSistema = !dr.IsDBNull(dr.GetOrdinal("EsSistema")) ? dr.GetBoolean(dr.GetOrdinal("EsSistema")) : false;
                    Item.Total = !dr.IsDBNull(dr.GetOrdinal("Total")) ? dr.GetDecimal(dr.GetOrdinal("Total")) : 0;
                    Item.Pagos = !dr.IsDBNull(dr.GetOrdinal("Pagos")) ? dr.GetDecimal(dr.GetOrdinal("Pagos")) : 0;
                    Item.Pendientes = !dr.IsDBNull(dr.GetOrdinal("Pendiente")) ? dr.GetDecimal(dr.GetOrdinal("Pendiente")) : 0;
                    Item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    Item.EstatusNombre = !dr.IsDBNull(dr.GetOrdinal("EstatusDocumento")) ? dr.GetString(dr.GetOrdinal("EstatusDocumento")) : string.Empty;
                    Lista.Add(Item);
                }
                return Lista;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}