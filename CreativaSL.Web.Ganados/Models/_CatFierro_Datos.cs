using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Hosting;
using CreativaSL.Web.Ganados.Models.Datatable;
using CreativaSL.Web.Ganados.Models.Dto.Base;
using CreativaSL.Web.Ganados.Models.Dto.CatBancos;
using CreativaSL.Web.Ganados.Models.Dto.CatFierros;
using CreativaSL.Web.Ganados.Models.System;
using Newtonsoft.Json;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatFierro_Datos : BaseSQL
    {

        public string DatatableIndex(DataTableAjaxPostModel dataTableAjaxPostModel)
        {
            try
            {
                var index = string.Empty;
                using (var sqlcon = new SqlConnection(ConexionSql))
                {
                    using (var cmd = new SqlCommand("[dbo].[spCSLDB_Catalogo_get_CatFierro]", sqlcon))
                    {
                        //parametros de entrada
                        cmd.CommandType = CommandType.StoredProcedure;

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

                                var item = new IndexCatFierrosDto();
                                item.IDFierro = reader["IDFierro"].ToString();
                                item.NombreFierro = reader["NombreFierro"].ToString();
                                item.ImgFierro = reader["ImgFierro"].ToString();
                                item.Observaciones = reader["Observaciones"].ToString();

                                item.ImgFierro =
                                    Auxiliar.ValidImageFormServer(item.ImgFierro, ProjectSettings.BaseDirCatFierro);

                                indexDatatableDto.Data.Add(item);
                            }
                        }

                        index = JsonConvert.SerializeObject(indexDatatableDto);

                        reader.Close();
                    }
                }
                return index;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtener la lista de todos los fierros activos y dibujarlos 
        /// </summary>
        /// <param name="datos">Recibe la cadena de conexion</param>
        /// <returns>Retornar la lista de los fierros</returns>
        public CatFierroModels ObtenerListaFierros(CatFierroModels datos)
        {
            try
            {
                List<CatFierroModels> Lista = new List<CatFierroModels>();
                CatFierroModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatFierro");
                while (dr.Read())
                {
                    Item = new CatFierroModels();
                    Item.IDFierro = !dr.IsDBNull(dr.GetOrdinal("IDFierro")) ? dr.GetString(dr.GetOrdinal("IDFierro")) : string.Empty;
                    Item.NombreFierro = !dr.IsDBNull(dr.GetOrdinal("NombreFierro")) ? dr.GetString(dr.GetOrdinal("NombreFierro")) : string.Empty;
                    Item.ImgFierro = !dr.IsDBNull(dr.GetOrdinal("ImgFierro")) ? dr.GetString(dr.GetOrdinal("ImgFierro")) : string.Empty;
                    Item.Observaciones = !dr.IsDBNull(dr.GetOrdinal("Observaciones")) ? dr.GetString(dr.GetOrdinal("Observaciones")) : string.Empty;

                    Bitmap bmpFromString = Item.ImgFierro.Base64StringToBitmap();
                    Item.ImagenContruida = bmpFromString.ToBase64ImageTag(ImageFormat.Png);
                    Lista.Add(Item);
                }
                dr.Close();
                datos.ListaFierro = Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// AB del altas y cambios para los Fierros 
        /// </summary>
        /// <param name="datos">Recibe los datos para la alta y las modificaciones</param>
        /// <returns>Retona si fue completado con exito o un error</returns>
        public CatFierroModels AbcCatFierro(CatFierroModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Opcion, datos.IDFierro, datos.NombreFierro, datos.ImgFierro, datos.Observaciones, datos.NombreArchivo, datos.Id_servicio ?? string.Empty, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatFierro", parametros);
                datos.IDFierro = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDFierro))
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


        public CatFierroModels ActualizarImagen(CatFierroModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDFierro, datos.ImgFierro, datos.NombreArchivo, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "[dbo].[spCSLDB_Catalogo_set_CatFierroImagen]", parametros);
                datos.IDFierro = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDFierro))
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

        /// <summary>
        /// Eliminar un registro de Fierro
        /// </summary>
        /// <param name="datos">Recibe el IDFierro y el Usuario logeado</param>
        /// <returns>Retorna Si se completo con exito o un error</returns>
        public CatFierroModels EliminarFierro(CatFierroModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDFierro, datos.Usuario
                };
                var dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_del_CatFierro", parametros);

                while (dr.Read())
                {
                    datos.IDFierro = !dr.IsDBNull(dr.GetOrdinal("IdFierro")) ? dr.GetString(dr.GetOrdinal("IdFierro")) : string.Empty;
                    datos.ImgFierro = !dr.IsDBNull(dr.GetOrdinal("ImgFierro")) ? dr.GetString(dr.GetOrdinal("ImgFierro")) : string.Empty;
                }
                dr.Close();

                if (!string.IsNullOrEmpty(datos.IDFierro))
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
        public CatFierroModels ObtenerDetalleCatFierro(CatFierroModels datos)
        {
            try
            {
                object[] parametros = { datos.IDFierro };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatFierroXID", parametros);
                while (dr.Read())
                {
                    datos.IDFierro = !dr.IsDBNull(dr.GetOrdinal("IDFierro")) ? dr.GetString(dr.GetOrdinal("IDFierro")) : string.Empty;
                    datos.NombreFierro = !dr.IsDBNull(dr.GetOrdinal("NombreFierro")) ? dr.GetString(dr.GetOrdinal("NombreFierro")) : string.Empty;
                    datos.Observaciones = !dr.IsDBNull(dr.GetOrdinal("Observaciones")) ? dr.GetString(dr.GetOrdinal("Observaciones")) : string.Empty;
                    datos.ImgFierro = !dr.IsDBNull(dr.GetOrdinal("ImgFierro")) ? dr.GetString(dr.GetOrdinal("ImgFierro")) : string.Empty;
                    datos.Extension = Auxiliar.ObtenerExtensionImagenBase64(datos.ImgFierro);
                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}