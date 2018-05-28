using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _RptProveedorMermaAlta_Datos
    {
        public List<RptProveedorMermaAltaModels> obtenerListaProveedoresMermaAlta(RptProveedorMermaAltaModels Datos) {
            try
            {
                List<RptProveedorMermaAltaModels> lista = new List<RptProveedorMermaAltaModels>();
                RptProveedorMermaAltaModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Reporte_get_ProveedoresConMerma");
                while (dr.Read())
                {
                    item = new RptProveedorMermaAltaModels();
                    item.nombreProveedor = !dr.IsDBNull(dr.GetOrdinal("nombreRazonSocial")) ? dr.GetString(dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                    item.toleranciaCompra = !dr.IsDBNull(dr.GetOrdinal("toleranciaCompra")) ? dr.GetDecimal(dr.GetOrdinal("toleranciaCompra")) : 0;
                    item.merma = !dr.IsDBNull(dr.GetOrdinal("toleranciaProveedor")) ? dr.GetInt32(dr.GetOrdinal("toleranciaProveedor")) : 0;
                    item.mermaTotal = !dr.IsDBNull(dr.GetOrdinal("mermaTotal")) ? dr.GetInt16(dr.GetOrdinal("mermaTotal")) : 0;

                    lista.Add(item);
                }
                Datos.listaRptProveedorMerma = lista;
                return Datos.listaRptProveedorMerma;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}