using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Xml;
using System.Linq;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatProveedorAlmacen_Datos
    {
        public CatProveedorAlmacenModels AbcCatProveedorAlmacen(CatProveedorAlmacenModels datos)
        {
            try
            {
                object[] parametros = { datos.Opcion, datos.IDProveedorAlmacen, datos.Descripcion, datos.RFC,datos.Usuario };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatProveedorAlmacen", parametros);
                datos.IDProveedorAlmacen =aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDProveedorAlmacen))
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
        public CatProveedorAlmacenModels EliminarProveedorAlmacen(CatProveedorAlmacenModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDProveedorAlmacen, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatProveedorAlmacen", parametros);
                datos.IDProveedorAlmacen = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDProveedorAlmacen))
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
        public CatProveedorAlmacenModels ObtenerDetalleProveedorAlmacenxID(CatProveedorAlmacenModels datos)
        {
            try
            {
                object[] parametros = { datos.IDProveedorAlmacen };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorAlmacenXID", parametros);
                while (dr.Read())
                {
                    datos.IDProveedorAlmacen = !dr.IsDBNull(dr.GetOrdinal("IDProveedor")) ? dr.GetString(dr.GetOrdinal("IDProveedor")) : string.Empty;// Convert.ToInt32(dr["ID_UnidadMedida"].ToString());
                    datos.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    datos.RFC = !dr.IsDBNull(dr.GetOrdinal("Rfc")) ? dr.GetString(dr.GetOrdinal("Rfc")) : string.Empty;

                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatProveedorAlmacenModels ObtenerListaProveedorAlmacen(CatProveedorAlmacenModels datos)
        {
            try
            {
                List<CatProveedorAlmacenModels> Lista = new List<CatProveedorAlmacenModels>();
                CatProveedorAlmacenModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorAlmacen");
                while (dr.Read())
                {
                    Item = new CatProveedorAlmacenModels();
                    Item.IDProveedorAlmacen = !dr.IsDBNull(dr.GetOrdinal("IDProveedor")) ? dr.GetString(dr.GetOrdinal("IDProveedor")) : string.Empty;
                    Item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    Item.RFC= !dr.IsDBNull(dr.GetOrdinal("Rfc")) ? dr.GetString(dr.GetOrdinal("Rfc")) : string.Empty;
                    Lista.Add(Item);
                }
                datos.LProveedorA= Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}