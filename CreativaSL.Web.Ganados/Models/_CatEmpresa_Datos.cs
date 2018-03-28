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
    }
}