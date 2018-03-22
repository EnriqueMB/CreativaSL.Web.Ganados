using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatChofer_Datos
    {
        public CatChoferModels EliminarChofer(CatChoferModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDChofer, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatChofer", parametros);
                datos.IDChofer = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDChofer))
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
        public CatChoferModels AbcCatChofer(CatChoferModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Opcion, datos.IDChofer, datos.Nombre, datos.ApPaterno, datos.ApMaterno,datos.Licencia,datos.numLicencia,datos.vigencia, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "EM_spCSLDB_abc_Chofer", parametros);
                datos.IDChofer = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDChofer))
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

        public List<CatChoferModels> ObtenerCatChofer(CatChoferModels Datos)
        {
            try
            {
                List<CatChoferModels> lista = new List<CatChoferModels>();
                CatChoferModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "EM_spCSLDB_get_Chofer");
                while (dr.Read())
                {
                    item = new CatChoferModels();
                    item.IDChofer = dr["IDChofer"].ToString();
                    item.Nombre = dr["NombreChofer"].ToString();
                    item.Licencia = Convert.ToBoolean(dr["Licencia"].ToString());
                    item.Estatus = Convert.ToBoolean(dr["Estatus"].ToString());
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public CatChoferModels ObtenerDetalleCatChofer(CatChoferModels datos)
        {
            try
            {
                object[] parametros = { datos.IDChofer };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "EM_spCSLDB_get_ChoferXID", parametros);
                while (dr.Read())
                {
                    datos.IDChofer = dr["IDChofer"].ToString();
                    datos.Nombre = dr["Nombre"].ToString();
                    datos.ApPaterno = dr["ApPaterno"].ToString();
                    datos.ApMaterno = dr["ApMaterno"].ToString();
                    datos.Licencia = dr.GetBoolean(dr.GetOrdinal("Licencia"));
                    datos.numLicencia = dr["numLicencia"].ToString();
                    datos.vigencia = dr.GetDateTime(dr.GetOrdinal("vigencia"));
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatGeneroModels> ObteneComboCatGenero(CatChoferModels Datos)
        {
            try
            {
                List<CatGeneroModels> lista = new List<CatGeneroModels>();
                CatGeneroModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatGenero");
               // lista.Add(new CatGeneroModels { IDGenero = string.Empty, NombreSucursal = " - Seleccione -" });
                while (dr.Read())
                {
                    item = new CatGeneroModels();
                    item.IDGenero = !dr.IsDBNull(dr.GetOrdinal("IDGenero")) ? dr.GetInt32(dr.GetOrdinal("IDGenero")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
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