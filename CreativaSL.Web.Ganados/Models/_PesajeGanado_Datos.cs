using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _PesajeGanado_Datos
    {
        public string PesajeGanado_spCIDDB_index(string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "pesajeGanado.spCIDDB_index");
                string datatable = Auxiliar.SqlReaderToJson(dr);

                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RespuestaAjax PesajeGanado_spCIDDB_ac(PesajeGanadoModels item, int opcion, string usuario, string conexion)
        {
            try
            {
                object[] parametros =
                {
                    item.Id,
                    item.Id_sucursal,
                    item.Id_cliente,
                    item.PesoTotal,
                    item.MontoPorCobrar,
                    usuario,
                    opcion
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "pesajeGanado.spCIDDB_ac", parametros);
                RespuestaAjax respuesta = new RespuestaAjax();
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

        public RespuestaAjax PesajeGanado_spCIDDB_del(int id, string conexion, string usuario)
        {
            try
            {
                object[] parametros = { id, usuario };
                SqlDataReader dr = null;
                RespuestaAjax respuesta = new RespuestaAjax();
                dr = SqlHelper.ExecuteReader(conexion, "pesajeGanado.spCIDDB_del", parametros);
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

        public RespuestaAjax pesajeGanado_spCIDDB_del_cobro(string id_documentoPorCobrar, string id_documentoPorCobrarDetallePagos, string usuario, string conexion)
        {
            try
            {
                object[] parametros = { id_documentoPorCobrar, id_documentoPorCobrarDetallePagos, usuario };
                SqlDataReader dr = null;
                RespuestaAjax respuesta = new RespuestaAjax();
                dr = SqlHelper.ExecuteReader(conexion, "pesajeGanado.spCIDDB_del_cobro", parametros);
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

        public PesajeGanadoModels PesajeGanado_spCIDDB_get_pesajeGanadoXID(int id, string conexion)
        {
            try
            {
                object[] parametros = { id };
                SqlDataReader dr = null;
                PesajeGanadoModels item = new PesajeGanadoModels();

                dr = SqlHelper.ExecuteReader(conexion, "pesajeGanado.spCIDDB_get_pesajeGanadoXID", parametros);
                while (dr.Read())
                {
                    item.Respuesta = new RespuestaAjax();

                    item.Respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    item.Respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;

                    if (item.Respuesta.Success)
                    {
                        item.Id = !dr.IsDBNull(dr.GetOrdinal("id")) ? dr.GetInt32(dr.GetOrdinal("id")) : 0;
                        item.Id_sucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                        item.Id_cliente = !dr.IsDBNull(dr.GetOrdinal("id_cliente")) ? dr.GetString(dr.GetOrdinal("id_cliente")) : string.Empty;
                        item.PesoTotal = !dr.IsDBNull(dr.GetOrdinal("pesoTotal")) ? dr.GetDecimal(dr.GetOrdinal("pesoTotal")) : 0;
                        item.MontoPorCobrar = !dr.IsDBNull(dr.GetOrdinal("montoPorCobrar")) ? dr.GetDecimal(dr.GetOrdinal("montoPorCobrar")) : 0;
                    }
                }
                dr.Close();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TransaccionesModels PesajeGanado_spCIDDB_get_Transacciones(string id, string conexion)
        {
            try
            {
                object[] parametros =
                {
                     id
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "pesajeGanado.spCIDDB_get_Transacciones", parametros);
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

        public string PesajeGanado_spCSLDB_get_DetallesDocumentoPorCobrar(string id, string id_documentoPorCobrar, string conexion)
        {
            try
            {
                object[] parametros = { id, id_documentoPorCobrar };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "pesajeGanado.spCSLDB_get_DetallesDocumentoPorCobrar", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PesajeGanado_spCSLDB_get_DetallesDocumentoPorCobrarCobros(string id, string id_documentoPorCobrar, string conexion)
        {
            try
            {
                object[] parametros = { id, id_documentoPorCobrar };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "pesajeGanado.spCSLDB_get_DetallesDocumentoPorCobrarCobros", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PesajeGanado_spCSLDB_Venta_DetallesDocumentoPorCobrarDeducciones(string id, string id_documentoPorCobrar, string conexion)
        {
            try
            {
                object[] parametros = { id, id_documentoPorCobrar };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "pesajeGanado.spCSLDB_Venta_DetallesDocumentoPorCobrarDeducciones", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PesajeGanado_spCIDDB_get_DetallesDocXcobrarPAGOS(int id, string conexion)
        {
            try
            {
                object[] parametros = { id };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "pesajeGanado.spCIDDB_get_DetallesDocXcobrarPAGOS", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentosPorCobrarDetallePagosModels PesajeGanado_spCSLDB_get_GetDetalleDocumentoPago(DocumentosPorCobrarDetallePagosModels DocumentoPago)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPago.Id_padre,
                    DocumentoPago.Id_documentoPorCobrarDetallePagos
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPago.Conexion, "pesajeGanado.spCSLDB_get_GetDetalleDocumentoPago", parametros);
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
                        DocumentoPago.pendiente = !dr.IsDBNull(dr.GetOrdinal("pendiente")) ? dr.GetDecimal(dr.GetOrdinal("pendiente")) : 0;
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

        public DocumentosPorCobrarDetallePagosModels PesajeGanado_spCSLDB_get_NombreCliente_Empresa(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarDetallePagos)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPorCobrarDetallePagos.Id_padre
                };
                SqlDataReader dr = null;
                string nombre = string.Empty;

                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "pesajeGanado.spCSLDB_get_NombreCliente_Empresa", parametros);
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

        public List<CuentaBancariaModels> PesajeGanado_spCIDDB_get_CuentasBancarias(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarDetallePagos)
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
                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "pesajeGanado.spCIDDB_get_CuentasBancarias", parametros);
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

        public DocumentosPorCobrarDetallePagosModels PesajeGanado_spCSLDB_ac_detallesPago(DocumentosPorCobrarDetallePagosModels DocumentosPorCobrarModels)
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
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "pesajeGanado.spCSLDB_ac_detallesPago", parametros);
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

        public PesajeGanadoDetalleModels PesajeGanado_spCIDDB_get_detalle(int id, string conexion)
        {
            try
            {
                object[] parametros =
                {
                    id
                };

                PesajeGanadoDetalleModels oDetalle = new PesajeGanadoDetalleModels();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "pesajeGanado.spCIDDB_get_detalle", parametros);

                while (dr.Read())
                {
                    oDetalle.Id = !dr.IsDBNull(dr.GetOrdinal("id")) ? dr.GetInt32(dr.GetOrdinal("id")) : 0;
                    oDetalle.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;
                    oDetalle.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("nombreSucursal")) ? dr.GetString(dr.GetOrdinal("nombreSucursal")) : string.Empty;
                    oDetalle.Id_documentoCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrar")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrar")) : string.Empty;
                    oDetalle.NombreCliente = !dr.IsDBNull(dr.GetOrdinal("nombreCliente")) ? dr.GetString(dr.GetOrdinal("nombreCliente")) : string.Empty;
                    oDetalle.FechaCreacion = !dr.IsDBNull(dr.GetOrdinal("fechaCreacion")) ? dr.GetString(dr.GetOrdinal("fechaCreacion")) : string.Empty;
                    oDetalle.FechaFinalizacion = !dr.IsDBNull(dr.GetOrdinal("fechaFinalizacion")) ? dr.GetString(dr.GetOrdinal("fechaFinalizacion")) : string.Empty;
                    oDetalle.MontoTotalKilo = !dr.IsDBNull(dr.GetOrdinal("pesoTotal")) ? dr.GetDecimal(dr.GetOrdinal("pesoTotal")) : 0;
                    oDetalle.MontoTotalPorCobrar = !dr.IsDBNull(dr.GetOrdinal("montoPorCobrar")) ? dr.GetDecimal(dr.GetOrdinal("montoPorCobrar")) : 0;
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

        public RespuestaAjax PesajeGanado_spCIDDB_finalizar(int id, string conexion, string usuario)
        {
            try
            {
                object[] parametros = { id, usuario };
                SqlDataReader dr = null;
                RespuestaAjax respuesta = new RespuestaAjax();
                dr = SqlHelper.ExecuteReader(conexion, "pesajeGanado.spCIDDB_finalizar", parametros);
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