using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatSocio_Datos
    {
        public List<CatSociosModels> ObtenerListaSocios(CatSociosModels Datos)
        {
            try
            {
                List<CatSociosModels> lista = new List<CatSociosModels>();
                CatSociosModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatSocios");
                while (dr.Read())
                {
                    item = new CatSociosModels();
                    item.IDSocio = !dr.IsDBNull(dr.GetOrdinal("IDSocio")) ? dr.GetString(dr.GetOrdinal("IDSocio")) : string.Empty;
                    item.NombreCompleto = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty;
                    item.Procentaje = !dr.IsDBNull(dr.GetOrdinal("Porcentaje")) ? dr.GetInt32(dr.GetOrdinal("Porcentaje")) : 0;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}