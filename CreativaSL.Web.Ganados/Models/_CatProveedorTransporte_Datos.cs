using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatProveedorTransporte_Datos
    {
        public List<CatProveedorTransporteModels> ObtenerCatProveedoresTransportes(string Conexion)
        {
            try
            {
                List<CatProveedorTransporteModels> lista = new List<CatProveedorTransporteModels>();
                CatProveedorTransporteModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Catalogo_get_CatProveedorServicio");
                while (dr.Read())
                {
                    item = new CatProveedorTransporteModels();
                    item.Id = !dr.IsDBNull(dr.GetOrdinal("id")) ? dr.GetInt32(dr.GetOrdinal("id")) : 0;
                    item.NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("nombreRazonSocial")) ? dr.GetString(dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                    item.RFC = !dr.IsDBNull(dr.GetOrdinal("rfc")) ? dr.GetString(dr.GetOrdinal("rfc")) : string.Empty;
                    item.DomicilioFiscal = !dr.IsDBNull(dr.GetOrdinal("domicilioFiscal")) ? dr.GetString(dr.GetOrdinal("domicilioFiscal")) : string.Empty;
                    
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
    }
}