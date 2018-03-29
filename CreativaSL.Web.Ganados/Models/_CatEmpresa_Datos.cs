using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatEmpresa_Datos
    {
        public List<CatEmpresaModels> GetListadoEmpresas(CatEmpresaModels Empresa)
        {
            try
            {
                CatEmpresaModels ItemEmpresa;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Empresa.Conexion, "spCSLDB_EMPRESA_GetListadoEmpresas");
                while (dr.Read())
                {
                    ItemEmpresa = new CatEmpresaModels
                    {
                        IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("id_empresa")) ? dr.GetString(dr.GetOrdinal("id_empresa")) : string.Empty,
                        LogoEmpresa = !dr.IsDBNull(dr.GetOrdinal("logoEmpresa")) ? dr.GetString(dr.GetOrdinal("logoEmpresa")) : string.Empty,
                        RazonFiscal = !dr.IsDBNull(dr.GetOrdinal("razonFiscal")) ? dr.GetString(dr.GetOrdinal("razonFiscal")) : string.Empty,
                        DireccionFiscal = !dr.IsDBNull(dr.GetOrdinal("direccionFiscal")) ? dr.GetString(dr.GetOrdinal("direccionFiscal")) : string.Empty,
                        RFC = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty,
                        Representante = !dr.IsDBNull(dr.GetOrdinal("representante")) ? dr.GetString(dr.GetOrdinal("representante")) : string.Empty,
                        NumTelefonico1 = !dr.IsDBNull(dr.GetOrdinal("numTelefonico1")) ? dr.GetString(dr.GetOrdinal("numTelefonico1")) : string.Empty,
                        NumTelefonico2 = !dr.IsDBNull(dr.GetOrdinal("numTelefonico2")) ? dr.GetString(dr.GetOrdinal("numTelefonico2")) : string.Empty,
                        Email = !dr.IsDBNull(dr.GetOrdinal("email")) ? dr.GetString(dr.GetOrdinal("email")) : string.Empty,
                        HorarioAtencion = !dr.IsDBNull(dr.GetOrdinal("horarioAtencion")) ? dr.GetString(dr.GetOrdinal("horarioAtencion")) : string.Empty
                    };
                    
                    Empresa.ListaEmpresas.Add(ItemEmpresa);
                }
                return Empresa.ListaEmpresas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatEmpresaModels GetEmpresaXID(CatEmpresaModels Empresa)
        {
            try
            {
                object[] parametros =
                {
                    Empresa.IDEmpresa
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Empresa.Conexion, "spCSLDB_EMPRESA_GetEmpresasXID", parametros);
                while (dr.Read())
                {
                    Empresa.LogoEmpresa = !dr.IsDBNull(dr.GetOrdinal("logoEmpresa")) ? dr.GetString(dr.GetOrdinal("logoEmpresa")) : string.Empty;
                    Empresa.LogoRFC = !dr.IsDBNull(dr.GetOrdinal("logoRFC")) ? dr.GetString(dr.GetOrdinal("logoRFC")) : string.Empty;
                    Empresa.RazonFiscal = !dr.IsDBNull(dr.GetOrdinal("razonFiscal")) ? dr.GetString(dr.GetOrdinal("razonFiscal")) : string.Empty;
                    Empresa.DireccionFiscal = !dr.IsDBNull(dr.GetOrdinal("direccionFiscal")) ? dr.GetString(dr.GetOrdinal("direccionFiscal")) : string.Empty;
                    Empresa.RFC = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty;
                    Empresa.Representante = !dr.IsDBNull(dr.GetOrdinal("representante")) ? dr.GetString(dr.GetOrdinal("representante")) : string.Empty;
                    Empresa.NumTelefonico1 = !dr.IsDBNull(dr.GetOrdinal("numTelefonico1")) ? dr.GetString(dr.GetOrdinal("numTelefonico1")) : string.Empty;
                    Empresa.NumTelefonico2 = !dr.IsDBNull(dr.GetOrdinal("numTelefonico2")) ? dr.GetString(dr.GetOrdinal("numTelefonico2")) : string.Empty;
                    Empresa.Email = !dr.IsDBNull(dr.GetOrdinal("email")) ? dr.GetString(dr.GetOrdinal("email")) : string.Empty;
                    Empresa.HorarioAtencion = !dr.IsDBNull(dr.GetOrdinal("horarioAtencion")) ? dr.GetString(dr.GetOrdinal("horarioAtencion")) : string.Empty;
               }
                return Empresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatEmpresaModels UpdateEmpresaXID(CatEmpresaModels Empresa)
        {
            try
            {
                object[] parametros =
                {
                    Empresa.IDEmpresa, // @id_empresa CHAR(36)
                    Empresa.RazonFiscal,//,@razonFiscal NVARCHAR(150)
                    Empresa.DireccionFiscal,//,@direccionFiscal NVARCHAR(150)
                    Empresa.RFC,//,@RFC NVARCHAR(20)
                    Empresa.NumTelefonico1,//,@numTelefonico1 NVARCHAR(15)
                    Empresa.NumTelefonico2,//,@numTelefonico2 NVARCHAR(15)
                    Empresa.Email,//,@email NVARCHAR(50)
                    Empresa.LogoEmpresa,//,@logoEmpresa NVARCHAR(MAX)
                    Empresa.HorarioAtencion,//,@horarioAtencion NVARCHAR(150)
                    Empresa.Representante,//,@representante NVARCHAR(100)
                    Empresa.LogoRFC,//,@logoRFC NVARCHAR(MAX)
                    Empresa.IDUsuario//,@id_usuario CHAR(36)
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Empresa.Conexion, "spCSLDB_EMPRESA_UpdateEmpresaXID", parametros);
                
                while (dr.Read())
                {
                    Empresa.MensajeJson = !dr.IsDBNull(dr.GetOrdinal("Mensaje")) ? dr.GetString(dr.GetOrdinal("Mensaje")) : string.Empty;
                    Empresa.Error       = !dr.IsDBNull(dr.GetOrdinal("Error")) ? dr.GetBoolean(dr.GetOrdinal("Error")) : true;
                }
                return Empresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}