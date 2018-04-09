using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Xml;
using System.Linq;

namespace CreativaSL.Web.Ganados.Models
{
    public class _NominaDiasFestivos_Datos
    {
        public NominaDiasFestivosModels AbcCatNominaDiasFestivos(NominaDiasFestivosModels datos)
        {
            try
            {
                object[] parametros = { datos.Opcion, datos.IdDiasFestivos, datos.Descripcion, datos.fecha, datos.Usuario };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Nomina_ac_DiasFestivos", parametros);
                datos.IdDiasFestivos = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IdDiasFestivos))
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
        public NominaDiasFestivosModels EliminarNomDiasFestivos(NominaDiasFestivosModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IdDiasFestivos, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Nomina_del_DiasFestivosXID", parametros);
                datos.IdDiasFestivos = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IdDiasFestivos))
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
        public NominaDiasFestivosModels ObtenerDetalleNomDiasFestivosxID(NominaDiasFestivosModels datos)
        {
            try
            {
                object[] parametros = { datos.IdDiasFestivos };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Nomina_get_DiasFesitvosXId", parametros);
                while (dr.Read())
                {
                    datos.IdDiasFestivos = !dr.IsDBNull(dr.GetOrdinal("IDDiasFestivos")) ? dr.GetString(dr.GetOrdinal("IDDiasFestivos")) : string.Empty;// Convert.ToInt32(dr["ID_UnidadMedida"].ToString());
                    datos.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    datos.fecha = !dr.IsDBNull(dr.GetOrdinal("Fecha")) ? dr.GetDateTime(dr.GetOrdinal("Fecha")) : DateTime.Today;

                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NominaDiasFestivosModels ObtenerListaNominaDias(NominaDiasFestivosModels datos)
        {
            try
            {
                List<NominaDiasFestivosModels> Lista = new List<NominaDiasFestivosModels>();
                NominaDiasFestivosModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Nomina_get_DiasFesitvos");
                while (dr.Read())
                {
                    Item = new NominaDiasFestivosModels();
                    Item.IdDiasFestivos = !dr.IsDBNull(dr.GetOrdinal("IDDiasFestivos")) ? dr.GetString(dr.GetOrdinal("IDDiasFestivos")) : string.Empty;// Convert.ToInt32(dr["ID_UnidadMedida"].ToString());
                    Item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    Item.fecha = !dr.IsDBNull(dr.GetOrdinal("Fecha")) ? dr.GetDateTime(dr.GetOrdinal("Fecha")) : DateTime.Today;
                    Lista.Add(Item);
                }
                datos.LNominaDias = Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}