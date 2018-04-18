using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _FleteImpuesto
    {
        public FleteImpuestoModels GetFleteImpuestoXIDFleteImpuesto(FleteImpuestoModels FleteImpuesto)
        {
            try
            {
                SqlDataReader dr = null;

                object[] parametros =
                {
                    FleteImpuesto.IDFleteImpuesto
                };
                dr = SqlHelper.ExecuteReader(FleteImpuesto.Conexion, "spCSLDB_Flete_get_FleteImpuestoXIDFleteImpuesto", parametros);
                while (dr.Read())
                {
                    FleteImpuesto.IDFlete = !dr.IsDBNull(dr.GetOrdinal("id_flete")) ? dr.GetString(dr.GetOrdinal("id_flete")) : string.Empty;
                    FleteImpuesto.Impuesto.Clave = !dr.IsDBNull(dr.GetOrdinal("id_impuesto")) ? dr.GetInt16(dr.GetOrdinal("id_impuesto")) : 0;
                    FleteImpuesto.TipoFactor.Clave = !dr.IsDBNull(dr.GetOrdinal("id_tipoFactor")) ? dr.GetInt16(dr.GetOrdinal("id_tipoFactor")) : 0;
                    FleteImpuesto.TipoImpuesto.Clave = !dr.IsDBNull(dr.GetOrdinal("id_tipoImpuesto")) ? dr.GetInt16(dr.GetOrdinal("id_tipoImpuesto")) : 0;
                    FleteImpuesto.Base = !dr.IsDBNull(dr.GetOrdinal("base")) ? dr.GetDecimal(dr.GetOrdinal("base")) : 0;
                    FleteImpuesto.TasaCuota = !dr.IsDBNull(dr.GetOrdinal("tasaCuota")) ? dr.GetDecimal(dr.GetOrdinal("tasaCuota")) : 0;
                    FleteImpuesto.Importe = !dr.IsDBNull(dr.GetOrdinal("importe")) ? dr.GetDecimal(dr.GetOrdinal("importe")) : 0;
                }
                return FleteImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}