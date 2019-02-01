using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatTipoCliente_Datos
    {
        public RespuestaAjax AcCatTipoCliente(CatTipoClienteModels datos, string conexion, string usuario, int opcion)
        {
            try
            {
                object[] parametros =
                {
                    opcion, datos.Id_tipoCliente, datos.Descripcion , usuario
                };
                SqlDataReader dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_Catalogo_ac_CatTipoCliente", parametros);
                RespuestaAjax respuesta = new RespuestaAjax();
                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    break;
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatTipoClienteModels ObtenerDetalleCatTipoCliente(CatTipoClienteModels datos, string conexion)
        {
            try
            {
                object[] parametros = { datos.Id_tipoCliente };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_Catalogo_get_CatTipoClienteXID", parametros);
                while (dr.Read())
                {

                    datos.Id_tipoCliente = !dr.IsDBNull(dr.GetOrdinal("id_tipoCliente")) ? dr.GetInt32(dr.GetOrdinal("id_tipoCliente")) : 0;
                    datos.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;

                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RespuestaAjax EliminarTipoCliente(int id, string usuario, string conexion)
        {
            try
            {
                object[] parametros =
                {
                    id, usuario
                };
                SqlDataReader dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_Catalogo_del_CatTipoCliente", parametros);
                RespuestaAjax respuesta = new RespuestaAjax();

                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    break;
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string CatTipoCliente_index_CatTipoCliente(string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "CatTipoCliente_index_CatTipoCliente");
                string datatable = Auxiliar.SqlReaderToJson(dr);

                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}