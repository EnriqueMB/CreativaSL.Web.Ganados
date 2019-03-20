using System;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Configuracion_Datos
    {
        public List<ConfiguracionModels> ObtenerListaTicket(ConfiguracionModels datos)
        {
            try
            {
                List<ConfiguracionModels> lista = new List<ConfiguracionModels>();
                ConfiguracionModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Get_ConfiguracionGeneral");
                while(dr.Read())
                {
                    item = new ConfiguracionModels();
                    item.idTicket = !dr.IsDBNull(dr.GetOrdinal("id_configuracion")) ? dr.GetInt32(dr.GetOrdinal("id_configuracion")) : 0;
                    item.CorreoTxt = !dr.IsDBNull(dr.GetOrdinal("CorreoTxt")) ? dr.GetString(dr.GetOrdinal("CorreoTxt")) : string.Empty;
                    item.Password = !dr.IsDBNull(dr.GetOrdinal("Password")) ? dr.GetString(dr.GetOrdinal("Password")) : string.Empty;
                    item.HtmlTxt = !dr.IsDBNull(dr.GetOrdinal("HtmlTxt")) ? dr.GetBoolean(dr.GetOrdinal("HtmlTxt")) : true;
                    item.HostTxt = !dr.IsDBNull(dr.GetOrdinal("HostTxt")) ? dr.GetString(dr.GetOrdinal("HostTxt")) : string.Empty;
                    item.PortTxt = !dr.IsDBNull(dr.GetOrdinal("PortTxt")) ? dr.GetString(dr.GetOrdinal("PortTxt")) : string.Empty;
                    item.EnableSslTxt = !dr.IsDBNull(dr.GetOrdinal("EnableSslTxt")) ? dr.GetBoolean(dr.GetOrdinal("EnableSslTxt")) : true;
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
        public ConfiguracionModels ObtenerTicket(ConfiguracionModels datos)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Get_ConfiguracionGeneralXID",datos.idTicket);
                while (dr.Read())
                {
                    datos.idTicket = !dr.IsDBNull(dr.GetOrdinal("id_configuracion")) ? dr.GetInt32(dr.GetOrdinal("id_configuracion")) : 0;
                    datos.CorreoTxt = !dr.IsDBNull(dr.GetOrdinal("CorreoTxt")) ? dr.GetString(dr.GetOrdinal("CorreoTxt")) : string.Empty;
                    datos.Password = !dr.IsDBNull(dr.GetOrdinal("Password")) ? dr.GetString(dr.GetOrdinal("Password")) : string.Empty;
                    datos.HtmlTxt = !dr.IsDBNull(dr.GetOrdinal("HtmlTxt")) ? dr.GetBoolean(dr.GetOrdinal("HtmlTxt")) : true;
                    datos.HostTxt = !dr.IsDBNull(dr.GetOrdinal("HostTxt")) ? dr.GetString(dr.GetOrdinal("HostTxt")) : string.Empty;
                    datos.PortTxt = !dr.IsDBNull(dr.GetOrdinal("PortTxt")) ? dr.GetString(dr.GetOrdinal("PortTxt")) : string.Empty;
                    datos.EnableSslTxt = !dr.IsDBNull(dr.GetOrdinal("EnableSslTxt")) ? dr.GetBoolean(dr.GetOrdinal("EnableSslTxt")) : true;
                }
                dr.Close();
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ConfiguracionModels C_Configuracion(ConfiguracionModels datos)
        {
            try
            {
                object[] parametros = { datos.idTicket, datos.CorreoTxt, datos.Password, datos.HtmlTxt, datos.HostTxt,
                                        datos.PortTxt, datos.EnableSslTxt, datos.Usuario};
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_c_ConfiguracionGeneral", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            datos.Completado = true;
                            datos.idTicket = IDRegistro;
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
    }
}