using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _VentaGeneral_Datos
    {
        public string VentaGeneral_spCIDDB_index(string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "ventaGeneral.spCIDDB_index");
                string datatable = Auxiliar.SqlReaderToJson(dr);

                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}