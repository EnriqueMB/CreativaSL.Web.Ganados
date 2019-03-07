using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Deduccion_Datos
    {
        public RespuestaAjax SpCSLDB_DocumentoPorPagar_AC_Deduccion(string conexion, string usuario, int opcion, int subOpcion, DeduccionModels deduccion)
        {
            try
            {
                object[] parametros = 
                {
                    opcion,
                    subOpcion, 
                    deduccion.IdGenerico,
                    usuario,
                    deduccion.IdDeduccion,
                    deduccion.Monto,
                    deduccion.IdDetalleDoctoPagar
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "dbo.spCSLDB_DocumentoPorPagar_AC_Deduccion", parametros);
                RespuestaAjax respuesta = new RespuestaAjax();

                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }

                dr.Close();
                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}