using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{

    public class _CatProductosAlmacen_Datos
    {

        public CatProductosAlmacenModels AcCatProductosAlmacen(CatProductosAlmacenModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Opcion, datos.IDProductoAlmacen,datos.IDTipoCodigo,datos.Clave,datos.Nombre,datos.Descripcion,
                    datos.UltimoCosto,datos.Almacen,datos.IDUnidadMedida,datos.Imagen,datos.BandImg,
                    datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatProductosAlmacen", parametros);
                datos.IDProductoAlmacen = Resultado.ToString();
                if (!string.IsNullOrEmpty(datos.IDProductoAlmacen))
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
        public List<CatProductosAlmacenModels> ObtenerCatProductosAlmacen(CatProductosAlmacenModels Datos)
        {
            try
            {
                List<CatProductosAlmacenModels> lista = new List<CatProductosAlmacenModels>();
                CatProductosAlmacenModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatProductosAlmacen");
                while (dr.Read())
                {
                    item = new CatProductosAlmacenModels();
                    item.IDProductoAlmacen = dr["id_productoAlmacen"].ToString();
                    item.Nombre = dr["nombre"].ToString();
                    item.UltimoCosto = Convert.ToDecimal(dr["ultimoCosto"].ToString());
                    item.Clave = dr["clave"].ToString();
                   
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public CatProductosAlmacenModels ObtenerDetalleCatProductoAlmacen(CatProductosAlmacenModels datos)
        {
            try
            {
                object[] parametros = { datos.IDProductoAlmacen };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatProductosAlmacenXID", parametros);
                while (dr.Read())
                {
                    datos.IDProductoAlmacen = dr["id_productoAlmacen"].ToString();
                    datos.IDTipoCodigo = Convert.ToInt32(dr["id_tipoCodigo"].ToString());
                    datos.Nombre = dr["nombre"].ToString();
                    datos.Clave = dr["clave"].ToString();
                    datos.Descripcion = dr["descripcion"].ToString();
                    datos.UltimoCosto = Convert.ToDecimal(dr["ultimoCosto"].ToString());
                    datos.Imagen = dr["imagen"].ToString();
                   
                   
                    datos.Almacen = Convert.ToBoolean(dr["almacen"].ToString());
                    datos.IDUnidadMedida = Convert.ToInt32(dr["id_unidadMedidaPrincipal"].ToString());
                   
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatProductosAlmacenModels EliminarProductoAlmancen(CatProductosAlmacenModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDProductoAlmacen, datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatProductosAlmacen", parametros);
                if (Resultado != null)
                {
                    if (!string.IsNullOrEmpty(datos.IDProductoAlmacen))
                    {
                        datos.Completado = true;
                    }
                    else
                    {
                        datos.Completado = false;
                    }
                    return datos;
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatUnidadMedidaModels> obtenerComboCatUnidadMedida(CatProductosAlmacenModels Datos)
        {
            try
            {
                List<CatUnidadMedidaModels> lista = new List<CatUnidadMedidaModels>();
                CatUnidadMedidaModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatUnidadMedida");
                while (dr.Read())
                {
                    item = new CatUnidadMedidaModels();
                    item.IDUnidadMedida = Convert.ToInt32(dr["IDUnidadMedida"].ToString());
                    item.Descripcion = dr["Descripcion"].ToString();

                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CatTipoCodigoProductoModels> obtenerComboCatTipoCodigo(CatProductosAlmacenModels Datos)
        {
            try
            {
                List<CatTipoCodigoProductoModels> lista = new List<CatTipoCodigoProductoModels>();
                CatTipoCodigoProductoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatTipoCodigoProducto");
                while (dr.Read())
                {
                    item = new CatTipoCodigoProductoModels();
                    item.IDTipoCodigo = Convert.ToInt32(dr["IDTipoCodigo"].ToString());
                    item.Descripcion = dr["Descripcion"].ToString();
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