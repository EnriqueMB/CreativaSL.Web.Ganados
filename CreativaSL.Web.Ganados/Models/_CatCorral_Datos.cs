using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatCorral_Datos
    {
        public List<CatCorralModels> ObtenerListaCorral(CatCorralModels datos)
        {
            try
            {
                List<CatCorralModels> Lista = new List<CatCorralModels>();
                CatCorralModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.conexion, "spCSLDB_Catalogo_get_CatCorral");
                while (dr.Read())
                {
                    Item = new CatCorralModels();
                    Item.Id_corral = !dr.IsDBNull(dr.GetOrdinal("IDCorral")) ? dr.GetInt16(dr.GetOrdinal("IDCorral")) : 0;
                    Item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    Lista.Add(Item);
                }
                dr.Close();
                return Lista;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatCorralModels insertCorral(CatCorralModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Opcion,datos.Id_corral,datos.Descripcion, datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.conexion, "spCSLDB_Catalogo_ac_CatCorral", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            datos.Completado = true;
                            datos.Id_corral = IDRegistro;
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

        public CatCorralModels ObtenerDetalleCatCorral(CatCorralModels datos)
        {
            try
            {
                object[] parametros = { datos.Id_corral };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.conexion, "spCSLDB_Catalogo_get_CatCorralkXID", parametros);
                while (dr.Read())
                {
                    datos.Id_corral = !dr.IsDBNull(dr.GetOrdinal("IDCorral")) ? dr.GetInt16(dr.GetOrdinal("IDCorral")) : 0;
                    datos.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    
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