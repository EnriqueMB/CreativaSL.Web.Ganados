using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatSocio_Datos
    {
        public List<CatSociosModels> ObtenerListaSocios(CatSociosModels Datos)
        {
            try
            {
                List<CatSociosModels> lista = new List<CatSociosModels>();
                CatSociosModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatSocios");
                while (dr.Read())
                {
                    item = new CatSociosModels();
                    item.IDSocio = !dr.IsDBNull(dr.GetOrdinal("IDSocio")) ? dr.GetString(dr.GetOrdinal("IDSocio")) : string.Empty;
                    item.NombreCompleto = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty;
                    item.Procentaje = !dr.IsDBNull(dr.GetOrdinal("Porcentaje")) ? dr.GetInt32(dr.GetOrdinal("Porcentaje")) : 0;
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

        public CatSociosModels ABCatSocios(CatSociosModels Datos)
        {
            try
            {
                object[] parametros =
                {
                    Datos.Opcion, Datos.IDSocio, Datos.NombreCompleto, Datos.Procentaje, Datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Catalogo_ac_CatSocios", parametros);
                Datos.IDSocio = Resultado.ToString();
                if (!string.IsNullOrEmpty(Datos.IDSocio))
                {
                    Datos.Completado = true;
                }
                else
                {
                    Datos.Completado = false;
                }
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatSociosModels ObternerDetalleCatSocio(CatSociosModels Datos)
        {
            try
            {
                object[] parametros = { Datos.IDSocio };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatSociosXID", parametros);
                while (dr.Read())
                {
                    Datos.IDSocio = !dr.IsDBNull(dr.GetOrdinal("IDSocio")) ? dr.GetString(dr.GetOrdinal("IDSocio")) : string.Empty;
                    Datos.NombreCompleto = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty;
                    Datos.Procentaje = !dr.IsDBNull(dr.GetOrdinal("Porcentaje")) ? dr.GetInt32(dr.GetOrdinal("Porcentaje")) : 0;
                }
                dr.Close();
                return Datos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CatSociosModels EliminarSocio(CatSociosModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDSocio, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatSocios", parametros);
                datos.IDSocio = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDSocio))
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
    }
}