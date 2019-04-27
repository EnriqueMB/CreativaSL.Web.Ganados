using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatRangoPesoVenta_Datos
    {
        public string RangoPesoVenta_index_RangoPesoVenta(string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_RangoPesoVenta_index_RangoPesoVenta");
                string datatable = Auxiliar.SqlReaderToJson(dr);

                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatTipoClienteModels> ObtenerListaTipoCliente(CatRangoPesoVentaModels Datos, string Conexion)
        {
            try
            {
                List<CatTipoClienteModels> lista = new List<CatTipoClienteModels>();
                CatTipoClienteModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatTipoCliente");
                while (dr.Read())
                {
                    item = new CatTipoClienteModels();
                    item.Id_tipoCliente = !dr.IsDBNull(dr.GetOrdinal("id_tipoCliente")) ? dr.GetInt32(dr.GetOrdinal("id_tipoCliente")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
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

        public RespuestaAjax AbcCatRangoPesoVenta(CatRangoPesoVentaModels item, int opcion, string usuario, string conexion)
        {
            try
            {
                object[] parametros =
                {
                    opcion,
                    item.Id_rango,
                    item.EsMacho,
                    item.PesoMinimo,
                    item.PesoMaximo,
                    item.Precio,
                    item.Id_tipoCliente,
                    usuario,
                    item.NombreRango ?? string.Empty
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_Catalogo_ac_CatRangoPesoVenta", parametros);
                RespuestaAjax respuesta = new RespuestaAjax();
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

        public CatRangoPesoVentaModels ObtenerDetalleCatRangoPesoVenta(CatRangoPesoVentaModels datos, string conexion)
        {
            try
            {
                object[] parametros = { datos.Id_rango };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_Catalogo_get_CatRangoPesoVentaXID", parametros);
                while (dr.Read())
                {
                    datos.Id_rango = !dr.IsDBNull(dr.GetOrdinal("IDRango")) ? dr.GetInt32(dr.GetOrdinal("IDRango")) : 0;
                    datos.EsMacho = !dr.IsDBNull(dr.GetOrdinal("EsMacho")) ? dr.GetBoolean(dr.GetOrdinal("EsMacho")) : false;
                    datos.PesoMinimo = !dr.IsDBNull(dr.GetOrdinal("PesoMinimo")) ? dr.GetDecimal(dr.GetOrdinal("PesoMinimo")) : 0;
                    datos.PesoMaximo = !dr.IsDBNull(dr.GetOrdinal("PesoMaximo")) ? dr.GetDecimal(dr.GetOrdinal("PesoMaximo")) : 0;
                    datos.Precio = !dr.IsDBNull(dr.GetOrdinal("Precio")) ? dr.GetDecimal(dr.GetOrdinal("Precio")) : 0;
                    datos.Id_tipoCliente = !dr.IsDBNull(dr.GetOrdinal("IDTipoCliente")) ? dr.GetInt32(dr.GetOrdinal("IDTipoCliente")) : 0;
                    datos.NombreRango = !dr.IsDBNull(dr.GetOrdinal("NombreRango")) ? dr.GetString(dr.GetOrdinal("NombreRango")) : string.Empty;
                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RespuestaAjax EliminarRangoPesoCompra(int id, string conexion, string usuario)
        {
            try
            {
                object[] parametros = { id, usuario};
                SqlDataReader dr = null;
                RespuestaAjax respuesta = new RespuestaAjax();
                dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_Catalogo_del_CatRangoPesoVenta", parametros);
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