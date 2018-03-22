using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatProductos_Datos
    {
        public CatProductosModels AcCatProductos(CatProductosModels datos)
        {
            try
            {
                object[] parametros =
                {
                   datos.Opcion,datos.IDProducto,datos.Clave,datos.nombre,datos.Descripcion,datos.Clave_cfdi,datos.Usuario
                    };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatProducto", parametros);
                datos.IDProducto = aux.ToString();

                if (!string.IsNullOrEmpty(datos.IDProducto))
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
        public CatProductosModels EliminarProducto(CatProductosModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDProducto, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatProducto", parametros);
                datos.IDProducto = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDProducto))
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
        public CatProductosModels ObtenerDetalleCatProducto(CatProductosModels datos)
        {
            try
            {
                object[] parametros = { datos.IDProducto };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatProductoXID", parametros);
                while (dr.Read())
                {
                    datos.IDProducto = dr["id_producto"].ToString();
                    datos.Clave = dr["clave"].ToString();
                    datos.nombre = dr["nombre"].ToString();
                    datos.Descripcion = dr["descripcion"].ToString();
                    datos.Clave_cfdi = dr["clave_cfdi"].ToString();
                    
                 }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatProductosModels> ObtenerCatProductos(CatProductosModels Datos)
        {
            try
            {
                List<CatProductosModels> lista = new List<CatProductosModels>();
                CatProductosModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatProductos");
                while (dr.Read())
                {
                    item = new CatProductosModels();
                    item.IDProducto = dr["id_producto"].ToString();
                    item.Clave = dr["clave"].ToString();
                    item.nombre = dr["nombre"].ToString();
                    item.Descripcion = dr["descripcion"].ToString();
                    item.Clave_cfdi = dr["clave_cfdi"].ToString();
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}