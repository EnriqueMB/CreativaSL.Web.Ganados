using System;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using CreativaSL.Web.Ganados.Models.Dto.Configuracion;
using CreativaSL.Web.Ganados.Models.System;
using System.Data;
using CreativaSL.Web.Ganados.Models.Datatable;
using CreativaSL.Web.Ganados.Models.Dto.Base;
using Newtonsoft.Json;
using CreativaSL.Web.Ganados.Models.Helpers;
using System.Web.Hosting;
using System.IO;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Configuracion_Datos : BaseSQL
    {
        public List<ConfiguracionModels> ObtenerListaTicket(ConfiguracionModels datos)
        {
            try
            {
                List<ConfiguracionModels> lista = new List<ConfiguracionModels>();
                ConfiguracionModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Get_ConfiguracionGeneral");
                while(dr.Read())
                {
                    item = new ConfiguracionModels();
                    item.idTicket = !dr.IsDBNull(dr.GetOrdinal("id_configuracion")) ? dr.GetInt32(dr.GetOrdinal("id_configuracion")) : 0;
                    item.CorreoTxt = !dr.IsDBNull(dr.GetOrdinal("CorreoTxt")) ? dr.GetString(dr.GetOrdinal("CorreoTxt")) : string.Empty;
                    item.Password = !dr.IsDBNull(dr.GetOrdinal("Password")) ? dr.GetString(dr.GetOrdinal("Password")) : string.Empty;
                    item.HtmlTxt = !dr.IsDBNull(dr.GetOrdinal("HtmlTxt")) ? dr.GetBoolean(dr.GetOrdinal("HtmlTxt")) : true;
                    item.HostTxt = !dr.IsDBNull(dr.GetOrdinal("HostTxt")) ? dr.GetString(dr.GetOrdinal("HostTxt")) : string.Empty;
                    item.PortTxt = !dr.IsDBNull(dr.GetOrdinal("PortTxt")) ? dr.GetString(dr.GetOrdinal("PortTxt")) : string.Empty;
                    item.EnableSslTxt = !dr.IsDBNull(dr.GetOrdinal("EnableSslTxt")) ? dr.GetBoolean(dr.GetOrdinal("EnableSslTxt")) : true;
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
        public ConfiguracionModels ObtenerTicket(ConfiguracionModels datos)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Get_ConfiguracionGeneralXID",datos.idTicket);
                while (dr.Read())
                {
                    datos.idTicket = !dr.IsDBNull(dr.GetOrdinal("id_configuracion")) ? dr.GetInt32(dr.GetOrdinal("id_configuracion")) : 0;
                    datos.CorreoTxt = !dr.IsDBNull(dr.GetOrdinal("CorreoTxt")) ? dr.GetString(dr.GetOrdinal("CorreoTxt")) : string.Empty;
                    datos.Password = !dr.IsDBNull(dr.GetOrdinal("Password")) ? dr.GetString(dr.GetOrdinal("Password")) : string.Empty;
                    datos.HtmlTxt = !dr.IsDBNull(dr.GetOrdinal("HtmlTxt")) ? dr.GetBoolean(dr.GetOrdinal("HtmlTxt")) : true;
                    datos.HostTxt = !dr.IsDBNull(dr.GetOrdinal("HostTxt")) ? dr.GetString(dr.GetOrdinal("HostTxt")) : string.Empty;
                    datos.PortTxt = !dr.IsDBNull(dr.GetOrdinal("PortTxt")) ? dr.GetString(dr.GetOrdinal("PortTxt")) : string.Empty;
                    datos.EnableSslTxt = !dr.IsDBNull(dr.GetOrdinal("EnableSslTxt")) ? dr.GetBoolean(dr.GetOrdinal("EnableSslTxt")) : true;
                }
                dr.Close();
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ConfiguracionModels C_Configuracion(ConfiguracionModels datos)
        {
            try
            {
                object[] parametros = { datos.idTicket, datos.CorreoTxt, datos.Password, datos.HtmlTxt, datos.HostTxt,
                                        datos.PortTxt, datos.EnableSslTxt, datos.Usuario};
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_c_ConfiguracionGeneral", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            datos.Completado = true;
                            datos.idTicket = IDRegistro;
                        }
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string ObtenerRegistrosTablaBase64ToUrl(int idTabla, DataTableAjaxPostModel dataTableAjaxPostModel, string baseDir)
        {
            try
            {
                using (var sqlcon = new SqlConnection(ConexionSql))
                {
                    using (var cmd = new SqlCommand("[dbo].[spCIDDB_Configuracion_ObtenerRegistrosTablaBase64ToUrl]", sqlcon))
                    {
                        //parametros de entrada
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@IdTabla", SqlDbType.Int).Value = idTabla; 
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
                            var firstData = true;
                            
                            while (reader.Read())
                            {
                                if (firstData)
                                {
                                    indexDatatableDto.Draw = int.Parse(reader["Draw"].ToString()); ;
                                    indexDatatableDto.RecordsFiltered = int.Parse(reader["RecordsFiltered"].ToString());
                                    indexDatatableDto.RecordsTotal = int.Parse(reader["RecordsTotal"].ToString());
                                    firstData = false;
                                }

                                var indexDto = new RegistrosDeTablaBase64ToUrlDto();

                                indexDto.Id = reader["Id"].ToString();
                                indexDto.Imagen = reader["Imagen"].ToString();

                                var uploadBase64ToServerModel =
                                    CidFaresHelper.UploadBase64ToServer(indexDto.Imagen, baseDir);

                                if (uploadBase64ToServerModel.Success)
                                {
                                    var responseDb = ActualizarRegistroPorImagen(indexDto.Id,
                                        uploadBase64ToServerModel.FileName, idTabla);
                                    indexDto.Imagen = uploadBase64ToServerModel.UrlRelative;
                                    indexDatatableDto.Data.Add(indexDto);
                                    continue;
                                }

                                var path = HostingEnvironment.MapPath(baseDir + indexDto.Imagen);
                                var fileName = indexDto.Imagen;

                                if (!File.Exists(path) || string.IsNullOrWhiteSpace(fileName))
                                {
                                    indexDto.Imagen = ProjectSettings.PathDefaultImage;
                                }
                                else
                                {
                                    indexDto.Imagen = baseDir + indexDto.Imagen;
                                }

                                indexDatatableDto.Data.Add(indexDto);
                            }
                        }

                        var json = JsonConvert.SerializeObject(indexDatatableDto);

                        reader.Close();

                        return json;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RespuestaAjax ActualizarRegistroPorImagen(string id, string fileName, int idTabla)
        {
            var respuestaDb = new RespuestaAjax();

            using (var sqlcon = new SqlConnection(ConexionSql))
            {
                using (var cmd = new SqlCommand("[dbo].[spCIDDB_ActualizarRegistroPorImagen]",
                    sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
                    cmd.Parameters.Add("@Imagen", SqlDbType.NVarChar).Value = fileName;
                    cmd.Parameters.Add("@IdTabla", SqlDbType.Int).Value = idTabla;

                    sqlcon.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            respuestaDb.Success = (bool)reader["Success"];
                            respuestaDb.Mensaje = reader["Message"].ToString();
                            if (!respuestaDb.Success)
                            {
                                respuestaDb.MensajeErrorSQL = reader["ErrorMessage"].ToString();
                            }

                        }
                    }
                    reader.Close();
                }
            }

            return respuestaDb;
        }
    }
}