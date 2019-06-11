using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatGanado_Datos
    {
        public CatGanadoModels ObtenerGanado(CatGanadoModels datos)
        {
            try
            {
                List<CatGanadoModels> Lista = new List<CatGanadoModels>();
                CatGanadoModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_get_Ganados");
                while (dr.Read())
                {
                    Item = new CatGanadoModels();
                    Item.IDGanado = !dr.IsDBNull(dr.GetOrdinal("IDGanado")) ? dr.GetString(dr.GetOrdinal("IDGanado")) : string.Empty;
                    Item.NumArete = !dr.IsDBNull(dr.GetOrdinal("NumArete")) ? dr.GetString(dr.GetOrdinal("NumArete")) : string.Empty; 
                    Item.Genero = !dr.IsDBNull(dr.GetOrdinal("Genero")) ? dr.GetString(dr.GetOrdinal("Genero")) : string.Empty; 
                    Item.Corral = !dr.IsDBNull(dr.GetOrdinal("Corral")) ? dr.GetString(dr.GetOrdinal("Corral")) : string.Empty; 
                    Item.Observacion = !dr.IsDBNull(dr.GetOrdinal("Observacion")) ? dr.GetString(dr.GetOrdinal("Observacion")) : string.Empty; 
                    Item.Staus = !dr.IsDBNull(dr.GetOrdinal("Estatus")) ? dr.GetString(dr.GetOrdinal("Estatus")) : string.Empty;
                    Item.PesoGanado = !dr.IsDBNull(dr.GetOrdinal("Peso")) ? dr.GetInt32(dr.GetOrdinal("Peso")) : 0;
                    Item.FechaCompra = !dr.IsDBNull(dr.GetOrdinal("Fecha")) ? dr.GetDateTime(dr.GetOrdinal("Fecha")) : DateTime.Today;
                    Item.Proveedor = !dr.IsDBNull(dr.GetOrdinal("Proveedor")) ? dr.GetString(dr.GetOrdinal("Proveedor")) : string.Empty;
                    Lista.Add(Item);
                }
                dr.Close();
                datos.ListaGanados = Lista;
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatGanadoModels ObtenerGanadoXID(CatGanadoModels datos)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_get_GanadosXIDGanado",datos.IDGanado);
                while (dr.Read())
                {
                    datos.IDGanado = !dr.IsDBNull(dr.GetOrdinal("IDGanado")) ? dr.GetString(dr.GetOrdinal("IDGanado")) : string.Empty;
                    datos.NumArete = !dr.IsDBNull(dr.GetOrdinal("NumArete")) ? dr.GetString(dr.GetOrdinal("NumArete")) : string.Empty;
                    datos.Genero = !dr.IsDBNull(dr.GetOrdinal("Genero")) ? dr.GetString(dr.GetOrdinal("Genero")) : string.Empty;
                    datos.Corral = !dr.IsDBNull(dr.GetOrdinal("Corral")) ? dr.GetString(dr.GetOrdinal("Corral")) : string.Empty;
                    datos.Observacion = !dr.IsDBNull(dr.GetOrdinal("Observacion")) ? dr.GetString(dr.GetOrdinal("Observacion")) : string.Empty;
                    datos.Staus = !dr.IsDBNull(dr.GetOrdinal("Estatus")) ? dr.GetString(dr.GetOrdinal("Estatus")) : string.Empty;
                    datos.IDTipoEventoEnvio = !dr.IsDBNull(dr.GetOrdinal("IDEstatus")) ? dr.GetInt32(dr.GetOrdinal("IDEstatus")) : 0;
                    datos.IdSucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                    datos.IdCorral = !dr.IsDBNull(dr.GetOrdinal("id_corral")) ? dr.GetInt16(dr.GetOrdinal("id_corral")) : 0;
                }
                dr.Close();
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatTipoEventoEnvioModels> ObtenerComboTipoServicio(string Conexion)
        {
            try
            {
                List<CatTipoEventoEnvioModels> Lista = new List<CatTipoEventoEnvioModels>();
                CatTipoEventoEnvioModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatTipoEventoEnvio2");
                while (Dr.Read())
                {
                    Item = new CatTipoEventoEnvioModels();
                    Item.IDTipoEventoEnvio = !Dr.IsDBNull(Dr.GetOrdinal("idTipoEvento")) ? Dr.GetInt32(Dr.GetOrdinal("idTipoEvento")) : 0;
                    Item.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("nombreEvento")) ? Dr.GetString(Dr.GetOrdinal("nombreEvento")) : string.Empty;
                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public CatGanadoModels C_Ganado(CatGanadoModels ganado)
        {
            try
            {
                object[] parametros =
                {
                    ganado.IDGanado,
                    ganado.Observacion,
                    ganado.IDTipoEventoEnvio,
                    ganado.IdCorral
                };
                object aux = SqlHelper.ExecuteScalar(ganado.Conexion, "spCSLDB_c_GanadosXIDGanado", parametros);
                ganado.IDGanado = aux.ToString();
                if (!string.IsNullOrEmpty(ganado.IDGanado))
                {
                    ganado.Completado = true;
                }
                else
                {
                    ganado.Completado = false;
                }
                return ganado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}