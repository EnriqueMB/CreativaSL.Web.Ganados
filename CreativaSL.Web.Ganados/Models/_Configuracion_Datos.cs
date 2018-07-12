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
                    item.textoTicket1 = !dr.IsDBNull(dr.GetOrdinal("TextoTicketUno")) ? dr.GetString(dr.GetOrdinal("TextoTicketUno")) : string.Empty;
                    item.textoTicket2 = !dr.IsDBNull(dr.GetOrdinal("TextoTicketDos")) ? dr.GetString(dr.GetOrdinal("TextoTicketDos")) : string.Empty;
                    item.textoTicket3 = !dr.IsDBNull(dr.GetOrdinal("TextoTicketTres")) ? dr.GetString(dr.GetOrdinal("TextoTicketTres")) : string.Empty;
                    item.pagosDiasFestivos = !dr.IsDBNull(dr.GetOrdinal("PagoDiasFestivos")) ? dr.GetDecimal(dr.GetOrdinal("PagoDiasFestivos")) :0;
                    item.pagosDiasDomingos = !dr.IsDBNull(dr.GetOrdinal("PagoDiasDomingo")) ? dr.GetDecimal(dr.GetOrdinal("PagoDiasDomingo")) : 0;
                    item.pagosDiasVacaciones = !dr.IsDBNull(dr.GetOrdinal("PagoDiasVacaciones")) ? dr.GetDecimal(dr.GetOrdinal("PagoDiasVacaciones")) : 0;
                    item.retardosFaltas = !dr.IsDBNull(dr.GetOrdinal("RetardosFalta")) ? dr.GetInt32(dr.GetOrdinal("RetardosFalta")) : 0;
                    lista.Add(item);
                }
                dr.Close();
                return datos.listaTicket = lista;
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
                    datos.textoTicket1 = !dr.IsDBNull(dr.GetOrdinal("TextoTicketUno")) ? dr.GetString(dr.GetOrdinal("TextoTicketUno")) : string.Empty;
                    datos.textoTicket2 = !dr.IsDBNull(dr.GetOrdinal("TextoTicketDos")) ? dr.GetString(dr.GetOrdinal("TextoTicketDos")) : string.Empty;
                    datos.textoTicket3 = !dr.IsDBNull(dr.GetOrdinal("TextoTicketTres")) ? dr.GetString(dr.GetOrdinal("TextoTicketTres")) : string.Empty;
                    datos.pagosDiasFestivos = !dr.IsDBNull(dr.GetOrdinal("PagoDiasFestivos")) ? dr.GetDecimal(dr.GetOrdinal("PagoDiasFestivos")) : 0;
                    datos.pagosDiasDomingos = !dr.IsDBNull(dr.GetOrdinal("PagoDiasDomingo")) ? dr.GetDecimal(dr.GetOrdinal("PagoDiasDomingo")) : 0;
                    datos.pagosDiasVacaciones = !dr.IsDBNull(dr.GetOrdinal("PagoDiasVacaciones")) ? dr.GetDecimal(dr.GetOrdinal("PagoDiasVacaciones")) : 0;
                    datos.retardosFaltas = !dr.IsDBNull(dr.GetOrdinal("RetardosFalta")) ? dr.GetInt32(dr.GetOrdinal("RetardosFalta")) : 0;
           
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
                object[] parametros = { datos.idTicket, datos.pagosDiasFestivos, datos.retardosFaltas, datos.pagosDiasVacaciones, datos.pagosDiasDomingos,
                                        datos.textoTicket1, datos.textoTicket2, datos.textoTicket3, datos.Usuario};
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