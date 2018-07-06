using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _DocumentoXCobrar_Datos
    {
        #region JSON tablas
        #region Json Documentos Detalles
        public SqlDataReader GetDocumentosDetallesCompra(DocumentosPorCobrarDetalleModels Documento)
        {
            object[] parametros =
            {
                Documento.Id_documentoCobrar
            };

            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_DocumentoPorCobrar_get_DocumentosDetalles", parametros);
                dr.Close();
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Json Documentos Detalles Pagos
        public SqlDataReader GetDocumentosDetallesCompraPagos(DocumentosPorCobrarDetallePagosModels Documento)
        {
            object[] parametros =
            {
                Documento.Id_documentoPorCobrar
            };

            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_DocumentoPorCobrar_get_DocumentosDetallesPagos", parametros);
                dr.Close();
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region Combos
        #region Asignado a
        public List<ListaGenerica> GetListadoAsignar(DocumentosPorCobrarDetalleModels DocumentosPorCobrarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorCobrarModels.TipoServicio,
                    DocumentosPorCobrarModels.Id_documentoCobrar
                };
                ListaGenerica item;
                List<ListaGenerica> lista = new List<ListaGenerica>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_DocumentoPorCobrar_get_IDDocPorAsignar", parametros);
                while (dr.Read())
                {
                    item = new ListaGenerica
                    {
                        Id = !dr.IsDBNull(dr.GetOrdinal("id_documento")) ? dr.GetString(dr.GetOrdinal("id_documento")) : string.Empty,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty,
                    };
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
        public List<ListaGenerica> GetListadoAsignarPagos(DocumentosPorCobrarDetallePagosModels DocumentosPorCobrarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorCobrarModels.TipoServicio,
                    DocumentosPorCobrarModels.Id_documentoPorCobrar
                };
                ListaGenerica item;
                List<ListaGenerica> lista = new List<ListaGenerica>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_DocumentoPorCobrar_get_IDDocPorAsignar", parametros);
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
        #endregion
        #region CFDI Forma de pago
        public List<CFDI_FormaPagoModels> GetListadoCFDIFormaPago(DocumentosPorCobrarDetalleModels DocumentosPorCobrarModels)
        {
            try
            {
                CFDI_FormaPagoModels item;
                List<CFDI_FormaPagoModels> lista = new List<CFDI_FormaPagoModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_Combo_get_CFDIFormaPago");
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
        #endregion
        #region CFDI Productos y servicios
        public List<CFDI_ProductoServicioModels> GetListadoCFDIProductosServiciosCompra(DocumentosPorCobrarDetalleModels DocumentosPorCobrarModels)
        {
            try
            {
                CFDI_ProductoServicioModels item;
                List<CFDI_ProductoServicioModels> lista = new List<CFDI_ProductoServicioModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_Combo_get_CFDIProductoServicioCompra");
                while (dr.Read())
                {
                    item = new CFDI_ProductoServicioModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetString(dr.GetOrdinal("ID")) : string.Empty,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
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
        #endregion
        #region Tipo de clasificacion
        public List<CatTipoClasificacionCobroModels> GetListadoTipoClasificacion(DocumentosPorCobrarDetalleModels DocumentosPorCobrarModels)
        {
            try
            {
                CatTipoClasificacionCobroModels item;
                List<CatTipoClasificacionCobroModels> lista = new List<CatTipoClasificacionCobroModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_Combo_get_CatTipoClasificacionCobro");
                while (dr.Read())
                {
                    item = new CatTipoClasificacionCobroModels
                    {
                        Id_tipoClasificacionCobro = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                        Inventario = !dr.IsDBNull(dr.GetOrdinal("Inventario")) ? dr.GetBoolean(dr.GetOrdinal("Inventario")) : false
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
        #endregion
        #region CFDI Forma de pago
        public List<CFDI_FormaPagoModels> GetListadoCFDIFormaPago(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarDetallePagos)
        {
            try
            {
                CFDI_FormaPagoModels item;
                List<CFDI_FormaPagoModels> lista = new List<CFDI_FormaPagoModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_Combo_get_CFDIFormaPago");
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
        #endregion
        #region Cuentas bancarias
        public List<CuentaBancariaModels> GetListadoCuentasBancarias(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarDetallePagos)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPorCobrarDetallePagos.TipoCuentaBancaria,
                    DocumentoPorCobrarDetallePagos.Id_documentoPorCobrar
                };
                CuentaBancariaModels item;
                List<CuentaBancariaModels> lista = new List<CuentaBancariaModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_Compras_get_CuentasBancarias", parametros);
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
        #endregion
        #region Nombre proveedor cliente
        public DocumentosPorCobrarDetallePagosModels GetNombreEmpresaProveedorCliente(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarDetallePagos)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPorCobrarDetallePagos.Id_documentoPorCobrar
                };
                SqlDataReader dr = null;
                string nombre = string.Empty;

                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_DocumentoPorCobrar_get_NombreProveedorCliente_Empresa", parametros);
                while (dr.Read())
                {
                    DocumentoPorCobrarDetallePagos.NombreEmpresa = !dr.IsDBNull(dr.GetOrdinal("nombreEmpresa")) ? dr.GetString(dr.GetOrdinal("nombreEmpresa")) : string.Empty;
                    DocumentoPorCobrarDetallePagos.NombreProveedor_Cliente = !dr.IsDBNull(dr.GetOrdinal("nombreProveedorCliente")) ? dr.GetString(dr.GetOrdinal("nombreProveedorCliente")) : string.Empty;
                }
                dr.Close();
                return DocumentoPorCobrarDetallePagos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Almacenes con producto habilitado para su cobro o venta
        public List<CatAlmacenModels> GetAlmacenesHabilitados(DocumentosPorCobrarDetalleModels DocumentoPorCobrarDetallePagos)
        {
            try
            {
                SqlDataReader dr = null;
                List<CatAlmacenModels> lista = new List<CatAlmacenModels>();
                CatAlmacenModels item;
                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_Compras_get_Almacenes_Productos_Cobro");
                while (dr.Read())
                {
                    item = new CatAlmacenModels();
                    item.IDAlmacen = !dr.IsDBNull(dr.GetOrdinal("id_almacen")) ? dr.GetString(dr.GetOrdinal("id_almacen")) : string.Empty;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
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
        #endregion
        #region Almacenes con producto habilitado para su cobro o venta
        public List<CatProductosAlmacenModels> GetProductosAlmacen(DocumentosPorCobrarDetalleModels DocumentoPorCobrarDetallePagos, string almacen)
        {
            try
            {
                object[] parametro =
                {
                    almacen
                };
                SqlDataReader dr = null;
                List<CatProductosAlmacenModels> lista = new List<CatProductosAlmacenModels>();
                CatProductosAlmacenModels item;
                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_Compras_get_ProductosAlmacen", parametro);
                while (dr.Read())
                {
                    item = new CatProductosAlmacenModels();
                    item.IDProductoAlmacen = !dr.IsDBNull(dr.GetOrdinal("id_productoAlmacen")) ? dr.GetString(dr.GetOrdinal("id_productoAlmacen")) : string.Empty;
                    item.Nombre = !dr.IsDBNull(dr.GetOrdinal("nombre")) ? dr.GetString(dr.GetOrdinal("nombre")) : string.Empty;
                    item.Existencia = !dr.IsDBNull(dr.GetOrdinal("existencia")) ? dr.GetDecimal(dr.GetOrdinal("existencia")) : 0;
                    item.PrecioUnidad = !dr.IsDBNull(dr.GetOrdinal("precioUnidad")) ? dr.GetDecimal(dr.GetOrdinal("precioUnidad")) : 0;
                    item.Id_unidadProducto = !dr.IsDBNull(dr.GetOrdinal("id_unidadProducto")) ? dr.GetString(dr.GetOrdinal("id_unidadProducto")) : string.Empty;
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
        #endregion
        #endregion

        #region Get
        #region Asignado a
        public List<ListaGenerica> GetGeneralesDocumentoPorCobrarPago(DocumentosPorCobrarDetalleModels DocumentosPorCobrarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorCobrarModels.Id_documentoCobrar
                };
                ListaGenerica item;
                List<ListaGenerica> lista = new List<ListaGenerica>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_DocumentoPorCobrar_get_GeneralesDocumentoPorCobrar", parametros);
                while (dr.Read())
                {
                    item = new ListaGenerica
                    {
                        Id = !dr.IsDBNull(dr.GetOrdinal("id_documento")) ? dr.GetString(dr.GetOrdinal("id_documento")) : string.Empty,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty,
                    };
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
        #endregion
        #region GetDetalleDocumentoPago
        public DocumentosPorCobrarDetallePagosModels GetDetalleDocumentoPago(DocumentosPorCobrarDetallePagosModels DocumentoPago)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPago.Id_documentoPorCobrarDetallePagos,
                    DocumentoPago.TipoServicio
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPago.Conexion, "spCSLDB_DocumentoPorCobrar_get_GetDetalleDocumentoPago", parametros);
                while (dr.Read())
                {
                    DocumentoPago.Id_documentoPorCobrarDetallePagos = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrarDetallePagos")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrarDetallePagos")) : string.Empty;
                    DocumentoPago.Monto = !dr.IsDBNull(dr.GetOrdinal("monto")) ? dr.GetDecimal(dr.GetOrdinal("monto")) : 0;
                    DocumentoPago.Id_documentoPorCobrarDetallePagosBancarizado = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrarDetallePagosBancarizado")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrarDetallePagosBancarizado")) : string.Empty;
                    DocumentoPago.NombreBancoOrdenante = !dr.IsDBNull(dr.GetOrdinal("nombreBancoOrdenante")) ? dr.GetString(dr.GetOrdinal("nombreBancoOrdenante")) : string.Empty;
                    DocumentoPago.NumeroAutorizacion = !dr.IsDBNull(dr.GetOrdinal("numeroAutorizacion")) ? dr.GetString(dr.GetOrdinal("numeroAutorizacion")) : string.Empty;
                    DocumentoPago.NombreBancoBeneficiante = !dr.IsDBNull(dr.GetOrdinal("nombreBancoBeneficiante")) ? dr.GetString(dr.GetOrdinal("nombreBancoBeneficiante")) : string.Empty;
                    DocumentoPago.TipoCadenaPago = !dr.IsDBNull(dr.GetOrdinal("tipoCadenaPago")) ? dr.GetString(dr.GetOrdinal("tipoCadenaPago")) : string.Empty;
                    DocumentoPago.Id_documentoPorCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrar")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrar")) : string.Empty;
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
                }
                dr.Close();
                return DocumentoPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Lista tipo de impuestos CFDI SAT
        public List<CFDI_ImpuestoModels> GetListadoImpuesto(DocumentosPorCobrarDetalleImpuestoModels DocumentoImpuesto)
        {

            try
            {
                CFDI_ImpuestoModels Impuesto;
                List<CFDI_ImpuestoModels> ListaImpuestos = new List<CFDI_ImpuestoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoImpuesto.Conexion, "spCSLDB_Combo_get_CFDIImpuesto");
                while (dr.Read())
                {
                    Impuesto = new CFDI_ImpuestoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    ListaImpuestos.Add(Impuesto);
                }
                dr.Close();
                return ListaImpuestos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Generales del impuesto
        public DocumentosPorCobrarDetalleImpuestoModels GetGeneralesImpuesto(DocumentosPorCobrarDetalleImpuestoModels Impuesto)
        {
            try
            {
                SqlDataReader dr = null;

                object[] parametros =
                {
                    Impuesto.Id_detalleDoctoCobrar
                };
                dr = SqlHelper.ExecuteReader(Impuesto.Conexion, "spCSLDB_FleteImpuesto_get_FleteImpuestoXIDFleteImpuesto", parametros);
                while (dr.Read())
                {
                    Impuesto.Impuesto.Clave = !dr.IsDBNull(dr.GetOrdinal("id_impuesto")) ? dr.GetInt16(dr.GetOrdinal("id_impuesto")) : 0;
                    Impuesto.TipoFactor.Clave = !dr.IsDBNull(dr.GetOrdinal("id_tipoFactor")) ? dr.GetInt16(dr.GetOrdinal("id_tipoFactor")) : 0;
                    Impuesto.TipoImpuesto.Clave = !dr.IsDBNull(dr.GetOrdinal("id_tipoImpuesto")) ? dr.GetInt16(dr.GetOrdinal("id_tipoImpuesto")) : 0;
                    Impuesto.Base = !dr.IsDBNull(dr.GetOrdinal("base")) ? dr.GetDecimal(dr.GetOrdinal("base")) : 0;
                    Impuesto.TasaCuota = !dr.IsDBNull(dr.GetOrdinal("tasaCuota")) ? dr.GetDecimal(dr.GetOrdinal("tasaCuota")) : 0;
                    Impuesto.Importe = !dr.IsDBNull(dr.GetOrdinal("importe")) ? dr.GetDecimal(dr.GetOrdinal("importe")) : 0;
                }
                dr.Close();
                return Impuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Documento detalles producto servicio
        public DocumentosPorCobrarDetalleModels GetDatosProductoServicio(DocumentosPorCobrarDetalleModels documento)
        {
            try
            {
                SqlDataReader dr = null;

                object[] parametros =
                {
                    documento.Id_detalleDoctoCobrar
                };
                dr = SqlHelper.ExecuteReader(documento.Conexion, "spCSLDB_DocumentoPorCobrar_get_DocumentosDetallesProductoServicio", parametros);
                while (dr.Read())
                {
                    documento.Id_documentoCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoCobrar")) ? dr.GetString(dr.GetOrdinal("id_documentoCobrar")) : string.Empty;
                    documento.Id_productoServicio = !dr.IsDBNull(dr.GetOrdinal("id_productoServicio")) ? dr.GetString(dr.GetOrdinal("id_productoServicio")) : string.Empty;
                    documento.Id_conceptoDocumento = !dr.IsDBNull(dr.GetOrdinal("id_conceptoDocumento")) ? dr.GetInt16(dr.GetOrdinal("id_conceptoDocumento")) : 0;
                    documento.Cantidad= !dr.IsDBNull(dr.GetOrdinal("cantidad")) ? dr.GetDecimal(dr.GetOrdinal("cantidad")) : 0;
                    documento.PrecioUnitario = !dr.IsDBNull(dr.GetOrdinal("precioUnitario")) ? dr.GetDecimal(dr.GetOrdinal("precioUnitario")) : 0;
                    documento.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0; ;
                    documento.Id_producto = !dr.IsDBNull(dr.GetOrdinal("id_productoServicio")) ? dr.GetString(dr.GetOrdinal("id_productoServicio")) : string.Empty;
                    documento.Id_almacen = !dr.IsDBNull(dr.GetOrdinal("id_almacen")) ? dr.GetString(dr.GetOrdinal("id_almacen")) : string.Empty;
                    documento.Id_unidadProducto = !dr.IsDBNull(dr.GetOrdinal("id_unidadProducto")) ? dr.GetString(dr.GetOrdinal("id_unidadProducto")) : string.Empty;
                    documento.Inventario = !dr.IsDBNull(dr.GetOrdinal("inventario")) ? dr.GetBoolean(dr.GetOrdinal("inventario")) : false;
                    documento.NombreAlmacen = !dr.IsDBNull(dr.GetOrdinal("nombreAlmacen")) ? dr.GetString(dr.GetOrdinal("nombreAlmacen")) : string.Empty;
                    documento.NombreProducto = !dr.IsDBNull(dr.GetOrdinal("nombreProducto")) ? dr.GetString(dr.GetOrdinal("nombreProducto")) : string.Empty;
                }
                dr.Close();
                return documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion

        #endregion

        #region AC_Comprobante Compra
        public DocumentosPorCobrarDetallePagosModels AC_ComprobanteCompra(DocumentosPorCobrarDetallePagosModels DocumentosPorCobrarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorCobrarModels.Id_documentoPorCobrarDetallePagos,    DocumentosPorCobrarModels.Id_documentoPorCobrar,
                    DocumentosPorCobrarModels.Id_formaPago,                         DocumentosPorCobrarModels.Monto,
                    DocumentosPorCobrarModels.Observacion,                          DocumentosPorCobrarModels.fecha,
                    DocumentosPorCobrarModels.Id_cuentaBancariaOrdenante,           DocumentosPorCobrarModels.Id_documentoPorCobrarDetallePagosBancarizado,
                    DocumentosPorCobrarModels.Id_cuentaBancariaBeneficiante,        DocumentosPorCobrarModels.NombreBancoOrdenante,
                    DocumentosPorCobrarModels.NumeroAutorizacion,                   DocumentosPorCobrarModels.NumCuentaOrdenante,
                    DocumentosPorCobrarModels.NombreBancoBeneficiante,              DocumentosPorCobrarModels.NumCuentaBeneficiante,
                    DocumentosPorCobrarModels.FolioIFE,                             DocumentosPorCobrarModels.Usuario,
                    DocumentosPorCobrarModels.Bancarizado,                          DocumentosPorCobrarModels.RfcEmisorOrdenante,
                    DocumentosPorCobrarModels.RfcEmisorBeneficiario,                DocumentosPorCobrarModels.ImagenBase64
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_DocumentoPorCobrar_AC_DetallesPago", parametros);
                while (dr.Read())
                {
                    DocumentosPorCobrarModels.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    DocumentosPorCobrarModels.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                }
                dr.Close();
                return DocumentosPorCobrarModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region AC_Producto/servicio compra
        public DocumentosPorCobrarDetalleModels AC_ProductoServicio_Compra(DocumentosPorCobrarDetalleModels DocumentosPorCobrarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorCobrarModels.Id_detalleDoctoCobrar,            DocumentosPorCobrarModels.Id_documentoCobrar,
                    DocumentosPorCobrarModels.Id_productoServicio,              DocumentosPorCobrarModels.Id_conceptoDocumento,
                    DocumentosPorCobrarModels.Cantidad,                         DocumentosPorCobrarModels.PrecioUnitario,
                    DocumentosPorCobrarModels.Subtotal,                         DocumentosPorCobrarModels.Usuario,
                    DocumentosPorCobrarModels.TipoServicio,                     DocumentosPorCobrarModels.Id_almacen,
                    DocumentosPorCobrarModels.Id_unidadProducto,                DocumentosPorCobrarModels.Id_producto,
                    DocumentosPorCobrarModels.Existencia
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_DocumentoPorCobrar_AC_ServicioProducto_Compra", parametros);
                while (dr.Read())
                {
                    DocumentosPorCobrarModels.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    DocumentosPorCobrarModels.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                dr.Close();
                return DocumentosPorCobrarModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Del Documento por cobrar pago
        #region Compra
        public DocumentosPorCobrarDetallePagosModels DEL_ComprobanteCompra(DocumentosPorCobrarDetallePagosModels DocumentosPorCobrarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorCobrarModels.Id_documentoPorCobrarDetallePagos,    DocumentosPorCobrarModels.Id_documentoPorCobrar,
                    DocumentosPorCobrarModels.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_DocumentoPorCobrar_Del_DetallesPago", parametros);
                while (dr.Read())
                {
                    DocumentosPorCobrarModels.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    DocumentosPorCobrarModels.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                }
                dr.Close();
                return DocumentosPorCobrarModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion
        #region Del Producto/Servicio compra
        public DocumentosPorCobrarDetalleModels DEL_ProductoServicioCompra(DocumentosPorCobrarDetalleModels DocumentosPorCobrarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorCobrarModels.Id_detalleDoctoCobrar,    DocumentosPorCobrarModels.Id_documentoCobrar,
                    DocumentosPorCobrarModels.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_DocumentoPorCobrar_DEL_ProductoServicio_Compra", parametros);
                while (dr.Read())
                {
                    DocumentosPorCobrarModels.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    DocumentosPorCobrarModels.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                }
                dr.Close();
                return DocumentosPorCobrarModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


    }
}