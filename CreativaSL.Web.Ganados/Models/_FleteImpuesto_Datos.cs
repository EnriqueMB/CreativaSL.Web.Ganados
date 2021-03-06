﻿using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _FleteImpuesto_Datos
    {
        public string GetJsonTableFleteImpuestoXIDDocumentoDetalle(FleteImpuestoModels FleteImpuesto)
        {
            try
            {
                object[] parametros =
                {
                    FleteImpuesto.IDFlete,
                    FleteImpuesto.Id_detalleDoctoCobrar
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(FleteImpuesto.Conexion, "spCSLDB_FleteImpuesto_get_FleteImpuestoXIDDocumentoDetalle", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string GetJsonTableFleteImpuestoXIDFlete(FleteImpuestoModels FleteImpuesto)
        {
            try
            {
                object[] parametros =
                {
                    FleteImpuesto.IDFlete
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(FleteImpuesto.Conexion, "spCSLDB_FleteImpuesto_get_FleteImpuestoXIDFlete", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public FleteImpuestoModels GetFleteImpuestoXIDFleteImpuesto(FleteImpuestoModels FleteImpuesto)
        {
            try
            {
                SqlDataReader dr = null;

                object[] parametros =
                {
                    FleteImpuesto.IDFleteImpuesto
                };
                dr = SqlHelper.ExecuteReader(FleteImpuesto.Conexion, "spCSLDB_FleteImpuesto_get_FleteImpuestoXIDFleteImpuesto", parametros);
                while (dr.Read())
                {
                    FleteImpuesto.Impuesto.Clave = !dr.IsDBNull(dr.GetOrdinal("id_impuesto")) ? dr.GetInt16(dr.GetOrdinal("id_impuesto")) : 0;
                    FleteImpuesto.TipoFactor.Clave = !dr.IsDBNull(dr.GetOrdinal("id_tipoFactor")) ? dr.GetInt16(dr.GetOrdinal("id_tipoFactor")) : 0;
                    FleteImpuesto.TipoImpuesto.Clave = !dr.IsDBNull(dr.GetOrdinal("id_tipoImpuesto")) ? dr.GetInt16(dr.GetOrdinal("id_tipoImpuesto")) : 0;
                    FleteImpuesto.Base = !dr.IsDBNull(dr.GetOrdinal("base")) ? dr.GetDecimal(dr.GetOrdinal("base")) : 0;
                    FleteImpuesto.TasaCuota = !dr.IsDBNull(dr.GetOrdinal("tasaCuota")) ? dr.GetDecimal(dr.GetOrdinal("tasaCuota")) : 0;
                    FleteImpuesto.Importe = !dr.IsDBNull(dr.GetOrdinal("importe")) ? dr.GetDecimal(dr.GetOrdinal("importe")) : 0;
                    FleteImpuesto.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                }
                dr.Close();
                return FleteImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CFDI_ImpuestoModels> GetListadoImpuesto(FleteImpuestoModels FleteImpuesto)
        {

            try
            {
                CFDI_ImpuestoModels Impuesto;
                FleteImpuesto.ListaImpuesto = new List<CFDI_ImpuestoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(FleteImpuesto.Conexion, "spCSLDB_Combo_get_CFDIImpuesto");
                while (dr.Read())
                {
                    Impuesto = new CFDI_ImpuestoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    FleteImpuesto.ListaImpuesto.Add(Impuesto);
                }
                dr.Close();
                return FleteImpuesto.ListaImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CFDI_TipoImpuestoModels> GetListadoTipoImpuesto(FleteImpuestoModels FleteImpuesto)
        {

            try
            {
                CFDI_TipoImpuestoModels TipoImpuesto;
                FleteImpuesto.ListaTipoImpuesto = new List<CFDI_TipoImpuestoModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(FleteImpuesto.Conexion, "spCSLDB_Combo_get_CFDITipoImpuesto");
                while (dr.Read())
                {
                    TipoImpuesto = new CFDI_TipoImpuestoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    FleteImpuesto.ListaTipoImpuesto.Add(TipoImpuesto);
                }
                dr.Close();
                return FleteImpuesto.ListaTipoImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CFDI_TipoFactorModels> GetListadoTipoFactor(FleteImpuestoModels FleteImpuesto)
        {

            try
            {
                CFDI_TipoFactorModels TipoFactor;
                FleteImpuesto.ListaTipoFactor = new List<CFDI_TipoFactorModels>();

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(FleteImpuesto.Conexion, "spCSLDB_Combo_get_CFDITipoFactor");
                while (dr.Read())
                {
                    TipoFactor = new CFDI_TipoFactorModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                    };

                    FleteImpuesto.ListaTipoFactor.Add(TipoFactor);
                }
                dr.Close();
                return FleteImpuesto.ListaTipoFactor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public FleteImpuestoModels FleteImpuesto_ac_FleteImpuesto(FleteImpuestoModels FleteImpuesto)
        {
            try
            {
                object[] parametros =
                {
                    FleteImpuesto.IDFlete,
                    FleteImpuesto.IDFleteImpuesto,//@id_documentoCobrarDetalleImpuesto char(36)
                    FleteImpuesto.Id_detalleDoctoCobrar,//,@id_detalleDoctoCobrar char(36)
                    FleteImpuesto.TipoImpuesto.Clave,//,@id_tipoImpuesto smallint
                    FleteImpuesto.Impuesto.Clave,//,@id_impuesto smallint
                    FleteImpuesto.TipoFactor.Clave,//,@id_tipoFactor smallint
                    FleteImpuesto.Base,//,@base decimal(18,3)
                    FleteImpuesto.TasaCuota,//,@tasaCuota decimal(18,6)
                    FleteImpuesto.Importe,//,@importe money
                    FleteImpuesto.Usuario//,@id_usuario char(36)
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(FleteImpuesto.Conexion, "spCSLDB_FleteImpuesto_ac_FleteImpuesto", parametros);
                while (dr.Read())
                {
                    FleteImpuesto.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    FleteImpuesto.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                    FleteImpuesto.IDFlete = !dr.IsDBNull(dr.GetOrdinal("id_flete")) ? dr.GetString(dr.GetOrdinal("id_flete")) : string.Empty;
                }
                dr.Close();
                return FleteImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public FleteImpuestoModels FleteImpuesto_del_FleteImpuesto(FleteImpuestoModels FleteImpuesto)
        {
            try
            {
                object[] parametros =
                {
                    FleteImpuesto.IDFlete
                    ,FleteImpuesto.IDFleteImpuesto
                    ,FleteImpuesto.Usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(FleteImpuesto.Conexion, "spCSLDB_FleteImpuesto_del_FleteImpuestoXIDFlete", parametros);
                while (dr.Read())
                {
                    FleteImpuesto.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    FleteImpuesto.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                }
                dr.Close();
                return FleteImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}