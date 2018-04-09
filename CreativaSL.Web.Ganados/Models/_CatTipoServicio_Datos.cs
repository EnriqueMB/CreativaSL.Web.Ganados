using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatTipoServicio_Datos
    {
        public CatTipoServicioModels ObtenerDetalleCatTipoServicio(CatTipoServicioModels datos)
        {
            try
            {
                object[] parametros = { datos.IDTipoServicio };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatTipoServicioXID", parametros);
                while (dr.Read())
                {
                    datos.IDTipoServicio = !dr.IsDBNull(dr.GetOrdinal("IDTipoServicio")) ? dr.GetString(dr.GetOrdinal("IDTipoServicio")) : string.Empty;
                    datos.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatTipoServicioModels EliminarTipoServicio(CatTipoServicioModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDTipoServicio , datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatTipoServicio", parametros);
                datos.IDTipoServicio = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDTipoServicio))
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
        public CatTipoServicioModels AcCatTipoServicio(CatTipoServicioModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Opcion, datos.IDTipoServicio, datos.Descripcion, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatTipoServicio", parametros);
                datos.IDTipoServicio = aux.ToString();

                if (!string.IsNullOrEmpty(datos.IDTipoServicio))
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
        public CatTipoServicioModels ObtenerListaTipoServicio(CatTipoServicioModels datos)
        {
            try
            {
                List<CatTipoServicioModels> Lista = new List<CatTipoServicioModels>();
                CatTipoServicioModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatTipoServicio");
                while (dr.Read())
                {
                    Item = new CatTipoServicioModels();
                    Item.IDTipoServicio = !dr.IsDBNull(dr.GetOrdinal("id_tipoServicioMant")) ? dr.GetString(dr.GetOrdinal("id_tipoServicioMant")) : string.Empty;
                    Item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    Lista.Add(Item);
                }
                datos.listaTipoServicio = Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}