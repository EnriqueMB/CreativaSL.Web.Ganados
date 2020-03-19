using CreativaSL.Web.Ganados.ViewModels;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CreativaSL.Web.Ganados.Models.System;

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
                dr.Close();
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
                    datos.IDEntregaCombustible = !dr.IsDBNull(dr.GetOrdinal("id_entregaCombustible")) ? dr.GetString(dr.GetOrdinal("id_entregaCombustible")) : string.Empty;
                    datos.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("IDProveedor")) ? dr.GetString(dr.GetOrdinal("IDProveedor")) : string.Empty;
                    datos.IDSucursal= !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                    datos.IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("id_vehiculo")) ? dr.GetString(dr.GetOrdinal("id_vehiculo")) : string.Empty;
                    datos.IDTipoCombustible = !dr.IsDBNull(dr.GetOrdinal("id_tipoCombustible")) ? dr.GetInt16(dr.GetOrdinal("id_tipoCombustible")) : 0;
                    datos.Fecha = !dr.IsDBNull(dr.GetOrdinal("fecha")) ? dr.GetDateTime(dr.GetOrdinal("fecha")) : DateTime.Today;
                    datos.NoTicket = !dr.IsDBNull(dr.GetOrdinal("noTicket")) ? dr.GetString(dr.GetOrdinal("noTicket")) : string.Empty;
                    datos.KMInicial = !dr.IsDBNull(dr.GetOrdinal("kmInicial")) ? dr.GetInt32(dr.GetOrdinal("kmInicial")) : 0;
                    datos.Litros = !dr.IsDBNull(dr.GetOrdinal("litros")) ? dr.GetDecimal(dr.GetOrdinal("litros")) : 0;
                    datos.Total = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;
                    datos.UrlImagen64 = !dr.IsDBNull(dr.GetOrdinal("imgTicket")) ? dr.GetString(dr.GetOrdinal("imgTicket")) : string.Empty;
                    datos.IDChofer= !dr.IsDBNull(dr.GetOrdinal("id_chofer")) ? dr.GetString(dr.GetOrdinal("id_chofer")) : string.Empty;

                    datos.UrlImagen64 =
                        Auxiliar.ValidImageFormServer(datos.UrlImagen64, ProjectSettings.BaseDirEntregaCombustible);
                    break;
                }
                dr.Close();
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
                object Result = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Mantenimiento_del_EntregaCombustible", parametros);
                if (Result != null)
                {
                    datos.UrlImagen64 = Result.ToString();
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
                    datos.NuevoRegistro, datos.IDEntregaCombustible ?? string.Empty, 
                    datos.IDSucursal ?? string.Empty,
                    datos.IDProveedor ?? string.Empty,
                    datos.IDVehiculo ?? string.Empty,
                    datos.IDTipoCombustible,
                    datos.Fecha,
                    datos.NoTicket ?? string.Empty,
                    datos.KMInicial,
                    datos.Litros,
                    datos.Precio,
                    datos.Total,
                    datos.UrlImagen64 ?? string.Empty,
                    datos.BandImg,
                    datos.Usuario ?? string.Empty,
                    datos.IDChofer ?? string.Empty
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Mantenimiento_ac_EntregaCombustible", parametros);
                datos.IDEntregaCombustible = Resultado.ToString();
                if (!string.IsNullOrEmpty(datos.IDEntregaCombustible))
                {
                    datos.Completado = true;
                }
                else
                {
                    datos.Resultado = -1;
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
                while (dr.Read())
                {
                    Item = new EntregaCombustibleModels();
                    Item.Total = !dr.IsDBNull(dr.GetOrdinal("total")) ? dr.GetDecimal(dr.GetOrdinal("total")) : 0;
                    Item.IDEntregaCombustible = !dr.IsDBNull(dr.GetOrdinal("id_entregaCombustible")) ? dr.GetString(dr.GetOrdinal("id_entregaCombustible")) : string.Empty;
                    Item.Fecha = !dr.IsDBNull(dr.GetOrdinal("fecha")) ? dr.GetDateTime(dr.GetOrdinal("fecha")) : DateTime.Now;
                    Item.Vehiculo = !dr.IsDBNull(dr.GetOrdinal("Vehiculo")) ? dr.GetString(dr.GetOrdinal("Vehiculo")) : string.Empty;
                    Item.Sucursal = !dr.IsDBNull(dr.GetOrdinal("Sucursal")) ? dr.GetString(dr.GetOrdinal("Sucursal")) : string.Empty;
                    Item.Estatus = !dr.IsDBNull(dr.GetOrdinal("Estatus")) ? dr.GetString(dr.GetOrdinal("Estatus")) : string.Empty;
                    Item.CssClassEstatus = !dr.IsDBNull(dr.GetOrdinal("CssClass")) ? dr.GetString(dr.GetOrdinal("CssClass")) : string.Empty;
                    Item.Proveedor = !dr.IsDBNull(dr.GetOrdinal("Proveedor")) ? dr.GetString(dr.GetOrdinal("Proveedor")) : string.Empty;
                    Lista.Add(Item);

                }
                dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public EntregaCombustibleModels ProcesarEntregaCombustible(EntregaCombustibleModels datos)
        {
            try
            {
                object Result = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Mantenimiento_ProcesarEntregaCombustibleXID", datos.IDEntregaCombustible, datos.Usuario);
                if (Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    datos.Resultado = Resultado;
                    if (Resultado == 1)
                        datos.Completado = true;
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntregaCombustibleModels MostrarTickets(EntregaCombustibleModels datos)
        {
            try
            {
                object[] parametros = { datos.IDEntregaCombustible };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "Mostrar_Ticket_EntregaCombustible", parametros);
                while (dr.Read())
                {

                    datos.UrlImagen64Tickets = !dr.IsDBNull(dr.GetOrdinal("imgTicket"))
                        ? dr.GetString(dr.GetOrdinal("imgTicket"))
                        : string.Empty;

                    datos.UrlImagen64Tickets = Auxiliar.ValidImageFormServer(datos.UrlImagen64Tickets,
                        ProjectSettings.BaseDirEntregaCombustible);

                    break;
                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}