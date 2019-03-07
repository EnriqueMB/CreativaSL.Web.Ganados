using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatDeduccion_Datos
    {
        public List<CatDeduccionModels> SpCIDDB_CatDeduccion_get_index(string conexion)
        {
            try
            {
                List<CatDeduccionModels> lista = new List<CatDeduccionModels>();
                CatDeduccionModels item = new CatDeduccionModels();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "dbo.spCIDDB_CatDeduccion_get_index");

                while (dr.Read())
                {
                    item = new CatDeduccionModels();
                    item.Id = !dr.IsDBNull(dr.GetOrdinal("id")) ? dr.GetInt32(dr.GetOrdinal("id")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    item.EsSistema = !dr.IsDBNull(dr.GetOrdinal("esSistema")) ? dr.GetBoolean(dr.GetOrdinal("esSistema")) : false;

                    lista.Add(item);
                }
                dr.Close();

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CatDeduccionModels> SpCIDDB_Combo_get_CatDeduccion(string conexion)
        {
            try
            {
                List<CatDeduccionModels> lista = new List<CatDeduccionModels>();
                CatDeduccionModels item = new CatDeduccionModels();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "[dbo].[spCIDDB_Combo_get_CatDeduccion]");

                while (dr.Read())
                {
                    item = new CatDeduccionModels();
                    item.Id = !dr.IsDBNull(dr.GetOrdinal("id")) ? dr.GetInt32(dr.GetOrdinal("id")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    item.EsSistema = !dr.IsDBNull(dr.GetOrdinal("esSistema")) ? dr.GetBoolean(dr.GetOrdinal("esSistema")) : false;

                    lista.Add(item);
                }
                dr.Close();

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RespuestaAjax SpCIDDB_Catalogo_ac_CatDeduccion(string conexion, int opcion, string usuario, CatDeduccionModels catDeduccion)
        {
            try
            {
                object[] parametros =
                {
                    opcion, 
                    catDeduccion.Id,
                    catDeduccion.Descripcion,
                    catDeduccion.EsSistema,
                    usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCIDDB_Catalogo_ac_CatDeduccion", parametros);
                RespuestaAjax respuesta = new RespuestaAjax();
                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CatDeduccionModels SpCIDDB_CatDeduccion_get_deduccionXId(string conexion, int id)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "dbo.spCIDDB_CatDeduccion_get_deduccionXId", id);
                CatDeduccionModels item = new CatDeduccionModels();

                while (dr.Read())
                {
                    item.Id = !dr.IsDBNull(dr.GetOrdinal("id")) ? dr.GetInt32(dr.GetOrdinal("id")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    item.EsSistema = !dr.IsDBNull(dr.GetOrdinal("esSistema")) ? dr.GetBoolean(dr.GetOrdinal("esSistema")) : false;
                }
                dr.Close();
                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RespuestaAjax SpCIDDB_CatDeduccion_del_deduccionXId(string conexion, int id, string usuario)
        {
            try
            {
                RespuestaAjax respuesta = new RespuestaAjax();
                SqlDataReader dr = null;
                object[] parametros = { id, usuario };
                dr = SqlHelper.ExecuteReader(conexion, "dbo.spCIDDB_CatDeduccion_del_deduccionXId", parametros);

                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }

                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}