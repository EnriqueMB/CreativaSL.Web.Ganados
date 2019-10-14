using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatSucursal_Datos 
    {
        public List<CatLugarModels> GetLugares(CatSucursalesModels Sucursal)
        {
            try
            {
                SqlDataReader dr = null;
                CatLugarModels lugar;
                List<CatLugarModels> listaLugares = new List<CatLugarModels>();

                dr = SqlHelper.ExecuteReader(Sucursal.Conexion, "spCSLDB_Combo_get_AllCatLugar");
                while (dr.Read())
                {
                    lugar = new CatLugarModels
                    {
                        id_lugar = !dr.IsDBNull(dr.GetOrdinal("IDLugar")) ? dr.GetString(dr.GetOrdinal("IDLugar")) : string.Empty,
                        descripcion = !dr.IsDBNull(dr.GetOrdinal("NombreLugar")) ? dr.GetString(dr.GetOrdinal("NombreLugar")) : string.Empty,
                        latitud = float.Parse(dr["GpsLatitud"].ToString()),
                        longitud = float.Parse(dr["GpsLongitud"].ToString())
                    };
                    listaLugares.Add(lugar);
                }
                dr.Close();
                return listaLugares;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<CatSucursalesModels> GetSucursalesXIDEmpresa(CatSucursalesModels Sucursal)
        {
            try
            {
                object[] parametros =
                {
                    Sucursal.IDEmpresa
                };
                SqlDataReader dr = null;
                CatSucursalesModels itemSucursal;

                dr = SqlHelper.ExecuteReader(Sucursal.Conexion, "spCSLDB_CATSUCURSAL_get_SucursalesXIDEmpresa", parametros);
                while (dr.Read())
                {
                    itemSucursal = new CatSucursalesModels
                    {
                        IDSucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty,
                        NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("nombreSuc")) ? dr.GetString(dr.GetOrdinal("nombreSuc")) : string.Empty,
                        Direccion = !dr.IsDBNull(dr.GetOrdinal("direccion")) ? dr.GetString(dr.GetOrdinal("direccion")) : string.Empty,
                        IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("id_empresa")) ? dr.GetString(dr.GetOrdinal("id_empresa")) : string.Empty
                    };
                    Sucursal.ListaSucursales.Add(itemSucursal);
                }
                dr.Close();
                return Sucursal.ListaSucursales;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CatSucursalesModels AC_Sucursal(CatSucursalesModels Sucursal)
        {
            try
            {
                object[] parametros =
                {
                    Sucursal.IDSucursal,
                    Sucursal.IDEmpresa,
                    Sucursal.NombreSucursal,
                    Sucursal.Direccion,
                    0, //Sucursal.MermaPredeterminada,
                    Sucursal.Usuario,
                    Sucursal.IDLugar
                };
                SqlDataReader dr = null;

                dr = SqlHelper.ExecuteReader(Sucursal.Conexion, "spCSLDB_CATSUCURSAL_ac_Sucursal", parametros);
                while (dr.Read())
                {
                    Sucursal.Completado = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                    Sucursal.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                dr.Close();
                return Sucursal;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CatSucursalesModels GetSucursalXIDSucursal(CatSucursalesModels Sucursal)
        {
            try
            {
                object[] parametros =
                {
                    Sucursal.IDSucursal
                };
                SqlDataReader dr = null;

                dr = SqlHelper.ExecuteReader(Sucursal.Conexion, "spCSLDB_CATSUCURSAL_get_SucursalXIDSucursal", parametros);
                while (dr.Read())
                {
                    Sucursal.IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("id_empresa")) ? dr.GetString(dr.GetOrdinal("id_empresa")) : string.Empty;
                    Sucursal.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("nombreSuc")) ? dr.GetString(dr.GetOrdinal("nombreSuc")) : string.Empty;
                    Sucursal.Direccion = !dr.IsDBNull(dr.GetOrdinal("direccion")) ? dr.GetString(dr.GetOrdinal("direccion")) : string.Empty;
                    //Sucursal.MermaPredeterminada = !dr.IsDBNull(dr.GetOrdinal("mermaPredeterminada")) ? dr.GetDecimal(dr.GetOrdinal("mermaPredeterminada")) : 0;
                    Sucursal.IDLugar = !dr.IsDBNull(dr.GetOrdinal("id_lugar")) ? dr.GetString(dr.GetOrdinal("id_lugar")) : string.Empty;

                }
                dr.Close();
                return Sucursal;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CatSucursalesModels Del_Sucursal(CatSucursalesModels Sucursal)
        {
            try
            {
                object[] parametros =
                {
                    Sucursal.IDSucursal,
                    Sucursal.Usuario
                };
                SqlDataReader dr = null;

                dr = SqlHelper.ExecuteReader(Sucursal.Conexion, "spCSLDB_CATSUCURSAL_del_Sucursal", parametros);
                while (dr.Read())
                {
                    Sucursal.Completado = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                    Sucursal.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                dr.Close();
                return Sucursal;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<CatSucursalesModels> GetSucursales(string conexion)
        {
            List<CatSucursalesModels> lista = new List<CatSucursalesModels>();
            SqlDataReader sqlDataReader = null;
            sqlDataReader = SqlHelper.ExecuteReader(conexion, "[dbo].[spCIDDB_CATSUCURSAL_get_Sucursales]");
            while (sqlDataReader.Read())
            {
                CatSucursalesModels item = new CatSucursalesModels();
                item.IDSucursal = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("id_sucursal")) ? sqlDataReader.GetString(sqlDataReader.GetOrdinal("id_sucursal")) : string.Empty;
                item.NombreSucursal = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("nombreSuc")) ? sqlDataReader.GetString(sqlDataReader.GetOrdinal("nombreSuc")) : string.Empty;
                lista.Add(item);
            }
            sqlDataReader.Close();
            return lista;
        }
        public List<CatSucursalesModels> GetSucursalesXPersona(string conexion, string id_persona)
        {
            List<CatSucursalesModels> lista = new List<CatSucursalesModels>();
            SqlDataReader sqlDataReader = null;
            object[] parametros = 
            {
                id_persona
            };
            sqlDataReader = SqlHelper.ExecuteReader(conexion, "[dbo].[spCIDDB_CATSUCURSAL_get_SucursalesXPersona]", parametros);
            while (sqlDataReader.Read())
            {
                CatSucursalesModels item = new CatSucursalesModels();
                item.IDSucursal = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("id_sucursal")) ? sqlDataReader.GetString(sqlDataReader.GetOrdinal("id_sucursal")) : string.Empty;
                lista.Add(item);
           }
            sqlDataReader.Close();
            return lista;
        }
        //public RespuestaAjax SetSucursalesXPersona(string[] sucursales, string id_persona, string conexion)
        public RespuestaAjax SetSucursalesXPersona(List<string> sucursales, string id_persona, string conexion)
        {
            RespuestaAjax respuestaAjax = new RespuestaAjax();

            using (SqlConnection sqlcon = new SqlConnection(conexion))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[spCIDDB_CATSUCURSAL_set_PersonasXSucursal]", sqlcon))
                {
                    //parametros de entrada
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dataTable;
                    dataTable = new DataTable();

                    dataTable.Columns.Add("Id", typeof(string));
                    if(sucursales != null)
                    {
                        foreach (var item in sucursales)
                        {
                            dataTable.Rows.Add(item);
                        }
                    }
                    

                    cmd.Parameters.Add("@id_persona", SqlDbType.Char).Value = id_persona;
                    cmd.Parameters.Add("@UDTT_Sucursales", SqlDbType.Structured).Value = dataTable;

                    //parametros de salida
                    cmd.Parameters.Add(new SqlParameter("@mensaje", SqlDbType.NVarChar, -1)); //-1 para tipo MAX
                    cmd.Parameters["@mensaje"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@success", SqlDbType.Bit));
                    cmd.Parameters["@success"].Direction = ParameterDirection.Output;
                    // execute
                    sqlcon.Open();

                    cmd.ExecuteNonQuery();
                    respuestaAjax.Mensaje = cmd.Parameters["@mensaje"].Value.ToString();

                    bool success = false;

                    if (Boolean.TryParse(cmd.Parameters["@success"].Value.ToString(), out success))
                    {
                        respuestaAjax.Success = success;
                    }
                }
            }
            
            return respuestaAjax;
        }
        public MetaXSucursal GetMetaXSucursal(string id_sucursal, string conexion)
        {
            MetaXSucursal metaXSucursal = new MetaXSucursal();
            metaXSucursal.Success = true;
            SqlDataReader sqlDataReader = null;
            object[] parametros =
            {
                id_sucursal
            };
            sqlDataReader = SqlHelper.ExecuteReader(conexion, "[Meta].[GetMetaXSucursal]", parametros);

            while (sqlDataReader.Read())
            {
                metaXSucursal.Success = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("success")) ? sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("success")) : false;
                metaXSucursal.Mensaje = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("mensaje")) ? sqlDataReader.GetString(sqlDataReader.GetOrdinal("mensaje")) : string.Empty;
                metaXSucursal.MensajeErrorSQL = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("mensajeErrorSQL")) ? sqlDataReader.GetString(sqlDataReader.GetOrdinal("mensajeErrorSQL")) : string.Empty;

                if (metaXSucursal.Success)
                {
                    metaXSucursal.Id = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("id")) ? sqlDataReader.GetString(sqlDataReader.GetOrdinal("id")) : string.Empty;
                    metaXSucursal.Id_sucursal = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("id_sucursal")) ? sqlDataReader.GetString(sqlDataReader.GetOrdinal("id_sucursal")) : string.Empty;
                    metaXSucursal.CantidadKilo = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("cantidadKilo")) ? sqlDataReader.GetDecimal(sqlDataReader.GetOrdinal("cantidadKilo")) : 0;
                    metaXSucursal.CantidadGanado = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("cantidadGanado")) ? sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("cantidadGanado")) : 0;
                }
            }

            sqlDataReader.Close();

            return metaXSucursal;
        }
        public MetaXSucursal SetMetaXSucursal(string conexion, MetaXSucursal metaXSucursal)
        {
            SqlDataReader sqlDataReader = null;
            object[] parametros =
            {
                  metaXSucursal.Id
                , metaXSucursal.Id_sucursal
                , metaXSucursal.CantidadGanado
                , metaXSucursal.CantidadKilo
            };
            sqlDataReader = SqlHelper.ExecuteReader(conexion, "[Meta].[SetMetaXSucursal]", parametros);

            while (sqlDataReader.Read())
            {
                metaXSucursal.Success = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("success")) ? sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("success")) : false;
                metaXSucursal.Mensaje = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("mensaje")) ? sqlDataReader.GetString(sqlDataReader.GetOrdinal("mensaje")) : string.Empty;
                metaXSucursal.MensajeErrorSQL = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("mensajeErrorSQL")) ? sqlDataReader.GetString(sqlDataReader.GetOrdinal("mensajeErrorSQL")) : string.Empty;
            }

            sqlDataReader.Close();

            return metaXSucursal;
        }
        public List<HomeMetaXSucursalDTO> GetMetasXSucursalToday(List<string> sucursales, string conexion)
        {
            List<HomeMetaXSucursalDTO> lista = new List<HomeMetaXSucursalDTO>();

            using (SqlConnection sqlcon = new SqlConnection(conexion))
            {
                using (SqlCommand cmd = new SqlCommand("Meta.GetMetaToday", sqlcon))
                {
                    //parametros de entrada
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dataTable;
                    dataTable = new DataTable();

                    dataTable.Columns.Add("Id", typeof(string));
                    if (sucursales != null)
                    {
                        foreach (var item in sucursales)
                        {
                            dataTable.Rows.Add(item);
                        }
                    }

                    cmd.Parameters.Add("@sucursales", SqlDbType.Structured).Value = dataTable;

                    // execute
                    sqlcon.Open();

                    SqlDataReader sqlDataReader = cmd.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        HomeMetaXSucursalDTO homeMetaXSucursalDTO = new HomeMetaXSucursalDTO
                        {
                            NombreSucursal = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("nombreSucursal")) ? sqlDataReader.GetString(sqlDataReader.GetOrdinal("nombreSucursal")) : string.Empty,
                            SucursalCantidadGanado = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("sucursalCantidadGanado")) ? sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("sucursalCantidadGanado")) : 0,
                            SucursalCantidadKilo = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("sucursalCantidadKilo")) ? sqlDataReader.GetDecimal(sqlDataReader.GetOrdinal("sucursalCantidadKilo")) : 0,
                            MetaCantidadGanado = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("metaCantidadGanado")) ? sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("metaCantidadGanado")) : 0,
                            MetaCantidadKilo = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("metaCantidadKilo")) ? sqlDataReader.GetDecimal(sqlDataReader.GetOrdinal("metaCantidadKilo")) : 0
                        };

                        lista.Add(homeMetaXSucursalDTO);
                    }
                    sqlDataReader.Close();
                }
            }

            return lista;
        }
    }
}
