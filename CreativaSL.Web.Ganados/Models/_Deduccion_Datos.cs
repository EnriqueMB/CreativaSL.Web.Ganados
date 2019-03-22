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
                    deduccion.IdDetalleDocto,
                    deduccion.Id_conceptoDocumento
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
        public void SpCSLDB_DocumentoPorPagar_get_Deduccion(string conexion, DeduccionModels deduccion)
        {
            try
            {
                object[] parametros =
                {
                   deduccion.IdDocumento ,
                   deduccion.IdDetalleDocto
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "dbo.spCSLDB_DocumentoPorPagar_get_Deduccion", parametros);

                while (dr.Read())
                {
                    bool success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    string mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;

                    if (success)
                    {
                        deduccion.IdDetalleDocto = !dr.IsDBNull(dr.GetOrdinal("id_detalleDoctoPagar")) ? dr.GetString(dr.GetOrdinal("id_detalleDoctoPagar")) : string.Empty;
                        deduccion.IdDocumento = !dr.IsDBNull(dr.GetOrdinal("id_documentoPagar")) ? dr.GetString(dr.GetOrdinal("id_documentoPagar")) : string.Empty;
                        deduccion.IdDeduccion = !dr.IsDBNull(dr.GetOrdinal("id_deduccion")) ? dr.GetInt32(dr.GetOrdinal("id_deduccion")) : 0;
                        deduccion.Monto = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0;
                        deduccion.Id_conceptoDocumento = !dr.IsDBNull(dr.GetOrdinal("id_conceptoDocumento")) ? dr.GetInt32(dr.GetOrdinal("id_conceptoDocumento")) : 0;
                    }
                }

                dr.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RespuestaAjax SpCSLDB_DocumentoPorPagar_del_Deduccion(string conexion, string idDocumento, string idDetalleDocumento, string usuario)
        {
            try
            {
                object[] parametros =
                {
                    idDocumento ,
                    idDetalleDocumento ,
                    usuario
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "dbo.spCSLDB_DocumentoPorPagar_del_Deduccion", parametros);
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
