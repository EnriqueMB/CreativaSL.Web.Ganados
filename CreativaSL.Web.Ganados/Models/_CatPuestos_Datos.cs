using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatPuestos_Datos
    {
        public CatPuestoModels ObtenerListaPuesto(CatPuestoModels datos)
        {
            try
            {
                List<CatPuestoModels> Lista = new List<CatPuestoModels>();
                CatPuestoModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatPuesto");
                while (dr.Read())
                {
                    Item = new CatPuestoModels();
                    Item.IDPuesto = !dr.IsDBNull(dr.GetOrdinal("IDPuesto")) ? dr.GetInt16(dr.GetOrdinal("IDPuesto")) : 0;
                    Item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    Item.EsGerente = !dr.IsDBNull(dr.GetOrdinal("EsGerente")) ? dr.GetBoolean(dr.GetOrdinal("EsGerente")) : false;
                    Lista.Add(Item);
                }
                dr.Close();
                datos.ListaPuesto = Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatPuestoModels AbcCatPuesto(CatPuestoModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Opcion, datos.IDPuesto, datos.Descripcion, datos.EsGerente, datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatPuestos", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            datos.Completado = true;
                            datos.IDPuesto = IDRegistro;
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

        public CatPuestoModels EliminarPuesto(CatPuestoModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDPuesto, datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatPuesto", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            datos.Completado = true;
                            datos.IDPuesto = IDRegistro;
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

        public CatPuestoModels ObtenerDetalleCatPuesto(CatPuestoModels datos)
        {
            try
            {
                object[] parametros = { datos.IDPuesto };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatPuestoXID", parametros);
                while (dr.Read())
                {
                    datos.IDPuesto = !dr.IsDBNull(dr.GetOrdinal("IDPuesto")) ? dr.GetInt16(dr.GetOrdinal("IDPuesto")) : 0;
                    datos.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    datos.EsGerente = !dr.IsDBNull(dr.GetOrdinal("EsGerente")) ? dr.GetBoolean(dr.GetOrdinal("EsGerente")) : false;
                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}