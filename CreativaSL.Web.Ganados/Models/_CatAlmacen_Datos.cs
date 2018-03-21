using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatAlmacen_Datos
    {
        public CatAlmacenModels AcCatAlmacen(CatAlmacenModels datos)
        {
            try
            {
                object[] parametros =
                {
                   datos.Opcion,datos.IDAlmacen,datos.IDSucursal,datos.ClaveAlmacen,datos.Descripcion,datos.Usuario
                    };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatAlmacen", parametros);
                datos.IDAlmacen = aux.ToString();

                if (!string.IsNullOrEmpty(datos.IDAlmacen))
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
        public CatAlmacenModels ObtenerDetalleCatAlmacen(CatAlmacenModels datos)
        {
            try
            {
                object[] parametros = { datos.IDAlmacen };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatAlmacenXID", parametros);
                while (dr.Read())
                {
                    datos.IDAlmacen = dr["id_almacen"].ToString();
                    datos.IDSucursal = dr["id_sucursal"].ToString();
                    datos.ClaveAlmacen = dr["clave"].ToString();
                    datos.Descripcion = dr["descripcion"].ToString();

                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatAlmacenModels> ObtenerCatAlmacen(CatAlmacenModels Datos)
        {
            try
            {
                List<CatAlmacenModels> lista = new List<CatAlmacenModels>();
                CatAlmacenModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatAlmacen");
                while (dr.Read())
                {
                    item = new CatAlmacenModels();
                    item.IDAlmacen = dr["id_almacen"].ToString();
                    item.nombreAlmacen = dr["nombreSuc"].ToString();
                    item.ClaveAlmacen = dr["clave"].ToString();
                    item.Descripcion = dr["descripcion"].ToString();
                   
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public CatAlmacenModels EliminarAlmacen(CatAlmacenModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDAlmacen, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatAlmacen", parametros);
                datos.IDAlmacen = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDAlmacen))
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
        public List<CatSucursalesModels> obtenerListaSucursales(CatAlmacenModels Datos)
        {
            try
            {
                List<CatSucursalesModels> lista = new List<CatSucursalesModels>();
                CatSucursalesModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_ComboSucursales");
                while (dr.Read())
                {
                    item = new CatSucursalesModels();
                    item.IDSucursal = dr["id_sucursal"].ToString();
                    item.NombreSucursal = dr["nombre"].ToString();

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