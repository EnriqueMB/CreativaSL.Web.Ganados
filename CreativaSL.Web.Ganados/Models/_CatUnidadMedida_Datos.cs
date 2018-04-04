using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Xml;
using System.Linq;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatUnidadMedida_Datos
    {
        public CatUnidadMedidaModels AbcCatUnidadMedida(CatUnidadMedidaModels datos)
        {
            try
            {
                object[] parametros = {datos.Opcion,datos.IDUnidadMedida,datos.Descripcion,datos.Usuario };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatUnidadMedida", parametros);
                datos.IDUnidadMedida = Convert.ToInt32(aux.ToString());
                if (aux != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(aux.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            datos.Completado = true;
                            datos.IDUnidadMedida = IDRegistro;
                        }
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CatUnidadMedidaModels EliminarUnidad(CatUnidadMedidaModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDUnidadMedida, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatUnidadMedida", parametros);
                datos.IDUnidadMedida = Convert.ToInt32(aux.ToString());
                if (aux != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(aux.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            datos.Completado = true;
                            datos.IDUnidadMedida = IDRegistro;
                        }
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatUnidadMedidaModels ObtenerDetalleUnidadxID(CatUnidadMedidaModels datos)
        {
            try
            {
                object[] parametros = { datos.IDUnidadMedida };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatUnidadMedidaXID", parametros);
                while (dr.Read())
                {
                    datos.IDUnidadMedida = !dr.IsDBNull(dr.GetOrdinal("IDUnidad")) ? dr.GetInt16(dr.GetOrdinal("IDUnidad")) : -1;// Convert.ToInt32(dr["ID_UnidadMedida"].ToString());
                    datos.Descripcion = dr["descripcion"].ToString();
                    
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public CatUnidadMedidaModels ObtenerListaUnidad(CatUnidadMedidaModels datos)
        {
            try
            {
                List<CatUnidadMedidaModels> Lista = new List<CatUnidadMedidaModels>();
                CatUnidadMedidaModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatUnidadMedida");
                while (dr.Read())
                {
                    Item = new CatUnidadMedidaModels();
                    Item.IDUnidadMedida = !dr.IsDBNull(dr.GetOrdinal("IDUnidad")) ? dr.GetInt16(dr.GetOrdinal("IDUnidad")) : 0;
                    Item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    Lista.Add(Item);
                }
                datos.LUnidades = Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}