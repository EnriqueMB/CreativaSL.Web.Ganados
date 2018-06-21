using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatEvento_Datos
    {
        public string DatatableIndex(CatTipoEventoEnvioModels Evento)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_CatEvento_get_Index");
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RespuestaAjax AC_Evento(CatTipoEventoEnvioModels Evento)
        {
            try
            {
                object[] parametros = 
                {
                    Evento.IDTipoEventoEnvio,       Evento.Descripcion,
                    Evento.MarcarMerma,             Evento.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_CatEvento_ac_Evento", parametros);
                RespuestaAjax RespuestaAjax = new RespuestaAjax();

                while (dr.Read())
                {
                    RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("Success")) ? dr.GetBoolean(dr.GetOrdinal("Success")) : false;
                    RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje")) ? dr.GetString(dr.GetOrdinal("Mensaje")) : string.Empty;
                }

                dr.Close();
                return RespuestaAjax;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatTipoEventoEnvioModels GET_EventoXIDEvento(CatTipoEventoEnvioModels Evento)
        {
            try
            {
                object[] parametros =
                {
                    Evento.IDTipoEventoEnvio
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Evento.Conexion, "spCSLDB_CatEvento_get_EventoXIDEvento", parametros);
                Evento.RespuestaAjax = new RespuestaAjax();

                while (dr.Read())
                {
                    Evento.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("Success")) ? dr.GetBoolean(dr.GetOrdinal("Success")) : false;
                    if (Evento.RespuestaAjax.Success)
                    {
                        Evento.IDTipoEventoEnvio = !dr.IsDBNull(dr.GetOrdinal("id_tipoEventoEnvio")) ? dr.GetInt32(dr.GetOrdinal("id_tipoEventoEnvio")) : 0;
                        Evento.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                        Evento.MarcarMerma = !dr.IsDBNull(dr.GetOrdinal("marcarMerma")) ? dr.GetBoolean(dr.GetOrdinal("marcarMerma")) : false;
                    }
                    else
                    {
                        Evento.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje")) ? dr.GetString(dr.GetOrdinal("Mensaje")) : string.Empty;
                    }
                }

                dr.Close();
                return Evento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}