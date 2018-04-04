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