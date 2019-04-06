using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _VentaGeneral_Datos
    {
        public string VentaGeneral_spCIDDB_index(string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "ventaGeneral.spCIDDB_index");
                string datatable = Auxiliar.SqlReaderToJson(dr);

                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RespuestaAjax VentaGeneral_spCIDDB_ac(VentaGeneralModels oVentaGeneral, string conexion, string usuario, int opcion)
        {
            try
            {
                RespuestaAjax oRespuesta = new RespuestaAjax();
                // construct sql connection and sql command objects.
                using (SqlConnection sqlcon = new SqlConnection(conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("ventaGeneral.spCIDDB_ac", sqlcon))
                    {
                        //parametros de entrada
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable dataTable;
                        dataTable = new DataTable("Items");

                        dataTable.Columns.Add("id", typeof(int));
                        dataTable.Columns.Add("fk_id", typeof(int));
                        dataTable.Columns.Add("id_producto", typeof(string));
                        dataTable.Columns.Add("id_tipoProducto", typeof(int));
                        dataTable.Columns.Add("id_documentoPorCobrarDetalle", typeof(string));
                        dataTable.Columns.Add("cantidad", typeof(decimal));
                        dataTable.Columns.Add("precioUnitario", typeof(decimal));
                        dataTable.Columns.Add("id_almacen", typeof(string));
                        dataTable.Columns.Add("id_unidadProducto", typeof(string));

                        foreach (var item in oVentaGeneral.ListaDetalles)
                        {
                            dataTable.Rows.Add(
                                item.Id, 
                                item.Fk_id, 
                                item.Id_producto, 
                                item.Id_tipoProducto,
                                item.Id_documentoPorCobrar, 
                                item.Cantidad, 
                                item.PrecioUnitario,
                                item.Id_almacen, 
                                item.Id_unidadProducto
                            );
                        }

                        cmd.Parameters.Add("@id", SqlDbType.Char).Value = oVentaGeneral.Id;
                        cmd.Parameters.Add("@id_sucursal", SqlDbType.Char).Value = oVentaGeneral.Id_sucursal;
                        cmd.Parameters.Add("@id_cliente", SqlDbType.Char).Value = oVentaGeneral.Id_cliente;
                        cmd.Parameters.Add("@tbl_productosDetalle", SqlDbType.Structured).Value = dataTable;
                        cmd.Parameters.Add("@usuario", SqlDbType.Char).Value = usuario;
                        cmd.Parameters.Add("@opcion", SqlDbType.SmallInt).Value = opcion;

                        //parametros de salida
                        cmd.Parameters.Add(new SqlParameter("@mensaje", SqlDbType.NVarChar, -1)); //-1 para tipo MAX
                        cmd.Parameters["@mensaje"].Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new SqlParameter("@success", SqlDbType.Bit));
                        cmd.Parameters["@success"].Direction = ParameterDirection.Output;
                        // execute
                        sqlcon.Open();

                        cmd.ExecuteNonQuery();
                        oRespuesta.Mensaje = cmd.Parameters["@mensaje"].Value.ToString();

                        bool success;

                        if (Boolean.TryParse(cmd.Parameters["@success"].Value.ToString(), out success))
                        {
                            oRespuesta.Success = success;
                        }
                    }
                }

                return oRespuesta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public TransaccionesModels VentaGeneral_spCIDDB_get_Transacciones(string id, string conexion)
        {
            try
            {
                object[] parametros =
                {
                     id
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "ventaGeneral.spCIDDB_get_Transacciones", parametros);
                TransaccionesModels oTransaccion = new TransaccionesModels();
                oTransaccion.Respuesta = new RespuestaAjax();

                while (dr.Read())
                {
                    oTransaccion.Respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                    if (oTransaccion.Respuesta.Success)
                    {
                        oTransaccion.IdModuloGenerico = !dr.IsDBNull(dr.GetOrdinal("id")) ? dr.GetString(dr.GetOrdinal("id")) : string.Empty;
                        oTransaccion.Id_documentoCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrar")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrar")) : string.Empty;
                        oTransaccion.Pagos = !dr.IsDBNull(dr.GetOrdinal("pagos")) ? dr.GetDecimal(dr.GetOrdinal("pagos")) : 0;
                        oTransaccion.Pendiente = !dr.IsDBNull(dr.GetOrdinal("pendiente")) ? dr.GetDecimal(dr.GetOrdinal("pendiente")) : 0;
                        oTransaccion.Impuestos = !dr.IsDBNull(dr.GetOrdinal("impuestos")) ? dr.GetDecimal(dr.GetOrdinal("impuestos")) : 0;
                        oTransaccion.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotalPercepcion")) ? dr.GetDecimal(dr.GetOrdinal("subtotalPercepcion")) : 0;
                        oTransaccion.Total = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;
                        oTransaccion.TotalPercepciones = !dr.IsDBNull(dr.GetOrdinal("totalPercepcion")) ? dr.GetDecimal(dr.GetOrdinal("totalPercepcion")) : 0;
                        oTransaccion.TotalDeducciones = !dr.IsDBNull(dr.GetOrdinal("totalDeduccion")) ? dr.GetDecimal(dr.GetOrdinal("totalDeduccion")) : 0;
                    }
                    else
                    {
                        oTransaccion.Respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    }
                }
                dr.Close();
                return oTransaccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string VentaGeneral_spCSLDB_get_DetallesDocumentoPorCobrar(string id, string id_documentoPorCobrar, string conexion)
        {
            try
            {
                object[] parametros = { id, id_documentoPorCobrar };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "ventaGeneral.spCSLDB_get_DetallesDocumentoPorCobrar", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string VentaGeneral_spCIDDB_get_DetallesDocumentoPorCobrarCobros(string id, string id_documentoPorCobrar, string conexion)
        {
            try
            {
                object[] parametros = { id, id_documentoPorCobrar };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "ventaGeneral.spCIDDB_get_DetallesDocumentoPorCobrarCobros", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentosPorCobrarDetallePagosModels VentaGeneral_spCIDDB_ac_detallesPago(DocumentosPorCobrarDetallePagosModels DocumentosPorCobrarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorCobrarModels.Id_documentoPorCobrarDetallePagos,    DocumentosPorCobrarModels.Id_padre,
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
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "ventaGeneral.spCIDDB_ac_detallesPago", parametros);
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

        public DocumentosPorCobrarDetallePagosModels VentaGeneral_spCIDDB_get_GetDetalleDocumentoPago(DocumentosPorCobrarDetallePagosModels DocumentoPago)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPago.Id_padre,
                    DocumentoPago.Id_documentoPorCobrarDetallePagos
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPago.Conexion, "ventaGeneral.spCIDDB_get_GetDetalleDocumentoPago", parametros);
                while (dr.Read())
                {
                    DocumentoPago.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    if (DocumentoPago.RespuestaAjax.Success)
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
                        DocumentoPago.ImagenServer = !dr.IsDBNull(dr.GetOrdinal("imagenServer")) ? dr.GetInt32(dr.GetOrdinal("imagenServer")) : 0;
                    }
                    else
                    {
                        DocumentoPago.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;

                    }
                }
                dr.Close();
                return DocumentoPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentosPorCobrarDetallePagosModels VentaGeneral_spCIDDB_get_NombreCliente_Empresa(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarDetallePagos)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPorCobrarDetallePagos.Id_padre
                };
                SqlDataReader dr = null;
                string nombre = string.Empty;

                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "ventaGeneral.spCIDDB_get_NombreCliente_Empresa", parametros);
                while (dr.Read())
                {
                    DocumentoPorCobrarDetallePagos.NombreEmpresa = !dr.IsDBNull(dr.GetOrdinal("nombreEmpresa")) ? dr.GetString(dr.GetOrdinal("nombreEmpresa")) : string.Empty;
                    DocumentoPorCobrarDetallePagos.NombreProveedor_Cliente = !dr.IsDBNull(dr.GetOrdinal("nombreProveedor")) ? dr.GetString(dr.GetOrdinal("nombreProveedor")) : string.Empty;
                }
                dr.Close();
                return DocumentoPorCobrarDetallePagos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CuentaBancariaModels> VentaGeneral_spCIDDB_get_CuentasBancarias(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarDetallePagos)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPorCobrarDetallePagos.TipoCuentaBancaria,
                    DocumentoPorCobrarDetallePagos.Id_padre
                };
                CuentaBancariaModels item;
                List<CuentaBancariaModels> lista = new List<CuentaBancariaModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "ventaGeneral.spCIDDB_get_CuentasBancarias", parametros);
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

        public RespuestaAjax VentaGeneral_spCIDDB_del_cobro(string id_documentoPorCobrar, string id_documentoPorCobrarDetallePagos, string usuario, string conexion)
        {
            try
            {
                object[] parametros = { id_documentoPorCobrar, id_documentoPorCobrarDetallePagos, usuario };
                SqlDataReader dr = null;
                RespuestaAjax respuesta = new RespuestaAjax();
                dr = SqlHelper.ExecuteReader(conexion, "ventaGeneral.spCIDDB_del_cobro", parametros);
                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VentaGeneralComprobanteDetalleModels> VentaGeneral_spCIDDB_Comprobante_get_detalles(string conexion, int id)
        {
            try
            {
                object[] parametros =
                {
                    id
                };
                VentaGeneralComprobanteDetalleModels item;
                List<VentaGeneralComprobanteDetalleModels> lista = new List<VentaGeneralComprobanteDetalleModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "ventaGeneral.spCIDDB_Comprobante_get_detalles", parametros);
                while (dr.Read())
                {
                    item = new VentaGeneralComprobanteDetalleModels();
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    item.Cantidad = !dr.IsDBNull(dr.GetOrdinal("cantidad")) ? dr.GetDecimal(dr.GetOrdinal("cantidad")) : 0;
                    item.PrecioUnitario = !dr.IsDBNull(dr.GetOrdinal("precioUnitario")) ? dr.GetDecimal(dr.GetOrdinal("precioUnitario")) : 0;
                    item.Subtotal = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;

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

        public VentaGeneralVistaDetallesModels VentaGeneral_spCIDDB_get_detalle(int id, string conexion)
        {
            try
            {
                object[] parametros =
                {
                    id
                };

                VentaGeneralVistaDetallesModels oDetalle = new VentaGeneralVistaDetallesModels();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "ventaGeneral.spCIDDB_get_detalle", parametros);

                while (dr.Read())
                {
                    oDetalle.Id = !dr.IsDBNull(dr.GetOrdinal("id")) ? dr.GetInt32(dr.GetOrdinal("id")) : 0;
                    oDetalle.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;
                    oDetalle.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("nombreSucursal")) ? dr.GetString(dr.GetOrdinal("nombreSucursal")) : string.Empty;
                    oDetalle.Id_documentoCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrar")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrar")) : string.Empty;
                    oDetalle.NombreCliente = !dr.IsDBNull(dr.GetOrdinal("nombreCliente")) ? dr.GetString(dr.GetOrdinal("nombreCliente")) : string.Empty;
                    oDetalle.FechaCreacion = !dr.IsDBNull(dr.GetOrdinal("fechaCreacion")) ? dr.GetString(dr.GetOrdinal("fechaCreacion")) : string.Empty;
                    oDetalle.FechaFinalizacion = !dr.IsDBNull(dr.GetOrdinal("fechaFinalizacion")) ? dr.GetString(dr.GetOrdinal("fechaFinalizacion")) : string.Empty;
                    oDetalle.TotalPercepciones = !dr.IsDBNull(dr.GetOrdinal("totalPercepciones")) ? dr.GetDecimal(dr.GetOrdinal("totalPercepciones")) : 0;
                    oDetalle.TotalDeducciones = !dr.IsDBNull(dr.GetOrdinal("totalDeducciones")) ? dr.GetDecimal(dr.GetOrdinal("totalDeducciones")) : 0;
                    oDetalle.Total = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;
                    oDetalle.Impuestos = !dr.IsDBNull(dr.GetOrdinal("impuestos")) ? dr.GetDecimal(dr.GetOrdinal("impuestos")) : 0;
                    oDetalle.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0;
                    oDetalle.Pagos = !dr.IsDBNull(dr.GetOrdinal("pagos")) ? dr.GetDecimal(dr.GetOrdinal("pagos")) : 0;
                }
                dr.Close();
                return oDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string VentaGeneral_spCIDDB_get_DetallesDocXcobrarPAGOS(int id, string conexion)
        {
            try
            {
                object[] parametros = { id };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "ventaGeneral.spCIDDB_get_DetallesDocXcobrarPAGOS", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RespuestaAjax VentaGeneral_spCIDDB_del(int id, string conexion, string usuario)
        {
            try
            {
                object[] parametros = { id, usuario };
                SqlDataReader dr = null;
                RespuestaAjax respuesta = new RespuestaAjax();
                dr = SqlHelper.ExecuteReader(conexion, "ventaGeneral.spCIDDB_del", parametros);
                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RespuestaAjax VentaGeneral_spCIDDB_finalizar(int id, string conexion, string usuario)
        {
            try
            {
                object[] parametros = { id, usuario };
                SqlDataReader dr = null;
                RespuestaAjax respuesta = new RespuestaAjax();
                dr = SqlHelper.ExecuteReader(conexion, "ventaGeneral.spCIDDB_finalizar", parametros);
                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}