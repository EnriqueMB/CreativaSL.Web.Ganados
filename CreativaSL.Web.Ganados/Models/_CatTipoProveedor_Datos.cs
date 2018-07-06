using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatTipoProveedor_Datos
    {
        public CatTipoProveedorModels AcCatProveedor(CatTipoProveedorModels datos)
        {
            try
            {
                int Resultado = 0;
                object[] parametros =
                {
                    datos.Opcion,datos.IDTipoProveedor,datos.Descripcion ,datos.Usuario
                };
                SqlDataReader dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_ac_CatTipoProveedor", parametros);
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
        public CatTipoProveedorModels EliminarProveedor(CatTipoProveedorModels datos)
        {
            try
            {
                int Resultado = 0;
                object[] parametros =
                {
                    datos.IDTipoProveedor, datos.Usuario
                };
                SqlDataReader dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_del_CatTipoProveedor", parametros);
                while (dr.Read())
                {
                    Resultado = !dr.IsDBNull(dr.GetOrdinal("id_TipoProveedor")) ? dr.GetInt32(dr.GetOrdinal("id_TipoProveedor")) : 0;
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
        public CatTipoProveedorModels ObtenerDetalleCatProveedor(CatTipoProveedorModels datos)
        {
            try
            {
                object[] parametros = { datos.IDTipoProveedor };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatTipoProveedorXID", parametros);
                while (dr.Read())
                {

                    datos.IDTipoProveedor = Convert.ToInt32(dr["id_tipoProveedor"].ToString());
                    datos.Descripcion = dr["descripcion"].ToString();

                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatTipoProveedorModels> ObtenerCatProveedores(CatTipoProveedorModels Datos)
        {
            try
            {
                List<CatTipoProveedorModels> lista = new List<CatTipoProveedorModels>();
                CatTipoProveedorModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatTipoProveedor");
                while (dr.Read())
                {
                    item = new CatTipoProveedorModels();
                    item.IDTipoProveedor = Convert.ToInt32(dr["id_tipoProveedor"].ToString());
                    item.Descripcion = dr["descripcion"].ToString();

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