using CreativaSL.Web.Ganados.ViewModels;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _ServicioMantenimiento_Datos
    {

        public ServiciosMantenimientoModels ObtenerDatosIndex(string Conexion, string IdSucursal)
        {
            try
            {
                ServiciosMantenimientoModels Result = new ServiciosMantenimientoModels();
                //Obtener el listado de vehículos
                
                DataSet Ds = SqlHelper.ExecuteDataset(Conexion, "spCSLDB_Mantenimiento_get_IndexServicios", IdSucursal);
                if (Ds != null)
                {
                    if (Ds.Tables.Count == 1)
                    {
                        DataTableReader Dr01 = Ds.Tables[0].CreateDataReader();
                        List<CatVehiculoModels> ListaV = new List<CatVehiculoModels>();
                        CatVehiculoModels ItemV;
                        while (Dr01.Read())
                        {
                            ItemV = new CatVehiculoModels();
                            ItemV.IDVehiculo = !Dr01.IsDBNull(Dr01.GetOrdinal("IDVehiculo")) ? Dr01.GetString(Dr01.GetOrdinal("IDVehiculo")) : string.Empty;
                            ItemV.Modelo = !Dr01.IsDBNull(Dr01.GetOrdinal("Descripcion")) ? Dr01.GetString(Dr01.GetOrdinal("Descripcion")) : string.Empty;
                            ItemV.DateLastService = !Dr01.IsDBNull(Dr01.GetOrdinal("LastDate")) ? Dr01.GetDateTime(Dr01.GetOrdinal("LastDate")) : DateTime.MinValue;
                            ListaV.Add(ItemV);
                        }
                        Dr01.Close();
                        //Obtener el listado de remolques
                        //DataTableReader Dr02 = Ds.Tables[1].CreateDataReader();
                        //List<CatRemolqueModels> ListaR = new List<CatRemolqueModels>();
                        //CatRemolqueModels ItemR;
                        //while (Dr02.Read())
                        //{
                        //    ItemR = new CatRemolqueModels();
                        //    ItemR.IDRemolque = !Dr02.IsDBNull(Dr02.GetOrdinal("IDRemolque")) ? Dr02.GetString(Dr02.GetOrdinal("IDRemolque")) : string.Empty;
                        //    ItemR.placa = !Dr02.IsDBNull(Dr02.GetOrdinal("Descripcion")) ? Dr02.GetString(Dr02.GetOrdinal("Descripcion")) : string.Empty;
                        //    ItemR.DateLastService = !Dr02.IsDBNull(Dr02.GetOrdinal("LastDate")) ? Dr02.GetDateTime(Dr02.GetOrdinal("LastDate")) : DateTime.MinValue;
                        //    ListaR.Add(ItemR);
                        //}
                        // Asignar listas a objeto principal y retornar
                        Result.ListaVehiculos = ListaV;
                        //Result.ListaRemolques = ListaR;
                    }
                }
                return Result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<ServiciosMantenimientoModels> ObtenerServiciosXIDVehiculo(string Conexion, string IDVehiculo)
        {
            try
            {
                List<ServiciosMantenimientoModels> Lista = new List<ServiciosMantenimientoModels>();
                ServiciosMantenimientoModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Mantenimiento_get_ServiciosXIDVehiculo", IDVehiculo);
                while (Dr.Read())
                {
                    Item = new ServiciosMantenimientoModels();
                    Item.IDServicio = !Dr.IsDBNull(Dr.GetOrdinal("IDServicio")) ? Dr.GetString(Dr.GetOrdinal("IDServicio")) : string.Empty;
                    Item.Fecha = !Dr.IsDBNull(Dr.GetOrdinal("Fecha")) ? Dr.GetDateTime(Dr.GetOrdinal("Fecha")) : DateTime.MinValue;
                    Item.Sucursal.NombreSucursal = !Dr.IsDBNull(Dr.GetOrdinal("Sucursal")) ? Dr.GetString(Dr.GetOrdinal("Sucursal")) : string.Empty;
                    Item.ImporteTotal = !Dr.IsDBNull(Dr.GetOrdinal("ImporteTotal")) ? Dr.GetDecimal(Dr.GetOrdinal("ImporteTotal")) : 0;
                    Item.ServiciosRealizados = !Dr.IsDBNull(Dr.GetOrdinal("Servicios")) ? Dr.GetString(Dr.GetOrdinal("Servicios")) : string.Empty;
                    Item.Estatus = !Dr.IsDBNull(Dr.GetOrdinal("Estatus")) ? Dr.GetString(Dr.GetOrdinal("Estatus")) : string.Empty;
                    Item.CssClassEstatus = !Dr.IsDBNull(Dr.GetOrdinal("CssClass")) ? Dr.GetString(Dr.GetOrdinal("CssClass")) : string.Empty;
                    Item.Proveedor = new CatProveedorModels();
                    Item.Proveedor.NombreRazonSocial = !Dr.IsDBNull(Dr.GetOrdinal("nombreRazonSocial")) ? Dr.GetString(Dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<ServiciosMantenimientoModels> ObtenerServiciosXIDRemolque(string Conexion, string IDRemolque)
        {
            try
            {
                List<ServiciosMantenimientoModels> Lista = new List<ServiciosMantenimientoModels>();
                ServiciosMantenimientoModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Mantenimiento_get_ServiciosXIDRemolque", IDRemolque);
                while (Dr.Read())
                {
                    Item = new ServiciosMantenimientoModels();
                    Item.IDServicio = !Dr.IsDBNull(Dr.GetOrdinal("IDServicio")) ? Dr.GetString(Dr.GetOrdinal("IDServicio")) : string.Empty;
                    Item.Fecha = !Dr.IsDBNull(Dr.GetOrdinal("Fecha")) ? Dr.GetDateTime(Dr.GetOrdinal("Fecha")) : DateTime.MinValue;
                    Item.Sucursal.NombreSucursal = !Dr.IsDBNull(Dr.GetOrdinal("Sucursal")) ? Dr.GetString(Dr.GetOrdinal("Sucursal")) : string.Empty;
                    Item.ImporteTotal = !Dr.IsDBNull(Dr.GetOrdinal("ImporteTotal")) ? Dr.GetDecimal(Dr.GetOrdinal("ImporteTotal")) : 0;
                    Item.ServiciosRealizados = !Dr.IsDBNull(Dr.GetOrdinal("Servicios")) ? Dr.GetString(Dr.GetOrdinal("Servicios")) : string.Empty;
                    Item.Estatus = !Dr.IsDBNull(Dr.GetOrdinal("Estatus")) ? Dr.GetString(Dr.GetOrdinal("Estatus")) : string.Empty;
                    Item.CssClassEstatus = !Dr.IsDBNull(Dr.GetOrdinal("CssClass")) ? Dr.GetString(Dr.GetOrdinal("CssClass")) : string.Empty;
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ServiciosMantenimientoModels ObtenerDetalleServicioXID(string Conexion, string IDServicio)
        {
            try
            {
                ServiciosMantenimientoModels Result = new ServiciosMantenimientoModels();

                List<ServiciosMantenimientoDetalleModels> Lista = new List<ServiciosMantenimientoDetalleModels>();
                ServiciosMantenimientoDetalleModels Item;
                 DataSet Ds = SqlHelper.ExecuteDataset(Conexion, "spCSLDB_Mantenimiento_get_DetalleServicioXID", IDServicio);
                if(Ds!= null)
                {
                    if (Ds.Tables.Count == 2)
                    {
                        if (Ds.Tables[0] != null)
                        {
                            if(Ds.Tables[0].Rows[0][0] != null)
                            {
                                Result.Vehiculo = new CatVehiculoModels { IDVehiculo = Ds.Tables[0].Rows[0][0].ToString() };
                            }
                        }

                        DataTableReader Dr = Ds.Tables[1].CreateDataReader();
                        while (Dr.Read())
                        {
                            Item = new ServiciosMantenimientoDetalleModels();
                            Item.IDServicioDetalle = !Dr.IsDBNull(Dr.GetOrdinal("IDServicioDetalle")) ? Dr.GetString(Dr.GetOrdinal("IDServicioDetalle")) : string.Empty;
                            Item.TipoServicio.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("TipoServicio")) ? Dr.GetString(Dr.GetOrdinal("TipoServicio")) : string.Empty;
                            Item.Encargado = !Dr.IsDBNull(Dr.GetOrdinal("Encargado")) ? Dr.GetString(Dr.GetOrdinal("Encargado")) : string.Empty;
                            Item.Observaciones = !Dr.IsDBNull(Dr.GetOrdinal("Observaciones")) ? Dr.GetString(Dr.GetOrdinal("Observaciones")) : string.Empty;
                            Item.Importe = !Dr.IsDBNull(Dr.GetOrdinal("Importe")) ? Dr.GetDecimal(Dr.GetOrdinal("Importe")) : 0;
                            Item.ImporteRefacciones = !Dr.IsDBNull(Dr.GetOrdinal("ImporteRefacciones")) ? Dr.GetDecimal(Dr.GetOrdinal("ImporteRefacciones")) : 0;
                            Lista.Add(Item);
                        }
                    }
                }
                
                Result.ListaDetalle = Lista;
                return Result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void ACServicio(ServiciosMantenimientoModels Datos)
        {
            try
            {
                object[] Parametros = { Datos.NuevoRegistro,
                                        //Datos.Opcion,
                                        Datos.IDServicio ?? string.Empty,
                                        Datos.Proveedor.IDProveedor ?? string.Empty,
                                        Datos.Vehiculo.IDVehiculo ?? string.Empty,
                                        Datos.Sucursal.IDSucursal ?? string.Empty,
                                        Datos.Fecha,
                                        Datos.Usuario ?? string.Empty};
                object Result = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Mantenimiento_ac_Servicios", Parametros);
                if(Result != null)
                {
                    if(!string.IsNullOrEmpty(Result.ToString().Trim()))
                    {
                        Datos.Completado = true;
                        Datos.IDServicio = Result.ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void ACServicioDetalle(ServiciosMantenimientoDetalleModels Datos)
        {
            try
            {
                object[] Parametros = { Datos.NuevoRegistro,
                                        Datos.IDServicioDetalle ?? string.Empty,
                                        Datos.IDServicio ?? string.Empty,
                                        Datos.TipoServicio.IDTipoServicio ?? string.Empty,
                                        Datos.Encargado ?? string.Empty,
                                        Datos.Importe,
                                        Datos.ImporteRefacciones,
                                        Datos.Observaciones ?? string.Empty,
                                        Datos.Usuario ?? string.Empty};
                object Result = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Mantenimiento_ac_ServiciosDetalle", Parametros);
                if (Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    Datos.Resultado = Resultado;
                    if (Resultado == 1)
                        Datos.Completado = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ServiciosMantenimientoDetalleViewModels ObtenerDatosServicioDetalle(string Conexion, string IDServicioDetalle)
        {
            try
            {
                ServiciosMantenimientoDetalleViewModels Resultado = new ServiciosMantenimientoDetalleViewModels();
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Mantenimiento_get_DatosServicioDetalle", IDServicioDetalle);
                while (Dr.Read())
                {
                    Resultado.IDServicioDetalle = !Dr.IsDBNull(Dr.GetOrdinal("IDServicioDetalle")) ? Dr.GetString(Dr.GetOrdinal("IDServicioDetalle")) : string.Empty;
                    Resultado.IDServicio = !Dr.IsDBNull(Dr.GetOrdinal("IDServicio")) ? Dr.GetString(Dr.GetOrdinal("IDServicio")) : string.Empty;
                    Resultado.IDTipoServicio = !Dr.IsDBNull(Dr.GetOrdinal("IDTipoServicio")) ? Dr.GetString(Dr.GetOrdinal("IDTipoServicio")) : string.Empty;
                    Resultado.Encargado = !Dr.IsDBNull(Dr.GetOrdinal("Encargado")) ? Dr.GetString(Dr.GetOrdinal("Encargado")) : string.Empty;
                    Resultado.Observaciones = !Dr.IsDBNull(Dr.GetOrdinal("Observaciones")) ? Dr.GetString(Dr.GetOrdinal("Observaciones")) : string.Empty;
                    Resultado.Importe = !Dr.IsDBNull(Dr.GetOrdinal("Importe")) ? Dr.GetDecimal(Dr.GetOrdinal("Importe")) : 0;
                    Resultado.ImporteRefacciones = !Dr.IsDBNull(Dr.GetOrdinal("ImporteRefacciones")) ? Dr.GetDecimal(Dr.GetOrdinal("ImporteRefacciones")) : 0;
                    break;
                }
                return Resultado;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ServiciosMantenimientoViewModels ObtenerDatosServicio(string Conexion, string IDServicio)
        {
            try
            {
                ServiciosMantenimientoViewModels Resultado = new ServiciosMantenimientoViewModels();
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Mantenimiento_get_DetalleServicio", IDServicio);
                while (Dr.Read())
                {
                    Resultado.IDServicio = IDServicio;
                    Resultado.ID = !Dr.IsDBNull(Dr.GetOrdinal("ID")) ? Dr.GetString(Dr.GetOrdinal("ID")) : string.Empty;
                    Resultado.IDSucursal = !Dr.IsDBNull(Dr.GetOrdinal("IDSucursal")) ? Dr.GetString(Dr.GetOrdinal("IDSucursal")) : string.Empty;
                    Resultado.IDProveedor = !Dr.IsDBNull(Dr.GetOrdinal("IDProveedor")) ? Dr.GetString(Dr.GetOrdinal("IDProveedor")) : string.Empty;
                    Resultado.Fecha = !Dr.IsDBNull(Dr.GetOrdinal("Fecha")) ? Dr.GetDateTime(Dr.GetOrdinal("Fecha")) : DateTime.MinValue;
                    break;
                }
                return Resultado;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarServicioDetalle(ServiciosMantenimientoDetalleModels Datos)
        {
            try
            {
                object[] Parametros = { Datos.IDServicioDetalle, Datos.Usuario };
                object Result = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Mantenimiento_del_ServicioDetalleXID", Parametros);
                if(Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    Datos.Resultado = Resultado;
                    if (Resultado == 1)
                        Datos.Completado = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarServicio(ServiciosMantenimientoModels Datos)
        {
            try
            {
                object[] Parametros = { Datos.IDServicio, Datos.Usuario };
                object Result = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Mantenimiento_del_ServicioXID", Parametros);
                if (Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    Datos.Resultado = Resultado;
                    if (Resultado == 1)
                        Datos.Completado = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void ProcesarServicio(ServiciosMantenimientoModels Datos)
        {
            try
            {
                object[] Parametros = { Datos.IDServicio, Datos.Usuario };
                object Result = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Mantenimiento_ProcesarServicioXID", Parametros);
                if (Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    Datos.Resultado = Resultado;
                    if (Resultado == 1)
                        Datos.Completado = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}