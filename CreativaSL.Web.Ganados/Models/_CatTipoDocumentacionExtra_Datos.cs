using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CreativaSL.Web.Ganados.Models.Datatable;
using CreativaSL.Web.Ganados.Models.Dto.Base;
using CreativaSL.Web.Ganados.Models.Dto.CatModulos;
using CreativaSL.Web.Ganados.Models.Dto.CatTipoDocumentacionExtra;
using CreativaSL.Web.Ganados.Models.System;
using Newtonsoft.Json;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatTipoDocumentacionExtra_Datos : BaseSQL
    {
        
        #region DataTable
        public string JsonIndex(DataTableAjaxPostModel dataTableAjaxPostModel)
        {
            try
            {
                using (var sqlcon = new SqlConnection(ConexionSql))
                {
                    using (var cmd = new SqlCommand("spCSLDB_Catalogo_CatTipoDocumentacionExtra_ObtenerIndex", sqlcon))
                    {
                        //parametros de entrada
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Start", SqlDbType.Int).Value = dataTableAjaxPostModel.start;
                        cmd.Parameters.Add("@Length", SqlDbType.Int).Value = dataTableAjaxPostModel.length;
                        cmd.Parameters.Add("@SearchValue", SqlDbType.NVarChar).Value = dataTableAjaxPostModel.search.value;
                        cmd.Parameters.Add("@Draw", SqlDbType.Int).Value = dataTableAjaxPostModel.draw;
                        cmd.Parameters.Add("@ColumnNumber", SqlDbType.Int).Value = dataTableAjaxPostModel.order[0].column;
                        cmd.Parameters.Add("@ColumnDir", SqlDbType.NVarChar).Value = dataTableAjaxPostModel.order[0].dir;

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

                                var indexDto = new IndexCatTipoDocumentacionExtraDto
                                {
                                    IdTipoDocumentacionExtra = reader["IdTipoDocumentacionExtra"].ToString(),
                                    Descripcion = reader["NombreTipoDocumentacionExtra"].ToString(),
                                    Modulos = reader["Modulos"].ToString()
                                };

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
        #endregion

        #region Create y Edit
        public CreateEditCatTipoDocumentacionExtraDto ObtenerTipoDocumentacionExtra(int IdCatTipoDocumentacionExtra)
        {
            using (var sqlcon = new SqlConnection(ConexionSql))
            {
                using (var cmd = new SqlCommand("spCIDDB_Catalogo_CatTipoDocumentacionExtra_ObtenerTipoDocumentacionExtra", sqlcon))
                {
                    //parametros de entrada
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdTipoDocumentacionExtra", SqlDbType.Int).Value = IdCatTipoDocumentacionExtra;

                    // execute
                    sqlcon.Open();

                    var reader = cmd.ExecuteReader();
                    var model = new CreateEditCatTipoDocumentacionExtraDto
                    {
                        CatTipoDocumentacionExtra = new CatTipoDocumentacionExtraModel(),
                        ListaModulosDto = new List<SelectCatModuloDto>()
                    };

                    if (reader.HasRows)
                    {
                        var firstData = true;

                        while (reader.Read())
                        {
                            if (firstData)
                            {
                                model.CatTipoDocumentacionExtra.IdTipoDocumentacionExtra = int.Parse(reader["IdTipoDocumentacionExtra"].ToString());
                                model.CatTipoDocumentacionExtra.Descripcion = reader["DescripcionTipoDocumentoExtra"].ToString();
                                firstData = false;
                            }

                            var item = new SelectCatModuloDto
                            {
                                Modulo = new CatModulosModel(),
                                Seleccionado = (bool)reader["Seleccionado"]
                            };
                            item.Modulo.IdModulo = int.Parse(reader["IdModulo"].ToString());
                            item.Modulo.Descripcion = reader["Descripcion"].ToString();
                            model.ListaModulosDto.Add(item);
                        }
                    }
                    reader.Close();
                    return model;
                }
            }
        }

        public RespuestaAjax GuardarTipoDocumentacionExtra(CreateEditCatTipoDocumentacionExtraDto documentacionExtra)
        {
            using (var sqlcon = new SqlConnection(ConexionSql))
            {
                using (var cmd = new SqlCommand("[dbo].[spCIDDB_Catalogo_CatTipoDocumentacionExtra_GuardarTipoDocumentacionExtra]", sqlcon))
                {
                    //parametros de entrada
                    cmd.CommandType = CommandType.StoredProcedure;

                    var  dataTable = new DataTable();

                    dataTable.Columns.Add("IdModulo", typeof(int));

                    foreach (var moduloDto in documentacionExtra.ListaModulosDto.Where(moduloDto => moduloDto.Seleccionado))
                    {
                        dataTable.Rows.Add(moduloDto.Modulo.IdModulo);
                    }

                    
                    cmd.Parameters.Add("@IdTipoDocumentacionExtra", SqlDbType.Int).Value = documentacionExtra.CatTipoDocumentacionExtra.IdTipoDocumentacionExtra;
                    cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar).Value = documentacionExtra.CatTipoDocumentacionExtra.Descripcion;
                    cmd.Parameters.Add("@Usuario", SqlDbType.Char).Value = UsuarioActual;
                    cmd.Parameters.Add("@UDTT_DocumentosXSucursal", SqlDbType.Structured).Value = dataTable;

                    // execute
                    sqlcon.Open();

                    var reader = cmd.ExecuteReader();
                    var respuestaAjax = new RespuestaAjax();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            respuestaAjax.Success = (bool) reader["Success"];
                            respuestaAjax.Mensaje = reader["Mensaje"].ToString();

                            if (!respuestaAjax.Success)
                            {
                                respuestaAjax.MensajeErrorSQL = reader["MensajeErrorSQL"].ToString();
                            }
                        }
                    }
                    reader.Close();
                    return respuestaAjax;
                }
            }
        }
        #endregion

        #region Delete
        public RespuestaAjax EliminarTipoDocumentacionExtra(int IdTipoDoumentacionExtra)
        {
            using (var sqlcon = new SqlConnection(ConexionSql))
            {
                using (var cmd = new SqlCommand("[dbo].[spCIDDB_Catalogo_CatTipoDocumentacionExtra_EliminarTipoDocumentacionExtra]", sqlcon))
                {
                    //parametros de entrada
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdTipoDocumentacionExtra", SqlDbType.NVarChar).Value = IdTipoDoumentacionExtra;
                    cmd.Parameters.Add("@Usuario", SqlDbType.Char).Value = UsuarioActual;

                    // execute
                    sqlcon.Open();

                    var reader = cmd.ExecuteReader();
                    var respuestaAjax = new RespuestaAjax();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            respuestaAjax.Success = (bool)reader["Success"];
                            respuestaAjax.Mensaje = reader["Mensaje"].ToString();

                            if (!respuestaAjax.Success)
                            {
                                respuestaAjax.MensajeErrorSQL = reader["MensajeErrorSQL"].ToString();
                            }
                        }
                    }
                    reader.Close();
                    return respuestaAjax;
                }
            }
        }
        #endregion

        #region Metodos diversos

        public List<CatTipoDocumentacionExtraModel> ObtenerComboCatTiposDocumentacionExtraXIdModulo(int idModulo)
        {
            var list = new List<CatTipoDocumentacionExtraModel>{
                new CatTipoDocumentacionExtraModel
                {
                    IdTipoDocumentacionExtra = 0
                    , Descripcion = "-- Seleccione --"
                }
            };
            using (var sqlcon = new SqlConnection(ConexionSql))
            {
                using (var cmd = new SqlCommand("spCSLDB_Catalogo_CatTipoDocumentacionExtra_ObtenerModulosXIdModulo", sqlcon))
                {
                    //parametros de entrada
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdModulo", SqlDbType.Int).Value = idModulo;

                    // execute
                    sqlcon.Open();

                    var reader = cmd.ExecuteReader();
                    

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new CatTipoDocumentacionExtraModel
                            {
                                IdTipoDocumentacionExtra = int.Parse(reader["IdTipoDocumentacionExtra"].ToString()),
                                Descripcion = reader["Descripcion"].ToString()
                            });
                        }
                    }
                    reader.Close();
                }
            }

            return list;
        }
        #endregion
    }
}