using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CreativaSL.Web.Ganados.Models.System;
using CreativaSL.Web.Ganados.Models.Datatable;
using System.Data;
using CreativaSL.Web.Ganados.Models.Dto.Base;
using CreativaSL.Web.Ganados.Models.Dto.Flete;
using Newtonsoft.Json;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Flete_Datos : BaseSQL
    {

        public string GetFleteIndexDataTable(FleteModels Flete)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_index");
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetDocumentosDataTable(DataTableAjaxPostModel dataTableAjaxPostModel, FleteModels Flete)
        {
            try
            {
                var datatable = string.Empty;
                using (SqlConnection sqlcon = new SqlConnection(ConexionSql))
                {
                    using (SqlCommand cmd = new SqlCommand("spCSLDB_Flete_get_DocumentosXIDFlete", sqlcon))
                    {
                        //parametros de entrada
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@id_flete", SqlDbType.Char).Value = Flete.id_flete;
                        cmd.Parameters.Add("@Start", SqlDbType.Int).Value = dataTableAjaxPostModel.start;
                        cmd.Parameters.Add("@Length", SqlDbType.Int).Value = dataTableAjaxPostModel.length;
                        cmd.Parameters.Add("@SearchValue", SqlDbType.NVarChar).Value = dataTableAjaxPostModel.search.value;
                        cmd.Parameters.Add("@Draw", SqlDbType.Int).Value = dataTableAjaxPostModel.draw;
                        cmd.Parameters.Add("@ColumnNumber", SqlDbType.Int).Value = dataTableAjaxPostModel.order[0].column;
                        cmd.Parameters.Add("@ColumnDir", SqlDbType.NVarChar).Value = dataTableAjaxPostModel.order[0].dir;

                        // execute
                        sqlcon.Open();

                        var reader = cmd.ExecuteReader();

                        var indexDatatableDto = new IndexDatatableDto();

                        if (reader.HasRows)
                        {
                            indexDatatableDto.Data = new List<object>();
                            bool firstData = true;

                            while (reader.Read())
                            {
                                if (firstData)
                                {
                                    indexDatatableDto.Draw = int.Parse(reader["Draw"].ToString()); ;
                                    indexDatatableDto.RecordsFiltered = int.Parse(reader["RecordsFiltered"].ToString());
                                    indexDatatableDto.RecordsTotal = int.Parse(reader["RecordsTotal"].ToString());
                                    firstData = false;
                                }

                                var item = new IndexDocumentoFleteDetalleDto();
                                item.IdDocumentoFleteDetalle = reader["IdDocumentoFleteDetalle"].ToString();
                                item.Clave = reader["Clave"].ToString();
                                item.Imagen = reader["Imagen"].ToString();
                                item.Descripcion = reader["Descripcion"].ToString();
                                item.Imagen = Auxiliar.ValidImageFormServer(item.Imagen,
                                    ProjectSettings.BaseDirFleteDocumentoDetalle);

                                indexDatatableDto.Data.Add(item);
                            }
                        }

                        var json = JsonConvert.SerializeObject(indexDatatableDto);

                        reader.Close();

                        return json;
                    }
                }

                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetGanadoDataTable(FleteModels Flete)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_get_GanadoActivo");
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetProductoGanadoXIDFlete(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                    {
                        Flete.id_flete
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_ProductoGanadoPropioXIDFlete", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetProductoGanadoNOPropioXIDFlete(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                    {
                        Flete.id_flete
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_ProductoGanadoNOPropioXIDFlete", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetEventoXIDFlete(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                    {
                        Flete.id_flete
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_Eventos", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();

                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SqlDataReader GetProductoGanadoNoAccidentadoXIDEvento(EventoEnvioModels Evento)
        {
            try
            {
                object[] parametros =
                    {
                        Evento.IDEnvio,
                        Evento.IDEvento
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Flete_get_ProductoGanadoCargadoSINEventoXIDFleteXIDEvento", parametros);
                dr.Close();
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SqlDataReader GetProductoGanadoAccidentadoXIDEvento(EventoEnvioModels Evento)
        {
            try
            {
                object[] parametros =
                    {
                        Evento.IDEnvio,
                        Evento.IDEvento
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Flete_get_ProductoGanadoCargadoCONEventoXIDFleteXIDEvento", parametros);
                dr.Close();
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableGanadoEnFlete(EventoFleteModels EventoFlete)
        {
            try
            {
                object[] parametros = { EventoFlete.Id_flete, EventoFlete.Id_eventoFlete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(EventoFlete.Conexion, "spCSLDB_Flete_get_DatatableGanadoEnFlete", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableGanadoConEvento(EventoFleteModels EventoFlete)
        {
            try
            {
                object[] parametros = { EventoFlete.Id_flete, EventoFlete.Id_eventoFlete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(EventoFlete.Conexion, "spCSLDB_Flete_get_DatatableGanadoConEvento", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableDetallesDocumentoPorCobrarFlete(FleteModels Flete)
        {
            try
            {
                object[] parametros = { Flete.id_flete, Flete.Id_documentoPorCobrar };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_DetallesDocumentoPorCobrar", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableDetallesDocumentoPorCobrarFleteDeduccions(FleteModels Flete)
        {
            try
            {
                object[] parametros = { Flete.id_flete, Flete.Id_documentoPorCobrar };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_DetallesDocumentoPorCobrarDeduccion", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableDetallesDocumentoPorCobrarFleteCobro(FleteModels Flete)
        {
            try
            {
                object[] parametros = { Flete.id_flete, Flete.Id_documentoPorCobrar };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_DetallesDocumentoPorCobrarCobros", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Vista Transacciones
        public TransaccionesFleteModels GetTransacciones(TransaccionesFleteModels Transacciones)
        {
            try
            {
                object[] parametros =
                {
                     Transacciones.Id_flete
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Transacciones.Conexion, "spCSLDB_Flete_get_Transacciones", parametros);
                Transacciones.DocumentosPorCobrar = new DocumentosPorCobrarModels();

                while (dr.Read())
                {
                    Transacciones.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                    if(Transacciones.RespuestaAjax.Success)
                    {
                        Transacciones.DocumentosPorCobrar.Id_documentoCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrar")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrar")) : string.Empty;
                        Transacciones.DocumentosPorCobrar.Pagos = !dr.IsDBNull(dr.GetOrdinal("pagos")) ? dr.GetDecimal(dr.GetOrdinal("pagos")) : 0;
                        Transacciones.DocumentosPorCobrar.Pendiente = !dr.IsDBNull(dr.GetOrdinal("pendiente")) ? dr.GetDecimal(dr.GetOrdinal("pendiente")) : 0;
                        Transacciones.DocumentosPorCobrar.Cambio = !dr.IsDBNull(dr.GetOrdinal("cambio")) ? dr.GetDecimal(dr.GetOrdinal("cambio")) : 0;
                        Transacciones.DocumentosPorCobrar.Impuestos = !dr.IsDBNull(dr.GetOrdinal("impuestos")) ? dr.GetDecimal(dr.GetOrdinal("impuestos")) : 0;
                        Transacciones.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0;
                        Transacciones.DocumentosPorCobrar.Total = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;
                        Transacciones.DocumentosPorCobrar.TotalPercepciones = !dr.IsDBNull(dr.GetOrdinal("totalPercepcion")) ? dr.GetDecimal(dr.GetOrdinal("totalPercepcion")) : 0;
                        Transacciones.DocumentosPorCobrar.TotalDeducciones = !dr.IsDBNull(dr.GetOrdinal("totalDeduccion")) ? dr.GetDecimal(dr.GetOrdinal("totalDeduccion")) : 0;
                    }
                    else
                    {
                        Transacciones.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    }
                }
                dr.Close();
                return Transacciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Vista Impuesto
        public FleteImpuestoModels Get_AC_FleteImpuestos(FleteImpuestoModels FleteImpuesto)
        {
            try
            {
                object[] parametros =
                {
                     FleteImpuesto.IDFlete,
                     FleteImpuesto.Id_detalleDoctoCobrar
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(FleteImpuesto.Conexion, "spCSLDB_FleteImpuesto_get_FleteImpuesto", parametros);

                while (dr.Read())
                {
                    FleteImpuesto.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                    if (FleteImpuesto.RespuestaAjax.Success)
                    {
                        FleteImpuesto.PrecioProducto = !dr.IsDBNull(dr.GetOrdinal("precioUnitario")) ? dr.GetDecimal(dr.GetOrdinal("precioUnitario")) : 0;
                        FleteImpuesto.TotalImpuestoRetencion = !dr.IsDBNull(dr.GetOrdinal("impuestos_retenidos")) ? dr.GetDecimal(dr.GetOrdinal("impuestos_retenidos")) : 0;
                        FleteImpuesto.TotalImpuestoTrasladado = !dr.IsDBNull(dr.GetOrdinal("impuestos_trasladados")) ? dr.GetDecimal(dr.GetOrdinal("impuestos_trasladados")) : 0;
                        FleteImpuesto.TotalImpuestos = !dr.IsDBNull(dr.GetOrdinal("impuestos")) ? dr.GetDecimal(dr.GetOrdinal("impuestos")) : 0;
                        FleteImpuesto.SubTotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0;
                        FleteImpuesto.Total = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;
                    }
                    else
                    {
                        FleteImpuesto.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    }
                }
                dr.Close();
                return FleteImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FleteImpuestoModels Flete_ac_ImpuestoProductoServicio(FleteImpuestoModels FleteImpuesto)
        {
            try
            {
                object[] parametros =
                {
                    FleteImpuesto.IDFlete,
                    FleteImpuesto.IDFleteImpuesto,//@id_documentoCobrarDetalleImpuesto char(36)
                    FleteImpuesto.Id_detalleDoctoCobrar,//,@id_detalleDoctoCobrar char(36)
                    FleteImpuesto.TipoImpuesto.Clave,//,@id_tipoImpuesto smallint
                    FleteImpuesto.Impuesto.Clave,//,@id_impuesto smallint
                    FleteImpuesto.TipoFactor.Clave,//,@id_tipoFactor smallint
                    FleteImpuesto.Base,//,@base decimal(18,3)
                    FleteImpuesto.TasaCuota,//,@tasaCuota decimal(18,6)
                    FleteImpuesto.Importe,//,@importe money
                    FleteImpuesto.Usuario//,@id_usuario char(36)
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(FleteImpuesto.Conexion, "spCSLDB_Flete_ac_ImpuestoProductoServicio", parametros);
                while (dr.Read())
                {
                    FleteImpuesto.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    FleteImpuesto.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return FleteImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Tipos deduccion
        public List<CatTipoClasificacionCobroModels> GetTiposDeduccion(EventoFleteModels EventoFlete)
        {
            try
            {
                CatTipoClasificacionCobroModels item;
                List<CatTipoClasificacionCobroModels> lista = new List<CatTipoClasificacionCobroModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(EventoFlete.Conexion, "spCSLDB_Combo_get_CatTipoClasificacionCobro");
                while (dr.Read())
                {
                    item = new CatTipoClasificacionCobroModels();

                    item.Id_tipoClasificacionCobro = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0;
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
        #endregion
        #region Tipos eventos
        public List<CatTipoEventoEnvioModels> GetTiposEventos(EventoFleteModels EventoFlete)
        {
            try
            {
                CatTipoEventoEnvioModels item;
                List<CatTipoEventoEnvioModels> lista = new List<CatTipoEventoEnvioModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(EventoFlete.Conexion, "spCSLDB_Combo_get_CatTipoEventoEnvio");
                while (dr.Read())
                {
                    item = new CatTipoEventoEnvioModels();

                    item.IDTipoEventoEnvio = !dr.IsDBNull(dr.GetOrdinal("idTipoEvento")) ? dr.GetInt32(dr.GetOrdinal("idTipoEvento")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("nombreEvento")) ? dr.GetString(dr.GetOrdinal("nombreEvento")) : string.Empty;
                    item.MarcarMerma = !dr.IsDBNull(dr.GetOrdinal("marcarMerma")) ? dr.GetBoolean(dr.GetOrdinal("marcarMerma")) : false;

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
        #region FleteEvento
        public EventoFleteModels GetFleteEvento(EventoFleteModels EventoFlete)
        {
            object[] parametros =
            {
                EventoFlete.Id_flete,
                EventoFlete.Id_eventoFlete
            };
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(EventoFlete.Conexion, "spCSLDB_Flete_get_FleteEventoXIDFlete", parametros);

            while (dr.Read())
            {
                EventoFlete.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;

                if (EventoFlete.RespuestaAjax.Success)
                {
                    EventoFlete.Id_eventoFlete = !dr.IsDBNull(dr.GetOrdinal("id_eventoFlete")) ? dr.GetInt32(dr.GetOrdinal("id_eventoFlete")) : 0;
                    EventoFlete.Id_documentoPorCobrarDetalle = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrarDetalle")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrarDetalle")) : string.Empty;
                    EventoFlete.Id_tipoEvento = !dr.IsDBNull(dr.GetOrdinal("id_tipoEvento")) ? dr.GetInt32(dr.GetOrdinal("id_tipoEvento")) : 0;
                    EventoFlete.Cantidad = !dr.IsDBNull(dr.GetOrdinal("cantidad")) ? dr.GetInt32(dr.GetOrdinal("cantidad")) : 0;
                    EventoFlete.Lugar = !dr.IsDBNull(dr.GetOrdinal("lugar")) ? dr.GetString(dr.GetOrdinal("lugar")) : string.Empty;
                    EventoFlete.FechaDeteccion = !dr.IsDBNull(dr.GetOrdinal("fechaDeteccion")) ? dr.GetDateTime(dr.GetOrdinal("fechaDeteccion")) : DateTime.Now;
                    EventoFlete.HoraDeteccion = !dr.IsDBNull(dr.GetOrdinal("horaDeteccion")) ? dr.GetTimeSpan(dr.GetOrdinal("horaDeteccion")) : DateTime.Now.TimeOfDay;
                    EventoFlete.Observacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;
                    EventoFlete.ImagenBase64 = !dr.IsDBNull(dr.GetOrdinal("imagenBase64")) ? dr.GetString(dr.GetOrdinal("imagenBase64")) : string.Empty;
                    EventoFlete.MontoDeduccion = !dr.IsDBNull(dr.GetOrdinal("deduccion")) ? dr.GetDecimal(dr.GetOrdinal("deduccion")) : 0;
                    EventoFlete.Id_TipoDeDeduccion = !dr.IsDBNull(dr.GetOrdinal("id_tipoDeduccion")) ? dr.GetInt32(dr.GetOrdinal("id_tipoDeduccion")) : 0;
                    EventoFlete.Id_conceptoDocumento = !dr.IsDBNull(dr.GetOrdinal("id_conceptoDocumento")) ? dr.GetInt32(dr.GetOrdinal("id_conceptoDocumento")) : 0;
                }
                else
                {
                    EventoFlete.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
            }
            dr.Close();
            return EventoFlete;
        }
        #endregion
        #region Vista cobro
        public DocumentosPorCobrarDetallePagosModels GetDetalleDocumentoPago(DocumentosPorCobrarDetallePagosModels DocumentoPago)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPago.Id_padre,
                    DocumentoPago.Id_documentoPorCobrarDetallePagos
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPago.Conexion, "spCSLDB_Flete_get_GetDetalleDocumentoPago", parametros);
                while (dr.Read())
                {
                    DocumentoPago.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    if(DocumentoPago.RespuestaAjax.Success)
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

                        DocumentoPago.ImagenBase64 = Auxiliar.ValidImageFormServer(DocumentoPago.ImagenBase64,
                            ProjectSettings.BaseDirDocumentoPorCobrarPagoBancarizado);
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
        public DocumentosPorCobrarDetallePagosModels GetNombreEmpresaProveedorCliente(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarDetallePagos)
        {
            try
            {
                object[] parametros =
                {
                    DocumentoPorCobrarDetallePagos.Id_padre
                };
                SqlDataReader dr = null;
                string nombre = string.Empty;

                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_Flete_get_NombreProveedorCliente_Empresa", parametros);
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
        
        public List<CuentaBancariaModels> GetListadoCuentasBancarias(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarDetallePagos)
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
                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_Flete_get_CuentasBancarias", parametros);
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

        #region Get
        #region Get AC_Flete
        public FleteModels Flete_get_ACFlete(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                     Flete.id_flete
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_ACFlete", parametros);

                while (dr.Read())
                {
                    Flete.Empresa.IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("id_empresa")) ? dr.GetString(dr.GetOrdinal("id_empresa")) : string.Empty;//f.id_empresa
                    Flete.Cliente.IDCliente = !dr.IsDBNull(dr.GetOrdinal("id_cliente")) ? dr.GetString(dr.GetOrdinal("id_cliente")) : string.Empty;//,f.id_cliente
                    Flete.Vehiculo.IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("id_vehiculo")) ? dr.GetString(dr.GetOrdinal("id_vehiculo")) : string.Empty;//,f.id_vehiculo
                    Flete.Chofer.IDChofer = !dr.IsDBNull(dr.GetOrdinal("id_chofer")) ? dr.GetString(dr.GetOrdinal("id_chofer")) : string.Empty;//,f.id_chofer
                    //Flete.Jaula.IDJaula = !dr.IsDBNull(dr.GetOrdinal("id_jaula")) ? dr.GetString(dr.GetOrdinal("id_jaula")) : string.Empty;//,f.id_jaula
                    //Flete.Remolque.IDRemolque = !dr.IsDBNull(dr.GetOrdinal("id_remolque")) ? dr.GetString(dr.GetOrdinal("id_remolque")) : string.Empty;//,f.id_remolque
                    //Flete.kmInicialVehiculo = !dr.IsDBNull(dr.GetOrdinal("kmInicialVehiculo")) ? dr.GetInt32(dr.GetOrdinal("kmInicialVehiculo")) : 0;//,f.kmInicialVehiculo
                    Flete.FechaTentativaEntrega = !dr.IsDBNull(dr.GetOrdinal("fechaTentativaEntrega")) ? dr.GetDateTime(dr.GetOrdinal("fechaTentativaEntrega")) : DateTime.Now;//,f.fechaTentativaEntrega
                    Flete.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;//,f.folio
                    Flete.Trayecto.id_trayecto = !dr.IsDBNull(dr.GetOrdinal("id_trayecto")) ? dr.GetString(dr.GetOrdinal("id_trayecto")) : string.Empty;//,t.id_trayecto
                    Flete.Trayecto.Destinatario.IDCliente = !dr.IsDBNull(dr.GetOrdinal("id_destinatario")) ? dr.GetString(dr.GetOrdinal("id_destinatario")) : string.Empty;//,t.id_destinatario
                    Flete.Trayecto.Remitente.IDCliente = !dr.IsDBNull(dr.GetOrdinal("id_remitente")) ? dr.GetString(dr.GetOrdinal("id_remitente")) : string.Empty;//,t.id_remitente
                    Flete.Trayecto.LugarOrigen.id_lugar = !dr.IsDBNull(dr.GetOrdinal("id_lugarOrigen")) ? dr.GetString(dr.GetOrdinal("id_lugarOrigen")) : string.Empty;//,t.id_lugarOrigen
                    Flete.Trayecto.LugarOrigen.Direccion = !dr.IsDBNull(dr.GetOrdinal("direccionLugarOrigen")) ? dr.GetString(dr.GetOrdinal("direccionLugarOrigen")) : string.Empty;//,(select direccion from dbo.tbl_CatLugar where id_lugar = t.id_lugarOrigen) as direccionLugarOrigen
                    Flete.Trayecto.LugarOrigen.descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcionLugarOrigen")) ? dr.GetString(dr.GetOrdinal("descripcionLugarOrigen")) : string.Empty;//,(select descripcion from dbo.tbl_CatLugar where id_lugar = t.id_lugarOrigen) as descripcionLugarOrigen
                    Flete.Trayecto.LugarDestino.id_lugar = !dr.IsDBNull(dr.GetOrdinal("id_lugarDestino")) ? dr.GetString(dr.GetOrdinal("id_lugarDestino")) : string.Empty;//,t.id_lugarDestino
                    Flete.Trayecto.LugarDestino.Direccion = !dr.IsDBNull(dr.GetOrdinal("direccionLugarDestino")) ? dr.GetString(dr.GetOrdinal("direccionLugarDestino")) : string.Empty;//,(select direccion from dbo.tbl_CatLugar where id_lugar = t.id_lugarDestino) as direccionLugarDestino
                    Flete.Trayecto.LugarDestino.descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcionLugarDestino")) ? dr.GetString(dr.GetOrdinal("descripcionLugarDestino")) : string.Empty;//,(select descripcion from dbo.tbl_CatLugar where id_lugar = t.id_lugarDestino) as descripcionLugarDestino
                    Flete.Cliente.RFC = !dr.IsDBNull(dr.GetOrdinal("rfc")) ? dr.GetString(dr.GetOrdinal("rfc")) : string.Empty;//,c.rfc
                    Flete.precioFlete = !dr.IsDBNull(dr.GetOrdinal("precioFlete")) ? dr.GetDecimal(dr.GetOrdinal("precioFlete")) : 0; //,f.precioFlete
                    Flete.TotalImpuestoRetenido = !dr.IsDBNull(dr.GetOrdinal("totalImpuestoRetenido")) ? dr.GetDecimal(dr.GetOrdinal("totalImpuestoRetenido")) : 0;//,f.totalImpuestoRetenido
                    Flete.TotalImpuestoTrasladado = !dr.IsDBNull(dr.GetOrdinal("totalImpuestoTrasladado")) ? dr.GetDecimal(dr.GetOrdinal("totalImpuestoTrasladado")) : 0;//,f.totalImpuestoTrasladado
                    Flete.TotalFlete = !dr.IsDBNull(dr.GetOrdinal("totalFlete")) ? dr.GetDecimal(dr.GetOrdinal("totalFlete")) : 0;//,f.totalFlete
                    Flete.CondicionPago = !dr.IsDBNull(dr.GetOrdinal("condicionPago")) ? dr.GetString(dr.GetOrdinal("condicionPago")) : string.Empty;//,f.condicionPago
                    Flete.MetodoPago.Clave = !dr.IsDBNull(dr.GetOrdinal("id_metodoPago")) ? dr.GetString(dr.GetOrdinal("id_metodoPago")) : string.Empty;//,f.id_metodoPago
                    Flete.FormaPago.Clave = !dr.IsDBNull(dr.GetOrdinal("id_formaPago")) ? dr.GetInt16(dr.GetOrdinal("id_formaPago")) : 0;//,f.id_formaPago
                    Flete.Id_sucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                }
                dr.Close();
                return Flete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Get get_Recepcion
        public FleteModels Flete_get_Recepcion(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                     Flete.id_flete
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_Recepcion", parametros);

                while (dr.Read())
                {
                    Flete.RecepcionOrigen.KilometrajeFinal = !dr.IsDBNull(dr.GetOrdinal("kilometrajeFinal")) ? dr.GetInt32(dr.GetOrdinal("kilometrajeFinal")) : 0;
                    Flete.RecepcionOrigen.HoraLlegada = !dr.IsDBNull(dr.GetOrdinal("horaLlegada")) ? dr.GetTimeSpan(dr.GetOrdinal("horaLlegada")) : DateTime.Now.TimeOfDay;
                    Flete.RecepcionOrigen.FechaLlegada = !dr.IsDBNull(dr.GetOrdinal("fechaLlegada")) ? dr.GetDateTime(dr.GetOrdinal("fechaLlegada")) : DateTime.Now;
                    Flete.RecepcionOrigen.Observacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;

                    Flete.RecepcionDestino.kiloTotalRecibido = !dr.IsDBNull(dr.GetOrdinal("kiloTotalRecibido")) ? dr.GetDecimal(dr.GetOrdinal("kiloTotalRecibido")) : 0;
                    Flete.RecepcionDestino.GanadosTotal = !dr.IsDBNull(dr.GetOrdinal("ganadoTotal")) ? dr.GetInt32(dr.GetOrdinal("ganadoTotal")) : 0;
                    Flete.RecepcionDestino.fechaLlegada = !dr.IsDBNull(dr.GetOrdinal("fechaLlegadaDestino")) ? dr.GetDateTime(dr.GetOrdinal("fechaLlegadaDestino")) : DateTime.Now;
                    Flete.RecepcionDestino.HoraLlegada = !dr.IsDBNull(dr.GetOrdinal("horaLlegadaDestino")) ? dr.GetTimeSpan(dr.GetOrdinal("horaLlegadaDestino")) : DateTime.Now.TimeOfDay;
                    Flete.RecepcionDestino.HoraDescarga = !dr.IsDBNull(dr.GetOrdinal("horaDescargaDestino")) ? dr.GetTimeSpan(dr.GetOrdinal("horaDescargaDestino")) : DateTime.Now.TimeOfDay;
                    Flete.RecepcionDestino.recibidoPor = !dr.IsDBNull(dr.GetOrdinal("recibidoPor")) ? dr.GetString(dr.GetOrdinal("recibidoPor")) : string.Empty;
                    Flete.RecepcionDestino.ValijaSellada = !dr.IsDBNull(dr.GetOrdinal("valijaSellada")) ? dr.GetBoolean(dr.GetOrdinal("valijaSellada")) : false;
                    Flete.RecepcionDestino.RecepcionDocumentos = !dr.IsDBNull(dr.GetOrdinal("recepcionDocumentos")) ? dr.GetBoolean(dr.GetOrdinal("recepcionDocumentos")) : false;
                    Flete.RecepcionDestino.observacion = !dr.IsDBNull(dr.GetOrdinal("observacionDestino")) ? dr.GetString(dr.GetOrdinal("observacionDestino")) : string.Empty;

                    Flete.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;
                }
                dr.Close();
                return Flete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Get EstatusFleteXIDFlete
        public FleteModels GetEstatusFleteXIDFlete(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                     Flete.id_flete
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_EstatusFleteXIDFlete", parametros);

                while (dr.Read())
                {
                    Flete.Estatus = !dr.IsDBNull(dr.GetOrdinal("estatus")) ? dr.GetInt16(dr.GetOrdinal("estatus")) : 0;//,(select descripcion from dbo.tbl_CatLugar where id_lugar = t.id_lugarDestino) as descripcionLugarDestino
                }
                dr.Close();
                return Flete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Get DocumentoXIDDocumeto
        public Flete_TipoDocumentoModels GetDocumentoXIDDocumento(Flete_TipoDocumentoModels Flete_Tipo)
        {
            try
            {
                object[] parametros =
                {
                     Flete_Tipo.IDDocumento
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete_Tipo.Conexion, "spCSLDB_Flete_get_DocumentoXIDDocumento", parametros);

                while (dr.Read())
                {
                    Flete_Tipo.IDTipoDocumento = !dr.IsDBNull(dr.GetOrdinal("id_tipoDocumento")) ? dr.GetInt16(dr.GetOrdinal("id_tipoDocumento")) : 0;
                    Flete_Tipo.Clave = !dr.IsDBNull(dr.GetOrdinal("clave")) ? dr.GetString(dr.GetOrdinal("clave")) : string.Empty;
                    //Solo para mostrar
                    Flete_Tipo.Imagen = !dr.IsDBNull(dr.GetOrdinal("imagen")) ? dr.GetString(dr.GetOrdinal("imagen")) : string.Empty;
                }
                if (string.IsNullOrEmpty(Flete_Tipo.Imagen))
                {
                    //No hay imagen en el server
                    Flete_Tipo.MostrarImagen = Auxiliar.SetDefaultImage();
                    Flete_Tipo.FlagImg = false;
                }
                else
                {
                    //Guardamos el string de la imagen
                    Flete_Tipo.MostrarImagen = Flete_Tipo.Imagen;
                    Flete_Tipo.FlagImg = true;
                }

                Flete_Tipo.ExtensionImagenBase64 = Auxiliar.ObtenerExtensionImagenBase64(Flete_Tipo.MostrarImagen);
                dr.Close();
                return Flete_Tipo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Get EventoXIDEvento
        public EventoEnvioModels GetEventoXIDEventoXIDFlete(EventoEnvioModels Evento)
        {
            try
            {
                object[] parametros =
                {
                     Evento.IDEvento,
                     Evento.IDEnvio
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Flete_get_EventoXIDEventoXIDFlete", parametros);

                while (dr.Read())
                {
                    Evento.IDEvento = !dr.IsDBNull(dr.GetOrdinal("id_evento")) ? dr.GetInt32(dr.GetOrdinal("id_evento")) : 0;
                    Evento.IDEnvio = !dr.IsDBNull(dr.GetOrdinal("id_flete")) ? dr.GetString(dr.GetOrdinal("id_flete")) : string.Empty;
                    Evento.IDTipoEvento = !dr.IsDBNull(dr.GetOrdinal("id_tipoEvento")) ? dr.GetInt32(dr.GetOrdinal("id_tipoEvento")) : 0;
                    Evento.Cantidad = !dr.IsDBNull(dr.GetOrdinal("cantidad")) ? dr.GetInt32(dr.GetOrdinal("cantidad")) : 0;
                    Evento.Lugar = !dr.IsDBNull(dr.GetOrdinal("lugar")) ? dr.GetString(dr.GetOrdinal("lugar")) : string.Empty;
                    Evento.FechaDeteccion = !dr.IsDBNull(dr.GetOrdinal("fechaDeteccion")) ? dr.GetDateTime(dr.GetOrdinal("fechaDeteccion")) : DateTime.Today;
                    Evento.HoraDetecccion = !dr.IsDBNull(dr.GetOrdinal("horaDeteccion")) ? dr.GetTimeSpan(dr.GetOrdinal("horaDeteccion")) : DateTime.Now.TimeOfDay;
                    Evento.Observacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;
                    Evento.ImagenBase64 = !dr.IsDBNull(dr.GetOrdinal("imagenBase64")) ? dr.GetString(dr.GetOrdinal("imagenBase64")) : string.Empty;
                }
                dr.Close();
                return Evento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region Combos
        public List<CatClienteModels> GetListadoClientes(FleteModels Flete)
        {

            try
            {
                CatClienteModels Cliente;
                Flete.Cliente.ListaClientes = new List<CatClienteModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_AllClienteProveedor");
                while (dr.Read())
                {
                    Cliente = new CatClienteModels
                    {
                        IDCliente = !dr.IsDBNull(dr.GetOrdinal("IDCliente")) ? dr.GetString(dr.GetOrdinal("IDCliente")) : string.Empty,
                        NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("NombreCliente")) ? dr.GetString(dr.GetOrdinal("NombreCliente")) : string.Empty,
                        RFC = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty,
                    };

                    Flete.Cliente.ListaClientes.Add(Cliente);
                }
                return Flete.Cliente.ListaClientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatEmpresaModels> GetListadoEmpresas(FleteModels Flete)
        {

            try
            {
                CatEmpresaModels Empresa;
                Flete.Empresa.ListaEmpresas = new List<CatEmpresaModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatEmpresa");
                while (dr.Read())
                {
                    Empresa = new CatEmpresaModels
                    {
                        IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("IDEmpresa")) ? dr.GetString(dr.GetOrdinal("IDEmpresa")) : string.Empty,
                        RazonFiscal = !dr.IsDBNull(dr.GetOrdinal("NombreEmpresa")) ? dr.GetString(dr.GetOrdinal("NombreEmpresa")) : string.Empty
                    };

                    Flete.Empresa.ListaEmpresas.Add(Empresa);
                }
                dr.Close();
                return Flete.Empresa.ListaEmpresas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatChoferModels> GetListadoChoferes(FleteModels Flete)
        {

            try
            {
                CatChoferModels Chofer;
                Flete.Chofer.ListaChoferes = new List<CatChoferModels>();

                SqlDataReader dr = null;
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa ,
                    Flete.Id_sucursal
                };
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatChoferesXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Chofer = new CatChoferModels
                    {
                        IDChofer = !dr.IsDBNull(dr.GetOrdinal("IDChofer")) ? dr.GetString(dr.GetOrdinal("IDChofer")) : string.Empty,
                        Nombre = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty
                    };

                    Flete.Chofer.ListaChoferes.Add(Chofer);
                }
                dr.Close();
                return Flete.Chofer.ListaChoferes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatVehiculoModels> GetListadoVehiculos(FleteModels Flete)
        {

            try
            {
                CatVehiculoModels Vehiculo;
                Flete.Vehiculo.listaVehiculos = new List<CatVehiculoModels>();

                SqlDataReader dr = null;
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa ,
                    Flete.Id_sucursal
                };
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatVehiculosXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Vehiculo = new CatVehiculoModels
                    {
                        IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("IDVehiculo")) ? dr.GetString(dr.GetOrdinal("IDVehiculo")) : string.Empty,
                        nombreMarca = !dr.IsDBNull(dr.GetOrdinal("NombreVehiculo")) ? dr.GetString(dr.GetOrdinal("NombreVehiculo")) : string.Empty,
                        nombreTipoVehiculo = !dr.IsDBNull(dr.GetOrdinal("Tipo")) ? dr.GetString(dr.GetOrdinal("Tipo")) : string.Empty,
                    };

                    Flete.Vehiculo.listaVehiculos.Add(Vehiculo);
                }
                dr.Close();
                return Flete.Vehiculo.listaVehiculos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatJaulaModels> GetListadoJaulas(FleteModels Flete)
        {

            try
            {
                CatJaulaModels Jaula;
                Flete.Jaula.listaJaulas = new List<CatJaulaModels>();

                SqlDataReader dr = null;
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa
                };
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatJaulaXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Jaula = new CatJaulaModels
                    {
                        IDJaula = !dr.IsDBNull(dr.GetOrdinal("IDJaula")) ? dr.GetString(dr.GetOrdinal("IDJaula")) : string.Empty,
                        Matricula = !dr.IsDBNull(dr.GetOrdinal("Placa")) ? dr.GetString(dr.GetOrdinal("Placa")) : string.Empty,
                    };

                    Flete.Jaula.listaJaulas.Add(Jaula);
                }
                dr.Close();
                return Flete.Jaula.listaJaulas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatRemolqueModels> GetListadoRemolque(FleteModels Flete)
        {

            try
            {
                CatRemolqueModels Remolque;
                Flete.Remolque.listaRemolque = new List<CatRemolqueModels>();

                SqlDataReader dr = null;
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa
                };
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatRemolqueXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Remolque = new CatRemolqueModels
                    {
                        IDRemolque = !dr.IsDBNull(dr.GetOrdinal("IDRemolque")) ? dr.GetString(dr.GetOrdinal("IDRemolque")) : string.Empty,
                        placa = !dr.IsDBNull(dr.GetOrdinal("Placa")) ? dr.GetString(dr.GetOrdinal("Placa")) : string.Empty,
                    };

                    Flete.Remolque.listaRemolque.Add(Remolque);
                }
                dr.Close();
                return Flete.Remolque.listaRemolque;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatLugarModels> GetListadoLugaresXIDProveedorIDCliente(FleteModels Flete, string id)
        {
            try
            {
                CatLugarModels Lugar;
                List<CatLugarModels> ListaLugares = new List<CatLugarModels>();

                SqlDataReader dr = null;
                object[] parametros =
                {
                    id
                };
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatLugarXIDProveedorIDCliente", parametros);
                while (dr.Read())
                {
                    Lugar = new CatLugarModels
                    {
                        id_lugar = !dr.IsDBNull(dr.GetOrdinal("IDLugar")) ? dr.GetString(dr.GetOrdinal("IDLugar")) : string.Empty,
                        descripcion = !dr.IsDBNull(dr.GetOrdinal("NombreLugar")) ? dr.GetString(dr.GetOrdinal("NombreLugar")) : string.Empty,
                        latitud = float.Parse(dr["GpsLatitud"].ToString()),
                        longitud = float.Parse(dr["GpsLongitud"].ToString()),
                        Direccion = !dr.IsDBNull(dr.GetOrdinal("Direccion")) ? dr.GetString(dr.GetOrdinal("Direccion")) : string.Empty,
                    };

                    ListaLugares.Add(Lugar);
                }
                dr.Close();
                return ListaLugares;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CFDI_FormaPagoModels> GetListadoFormaPagos(FleteModels Flete)
        {

            try
            {
                CFDI_FormaPagoModels FormaPago;
                Flete.ListaFormaPago = new List<CFDI_FormaPagoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CFDIFormaPago");
                while (dr.Read())
                {
                    FormaPago = new CFDI_FormaPagoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    Flete.ListaFormaPago.Add(FormaPago);
                }
                dr.Close();
                return Flete.ListaFormaPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CFDI_MetodoPagoModels> GetListadoMetodoPago(FleteModels Flete)
        {

            try
            {
                CFDI_MetodoPagoModels MetodoPago;
                Flete.ListaMetodoPago = new List<CFDI_MetodoPagoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CFDIMetodoPago");
                while (dr.Read())
                {
                    MetodoPago = new CFDI_MetodoPagoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetString(dr.GetOrdinal("ID")) : string.Empty,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    Flete.ListaMetodoPago.Add(MetodoPago);
                }
                dr.Close();
                return Flete.ListaMetodoPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatChoferModels> GetChoferesXIDEmpresa(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa , 
                    Flete.Id_sucursal
                };
                CatChoferModels Chofer;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatChoferesXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Chofer = new CatChoferModels
                    {
                        IDChofer = !dr.IsDBNull(dr.GetOrdinal("IDChofer")) ? dr.GetString(dr.GetOrdinal("IDChofer")) : string.Empty,
                        Nombre = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty,
                    };

                    Flete.Chofer.ListaChoferes.Add(Chofer);
                }
                dr.Close();
                return Flete.Chofer.ListaChoferes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatVehiculoModels> GetVehiculosXIDEmpresa(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa ,
                    Flete.Id_sucursal
                };
                CatVehiculoModels Vehiculo;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatVehiculosXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Vehiculo = new CatVehiculoModels
                    {
                        IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("IDVehiculo")) ? dr.GetString(dr.GetOrdinal("IDVehiculo")) : string.Empty,
                        nombreMarca = !dr.IsDBNull(dr.GetOrdinal("NombreVehiculo")) ? dr.GetString(dr.GetOrdinal("NombreVehiculo")) : string.Empty,
                        Modelo = !dr.IsDBNull(dr.GetOrdinal("Tipo")) ? dr.GetString(dr.GetOrdinal("Tipo")) : string.Empty
                    };

                    Flete.Vehiculo.listaVehiculos.Add(Vehiculo);
                }
                dr.Close();
                return Flete.Vehiculo.listaVehiculos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatJaulaModels> GetJaulasXIDEmpresa(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa
                };
                CatJaulaModels Jaula;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatJaulaXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Jaula = new CatJaulaModels
                    {
                        IDJaula = !dr.IsDBNull(dr.GetOrdinal("IDJaula")) ? dr.GetString(dr.GetOrdinal("IDJaula")) : string.Empty,
                        Matricula = !dr.IsDBNull(dr.GetOrdinal("Placa")) ? dr.GetString(dr.GetOrdinal("Placa")) : string.Empty,
                    };

                    Flete.Jaula.listaJaulas.Add(Jaula);
                }
                dr.Close();
                return Flete.Jaula.listaJaulas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatRemolqueModels> GetRemolquesXIDEmpresa(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                    Flete.Empresa.IDEmpresa
                };
                CatRemolqueModels Remolque;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatRemolqueXIDEmpresa", parametros);
                while (dr.Read())
                {
                    Remolque = new CatRemolqueModels
                    {
                        IDRemolque = !dr.IsDBNull(dr.GetOrdinal("IDRemolque")) ? dr.GetString(dr.GetOrdinal("IDRemolque")) : string.Empty,
                        placa = !dr.IsDBNull(dr.GetOrdinal("Placa")) ? dr.GetString(dr.GetOrdinal("Placa")) : string.Empty,
                    };

                    Flete.Remolque.listaRemolque.Add(Remolque);
                }
                dr.Close();
                return Flete.Remolque.listaRemolque;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatTipoDocumentoModels> GetListaTiposDocumentos(Flete_TipoDocumentoModels Flete)
        {
            try
            {
                CatTipoDocumentoModels TipoDocumento;
                List<CatTipoDocumentoModels> ListaTipoDocumentos = new List<CatTipoDocumentoModels>();
                object[] parametro =
                {
                    Flete.IDFlete
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_CatTipoDocumentoXIDFlete", parametro);
                while (dr.Read())
                {
                    TipoDocumento = new CatTipoDocumentoModels
                    {
                        IDTipoDocumento = !dr.IsDBNull(dr.GetOrdinal("id_tipoDocumento")) ? dr.GetInt16(dr.GetOrdinal("id_tipoDocumento")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    ListaTipoDocumentos.Add(TipoDocumento);
                }
                dr.Close();
                return ListaTipoDocumentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatTipoEventoEnvioModels> GetListaTiposEventos(EventoEnvioModels Evento)
        {
            try
            {
                CatTipoEventoEnvioModels TipoEvento;
                List<CatTipoEventoEnvioModels> ListaTiposEventos = new List<CatTipoEventoEnvioModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Combo_get_CatTipoEventoEnvio");
                while (dr.Read())
                {
                    TipoEvento = new CatTipoEventoEnvioModels
                    {
                        IDTipoEventoEnvio = !dr.IsDBNull(dr.GetOrdinal("idTipoEvento")) ? dr.GetInt32(dr.GetOrdinal("idTipoEvento")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("nombreEvento")) ? dr.GetString(dr.GetOrdinal("nombreEvento")) : string.Empty,
                    };

                    ListaTiposEventos.Add(TipoEvento);
                }
                dr.Close();
                return ListaTiposEventos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
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
        #region Tipo de clasificacion pago
        public List<CatTipoClasificacionModels> GetListadoTipoClasificacionPago(DocumentoModels Documento)
        {
            try
            {
                CatTipoClasificacionModels item;
                List<CatTipoClasificacionModels> lista = new List<CatTipoClasificacionModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Combo_get_CatTipoClasificacionAll");
                while (dr.Read())
                {
                    item = new CatTipoClasificacionModels
                    {
                        IDTipoClasificacionGasto = !dr.IsDBNull(dr.GetOrdinal("IDTipoClasificacion")) ? dr.GetInt32(dr.GetOrdinal("IDTipoClasificacion")) : 0,
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
        #region Sucursales
        public List<CatSucursalesModels> GetListadoSucursales(FleteModels Flete)
        {
            List<CatSucursalesModels> ListaSucursales = new List<CatSucursalesModels>();
            CatSucursalesModels Sucursal;
            SqlDataReader dr = null;
            object[] parametros =
            {
                1
            };

            dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Combo_get_SucursalesXTipoEmpresa", parametros);

            while (dr.Read())
            {
                Sucursal = new CatSucursalesModels
                {
                    IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty,
                    NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty,
                };

                ListaSucursales.Add(Sucursal);
            }
            dr.Close();
            return ListaSucursales;
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
        #region Almacenes con producto habilitado para generar un detalle del documento por cobrar
        public List<CatAlmacenModels> GetAlmacenesHabilitados(DocumentosPorCobrarDetalleModels DocumentoPorCobrarDetallePagos)
        {
            try
            {
                SqlDataReader dr = null;
                List<CatAlmacenModels> lista = new List<CatAlmacenModels>();
                CatAlmacenModels item;
                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_Combo_get_Almacenes_DocumentoDetalleCobro");
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
        #region Almacenes con producto habilitado para cobrar
        public List<CatProductosAlmacenModels> GetProductosAlmacen(DocumentosPorCobrarDetalleModels Documento)
        {
            try
            {
                object[] parametro =
                {
                    Documento.Id_almacen
                };
                SqlDataReader dr = null;
                List<CatProductosAlmacenModels> lista = new List<CatProductosAlmacenModels>();
                CatProductosAlmacenModels item;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_combos_get_ProductosAlmacen_DocumentoPorCobrarDetalle", parametro);
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

        #region Funciones AC
        #region AC_Cliente
        public RespuestaAjax Flete_ac_FleteCliente(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                     Flete.id_flete
                    ,Flete.Empresa.IDEmpresa
                    ,Flete.Cliente.IDCliente
                    ,Flete.Vehiculo.IDVehiculo
                    ,Flete.Chofer.IDChofer
                    ,Flete.kmInicialVehiculo
                    ,Flete.FechaTentativaEntrega
                    ,Flete.Usuario
                    ,Flete.Id_sucursal
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_ac_FleteCliente", parametros);
                RespuestaAjax RespuestaAjax = new RespuestaAjax();

                while (dr.Read())
                {
                    RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return RespuestaAjax;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Flete_ac_FleteTrayecto
        public FleteModels Flete_ac_FleteTrayecto(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                     Flete.Trayecto.id_trayecto
                    ,Flete.id_flete
                    ,Flete.Trayecto.Remitente.IDCliente
                    ,Flete.Trayecto.LugarOrigen.id_lugar
                    ,Flete.Trayecto.Destinatario.IDCliente
                    ,Flete.Trayecto.LugarDestino.id_lugar
                    ,Flete.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_ac_FleteTrayecto", parametros);

                while (dr.Read())
                {
                    Flete.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Flete.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Flete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region A_Cobro
        public RespuestaAjax Flete_ac_FleteCobro(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                     Flete.id_flete
                    ,Flete.precioFlete
                    ,Flete.CondicionPago
                    ,Flete.MetodoPago.Clave
                    ,Flete.FormaPago.Clave
                    ,Flete.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_ac_FleteCobro", parametros);
                RespuestaAjax RespuestaAjax = new RespuestaAjax();

                while (dr.Read())
                {
                    RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return RespuestaAjax;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region A_EstatusFleteXIDFlete
        public FleteModels Flete_a_CambiarEstatusFleteXIDFlete(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                     Flete.id_flete
                    ,Flete.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_a_CambiarEstatusFleteXIDFlete", parametros);

                while (dr.Read())
                {
                    Flete.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Flete.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Flete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region Producto Ganado Externo
        public Flete_ProductoModels AC_ProductoGanadoExterno(Flete_ProductoModels ganado)
        {
            try
            {
                object[] parametros =
                {
                    ganado.ID_Producto,
                    ganado.ID_Flete,
                    ganado.NumArete,
                    ganado.Genero,
                    ganado.PesoAproximado,
                    ganado.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(ganado.Conexion, "spCSLDB_Flete_ac_ProductoGanadoExterno", parametros);

                while (dr.Read())
                {
                    ganado.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    ganado.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                ganado.RespuestaAjax.Mensaje = ex.ToString();
                ganado.RespuestaAjax.Success = false;
            }

            return ganado;
        }
        public Flete_ProductoModels DEL_ProductoGanadoExterno(Flete_ProductoModels ganado)
        {
            try
            {
                object[] parametros =
                {
                    ganado.ID_Producto,
                    ganado.ID_Flete,
                    ganado.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(ganado.Conexion, "spCSLDB_Flete_del_ProductoGanadoExterno", parametros);

                while (dr.Read())
                {
                    ganado.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    ganado.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                ganado.RespuestaAjax.Mensaje = ex.ToString();
                ganado.RespuestaAjax.Success = false;
            }

            return ganado;
        }
        #endregion
        #region Evento
        public RespuestaAjax AC_Evento(EventoFleteModels Evento)
        {
            try
            {
                object[] parametros =
                {
                    Evento.Id_eventoFlete,      Evento.Id_flete,
                    Evento.Id_tipoEvento,       
                    Evento.Cantidad,
                    Evento.Lugar,               Evento.FechaDeteccion,
                    Evento.HoraDeteccion,       Evento.Observacion,
                    Evento.ImagenBase64,        Evento.ListaIDGanadosDelEvento,
                    Evento.Usuario,             Evento.Id_conceptoDocumento,
                    Evento.MontoDeduccion,      Evento.Id_TipoDeDeduccion
                };

                RespuestaAjax RespuestaAjax = new RespuestaAjax();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Flete_ac_Evento", parametros);
                while (dr.Read())
                {
                    RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje")) ? dr.GetString(dr.GetOrdinal("Mensaje")) : string.Empty;
                }
                dr.Close();
                return RespuestaAjax;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public EventoEnvioModels DEL_Evento(EventoEnvioModels Evento)
        {
            try
            {
                object[] parametros =
                {
                    Evento.IDEvento,
                    Evento.IDEnvio,
                    Evento.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_Flete_del_Evento", parametros);

                while (dr.Read())
                {
                    Evento.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Evento.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Evento.RespuestaAjax.Mensaje = ex.ToString();
                Evento.RespuestaAjax.Success = false;
            }

            return Evento;
        }
        #endregion
        #region Recepcion Destino
        public FleteModels AC_RecepcionDestino(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                   Flete.RecepcionDestino.IDFlete,                 Flete.RecepcionDestino.kiloTotalRecibido,
                   Flete.RecepcionDestino.GanadosTotal,         Flete.RecepcionDestino.fechaLlegada,            Flete.RecepcionDestino.HoraLlegada,
                   Flete.RecepcionDestino.HoraDescarga,         Flete.RecepcionDestino.recibidoPor,             Flete.RecepcionDestino.ValijaSellada,        Flete.RecepcionDestino.RecepcionDocumentos,     Flete.RecepcionDestino.observacion,
                   Flete.RecepcionDestino.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.RecepcionDestino.Conexion, "spCSLDB_Flete_ac_RecepcionDestino", parametros);
                Flete.RecepcionDestino.RespuestaAjax = new RespuestaAjax();
                while (dr.Read())
                {
                    Flete.RecepcionDestino.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Flete.RecepcionDestino.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Flete.RecepcionDestino.RespuestaAjax.Mensaje = ex.ToString();
                Flete.RecepcionDestino.RespuestaAjax.Success = false;
            }

            return Flete;
        }
        #endregion
        #region Recepcion Origen
        public FleteModels AC_RecepcionOrigen(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                   Flete.RecepcionOrigen.IDFlete,             Flete.RecepcionOrigen.KilometrajeFinal,
                   Flete.RecepcionOrigen.HoraLlegada,              Flete.RecepcionOrigen.FechaLlegada,        Flete.RecepcionOrigen.Observacion,
                   Flete.RecepcionOrigen.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.RecepcionOrigen.Conexion, "spCSLDB_Flete_ac_RecepcionOrigen", parametros);
                Flete.RecepcionOrigen.RespuestaAjax = new RespuestaAjax();
                while (dr.Read())
                {
                    Flete.RecepcionOrigen.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Flete.RecepcionOrigen.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Flete.RecepcionOrigen.RespuestaAjax.Mensaje = ex.ToString();
                Flete.RecepcionOrigen.RespuestaAjax.Success = false;
            }

            return Flete;
        }
        #endregion
        #region AC_Producto/servicio detalle del documento por cobrar
        public DocumentosPorCobrarDetalleModels AC_ProductoServicio_Compra(DocumentosPorCobrarDetalleModels DocumentosPorCobrarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorCobrarModels.Id_servicio,
                    DocumentosPorCobrarModels.Id_detalleDoctoCobrar,            DocumentosPorCobrarModels.Id_documentoCobrar,
                    DocumentosPorCobrarModels.Id_productoServicio,              DocumentosPorCobrarModels.Id_conceptoDocumento,
                    DocumentosPorCobrarModels.Cantidad,                         DocumentosPorCobrarModels.PrecioUnitario,
                    DocumentosPorCobrarModels.Subtotal,                         DocumentosPorCobrarModels.Usuario,
                    DocumentosPorCobrarModels.TipoServicio,                     DocumentosPorCobrarModels.Id_almacen,
                    DocumentosPorCobrarModels.Id_unidadProducto,                DocumentosPorCobrarModels.Id_producto,
                    DocumentosPorCobrarModels.Existencia
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_Flete_AC_ServicioProducto_DocumentoPorCobrarDetalle", parametros);
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
        #region Del Producto/Servicio compra
        public DocumentosPorCobrarDetalleModels DEL_ProductoServicioCompra(DocumentosPorCobrarDetalleModels DocumentosPorCobrarModels)
        {
            try
            {
                object[] parametros =
                {
                    DocumentosPorCobrarModels.Id_servicio,
                    DocumentosPorCobrarModels.Id_detalleDoctoCobrar,    DocumentosPorCobrarModels.Id_documentoCobrar,
                    DocumentosPorCobrarModels.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_Flete_DEL_ProductoServicio_Flete", parametros);
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
        #region AC_Comprobante Pago
        public DocumentosPorCobrarDetallePagosModels AC_ComprobanteCobro(DocumentosPorCobrarDetallePagosModels DocumentosPorCobrarModels)
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
                dr = SqlHelper.ExecuteReader(DocumentosPorCobrarModels.Conexion, "spCSLDB_Flete_AC_DetallesPago", parametros);
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

        #region DEL_ComprobanteCobro
        public DocumentosPorCobrarDetallePagosModels DEL_ComprobanteCobro(DocumentosPorCobrarDetallePagosModels DocumentosPorCobrarModels)
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

        #region Documento detalles producto servicio
        public DocumentosPorCobrarDetalleModels GetDetalleDocumentoPorCobrar(DocumentosPorCobrarDetalleModels documento)
        {
            try
            {
                SqlDataReader dr = null;

                object[] parametros =
                {
                    documento.Id_servicio,
                    documento.Id_documentoCobrar,
                    documento.Id_detalleDoctoCobrar
                };
                dr = SqlHelper.ExecuteReader(documento.Conexion, "spCSLDB_Flete_get_DetalleDocumentoPorCobrar", parametros);
                while (dr.Read())
                {
                    documento.Id_documentoCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoCobrar")) ? dr.GetString(dr.GetOrdinal("id_documentoCobrar")) : string.Empty;
                    documento.Id_productoServicio = !dr.IsDBNull(dr.GetOrdinal("id_productoServicio")) ? dr.GetString(dr.GetOrdinal("id_productoServicio")) : string.Empty;
                    documento.Id_conceptoDocumento = !dr.IsDBNull(dr.GetOrdinal("id_conceptoDocumento")) ? dr.GetInt16(dr.GetOrdinal("id_conceptoDocumento")) : 0;
                    documento.Cantidad = !dr.IsDBNull(dr.GetOrdinal("cantidad")) ? dr.GetDecimal(dr.GetOrdinal("cantidad")) : 0;
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
        public DocumentosPorCobrarDetalleModels GetDetalleDocumentoPorCobrarEdit(DocumentosPorCobrarDetalleModels documento)
        {
            try
            {
                SqlDataReader dr = null;

                object[] parametros =
                {
                    documento.Id_servicio,
                    documento.Id_documentoCobrar,
                    documento.Id_detalleDoctoCobrar
                };
                dr = SqlHelper.ExecuteReader(documento.Conexion, "spCSLDB_Flete_get_DetalleDocumentoPorCobrarEdit", parametros);
                while (dr.Read())
                {
                    documento.Id_documentoCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoCobrar")) ? dr.GetString(dr.GetOrdinal("id_documentoCobrar")) : string.Empty;
                    documento.Id_productoServicio = !dr.IsDBNull(dr.GetOrdinal("id_productoServicio")) ? dr.GetString(dr.GetOrdinal("id_productoServicio")) : string.Empty;
                    documento.DescripcionDocumento = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    documento.Cantidad = !dr.IsDBNull(dr.GetOrdinal("cantidad")) ? dr.GetDecimal(dr.GetOrdinal("cantidad")) : 0;
                    documento.PrecioUnitario = !dr.IsDBNull(dr.GetOrdinal("precioUnitario")) ? dr.GetDecimal(dr.GetOrdinal("precioUnitario")) : 0;
                    documento.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0; ;
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

        #region Carta Porte
        public CartaPorteModels GetCartaPorte(FleteModels Flete)
        {
            try
            {
                CartaPorteModels CartaPorte = new CartaPorteModels();
                object[] parametros = { Flete.id_flete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_get_Flete_CartaPorte", parametros);

                while (dr.Read())
                {
                    CartaPorte.LogoRFC = !dr.IsDBNull(dr.GetOrdinal("logoRFC")) ? dr.GetString(dr.GetOrdinal("logoRFC")) : string.Empty;
                    CartaPorte.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;
                    CartaPorte.NombreCliente = !dr.IsDBNull(dr.GetOrdinal("cliente")) ? dr.GetString(dr.GetOrdinal("cliente")) : string.Empty;
                    CartaPorte.RFCCliente = !dr.IsDBNull(dr.GetOrdinal("rfc")) ? dr.GetString(dr.GetOrdinal("rfc")) : string.Empty;
                    CartaPorte.NombreConductor = !dr.IsDBNull(dr.GetOrdinal("nombreConductor")) ? dr.GetString(dr.GetOrdinal("nombreConductor")) : string.Empty;
                    CartaPorte.Vehiculo = !dr.IsDBNull(dr.GetOrdinal("vehiculo")) ? dr.GetString(dr.GetOrdinal("vehiculo")) : string.Empty;
                    CartaPorte.PlacaVehiculo = !dr.IsDBNull(dr.GetOrdinal("placas")) ? dr.GetString(dr.GetOrdinal("placas")) : string.Empty;
                    CartaPorte.Remitente = !dr.IsDBNull(dr.GetOrdinal("nombreRemitente")) ? dr.GetString(dr.GetOrdinal("nombreRemitente")) : string.Empty;
                    CartaPorte.Destinatario = !dr.IsDBNull(dr.GetOrdinal("nombreDestinatario")) ? dr.GetString(dr.GetOrdinal("nombreDestinatario")) : string.Empty;
                    CartaPorte.DomicilioRemitente = !dr.IsDBNull(dr.GetOrdinal("DireccionRemitente")) ? dr.GetString(dr.GetOrdinal("DireccionRemitente")) : string.Empty;
                    CartaPorte.DomicilioDestinatario = !dr.IsDBNull(dr.GetOrdinal("DireccionDestinatario")) ? dr.GetString(dr.GetOrdinal("DireccionDestinatario")) : string.Empty;
                    CartaPorte.LugarOrigen = !dr.IsDBNull(dr.GetOrdinal("recogeraEn")) ? dr.GetString(dr.GetOrdinal("recogeraEn")) : string.Empty;
                    CartaPorte.LugarDestino = !dr.IsDBNull(dr.GetOrdinal("entregaraEn")) ? dr.GetString(dr.GetOrdinal("entregaraEn")) : string.Empty;
                    CartaPorte.FechaEntrega = !dr.IsDBNull(dr.GetOrdinal("fechaTentativaEntrega")) ? dr.GetDateTime(dr.GetOrdinal("fechaTentativaEntrega")) : DateTime.Now;
                    CartaPorte.PesoAproximado = !dr.IsDBNull(dr.GetOrdinal("pesoAproximado")) ? dr.GetDecimal(dr.GetOrdinal("pesoAproximado")) : 0;
                    CartaPorte.Total = !dr.IsDBNull(dr.GetOrdinal("totalFlete")) ? dr.GetDecimal(dr.GetOrdinal("totalFlete")) : 0;
                    CartaPorte.ImporteConLetra = CartaPorte.Total.NumeroALetras();
                    CartaPorte.CondicionPago = !dr.IsDBNull(dr.GetOrdinal("condicionPago")) ? dr.GetString(dr.GetOrdinal("condicionPago")) : string.Empty;
                    CartaPorte.FormaPago = !dr.IsDBNull(dr.GetOrdinal("formaPago")) ? dr.GetString(dr.GetOrdinal("formaPago")) : string.Empty;
                    CartaPorte.MetodoPago = !dr.IsDBNull(dr.GetOrdinal("metodoPago")) ? dr.GetString(dr.GetOrdinal("metodoPago")) : string.Empty;
                }
                dr.Close();
                return CartaPorte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CartaPorteDetallesModels> GetCartaPorteDetalles(FleteModels Flete)
        {
            try
            {
                List<CartaPorteDetallesModels> ListaCartaPorteDetalles = new List<CartaPorteDetallesModels>();
                CartaPorteDetallesModels CartaPorte;
                object[] parametros = { Flete.id_flete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_CartaPorteDetalles", parametros);

                while (dr.Read())
                {
                    CartaPorte = new CartaPorteDetallesModels();
                    CartaPorte.Cantidad = !dr.IsDBNull(dr.GetOrdinal("cantidad")) ? dr.GetDecimal(dr.GetOrdinal("cantidad")) : 0;
                    CartaPorte.UnidadMedida = !dr.IsDBNull(dr.GetOrdinal("unidad")) ? dr.GetString(dr.GetOrdinal("unidad")) : string.Empty;
                    CartaPorte.DescripcionProductoServicio = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    CartaPorte.PrecioUnitario = !dr.IsDBNull(dr.GetOrdinal("precioUnitario")) ? dr.GetDecimal(dr.GetOrdinal("precioUnitario")) : 0;
                    CartaPorte.ImpuestosTrasladados = !dr.IsDBNull(dr.GetOrdinal("impuestos_trasladados")) ? dr.GetDecimal(dr.GetOrdinal("impuestos_trasladados")) : 0;
                    CartaPorte.ImpuestosRetenidos = !dr.IsDBNull(dr.GetOrdinal("impuestos_retenidos")) ? dr.GetDecimal(dr.GetOrdinal("impuestos_retenidos")) : 0;
                    CartaPorte.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0;
                    CartaPorte.Total = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;

                    ListaCartaPorteDetalles.Add(CartaPorte);
                }
                dr.Close();
                return ListaCartaPorteDetalles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Reporte Ganado
        public List<ReporteGanadoModels> GetReporteGanadoDetalles(FleteModels Flete)
        {
            try
            {
                List<ReporteGanadoModels> Lista = new List<ReporteGanadoModels>();
                ReporteGanadoModels Item;
                object[] parametros = { Flete.id_flete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_ReporteListaGanado", parametros);

                while (dr.Read())
                {
                    Item = new ReporteGanadoModels();
                    Item.NumArete = !dr.IsDBNull(dr.GetOrdinal("numArete")) ? dr.GetString(dr.GetOrdinal("numArete")) : string.Empty;
                    Item.Genero = !dr.IsDBNull(dr.GetOrdinal("genero")) ? dr.GetString(dr.GetOrdinal("genero")) : string.Empty;
                    Item.ImagenBase64_1 = !dr.IsDBNull(dr.GetOrdinal("imagen1")) ? dr.GetString(dr.GetOrdinal("imagen1")) : Auxiliar.SinImagen();
                    Item.ImagenBase64_2 = !dr.IsDBNull(dr.GetOrdinal("imagen2")) ? dr.GetString(dr.GetOrdinal("imagen2")) : Auxiliar.SinImagen();
                    Item.ImagenBase64_3 = !dr.IsDBNull(dr.GetOrdinal("imagen3")) ? dr.GetString(dr.GetOrdinal("imagen3")) : Auxiliar.SinImagen();
                    Item.NombreCorral = !dr.IsDBNull(dr.GetOrdinal("corral")) ? dr.GetString(dr.GetOrdinal("corral")) : string.Empty;
                    Item.PesoInicial = !dr.IsDBNull(dr.GetOrdinal("pesoInicial")) ? dr.GetString(dr.GetOrdinal("pesoInicial")) : string.Empty;
                    Item.PesoFinal = !dr.IsDBNull(dr.GetOrdinal("pesoFinal")) ? dr.GetString(dr.GetOrdinal("pesoFinal")) : string.Empty;
                    Item.PesoPagado = !dr.IsDBNull(dr.GetOrdinal("pesoPagado")) ? dr.GetString(dr.GetOrdinal("pesoPagado")) : string.Empty;
                    Item.PrecioPorKilo = !dr.IsDBNull(dr.GetOrdinal("precioKilo")) ? dr.GetString(dr.GetOrdinal("precioKilo")) : string.Empty;
                    Item.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetString(dr.GetOrdinal("subtotal")) : string.Empty;
                    Item.Repeso = !dr.IsDBNull(dr.GetOrdinal("repeso")) ? dr.GetBoolean(dr.GetOrdinal("repeso")) : false;

                    Item.PesoInicial = Auxiliar.StringToKilos(Item.PesoInicial);
                    Item.PesoFinal = Auxiliar.StringToKilos(Item.PesoFinal);
                    Item.PesoPagado = Auxiliar.StringToKilos(Item.PesoPagado);

                    Item.PrecioPorKilo = Auxiliar.StringToMoneda_MX(Item.PrecioPorKilo);
                    Item.Subtotal = Auxiliar.StringToMoneda_MX(Item.Subtotal);

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

        public List<ReporteGanadoModels> GetReporteGanadoDetallesPropioYNoPropio(FleteModels Flete)
        {
            try
            {
                List<ReporteGanadoModels> Lista = new List<ReporteGanadoModels>();
                ReporteGanadoModels Item;
                object[] parametros = { Flete.id_flete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_ReporteListaGanadoPropioYNoPropio", parametros);

                while (dr.Read())
                {
                    Item = new ReporteGanadoModels();
                    Item.NumArete = !dr.IsDBNull(dr.GetOrdinal("numArete")) ? dr.GetString(dr.GetOrdinal("numArete")) : string.Empty;
                    Item.Genero = !dr.IsDBNull(dr.GetOrdinal("genero")) ? dr.GetString(dr.GetOrdinal("genero")) : string.Empty;
                    Item.ImagenBase64_1 = !dr.IsDBNull(dr.GetOrdinal("imagen1")) ? dr.GetString(dr.GetOrdinal("imagen1")) : Auxiliar.SinImagen();
                    Item.ImagenBase64_2 = !dr.IsDBNull(dr.GetOrdinal("imagen2")) ? dr.GetString(dr.GetOrdinal("imagen2")) : Auxiliar.SinImagen();
                    Item.ImagenBase64_3 = !dr.IsDBNull(dr.GetOrdinal("imagen3")) ? dr.GetString(dr.GetOrdinal("imagen3")) : Auxiliar.SinImagen();
                    Item.NombreCorral = !dr.IsDBNull(dr.GetOrdinal("corral")) ? dr.GetString(dr.GetOrdinal("corral")) : string.Empty;
                    Item.PesoInicial = !dr.IsDBNull(dr.GetOrdinal("pesoInicial")) ? dr.GetString(dr.GetOrdinal("pesoInicial")) : string.Empty;
                    Item.PesoFinal = !dr.IsDBNull(dr.GetOrdinal("pesoFinal")) ? dr.GetString(dr.GetOrdinal("pesoFinal")) : string.Empty;
                    Item.PesoPagado = !dr.IsDBNull(dr.GetOrdinal("pesoPagado")) ? dr.GetString(dr.GetOrdinal("pesoPagado")) : string.Empty;
                    Item.PrecioPorKilo = !dr.IsDBNull(dr.GetOrdinal("precioKilo")) ? dr.GetString(dr.GetOrdinal("precioKilo")) : string.Empty;
                    Item.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetString(dr.GetOrdinal("subtotal")) : string.Empty;
                    Item.Repeso = !dr.IsDBNull(dr.GetOrdinal("repeso")) ? dr.GetBoolean(dr.GetOrdinal("repeso")) : false;

                    Item.PesoInicial = Auxiliar.StringToKilos(Item.PesoInicial);
                    Item.PesoFinal = Auxiliar.StringToKilos(Item.PesoFinal);
                    Item.PesoPagado = Auxiliar.StringToKilos(Item.PesoPagado);

                    Item.PrecioPorKilo = Auxiliar.StringToMoneda_MX(Item.PrecioPorKilo);
                    Item.Subtotal = Auxiliar.StringToMoneda_MX(Item.Subtotal);

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

        public ReporteCabeceraGanado GetReporteCabeceraGanadoDetalles(FleteModels Flete)
        {
            try
            {
                ReporteCabeceraGanado Item = new ReporteCabeceraGanado(); ;
                object[] parametros = { Flete.id_flete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_ReporteCabeceraListaGanado", parametros);

                while (dr.Read())
                {
                    Item.NombreChofer = !dr.IsDBNull(dr.GetOrdinal("nombreChofer")) ? dr.GetString(dr.GetOrdinal("nombreChofer")) : string.Empty;
                    Item.UnidadVehiculo = !dr.IsDBNull(dr.GetOrdinal("numeroUnidad")) ? dr.GetString(dr.GetOrdinal("numeroUnidad")) : string.Empty;
                    Item.ModeloVehiculo = !dr.IsDBNull(dr.GetOrdinal("modelo")) ? dr.GetString(dr.GetOrdinal("modelo")) : string.Empty;
                    Item.MarcaVehiculo = !dr.IsDBNull(dr.GetOrdinal("marca")) ? dr.GetString(dr.GetOrdinal("marca")) : string.Empty;
                    Item.ColorVehiculo = !dr.IsDBNull(dr.GetOrdinal("color")) ? dr.GetString(dr.GetOrdinal("color")) : string.Empty;
                    Item.CapacidadVehiculo = !dr.IsDBNull(dr.GetOrdinal("capacidad")) ? dr.GetString(dr.GetOrdinal("capacidad")) : string.Empty;
                    Item.GPS = !dr.IsDBNull(dr.GetOrdinal("gps")) ? dr.GetString(dr.GetOrdinal("gps")) : string.Empty;
                    Item.FechaHoraSalida = !dr.IsDBNull(dr.GetOrdinal("fechaHoraSalida")) ? dr.GetString(dr.GetOrdinal("fechaHoraSalida")) : string.Empty;
                    Item.FechaHoraEmbarque = !dr.IsDBNull(dr.GetOrdinal("fechaHoraEmbarque")) ? dr.GetString(dr.GetOrdinal("fechaHoraEmbarque")) : string.Empty;
                    Item.LugarOrigen = !dr.IsDBNull(dr.GetOrdinal("lugarOrigen")) ? dr.GetString(dr.GetOrdinal("lugarOrigen")) : string.Empty;
                    Item.LugarDestino = !dr.IsDBNull(dr.GetOrdinal("lugarDestino")) ? dr.GetString(dr.GetOrdinal("lugarDestino")) : string.Empty;
                    Item.PSGOrigen = !dr.IsDBNull(dr.GetOrdinal("PSGOrigen")) ? dr.GetString(dr.GetOrdinal("PSGOrigen")) : string.Empty;
                    Item.PSGDestino = !dr.IsDBNull(dr.GetOrdinal("PSGDestino")) ? dr.GetString(dr.GetOrdinal("PSGDestino")) : string.Empty;
                    Item.TotalGanadoMachos = !dr.IsDBNull(dr.GetOrdinal("totalGanadoMachos")) ? dr.GetInt32(dr.GetOrdinal("totalGanadoMachos")) : 0;
                    Item.TotalGanadoHembras = !dr.IsDBNull(dr.GetOrdinal("totalGanadoHembras")) ? dr.GetInt32(dr.GetOrdinal("totalGanadoHembras")) : 0;
                    Item.TotalGanado = !dr.IsDBNull(dr.GetOrdinal("totalGanado")) ? dr.GetInt32(dr.GetOrdinal("totalGanado")) : 0;
                    Item.TotalKilosGanado = !dr.IsDBNull(dr.GetOrdinal("totalKilosGanado")) ? dr.GetDecimal(dr.GetOrdinal("totalKilosGanado")) : 0;
                    Item.PlacaJaula = !dr.IsDBNull(dr.GetOrdinal("placaJaula")) ? dr.GetString(dr.GetOrdinal("placaJaula")) : string.Empty;
                    Item.PlacaTracto = !dr.IsDBNull(dr.GetOrdinal("placas")) ? dr.GetString(dr.GetOrdinal("placas")) : string.Empty;
                }
                dr.Close();
                return Item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReporteCabeceraGanado GetReporteCabeceraGanadoDetallesPropioYNoPropio(FleteModels Flete)
        {
            try
            {
                ReporteCabeceraGanado Item = new ReporteCabeceraGanado(); ;
                object[] parametros = { Flete.id_flete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_ReporteCabeceraListaGanadoPropioYNoPropio", parametros);

                while (dr.Read())
                {
                    Item.NombreChofer = !dr.IsDBNull(dr.GetOrdinal("nombreChofer")) ? dr.GetString(dr.GetOrdinal("nombreChofer")) : string.Empty;
                    Item.UnidadVehiculo = !dr.IsDBNull(dr.GetOrdinal("numeroUnidad")) ? dr.GetString(dr.GetOrdinal("numeroUnidad")) : string.Empty;
                    Item.ModeloVehiculo = !dr.IsDBNull(dr.GetOrdinal("modelo")) ? dr.GetString(dr.GetOrdinal("modelo")) : string.Empty;
                    Item.MarcaVehiculo = !dr.IsDBNull(dr.GetOrdinal("marca")) ? dr.GetString(dr.GetOrdinal("marca")) : string.Empty;
                    Item.ColorVehiculo = !dr.IsDBNull(dr.GetOrdinal("color")) ? dr.GetString(dr.GetOrdinal("color")) : string.Empty;
                    Item.CapacidadVehiculo = !dr.IsDBNull(dr.GetOrdinal("capacidad")) ? dr.GetString(dr.GetOrdinal("capacidad")) : string.Empty;
                    Item.GPS = !dr.IsDBNull(dr.GetOrdinal("gps")) ? dr.GetString(dr.GetOrdinal("gps")) : string.Empty;
                    Item.FechaHoraSalida = !dr.IsDBNull(dr.GetOrdinal("fechaHoraSalida")) ? dr.GetString(dr.GetOrdinal("fechaHoraSalida")) : string.Empty;
                    Item.FechaHoraEmbarque = !dr.IsDBNull(dr.GetOrdinal("fechaHoraEmbarque")) ? dr.GetString(dr.GetOrdinal("fechaHoraEmbarque")) : string.Empty;
                    Item.LugarOrigen = !dr.IsDBNull(dr.GetOrdinal("lugarOrigen")) ? dr.GetString(dr.GetOrdinal("lugarOrigen")) : string.Empty;
                    Item.LugarDestino = !dr.IsDBNull(dr.GetOrdinal("lugarDestino")) ? dr.GetString(dr.GetOrdinal("lugarDestino")) : string.Empty;
                    Item.PSGOrigen = !dr.IsDBNull(dr.GetOrdinal("PSGOrigen")) ? dr.GetString(dr.GetOrdinal("PSGOrigen")) : string.Empty;
                    Item.PSGDestino = !dr.IsDBNull(dr.GetOrdinal("PSGDestino")) ? dr.GetString(dr.GetOrdinal("PSGDestino")) : string.Empty;
                    Item.TotalGanadoMachos = !dr.IsDBNull(dr.GetOrdinal("totalGanadoMachos")) ? dr.GetInt32(dr.GetOrdinal("totalGanadoMachos")) : 0;
                    Item.TotalGanadoHembras = !dr.IsDBNull(dr.GetOrdinal("totalGanadoHembras")) ? dr.GetInt32(dr.GetOrdinal("totalGanadoHembras")) : 0;
                    Item.TotalGanado = !dr.IsDBNull(dr.GetOrdinal("totalGanado")) ? dr.GetInt32(dr.GetOrdinal("totalGanado")) : 0;
                    Item.TotalKilosGanado = !dr.IsDBNull(dr.GetOrdinal("totalKilosGanado")) ? dr.GetDecimal(dr.GetOrdinal("totalKilosGanado")) : 0;
                    Item.PlacaJaula = !dr.IsDBNull(dr.GetOrdinal("placaJaula")) ? dr.GetString(dr.GetOrdinal("placaJaula")) : string.Empty;
                    Item.PlacaTracto = !dr.IsDBNull(dr.GetOrdinal("placas")) ? dr.GetString(dr.GetOrdinal("placas")) : string.Empty;
                }
                dr.Close();
                return Item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatFierroModels> GetReporteFierrosVenta(FleteModels Flete)
        {
            try
            {
                CatFierroModels Item;
                List<CatFierroModels> Lista = new List<CatFierroModels>();

                object[] parametros = { Flete.id_flete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_ListaFierros", parametros);

                while (dr.Read())
                {
                    Item = new CatFierroModels();
                    Item.ImgFierro = !dr.IsDBNull(dr.GetOrdinal("fierro")) ? dr.GetString(dr.GetOrdinal("fierro")) : string.Empty;

                    Item.ImgFierro =
                        Auxiliar.ImagePathToBase64(Item.ImgFierro, ProjectSettings.BaseDirCatFierro);

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

        #endregion


        public Flete_ProductoModels Flete_c_ProductoGanado(Flete_ProductoModels ganado)
        {
            try
            {
                object[] parametros =
                {
                     ganado.ListaStringIDProductoSeleccionado
                    ,ganado.ID_Flete
                    ,ganado.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(ganado.Conexion, "spCSLDB_Flete_c_ProductoGanadoPropio", parametros);
                ganado.RespuestaAjax = new RespuestaAjax();

                while (dr.Read())
                {
                    ganado.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    ganado.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return ganado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Flete_ProductoModels Flete_del_ProductoGanado(Flete_ProductoModels ganado)
        {
            try
            {
                object[] parametros =
                {
                     ganado.ListaStringIDProductoSeleccionado
                    ,ganado.ID_Flete
                    ,ganado.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(ganado.Conexion, "spCSLDB_Flete_del_ProductoGanadoXIDGanadoIDFlete", parametros);
                ganado.RespuestaAjax = new RespuestaAjax();

                while (dr.Read())
                {
                    ganado.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    ganado.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return ganado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Vista Documentos
        public DocumentoModels GetGeneralesDocumentosFlete(DocumentoModels Documento)
        {
            try
            {
                object[] parametros =
                {
                     Documento.Id_servicio, 2
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_get_DocumentoGeneral", parametros);

                while (dr.Read())
                {
                    Documento.PrecioUnitarioDocumentacion = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;
                    Documento.Id_conceptoSalidaDeduccion = !dr.IsDBNull(dr.GetOrdinal("id_conceptoDocumento")) ? dr.GetInt32(dr.GetOrdinal("id_conceptoDocumento")) : 0;
                    Documento.Id_documentoPorPagar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorPagar")) ? dr.GetString(dr.GetOrdinal("id_documentoPorPagar")) : string.Empty;
                }

                dr.Close();

                return Documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DocumentoModels AC_CostoDocumentos(DocumentoModels Documento)
        {
            try
            {
                object[] parametros =
                {
                     Documento.Id_servicio
                    ,Documento.Id_documentoPorPagar
                    ,Documento.Usuario
                    ,Documento.PrecioUnitarioDocumentacion
                    ,Documento.Id_conceptoSalidaDeduccion
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Flete_ac_PrecioDocumento", parametros);

                while (dr.Read())
                {
                    Documento.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Documento.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Vista Documento
        public DocumentoModels GetDocumentoXIDDocumento(DocumentoModels Documento)
        {
            try
            {
                object[] parametros =
                {
                     Documento.IDDocumento, Documento.Id_servicio
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Flete_get_DocumentoXIDDocumento", parametros);
                Documento.ImagenServer = ProjectSettings.PathDefaultImage;

                while (dr.Read())
                {
                    Documento.IDTipoDocumento = !dr.IsDBNull(dr.GetOrdinal("id_tipoDocumento")) ? dr.GetInt16(dr.GetOrdinal("id_tipoDocumento")) : 0;
                    Documento.Clave = !dr.IsDBNull(dr.GetOrdinal("clave")) ? dr.GetString(dr.GetOrdinal("clave")) : string.Empty;
                    //Solo para mostrar
                    Documento.ImagenServer = !dr.IsDBNull(dr.GetOrdinal("imagen")) ? dr.GetString(dr.GetOrdinal("imagen")) : string.Empty;

                    Documento.ImagenServer = Auxiliar.ValidImageFormServer(Documento.ImagenServer,
                        ProjectSettings.BaseDirFleteDocumentoDetalle);
                }
                
                dr.Close();

                return Documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatTipoDocumentoModels> GetListaTiposDocumentos(DocumentoModels Documento)
        {
            try
            {
                CatTipoDocumentoModels TipoDocumento;
                List<CatTipoDocumentoModels> ListaTipoDocumentos = new List<CatTipoDocumentoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Combo_get_CatTipoDocumento2");
                while (dr.Read())
                {
                    TipoDocumento = new CatTipoDocumentoModels
                    {
                        IDTipoDocumento = !dr.IsDBNull(dr.GetOrdinal("id_tipoDocumento")) ? dr.GetInt16(dr.GetOrdinal("id_tipoDocumento")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    ListaTipoDocumentos.Add(TipoDocumento);
                }
                dr.Close();
                return ListaTipoDocumentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DocumentoModels AC_Documento(DocumentoModels Documento)
        {
            try
            {
                object[] parametros =
                {
                     Documento.IDDocumento
                    ,Documento.Id_servicio
                    ,Documento.IDTipoDocumento
                    ,Documento.Clave
                    ,Documento.ImagenServer
                    ,Documento.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Flete_ac_Documento", parametros);

                while (dr.Read())
                {
                    Documento.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Documento.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DocumentoModels DEL_DocumentoXIDDocumento(DocumentoModels Documento)
        {
            try
            {
                object[] parametros =
                {
                     Documento.IDDocumento
                    ,Documento.Usuario
                    ,Documento.Id_servicio
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Flete_del_DocumentoXIDDocumento", parametros);

                while (dr.Read())
                {
                    Documento.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Documento.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Vista detalles

        public FleteDetallesModels Get_detallesFlete(FleteDetallesModels FleteDetalles)
        {
            try
            {
                object[] parametros =
                {
                     FleteDetalles.Id_flete
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(FleteDetalles.Conexion, "spCSLDB_Flete_get_Detalles", parametros);

                while (dr.Read())
                {
                    FleteDetalles.FleteFolio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;
                    FleteDetalles.FleteSucursal = !dr.IsDBNull(dr.GetOrdinal("Sucursal")) ? dr.GetString(dr.GetOrdinal("Sucursal")) : string.Empty;
                    FleteDetalles.FleteFecha = !dr.IsDBNull(dr.GetOrdinal("fechaFinalizadoFlete")) ? dr.GetString(dr.GetOrdinal("fechaFinalizadoFlete")) : string.Empty;
                    FleteDetalles.FleteObservacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;
                    FleteDetalles.FleteLineaFletera = !dr.IsDBNull(dr.GetOrdinal("lineaFletera")) ? dr.GetString(dr.GetOrdinal("lineaFletera")) : string.Empty;
                    FleteDetalles.FletePrecio = !dr.IsDBNull(dr.GetOrdinal("precioFlete")) ? dr.GetDecimal(dr.GetOrdinal("precioFlete")) : 0;
                    FleteDetalles.FleteChofer = !dr.IsDBNull(dr.GetOrdinal("NombreChofer")) ? dr.GetString(dr.GetOrdinal("NombreChofer")) : string.Empty;
                    FleteDetalles.FleteVehiculo = !dr.IsDBNull(dr.GetOrdinal("vehiculo")) ? dr.GetString(dr.GetOrdinal("vehiculo")) : string.Empty;
                    FleteDetalles.ClienteNombreRazonSocialCompleto = !dr.IsDBNull(dr.GetOrdinal("nombreCliente")) ? dr.GetString(dr.GetOrdinal("nombreCliente")) : string.Empty;
                    FleteDetalles.FleteLugarOrigen = !dr.IsDBNull(dr.GetOrdinal("lugarOrigen")) ? dr.GetString(dr.GetOrdinal("lugarOrigen")) : string.Empty;
                    FleteDetalles.FleteDireccionOrigen = !dr.IsDBNull(dr.GetOrdinal("direccionLugarOrigen")) ? dr.GetString(dr.GetOrdinal("direccionLugarOrigen")) : string.Empty;
                    FleteDetalles.FleteLugarDestino = !dr.IsDBNull(dr.GetOrdinal("lugarDestino")) ? dr.GetString(dr.GetOrdinal("lugarDestino")) : string.Empty;
                    FleteDetalles.FleteDireccionDestino = !dr.IsDBNull(dr.GetOrdinal("direccionLugarDestino")) ? dr.GetString(dr.GetOrdinal("direccionLugarDestino")) : string.Empty;
                    FleteDetalles.DocumentosPrecioDocumentacion = !dr.IsDBNull(dr.GetOrdinal("precioDocumentacion")) ? dr.GetDecimal(dr.GetOrdinal("precioDocumentacion")) : 0;
                    FleteDetalles.DocumentosTipoSalidaDocumentacion = !dr.IsDBNull(dr.GetOrdinal("tipoSalidaDocumentacion")) ? dr.GetString(dr.GetOrdinal("tipoSalidaDocumentacion")) : string.Empty;
                    FleteDetalles.Id_documentoPorCobrar = !dr.IsDBNull(dr.GetOrdinal("id_documentoPorCobrar")) ? dr.GetString(dr.GetOrdinal("id_documentoPorCobrar")) : string.Empty;
                }

                dr.Close();

                return FleteDetalles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableGeneralesGanado(FleteModels Flete)
        {
            try
            {
                object[] parametros = { Flete.id_flete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_DatatableGeneralesGanado", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableGanadoMacho(FleteModels Flete)
        {
            try
            {
                object[] parametros = { Flete.id_flete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_DatatableGeneralesGanadoMacho", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableGanadoHembra(FleteModels Flete)
        {
            try
            {
                object[] parametros = { Flete.id_flete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_DatatableGeneralesGanadoHembra", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableDetallesDocXcobrar(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                    {
                        Flete.id_flete
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_DetallesDocXcobrar", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DatatableDetalleDocXCobrarPagos(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                    {
                        Flete.id_flete
                    };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_DetallesDocXcobrarPAGOS", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Del flete
        public FleteModels Flete_del_Flete(FleteModels Flete)
        {
            try
            {
                object[] parametros =
                {
                    Flete.id_flete
                    ,Flete.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_del_Flete", parametros);
                while (dr.Read())
                {
                    Flete.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    Flete.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return Flete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Comprobante flete
        public ComprobanteFleteModels GetComprobanteFlete(FleteModels Flete)
        {
            try
            {
                ComprobanteFleteModels item = new ComprobanteFleteModels();
                object[] parametros = { Flete.id_flete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_get_Flete_ComprobanteFlete", parametros);

                while (dr.Read())
                {
                    item.LogoEmpresa = !dr.IsDBNull(dr.GetOrdinal("logoEmpresa")) ? dr.GetString(dr.GetOrdinal("logoEmpresa")) : string.Empty;
                    item.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;
                    item.AnnoImpresion = !dr.IsDBNull(dr.GetOrdinal("anno")) ? dr.GetString(dr.GetOrdinal("anno")) : string.Empty;
                    item.MesImpresion = !dr.IsDBNull(dr.GetOrdinal("mes")) ? dr.GetString(dr.GetOrdinal("mes")) : string.Empty;
                    item.DiaImpresion = !dr.IsDBNull(dr.GetOrdinal("dia")) ? dr.GetString(dr.GetOrdinal("dia")) : string.Empty;
                    item.NombreEmpresa = !dr.IsDBNull(dr.GetOrdinal("rubroEmpresa")) ? dr.GetString(dr.GetOrdinal("rubroEmpresa")) : string.Empty;
                    item.DireccionEmpresa = !dr.IsDBNull(dr.GetOrdinal("direccionEmpresa")) ? dr.GetString(dr.GetOrdinal("direccionEmpresa")) : string.Empty;

                    item.NombreCliente = !dr.IsDBNull(dr.GetOrdinal("cliente")) ? dr.GetString(dr.GetOrdinal("cliente")) : string.Empty;
                    item.RFCCliente = !dr.IsDBNull(dr.GetOrdinal("rfc")) ? dr.GetString(dr.GetOrdinal("rfc")) : string.Empty;
                    item.NombreConductor = !dr.IsDBNull(dr.GetOrdinal("nombreConductor")) ? dr.GetString(dr.GetOrdinal("nombreConductor")) : string.Empty;
                    item.Vehiculo = !dr.IsDBNull(dr.GetOrdinal("vehiculo")) ? dr.GetString(dr.GetOrdinal("vehiculo")) : string.Empty;
                    item.PlacaVehiculo = !dr.IsDBNull(dr.GetOrdinal("placas")) ? dr.GetString(dr.GetOrdinal("placas")) : string.Empty;
                    item.Remitente = !dr.IsDBNull(dr.GetOrdinal("nombreRemitente")) ? dr.GetString(dr.GetOrdinal("nombreRemitente")) : string.Empty;
                    item.Destinatario = !dr.IsDBNull(dr.GetOrdinal("nombreDestinatario")) ? dr.GetString(dr.GetOrdinal("nombreDestinatario")) : string.Empty;
                    item.DomicilioRemitente = !dr.IsDBNull(dr.GetOrdinal("DireccionRemitente")) ? dr.GetString(dr.GetOrdinal("DireccionRemitente")) : string.Empty;
                    item.DomicilioDestinatario = !dr.IsDBNull(dr.GetOrdinal("DireccionDestinatario")) ? dr.GetString(dr.GetOrdinal("DireccionDestinatario")) : string.Empty;
                    item.LugarOrigen = !dr.IsDBNull(dr.GetOrdinal("recogeraEn")) ? dr.GetString(dr.GetOrdinal("recogeraEn")) : string.Empty;
                    item.LugarDestino = !dr.IsDBNull(dr.GetOrdinal("entregaraEn")) ? dr.GetString(dr.GetOrdinal("entregaraEn")) : string.Empty;
                    item.FechaEntrega = !dr.IsDBNull(dr.GetOrdinal("fechaTentativaEntrega")) ? dr.GetDateTime(dr.GetOrdinal("fechaTentativaEntrega")) : DateTime.Now;
                    item.Total = !dr.IsDBNull(dr.GetOrdinal("totalFlete")) ? dr.GetDecimal(dr.GetOrdinal("totalFlete")) : 0;
                    item.CondicionPago = !dr.IsDBNull(dr.GetOrdinal("condicionPago")) ? dr.GetString(dr.GetOrdinal("condicionPago")) : string.Empty;
                    item.FormaPago = !dr.IsDBNull(dr.GetOrdinal("formaPago")) ? dr.GetString(dr.GetOrdinal("formaPago")) : string.Empty;
                    item.MetodoPago = !dr.IsDBNull(dr.GetOrdinal("metodoPago")) ? dr.GetString(dr.GetOrdinal("metodoPago")) : string.Empty;
                    
                }
                dr.Close();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ComprobanteFleteDetallesModels> GetComprobanteFleteDetalles(FleteModels Flete)
        {
            try
            {
                List<ComprobanteFleteDetallesModels> lista = new List<ComprobanteFleteDetallesModels>();
                ComprobanteFleteDetallesModels item;
                object[] parametros = { Flete.id_flete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_ComprobanteFleteDetalles", parametros);

                while (dr.Read())
                {
                    item = new ComprobanteFleteDetallesModels();
                    item.Cantidad = !dr.IsDBNull(dr.GetOrdinal("cantidad")) ? dr.GetDecimal(dr.GetOrdinal("cantidad")) : 0;
                    item.UnidadMedida = !dr.IsDBNull(dr.GetOrdinal("unidad")) ? dr.GetString(dr.GetOrdinal("unidad")) : string.Empty;
                    item.DescripcionProductoServicio = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    item.PrecioUnitario = !dr.IsDBNull(dr.GetOrdinal("precioUnitario")) ? dr.GetDecimal(dr.GetOrdinal("precioUnitario")) : 0;
                    item.ImpuestosTrasladados = !dr.IsDBNull(dr.GetOrdinal("impuestos_trasladados")) ? dr.GetDecimal(dr.GetOrdinal("impuestos_trasladados")) : 0;
                    item.ImpuestosRetenidos = !dr.IsDBNull(dr.GetOrdinal("impuestos_retenidos")) ? dr.GetDecimal(dr.GetOrdinal("impuestos_retenidos")) : 0;
                    item.Subtotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0;
                    item.Total = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;

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

        public List<ComprobanteFletePagosModels> GetComprobanteFleteDetallesPagos(FleteModels Flete)
        {
            try
            {
                List<ComprobanteFletePagosModels> Lista = new List<ComprobanteFletePagosModels>();
                ComprobanteFletePagosModels Item;
                object[] parametros = { Flete.id_flete };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_ComprobanteFleteDetallesPagos", parametros);

                while (dr.Read())
                {
                    Item = new ComprobanteFletePagosModels();
                    Item.FormaPago = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    Item.FechaPago = !dr.IsDBNull(dr.GetOrdinal("fecha")) ? dr.GetString(dr.GetOrdinal("fecha")) : string.Empty;
                    Item.MontoPagado = !dr.IsDBNull(dr.GetOrdinal("monto")) ? dr.GetDecimal(dr.GetOrdinal("monto")) : 0;

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
        #endregion
    }
}