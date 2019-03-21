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
                dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Catalogo_get_CatProveedorTransporte");
                while (dr.Read())
                {
                    item = new CatProveedorTransporteModels();
                    item.Id = !dr.IsDBNull(dr.GetOrdinal("id")) ? dr.GetString(dr.GetOrdinal("id")) : string.Empty;
                    item.RazonFiscal = !dr.IsDBNull(dr.GetOrdinal("razonFiscal")) ? dr.GetString(dr.GetOrdinal("razonFiscal")) : string.Empty;
                    item.RFC = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty;
                    item.DireccionFiscal = !dr.IsDBNull(dr.GetOrdinal("direccionFiscal")) ? dr.GetString(dr.GetOrdinal("direccionFiscal")) : string.Empty;
                    item.PsgEmpresa = !dr.IsDBNull(dr.GetOrdinal("psgEmpresa")) ? dr.GetString(dr.GetOrdinal("psgEmpresa")) : string.Empty;

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

        public RespuestaAjax Catalogo_ac_CatProveedorTransporte (string Conexion, int opcion, string usuario, CatProveedorTransporteModels oProveedor)
        {
            try
            {
                object[] parametros =
                {
                    opcion, 
                    oProveedor.Id,
                    oProveedor.RazonFiscal,
                    oProveedor.DireccionFiscal,
                    oProveedor.RFC,
                    oProveedor.PsgEmpresa,
                    usuario
                };

                RespuestaAjax respuesta = new RespuestaAjax();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Catalogo_ac_CatProveedorTransporte", parametros);
                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public CatProveedorTransporteModels Catalogo_get_CatProveedorTransporteXId(string Conexion, string id)
        {
            try
            {
                object[] parametros =
                {
                    id
                };

                RespuestaAjax respuesta = new RespuestaAjax();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Catalogo_get_CatProveedorTransporteXId", parametros);
                CatProveedorTransporteModels oProveedor = new CatProveedorTransporteModels();

                while (dr.Read())
                {
                    oProveedor.Id = !dr.IsDBNull(dr.GetOrdinal("id_empresa")) ? dr.GetString(dr.GetOrdinal("id_empresa")) : string.Empty; 
                    oProveedor.RazonFiscal = !dr.IsDBNull(dr.GetOrdinal("razonFiscal")) ? dr.GetString(dr.GetOrdinal("razonFiscal")) : string.Empty;
                    oProveedor.DireccionFiscal = !dr.IsDBNull(dr.GetOrdinal("direccionFiscal")) ? dr.GetString(dr.GetOrdinal("direccionFiscal")) : string.Empty;
                    oProveedor.RFC = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty;
                    oProveedor.PsgEmpresa = !dr.IsDBNull(dr.GetOrdinal("psgEmpresa")) ? dr.GetString(dr.GetOrdinal("psgEmpresa")) : string.Empty;
                }

                dr.Close();
                return oProveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public RespuestaAjax Catalogo_del_CatProveedorTransporte(string Conexion, string id, string usuario)
        {
            try
            {
                object[] parametros =
                {
                    id,
                    usuario
                };

                RespuestaAjax respuesta = new RespuestaAjax();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Catalogo_del_CatProveedorTransporte", parametros);
                

                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }

                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}