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
        public CatTipoClienteModels AcCatTipoCliente(CatTipoClienteModels datos)
        {
            try
            {
                int Resultado = 0;
                object[] parametros =
                {
                    datos.Opcion,datos.id_tipoCliente,datos.descripcion ,datos.Usuario
                };
                SqlDataReader dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_ac_CatTipoCliente", parametros);
                while (dr.Read())
                {
                    Resultado = !dr.IsDBNull(dr.GetOrdinal("resultado")) ? dr.GetInt32(dr.GetOrdinal("resultado")) : 0;
                    if (Resultado == 1)
                    {
                        datos.Completado = true;
                    }
                    else
                    {
                        datos.Completado = false;
                    }
                    break;
                }
                dr.Close();
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatTipoClienteModels EliminarTipoCliente(CatTipoClienteModels datos)
        {
            try
            {
                int Resultado = 0;
                object[] parametros =
                {
                    datos.id_tipoCliente, datos.Usuario
                };
                SqlDataReader dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_del_CatTipoCliente", parametros);
                while (dr.Read())
                {
                    Resultado = !dr.IsDBNull(dr.GetOrdinal("id_TipoCliente")) ? dr.GetInt32(dr.GetOrdinal("id_TipoCliente")) : 0;
                    if (Resultado == 1)
                    {
                        datos.Completado = true;
                    }
                    else
                    {
                        datos.Completado = false;
                    }
                    break;
                }
                dr.Close();
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatTipoClienteModels ObtenerDetalleCatTipoCliente(CatTipoClienteModels datos)
        {
            try
            {
                object[] parametros = { datos.id_tipoCliente };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatTipoClienteXID", parametros);
                while (dr.Read())
                {

                    datos.id_tipoCliente = Convert.ToInt32(dr["id_tipoProveedor"].ToString());
                    datos.descripcion = dr["descripcion"].ToString();

                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatTipoClienteModels> ObtenerCatTipoCliente(CatTipoClienteModels Datos)
        {
            try
            {
                List<CatTipoClienteModels> lista = new List<CatTipoClienteModels>();
                CatTipoClienteModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatTipoCliente");
                while (dr.Read())
                {
                    item = new CatTipoClienteModels();
                    item.id_tipoCliente = !dr.IsDBNull(dr.GetOrdinal("id_tipoCliente")) ? dr.GetInt32(dr.GetOrdinal("id_tipoCliente")) : 0;
                    item.descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;

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
    }
}