﻿using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
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
                    Item.Dinero = !dr.IsDBNull(dr.GetOrdinal("dinero")) ? dr.GetDecimal(dr.GetOrdinal("dinero")) : 0;

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

        public string DatatableGanadoActual(CatGanadoModels venta)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(venta.Conexion, "[dbo].[spCSLDB_Transferancia_get_DatatableGanadoActual]");
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ActializarGanado(string Conexion, string IdUsuario, DataTable Ganado, string IdSucursal, Int64 IdCorral)
        {
            try
            {
                DataSet dt = SqlHelper.ExecuteDataset(Conexion, CommandType.StoredProcedure, "[dbo].[spCSLDB_Transferancia_ActializarGanado]",
                new SqlParameter("@IdUsuario", IdUsuario),
                new SqlParameter("@tbl_GanadoParaVenta", Ganado),
                new SqlParameter("@IDSucursal", IdSucursal),
                new SqlParameter("@IDCorral", IdCorral));
                return Convert.ToInt32(dt.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public bool PuedeTransferirGanado(string conexion, string id_persona)
        {
            SqlDataReader Dr = SqlHelper.ExecuteReader(conexion, "[dbo].[spCSLDB_ganado_puedeTransferirGanado]", id_persona);
            RespuestaAjax respuesta = new RespuestaAjax();

            while (Dr.Read())
            {
                respuesta.Success = !Dr.IsDBNull(Dr.GetOrdinal("success")) ? Dr.GetBoolean(Dr.GetOrdinal("success")) : false;
            }
            Dr.Close();

            return respuesta.Success;
        }
    }
}