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
                    Item.IDDetalleDoctoPagar = !dr.IsDBNull(dr.GetOrdinal("IDDetalleDocumento")) ? dr.GetString(dr.GetOrdinal("IDDetalleDocumento")) : string.Empty;
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
                        //Item.tipoServicioVehiculo = !dr.IsDBNull(dr.GetOrdinal("TipoServicioVehiculo")) ? dr.GetString(dr.GetOrdinal("TipoServicioVehiculo")) : string.Empty;
                        
                    }
                    Lista.Add(Item);
                }
                dr.Close();
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
                    Item.numFactura = !dr.IsDBNull(dr.GetOrdinal("Factura")) ? dr.GetString(dr.GetOrdinal("Factura")) : string.Empty;
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

                    Item.NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("nombreRazonSocial")) ? dr.GetString(dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                    Item.MontoPagado = !dr.IsDBNull(dr.GetOrdinal("montoPagado")) ? dr.GetDecimal(dr.GetOrdinal("montoPagado")) : 0;

                    Lista.Add(Item);
                }
                dr.Close();
                return Lista;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DocumentoPorPagarDetalleModels> ObtenerDetalleListaDocumentosPagarDetalle(DocumentoPorPagarDetalleModels datos)
        {
            try
            {
                List<DocumentoPorPagarDetalleModels> Lista = new List<DocumentoPorPagarDetalleModels>();
                DocumentoPorPagarDetalleModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Documento_get_DocumentoXPagarDetalleDetalle", datos.IDDocumentoPagar,  datos.IDDetalleDoctoPagar, datos.IDTipoDocumento);
                while (dr.Read())
                {
                    Item = new DocumentoPorPagarDetalleModels();
                    Item.IDTipoDocumento = !dr.IsDBNull(dr.GetOrdinal("IDTipo")) ? dr.GetInt32(dr.GetOrdinal("IDTipo")) : 0;
                    Item.IDDocumentoPagar = !dr.IsDBNull(dr.GetOrdinal("IDDocumentoPagar")) ? dr.GetString(dr.GetOrdinal("IDDocumentoPagar")) : string.Empty;
                    Item.nombreConciliacion = !dr.IsDBNull(dr.GetOrdinal("NombreConciliacion")) ? dr.GetString(dr.GetOrdinal("NombreConciliacion")) : string.Empty;
                    Item.nombreClasificacion = !dr.IsDBNull(dr.GetOrdinal("NombreClasificacion")) ? dr.GetString(dr.GetOrdinal("NombreClasificacion")) : string.Empty;
                    Item.cantidad = !dr.IsDBNull(dr.GetOrdinal("Cantidad")) ? dr.GetDecimal(dr.GetOrdinal("Cantidad")) : 0;
                    Item.PrecioUnitario = !dr.IsDBNull(dr.GetOrdinal("PrecioUnitario")) ? dr.GetDecimal(dr.GetOrdinal("PrecioUnitario")) : 0;
                    Item.SubTotal = !dr.IsDBNull(dr.GetOrdinal("Subtotal")) ? dr.GetDecimal(dr.GetOrdinal("Subtotal")) : 0;
                    if (Item.IDTipoDocumento == 1)
                    {
                        Item.nombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("NombreORazonSocial")) ? dr.GetString(dr.GetOrdinal("NombreORazonSocial")) : string.Empty;
                        Item.ganadoTotal = !dr.IsDBNull(dr.GetOrdinal("GanadoTotalComprado")) ? dr.GetInt32(dr.GetOrdinal("GanadoTotalComprado")) : 0;
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
                        //Item.tipoServicioVehiculo = !dr.IsDBNull(dr.GetOrdinal("TipoServicioVehiculo")) ? dr.GetString(dr.GetOrdinal("TipoServicioVehiculo")) : string.Empty;

                    }
                    Lista.Add(Item);
                }
                dr.Close();
                return Lista;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DocumentoPorPagarDetallePagosModels> ObtenerListaDetallePagos(DocumentoPorPagarDetallePagosModels datos)
        {
            try
            {
                List<DocumentoPorPagarDetallePagosModels> Lista = new List<DocumentoPorPagarDetallePagosModels>();
                DocumentoPorPagarDetallePagosModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_DocumentoPorPagar_get_DocumentosDetallesPagos", datos.Id_documentoPorPagar);
                while (dr.Read())
                {
                    Item = new DocumentoPorPagarDetallePagosModels();
                    Item.Id_documentoPorPagarDetallePagos = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorPagarDetallePagos")) ? dr.GetString(dr.GetOrdinal("id_documentoPorPagarDetallePagos")) : string.Empty;
                    Item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    Item.Monto = !dr.IsDBNull(dr.GetOrdinal("monto")) ? dr.GetDecimal(dr.GetOrdinal("monto")) : 0;
                    Item.fecha = !dr.IsDBNull(dr.GetOrdinal("fecha")) ? dr.GetDateTime(dr.GetOrdinal("fecha")) : DateTime.Now;
                    Lista.Add(Item);
                }
                dr.Close();
                return Lista;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DocumentoPorPagarModels AbcDocumentoXPagar(DocumentoPorPagarModels datos)
        {
            try
            {

                object[] parametros = {
                            datos.Opcion,
                            datos.IDDocumentoPagar ?? string.Empty,
                            datos.IDSucursal ?? string.Empty,
                            datos.Fecha,
                            datos.Usuario,
                            datos.id_concepto,
                            datos.precio,
                            datos.IDTProveedor,
                            datos.IDProveedor
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_DocumentoPorPagar_AC", parametros);
                datos.IDDocumentoPagar = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDDocumentoPagar))
                {
                    datos.Completado = true;
                }
                else
                {
                    datos.Completado = false;
                }
                
                return datos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<CatTipoDocumentoModels> ObtenerConceptosDocumento(string Conexion)
        {
            try
            {
                List<CatTipoDocumentoModels> Lista = new List<CatTipoDocumentoModels>();
                CatTipoDocumentoModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_ConceptosDocPagar");
                while (Dr.Read())
                {
                    Item = new CatTipoDocumentoModels();
                    Item.IDTipoDocumento = !Dr.IsDBNull(Dr.GetOrdinal("IdConcepto")) ? Dr.GetInt32(Dr.GetOrdinal("IdConcepto")) : 0;
                    Item.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ListaGenerica> GetListadoAsignarPagos(DocumentoPorPagarDetallePagosModels DocumentosPorPagarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorPagarModels.TipoServicio,
                    DocumentosPorPagarModels.Id_documentoPorPagar
                };
                ListaGenerica item;
                List<ListaGenerica> lista = new List<ListaGenerica>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorPagarModels.Conexion, "spCSLDB_DocumentoPorPagar_get_IDDocPorAsignar", parametros);
                while (dr.Read())
                {
                    item = new ListaGenerica();
                    item.Id = !dr.IsDBNull(dr.GetOrdinal("id_documento")) ? dr.GetString(dr.GetOrdinal("id_documento")) : string.Empty;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    item.Nombre = !dr.IsDBNull(dr.GetOrdinal("nombre")) ? dr.GetString(dr.GetOrdinal("nombre")) : string.Empty;
                    item.Id_2 = !dr.IsDBNull(dr.GetOrdinal("id2")) ? dr.GetString(dr.GetOrdinal("id2")) : string.Empty;

                    if (!string.IsNullOrEmpty(item.Id.Trim()))
                        lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CFDI_FormaPagoModels> GetListadoCFDIFormaPago(DocumentoPorPagarDetallePagosModels DocumentosPorPagarModels)
        {
            try
            {
                CFDI_FormaPagoModels item;
                List<CFDI_FormaPagoModels> lista = new List<CFDI_FormaPagoModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorPagarModels.Conexion, "spCSLDB_Combo_get_CFDIFormaPago");
                while (dr.Read())
                {
                    item = new CFDI_FormaPagoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                        Bancarizado = !dr.IsDBNull(dr.GetOrdinal("Bancarizado")) ? dr.GetInt32(dr.GetOrdinal("Bancarizado")) : 0,
                    };
                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DocumentoPorPagarDetallePagosModels GetNombreEmpresaProveedorCliente(DocumentoPorPagarDetallePagosModels DocumentoPagarDetallePagos)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPagarDetallePagos.Id_documentoPorPagar
                };
                SqlDataReader dr = null;
                string nombre = string.Empty;

                dr = SqlHelper.ExecuteReader(DocumentoPagarDetallePagos.Conexion, "spCSLDB_DocumentoPorPagar_get_NombreProveedorCliente_Empresa", parametros);
                while (dr.Read())
                {
                    DocumentoPagarDetallePagos.NombreEmpresa = !dr.IsDBNull(dr.GetOrdinal("nombreEmpresa")) ? dr.GetString(dr.GetOrdinal("nombreEmpresa")) : string.Empty;
                    DocumentoPagarDetallePagos.NombreProveedor_Cliente = !dr.IsDBNull(dr.GetOrdinal("nombreProveedorCliente")) ? dr.GetString(dr.GetOrdinal("nombreProveedorCliente")) : string.Empty;
                }
                dr.Close();
                return DocumentoPagarDetallePagos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CuentaBancariaModels> GetListadoCuentasBancarias(DocumentoPorPagarDetallePagosModels DocumentoPagarDetallePagos)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPagarDetallePagos.TipoCuentaBancaria,
                    DocumentoPagarDetallePagos.Id_documentoPorPagar
                };
                CuentaBancariaModels item;
                List<CuentaBancariaModels> lista = new List<CuentaBancariaModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPagarDetallePagos.Conexion, "spCSLDB_DocumentosPorPagar_Compras_get_CuentasBancarias", parametros);
                while (dr.Read())
                {
                    item = new CuentaBancariaModels();
                    item.IDDatosBancarios = !dr.IsDBNull(dr.GetOrdinal("id_datosBancarios")) ? dr.GetString(dr.GetOrdinal("id_datosBancarios")) : string.Empty;
                    item.Banco = new CatBancoModels();
                    item.Banco.IDBanco = !dr.IsDBNull(dr.GetOrdinal("id_banco")) ? dr.GetInt32(dr.GetOrdinal("id_banco")) : 0;
                    item.Banco.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    item.Titular = !dr.IsDBNull(dr.GetOrdinal("titular")) ? dr.GetString(dr.GetOrdinal("titular")) : string.Empty;
                    item.Clabe = !dr.IsDBNull(dr.GetOrdinal("clabeInterbancaria")) ? dr.GetString(dr.GetOrdinal("clabeInterbancaria")) : string.Empty;
                    item.NumCuenta = !dr.IsDBNull(dr.GetOrdinal("numCuenta")) ? dr.GetString(dr.GetOrdinal("numCuenta")) : string.Empty;
                    item.NumTarjeta = !dr.IsDBNull(dr.GetOrdinal("numTarjeta")) ? dr.GetString(dr.GetOrdinal("numTarjeta")) : string.Empty;

                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DocumentoPorPagarDetallePagosModels GetDetalleDocumentoPago(DocumentoPorPagarDetallePagosModels DocumentoPago)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPago.Id_documentoPorPagarDetallePagos
                    //DocumentoPago.TipoServicio
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPago.Conexion, "spCSLDB_DocumentoPorPagar_get_GetDetalleDocumentoPago", parametros);
                while (dr.Read())
                {
                    DocumentoPago.Id_documentoPorPagarDetallePagos = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorPagarDetallePagos")) ? dr.GetString(dr.GetOrdinal("id_documentoPorPagarDetallePagos")) : string.Empty;
                    DocumentoPago.Monto = !dr.IsDBNull(dr.GetOrdinal("monto")) ? dr.GetDecimal(dr.GetOrdinal("monto")) : 0;
                    DocumentoPago.Id_documentoPorPagarDetallePagosBancarizado = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorPagarDetallePagosBancarizado")) ? dr.GetString(dr.GetOrdinal("id_documentoPorPagarDetallePagosBancarizado")) : string.Empty;
                    DocumentoPago.NombreBancoOrdenante = !dr.IsDBNull(dr.GetOrdinal("nombreBancoOrdenante")) ? dr.GetString(dr.GetOrdinal("nombreBancoOrdenante")) : string.Empty;
                    DocumentoPago.NumeroAutorizacion = !dr.IsDBNull(dr.GetOrdinal("numeroAutorizacion")) ? dr.GetString(dr.GetOrdinal("numeroAutorizacion")) : string.Empty;
                    DocumentoPago.NombreBancoBeneficiante = !dr.IsDBNull(dr.GetOrdinal("nombreBancoBeneficiante")) ? dr.GetString(dr.GetOrdinal("nombreBancoBeneficiante")) : string.Empty;
                    DocumentoPago.TipoCadenaPago = !dr.IsDBNull(dr.GetOrdinal("tipoCadenaPago")) ? dr.GetString(dr.GetOrdinal("tipoCadenaPago")) : string.Empty;
                    DocumentoPago.Id_documentoPorPagar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorPagar")) ? dr.GetString(dr.GetOrdinal("id_documentoPorPagar")) : string.Empty;
                    DocumentoPago.Observacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;
                    DocumentoPago.Id_cuentaBancariaOrdenante = !dr.IsDBNull(dr.GetOrdinal("id_cuentaBancariaOrdenante")) ? dr.GetString(dr.GetOrdinal("id_cuentaBancariaOrdenante")) : string.Empty;
                    DocumentoPago.Id_cuentaBancariaBeneficiante = !dr.IsDBNull(dr.GetOrdinal("id_cuentaBancariaBeneficiante")) ? dr.GetString(dr.GetOrdinal("id_cuentaBancariaBeneficiante")) : string.Empty;
                    DocumentoPago.RfcEmisorOrdenante = !dr.IsDBNull(dr.GetOrdinal("rfcEmisorOrdenante")) ? dr.GetString(dr.GetOrdinal("rfcEmisorOrdenante")) : string.Empty;
                    DocumentoPago.RfcEmisorBeneficiario = !dr.IsDBNull(dr.GetOrdinal("rfcEmisorBeneficiario")) ? dr.GetString(dr.GetOrdinal("rfcEmisorBeneficiario")) : string.Empty;
                    DocumentoPago.FolioIFE = !dr.IsDBNull(dr.GetOrdinal("folioIFE")) ? dr.GetString(dr.GetOrdinal("folioIFE")) : string.Empty;
                    DocumentoPago.Id_formaPago = !dr.IsDBNull(dr.GetOrdinal("id_formaPago")) ? dr.GetInt16(dr.GetOrdinal("id_formaPago")) : 0;
                    DocumentoPago.fecha = !dr.IsDBNull(dr.GetOrdinal("fecha")) ? dr.GetDateTime(dr.GetOrdinal("fecha")) : DateTime.Now;
                    DocumentoPago.Id_cuentaBancariaBeneficiante = !dr.IsDBNull(dr.GetOrdinal("id_cuentaBancariaBeneficiante")) ? dr.GetString(dr.GetOrdinal("id_cuentaBancariaBeneficiante")) : string.Empty;
                    DocumentoPago.NombreBancoOrdenante = !dr.IsDBNull(dr.GetOrdinal("nombreBancoOrdenante")) ? dr.GetString(dr.GetOrdinal("nombreBancoOrdenante")) : string.Empty;
                    DocumentoPago.NumCuentaOrdenante = !dr.IsDBNull(dr.GetOrdinal("numCuentaOrdenante")) ? dr.GetString(dr.GetOrdinal("numCuentaOrdenante")) : string.Empty;
                    DocumentoPago.NumCuentaBeneficiante = !dr.IsDBNull(dr.GetOrdinal("numCuentaBeneficiante")) ? dr.GetString(dr.GetOrdinal("numCuentaBeneficiante")) : string.Empty;
                    DocumentoPago.Bancarizado = !dr.IsDBNull(dr.GetOrdinal("bancarizado")) ? dr.GetBoolean(dr.GetOrdinal("bancarizado")) : false;
                    DocumentoPago.ImagenBase64 = !dr.IsDBNull(dr.GetOrdinal("imagen")) ? dr.GetString(dr.GetOrdinal("imagen")) : string.Empty;
                    DocumentoPago.ImagenServer = !dr.IsDBNull(dr.GetOrdinal("imagenServer")) ? dr.GetInt32(dr.GetOrdinal("imagenServer")) : 0;
                    DocumentoPago.pendiente = !dr.IsDBNull(dr.GetOrdinal("pendiente")) ? dr.GetDecimal(dr.GetOrdinal("pendiente")) : 0;
                    DocumentoPago.PagarA = !dr.IsDBNull(dr.GetOrdinal("PagarA"))
                        ? dr.GetString(dr.GetOrdinal("PagarA"))
                        : string.Empty;
                }
                dr.Close();
                return DocumentoPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DocumentoPorPagarDetallePagosModels AC_ComprobanteCompra(DocumentoPorPagarDetallePagosModels DocumentosPagarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPagarModels.Id_documentoPorPagarDetallePagos,    DocumentosPagarModels.Id_documentoPorPagar,
                    DocumentosPagarModels.Id_formaPago,                         DocumentosPagarModels.Monto,
                    DocumentosPagarModels.Observacion,                          DocumentosPagarModels.fecha,
                    DocumentosPagarModels.Id_cuentaBancariaOrdenante,           DocumentosPagarModels.Id_documentoPorPagarDetallePagosBancarizado,
                    DocumentosPagarModels.Id_cuentaBancariaBeneficiante,        DocumentosPagarModels.NombreBancoOrdenante,
                    DocumentosPagarModels.NumeroAutorizacion,                   DocumentosPagarModels.NumCuentaOrdenante,
                    DocumentosPagarModels.NombreBancoBeneficiante,              DocumentosPagarModels.NumCuentaBeneficiante,
                    DocumentosPagarModels.FolioIFE,                             DocumentosPagarModels.Usuario,
                    DocumentosPagarModels.Bancarizado,                          DocumentosPagarModels.RfcEmisorOrdenante,
                    DocumentosPagarModels.RfcEmisorBeneficiario,                DocumentosPagarModels.ImagenBase64
                    , DocumentosPagarModels.PagarA
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPagarModels.Conexion, "spCSLDB_DocumentoPorPagar_AC_DetallesPago", parametros);
                while (dr.Read())
                {
                    DocumentosPagarModels.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    DocumentosPagarModels.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    DocumentosPagarModels.Completado = true;
                    DocumentosPagarModels.pendiente= !dr.IsDBNull(dr.GetOrdinal("pendiente")) ? dr.GetDecimal(dr.GetOrdinal("pendiente")) :0;
                }
                dr.Close();
                return DocumentosPagarModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatTipoProveedorModels> ObteneComboCatTipoProveedor(string Conexion)
        {
            try
            {
                List<CatTipoProveedorModels> lista = new List<CatTipoProveedorModels>();
                CatTipoProveedorModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatTipoProveedor");
                // lista.Add(new CatGeneroModels { IDGenero = string.Empty, NombreSucursal = " - Seleccione -" });
                while (dr.Read())
                {
                    item = new CatTipoProveedorModels();
                    item.IDTipoProveedor = !dr.IsDBNull(dr.GetOrdinal("id_tipoProveedor")) ? dr.GetInt32(dr.GetOrdinal("id_tipoProveedor")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatProveedorModels> ObteneComboProveedoresXID(DocumentoPorPagarModels Datos)
        {
            try
            {
                object[] parametros =
                {
                    Datos.IDTProveedor,

                };
                CatProveedorModels item;
                List<CatProveedorModels> lista = new List<CatProveedorModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatTipoProveedorXID", parametros);
                while (dr.Read())
                {
                    item = new CatProveedorModels();
                    item.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("id_proveedor")) ? dr.GetString(dr.GetOrdinal("id_proveedor")) : string.Empty;
                    item.NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;

                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DocumentoPorPagarDetallePagosModels EliminarPagoDocumentoPorPagar(DocumentoPorPagarDetallePagosModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Id_documentoPorPagarDetallePagos,datos.Id_documentoPorPagar, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_DocumentoPorPagar_DEL_DetallesPago", parametros);
                if (aux != null)
                {
                    int Resultado = 0;
                    int.TryParse(aux.ToString(), out Resultado);
                    if (Resultado == 1)
                    {
                        datos.Completado = true;
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentoPorPagarDetallePagosModels SpCSLDB_get_GetDetalleDocumentoPago(DocumentoPorPagarDetallePagosModels oModel)
        {
            try
            {
                object[] parametros =
                {
                    oModel.Id_documentoPorPagar
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(oModel.Conexion, "dbo.spCSLDB_get_GetDetalleDocumentoPago", parametros);
                while (dr.Read())
                {
                    oModel.pendiente = !dr.IsDBNull(dr.GetOrdinal("pendiente")) ? dr.GetDecimal(dr.GetOrdinal("pendiente")) : 0;
                }
                dr.Close();
                return oModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}