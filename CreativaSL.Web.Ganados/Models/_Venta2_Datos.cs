using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Venta2_Datos
    {
        public SqlDataReader DatatableGanadoActual(VentaModels2 venta)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(venta.Conexion, "spCSLDB_Venta_get_DatatableGanadoActual");

                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}