using CreativaSL.Web.Ganados.ViewModels;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _PrestamoHerramienta_Datos
    {
        public List<PrestamoHerramientaModels> ObtenerPrestamoHerramienta(string Conexion)
        {
            try
            {
                List<PrestamoHerramientaModels> Lista = new List<PrestamoHerramientaModels>();
                PrestamoHerramientaModels Item;
                SqlDataReader dr = SqlHelper.ExecuteReader(Conexion, "[prestamoHerramienta].[spCSLDB_get_PrestamoHerramientaAlmacen]");
                while (dr.Read())
                {
                    Item = new PrestamoHerramientaModels();
                    Item.IDPrestamo = !dr.IsDBNull(dr.GetOrdinal("IDPrestamo")) ? dr.GetInt32(dr.GetOrdinal("IDPrestamo")) : 0;
                    Item.Sucursal.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("Sucursal")) ? dr.GetString(dr.GetOrdinal("Sucursal")) : string.Empty;
                    Item.Almacen.nombreAlmacen = !dr.IsDBNull(dr.GetOrdinal("Almacen")) ? dr.GetString(dr.GetOrdinal("Almacen")) : string.Empty;
                    Item.FolioPrestamo = !dr.IsDBNull(dr.GetOrdinal("FolioPrestamo")) ? dr.GetString(dr.GetOrdinal("FolioPrestamo")) : string.Empty;
                    Item.FechaPrestamo = !dr.IsDBNull(dr.GetOrdinal("FechaDevolucion")) ? dr.GetDateTime(dr.GetOrdinal("FechaDevolucion")) : DateTime.MinValue;
                    Item.IDEstatus = !dr.IsDBNull(dr.GetOrdinal("IDEstatus")) ? dr.GetInt32(dr.GetOrdinal("IDEstatus")) : 0;
                    Item.EstatusDes = !dr.IsDBNull(dr.GetOrdinal("Estatus")) ? dr.GetString(dr.GetOrdinal("Estatus")) : string.Empty;
                    Item.Css = !dr.IsDBNull(dr.GetOrdinal("CssClass")) ? dr.GetString(dr.GetOrdinal("CssClass")) : string.Empty;
                    Item.Observacion = !dr.IsDBNull(dr.GetOrdinal("Comentarios")) ? dr.GetString(dr.GetOrdinal("Comentarios")) : string.Empty;
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ACPrestamosHerramientasAlmacen(PrestamoHerramientaModels Datos)
        {
            try
            {
                object[] Parametros = {
                            Datos.NuevoRegistro,
                            Datos.IDPrestamo,
                            Datos.Sucursal.IDSucursal ?? string.Empty,
                            Datos.Almacen.IDAlmacen ?? string.Empty,
                            Datos.Empleado.IDEmpleado ?? string.Empty,
                            Datos.FechaPrestamo,
                            Datos.Observacion ?? string.Empty,
                            Datos.Usuario
                        };
                SqlDataReader Dr = SqlHelper.ExecuteReader(Datos.Conexion, "[prestamoHerramienta].[spCSLDB_AC_PrestamoHerramienta]", Parametros);
                while (Dr.Read())
                {
                    Datos.Resultado = !Dr.IsDBNull(Dr.GetOrdinal("Resultado")) ? Dr.GetInt32(Dr.GetOrdinal("Resultado")) : -1;
                    Datos.IDPrestamo = !Dr.IsDBNull(Dr.GetOrdinal("IDPrestamo")) ? Dr.GetInt32(Dr.GetOrdinal("IDPrestamo")) : 0;
                    Datos.Completado = (Datos.Resultado == 1);
                    break;
                }
                Dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PrestamoHerramientaViewModels ObtenerDetallePrestamosHerramienta(string Conexion, int IDPrestamo)
        {
            try
            {
                PrestamoHerramientaViewModels Resultado = new PrestamoHerramientaViewModels();
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "[prestamoHerramienta].[spCSLDB_get_DatosPrestamoHerramientaAlmacenXID]", IDPrestamo);
                while (Dr.Read())
                {
                    Resultado.IDPrestamo = !Dr.IsDBNull(Dr.GetOrdinal("IDPrestamo")) ? Dr.GetInt32(Dr.GetOrdinal("IDPrestamo")) : 0;
                    Resultado.IDSucursal = !Dr.IsDBNull(Dr.GetOrdinal("IDSucursal")) ? Dr.GetString(Dr.GetOrdinal("IDSucursal")) : string.Empty;
                    Resultado.IDAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDAlmacen")) : string.Empty;
                    Resultado.IDEmpleado = !Dr.IsDBNull(Dr.GetOrdinal("IDEmpleado")) ? Dr.GetString(Dr.GetOrdinal("IDEmpleado")) : string.Empty;
                    Resultado.FechaPrestamo = !Dr.IsDBNull(Dr.GetOrdinal("FechaDevolucion")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaDevolucion")) : DateTime.MinValue;
                    Resultado.Comentario = !Dr.IsDBNull(Dr.GetOrdinal("Comentarios")) ? Dr.GetString(Dr.GetOrdinal("Comentarios")) : string.Empty;
                    break;
                }
                Dr.Close();
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PrestamoHerramientaDetalleModels> ObtenerListaDetallePrestamosHerramientas(string Conexion, int IDPrestamo)
        {
            try
            {
                List<PrestamoHerramientaDetalleModels> Lista = new List<PrestamoHerramientaDetalleModels>();
                PrestamoHerramientaDetalleModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "[prestamoHerramienta].[spCSLDB_get_PrestamoHerramientaDetalleSalidaXID]", IDPrestamo);
                while (Dr.Read())
                {
                    Item = new PrestamoHerramientaDetalleModels();
                    Item.IDPrestamoDetalle = !Dr.IsDBNull(Dr.GetOrdinal("IDPrestamoDetalle")) ? Dr.GetInt32(Dr.GetOrdinal("IDPrestamoDetalle")) : 0;
                    Item.Producto.Nombre = !Dr.IsDBNull(Dr.GetOrdinal("Producto")) ? Dr.GetString(Dr.GetOrdinal("Producto")) : string.Empty;
                    Item.Cantidad = !Dr.IsDBNull(Dr.GetOrdinal("Cantidad")) ? Dr.GetDecimal(Dr.GetOrdinal("Cantidad")) : 0;
                    Item.UnidadMedida.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("UnidadMedida")) ? Dr.GetString(Dr.GetOrdinal("UnidadMedida")) : string.Empty;
                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DevolucionHerramientaDetalleModels> ObtenerListaDetalleDevolucionHerramientas(string Conexion, int IDPrestamo)
        {
            try
            {
                List<DevolucionHerramientaDetalleModels> Lista = new List<DevolucionHerramientaDetalleModels>();
                DevolucionHerramientaDetalleModels Item;
                SqlDataReader Dr = null;
                Dr = SqlHelper.ExecuteReader(Conexion, "[prestamoHerramienta].[spCSLDB_get_DevolucionHerramientaDetalle]", IDPrestamo);
                while (Dr.Read())
                {
                    Item = new DevolucionHerramientaDetalleModels();
                    Item.IDPrestamoDetalle = !Dr.IsDBNull(Dr.GetOrdinal("IDPrestamoDetalle")) ? Dr.GetInt32(Dr.GetOrdinal("IDPrestamoDetalle")) : 0;
                    Item.IDPrestamo = !Dr.IsDBNull(Dr.GetOrdinal("IDPrestamo")) ? Dr.GetInt32(Dr.GetOrdinal("IDPrestamo")) : 0;
                    Item.Estatus = !Dr.IsDBNull(Dr.GetOrdinal("Estatus")) ? Dr.GetInt32(Dr.GetOrdinal("Estatus")) : 0;
                    Item.Producto.Nombre = !Dr.IsDBNull(Dr.GetOrdinal("Producto")) ? Dr.GetString(Dr.GetOrdinal("Producto")) : string.Empty;
                    Item.Cantidad = !Dr.IsDBNull(Dr.GetOrdinal("Cantidad")) ? Dr.GetDecimal(Dr.GetOrdinal("Cantidad")) : 0;
                    Item.CantidadPendiente = !Dr.IsDBNull(Dr.GetOrdinal("CantidadPendiente")) ? Dr.GetDecimal(Dr.GetOrdinal("CantidadPendiente")) : 0;
                    Item.UnidadMedida.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("UnidadMedida")) ? Dr.GetString(Dr.GetOrdinal("UnidadMedida")) : string.Empty;
                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal ObtenerExistenciaXIDProductoIDUnidadIDPrestamo(string Conexion, int IDPrestamo, string IDProducto, string IDUnidad)
        {
            try
            {
                decimal Existencia = 0;
                object[] Parametros = { IDProducto, IDUnidad, IDPrestamo };
                object Result = SqlHelper.ExecuteScalar(Conexion, "[prestamoHerramienta].[spCSLDB_get_ExistenciaXIDProductoIDPrestamo]", Parametros);
                if (Result != null)
                {
                    decimal.TryParse(Result.ToString(), out Existencia);
                }
                return Existencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ACPrestamosHerramientasAlmacenDetalle(PrestamoHerramientaDetalleModels Datos)
        {
            try
            {
                object[] Parametros = {
                    Datos.NuevoRegistro,
                    Datos.IDPrestamoDetalle,
                    Datos.IDPrestamo,
                    Datos.Producto.IDProductoAlmacen ?? string.Empty,
                    Datos.UnidadMedida.id_unidadProducto ?? string.Empty,
                    Datos.Cantidad,
                    Datos.Usuario ?? string.Empty };
                object Result = SqlHelper.ExecuteScalar(Datos.Conexion, "[prestamoHerramienta].[spCSLDB_AC_PrestamoHerramientaAlmacenDetalle]", Parametros);
                if (Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    Datos.Completado = (Resultado == 1);
                    Datos.Resultado = Resultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RespuestaAjax ACDevolucionHerramientasAlmacenDetalle(DevolucionHerramientaDetalleModels Datos)
        {
            try
            {

                RespuestaAjax respuesta = new RespuestaAjax();
                SqlDataReader dr = null;
                object[] Parametros = {
                    Datos.IDPrestamo,
                    Datos.IDPrestamoDetalle,
                    Datos.CantDevolver,
                    Datos.Usuario ?? string.Empty };
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "[prestamoHerramienta].[spCSLDB_AC_DevolucionHerramientaAlmacenDetalle]", Parametros);

                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DevolucionHerramientaDetalleModels FinalizarDevolucionHerramientasAlmacen(string Conexion, int IdPrestamo, string IDUsuario)
        {
            try
            {
                DevolucionHerramientaDetalleModels Datos = new DevolucionHerramientaDetalleModels();
                object[] Parametros = { IdPrestamo, IDUsuario };
                
                object Result = SqlHelper.ExecuteScalar(Conexion, "[prestamoHerramienta].[spCSLDB_set_FinalizarDevolucion]", Parametros);
                if (Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    Datos.Completado = (Resultado == 1);
                    Datos.Resultado = Resultado;
                }
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PrestamosHerramientasDetalleViewModels ObtenerDatosSalidaDetalle(string Conexion, int IDPrestamoDetalle)
        {
            try
            {
                PrestamosHerramientasDetalleViewModels Result = new PrestamosHerramientasDetalleViewModels();
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "[prestamoHerramienta].[spCSLDB_get_DatosDetallePrestamoHerramiendaXID]", IDPrestamoDetalle);
                while (Dr.Read())
                {
                    Result.IDPrestamoDetalle = !Dr.IsDBNull(Dr.GetOrdinal("IDPrestamoDetalle")) ? Dr.GetInt32(Dr.GetOrdinal("IDPrestamoDetalle")) : 0;
                    Result.IDPrestamo = !Dr.IsDBNull(Dr.GetOrdinal("IDPrestamo")) ? Dr.GetInt32(Dr.GetOrdinal("IDPrestamo")) : 0;
                    Result.IDProductoAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDProducto")) ? Dr.GetString(Dr.GetOrdinal("IDProducto")) : string.Empty;
                    Result.IDUnidadProducto = !Dr.IsDBNull(Dr.GetOrdinal("IDUnidad")) ? Dr.GetString(Dr.GetOrdinal("IDUnidad")) : string.Empty;
                    Result.Cantidad = !Dr.IsDBNull(Dr.GetOrdinal("Cantidad")) ? Dr.GetDecimal(Dr.GetOrdinal("Cantidad")) : 0;
                    break;
                }
                Dr.Close();
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PrestamoHerramientaModels EliminarPrestamo(string Conexion, int IDPrestamo, string Usuario)
        {
            try
            {
                PrestamoHerramientaModels Datos = new PrestamoHerramientaModels();
                object[] Parametros = { IDPrestamo, Usuario };
                object Result = SqlHelper.ExecuteScalar(Conexion, "[prestamoHerramienta].[spCSLDB_del_PresatamoHerramientaAlmacen]", Parametros);
                if (Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    Datos.Completado = (Resultado == 1);
                    Datos.Resultado = Resultado;
                }
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PrestamoHerramientaDetalleModels EliminarPrestamoDetalle(string Conexion, int IdPrestamoDetalle, string Usuario)
        {
            try
            {
                PrestamoHerramientaDetalleModels Datos = new PrestamoHerramientaDetalleModels();
                object[] Parametros = { IdPrestamoDetalle, Usuario };
                object Result = SqlHelper.ExecuteScalar(Conexion, "[prestamoHerramienta].[spCSLDB_del_PrestamoHerramientaAlmacenDetalle]", Parametros);
                if (Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    Datos.Completado = (Resultado == 1);
                    Datos.Resultado = Resultado;
                }
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PrestamoHerramientaModels ProcesarPrestamoHerramientasAlmacen(string Conexion, int IdPrestamo, string IDUsuario)
        {
            try
            {
                PrestamoHerramientaModels Datos = new PrestamoHerramientaModels();
                object[] Parametros = { IdPrestamo, IDUsuario };
                object Result = SqlHelper.ExecuteScalar(Conexion, "[prestamoHerramienta].[spCSLDB_set_ProcesarPrestamo]", Parametros);
                if (Result != null)
                {

                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    Datos.Completado = (Resultado == 1);
                    Datos.Resultado = Resultado;
                }
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}