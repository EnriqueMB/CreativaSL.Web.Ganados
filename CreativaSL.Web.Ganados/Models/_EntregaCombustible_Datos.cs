using CreativaSL.Web.Ganados.ViewModels;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _EntregaCombustible_Datos
    {
        public RendimientoCombustibleViewModels setRendimiento(RendimientoCombustibleViewModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDEntregaCombustible,
                    datos.KMFinal,
                    datos.Rendimiento,
                    datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Mantenimiento_set_RendimientoEntregaCombustible", parametros);
                datos.IDEntregaCombustible = Resultado.ToString();
                if (!string.IsNullOrEmpty(datos.IDEntregaCombustible))
                {
                    datos.Completado = true;
                }
                else
                {
                    datos.Completado = false;
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RendimientoCombustibleViewModels ObtenerDatosRendimiento(RendimientoCombustibleViewModels datos)
        {
            try
            {
                object[] parametros = { datos.IDEntregaCombustible };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Mantenimiento_get_EntregaCombustibleRendimientoXID", parametros);
                while (dr.Read())
                {
                    datos.IDEntregaCombustible = dr["id_entregaCombustible"].ToString();
                   
                    datos.Fecha = Convert.ToDateTime(dr["fecha"].ToString());
                    datos.NoTicket = dr["noTicket"].ToString();
                    datos.KMInicial = Convert.ToInt32(dr["kmInicial"].ToString());
                    datos.KMFinal = Convert.ToInt32(dr["kmFinal"].ToString());
                    datos.Litros = Convert.ToDecimal(dr["litros"].ToString());
                    datos.Rendimiento = Convert.ToDecimal(dr["rendimiento"].ToString());

                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EntregaCombustibleModels ObtenerDetalleEntregaCombustible(EntregaCombustibleModels datos)
        {
            try
            {
                object[] parametros = { datos.IDEntregaCombustible };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Mantenimiento_get_EntregaCombustibleXID", parametros);
                while (dr.Read())
                {
                    datos.IDEntregaCombustible = dr["id_entregaCombustible"].ToString();
                    datos.IDSucursal= dr["id_sucursal"].ToString();
                    datos.IDVehiculo = dr["id_vehiculo"].ToString();
                    datos.IDTipoCombustible = Convert.ToInt32(dr["id_tipoCombustible"].ToString());
                    datos.Fecha = Convert.ToDateTime(dr["fecha"].ToString());
                    datos.NoTicket = dr["noTicket"].ToString();
                    datos.KMInicial = Convert.ToInt32(dr["kmInicial"].ToString());
                    datos.Litros =Convert.ToDecimal(dr["litros"].ToString());
                    datos.Total= Convert.ToDecimal(dr["total"].ToString());
                    datos.UrlImagen64 = dr["imgTicket"].ToString();
                    
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EntregaCombustibleModels EliminarEntregaCombustible(EntregaCombustibleModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDEntregaCombustible, datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Mantenimiento_del_EntregaCombustible", parametros);
                if (Resultado != null)
                {
                    if (!string.IsNullOrEmpty(datos.IDEntregaCombustible))
                    {
                        datos.Completado = true;
                    }
                    else
                    {
                        datos.Completado = false;
                    }
                    return datos;
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EntregaCombustibleModels AcEntregaCombustible(EntregaCombustibleModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Opcion, datos.IDEntregaCombustible,
                    datos.IDVehiculo,
                    datos.IDTipoCombustible,
                    datos.Fecha,
                    datos.NoTicket,
                    datos.KMInicial,
                    datos.Litros,
                    datos.Precio,
                    datos.Total,
                    datos.UrlImagen64,datos.BandImg,
                    datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Mantenimiento_ac_EntregaCombustible", parametros);
                datos.IDEntregaCombustible = Resultado.ToString();
                if (!string.IsNullOrEmpty(datos.IDEntregaCombustible))
                {
                    datos.Completado = true;
                }
                else
                {
                    datos.Completado = false;
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntregaCombustibleModels> ObtenerEntregasCombustible(EntregaCombustibleModels Datos)
        {
            try
            {
                List<EntregaCombustibleModels> Lista = new List<EntregaCombustibleModels>();
               
               

                object[] Parametros = { Datos.IDSucursal, Datos.IDVehiculo, Datos.Fecha, Datos.BandIDSucursal, Datos.BandIDVehiuculo, Datos.BandFechaEntrega };
                SqlDataReader dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Mantenimiento_get_EntregaCombustible", Parametros);
                 EntregaCombustibleModels Item;
                while(dr.Read())
                {
                    Item = new EntregaCombustibleModels();
                    Item.Total = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;
                    Item.IDEntregaCombustible = !dr.IsDBNull(dr.GetOrdinal("id_entregaCombustible")) ? dr.GetString(dr.GetOrdinal("id_entregaCombustible")) : string.Empty;
                  
                    Item.Fecha = !dr.IsDBNull(dr.GetOrdinal("fecha")) ? dr.GetDateTime(dr.GetOrdinal("fecha")) : DateTime.Now;
                    Item.Vehiculo = !dr.IsDBNull(dr.GetOrdinal("Vehiculo")) ? dr.GetString(dr.GetOrdinal("Vehiculo")) : string.Empty;
                    Item.Sucursal = !dr.IsDBNull(dr.GetOrdinal("Sucursal")) ? dr.GetString(dr.GetOrdinal("Sucursal")) : string.Empty;

                    Lista.Add(Item);
                
                }
                return Lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}