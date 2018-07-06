using CreativaSL.Web.Ganados.ViewModels;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _SalidaAlmacen_Datos
    {
        public List<SalidaAlmacenModels> ObtenerSalidas(string Conexion)
        {
            try
            {
                List<SalidaAlmacenModels> Lista = new List<SalidaAlmacenModels>();
                SalidaAlmacenModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Inventario_get_SalidasAlmacen");
                while(Dr.Read())
                {
                    Item = new SalidaAlmacenModels();
                    Item.IDSalidaAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDSalidaAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDSalidaAlmacen")) : string.Empty;
                    Item.FolioSalida = !Dr.IsDBNull(Dr.GetOrdinal("FolioSalida")) ? Dr.GetString(Dr.GetOrdinal("FolioSalida")) : string.Empty;
                    Item.Comentario = !Dr.IsDBNull(Dr.GetOrdinal("Comentarios")) ? Dr.GetString(Dr.GetOrdinal("Comentarios")) : string.Empty;
                    Item.Sucursal.NombreSucursal = !Dr.IsDBNull(Dr.GetOrdinal("Sucursal")) ? Dr.GetString(Dr.GetOrdinal("Sucursal")) : string.Empty;
                    Item.Almacen.nombreAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("Almacen")) ? Dr.GetString(Dr.GetOrdinal("Almacen")) : string.Empty;
                    Item.FechaSalida = !Dr.IsDBNull(Dr.GetOrdinal("FechaSalida")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaSalida")) : DateTime.MinValue;
                    Item.Estatus = !Dr.IsDBNull(Dr.GetOrdinal("Estatus")) ? Dr.GetString(Dr.GetOrdinal("Estatus")) : string.Empty;
                    Item.CssEstatus = !Dr.IsDBNull(Dr.GetOrdinal("CssClass")) ? Dr.GetString(Dr.GetOrdinal("CssClass")) : string.Empty;
                    Item.IDEstatus = !Dr.IsDBNull(Dr.GetOrdinal("IDEstatus")) ? Dr.GetInt16(Dr.GetOrdinal("IDEstatus")) : -1;
                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void ACSalidaAlmacen(SalidaAlmacenModels Datos)
        {
            try
            {
                object[] Parametros = {
                            Datos.NuevoRegistro,
                            Datos.IDSalidaAlmacen ?? string.Empty,
                            Datos.Sucursal.IDSucursal ?? string.Empty,
                            Datos.Almacen.IDAlmacen ?? string.Empty,
                            Datos.Empleado.IDEmpleado ?? string.Empty,
                            Datos.FechaSalida,
                            Datos.Comentario ?? string.Empty, 
                            Datos.Usuario
                        };
                SqlDataReader Dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Inventario_AC_SalidaAlmacen", Parametros);
                while (Dr.Read())
                {
                    Datos.Resultado = !Dr.IsDBNull(Dr.GetOrdinal("Resultado")) ? Dr.GetInt32(Dr.GetOrdinal("Resultado")) : -1;
                    Datos.IDSalidaAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDSalidaAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDSalidaAlmacen")) : string.Empty;
                    Datos.Completado = (Datos.Resultado == 1);
                    break;
                }
                Dr.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public SalidaAlmacenModels EliminarSalida(string Conexion, string IDSalidaAlmacen, string Usuario)
        {
            try
            {
                SalidaAlmacenModels Datos = new SalidaAlmacenModels();
                object[] Parametros = { IDSalidaAlmacen, Usuario };
                object Result = SqlHelper.ExecuteScalar(Conexion, "spCSLDB_Inventario_del_SalidaAlmacen", Parametros);
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

        public SalidaAlmacenViewModels ObtenerDetalleSalida(string Conexion, string IDSalida)
        {
            try
            {
                SalidaAlmacenViewModels Resultado = new SalidaAlmacenViewModels();
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Inventario_get_DatosGeneralesSalidaAlmacenXID", IDSalida);
                while(Dr.Read())
                {
                    Resultado.IDSalidaAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDSalidaAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDSalidaAlmacen")) : string.Empty;
                    Resultado.IDSucursal = !Dr.IsDBNull(Dr.GetOrdinal("IDSucursal")) ? Dr.GetString(Dr.GetOrdinal("IDSucursal")) : string.Empty;
                    Resultado.IDAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDAlmacen")) : string.Empty;
                    Resultado.IDEmpleado = !Dr.IsDBNull(Dr.GetOrdinal("IDEmpleado")) ? Dr.GetString(Dr.GetOrdinal("IDEmpleado")) : string.Empty;
                    Resultado.FechaSalida = !Dr.IsDBNull(Dr.GetOrdinal("FechaSalida")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaSalida")) : DateTime.MinValue;
                    Resultado.Comentario = !Dr.IsDBNull(Dr.GetOrdinal("Comentarios")) ? Dr.GetString(Dr.GetOrdinal("Comentarios")) : string.Empty;
                    break;
                }
                Dr.Close();
                return Resultado;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public decimal ObtenerExistenciaXIDProductoIDUnidadIDSalida(string Conexion, string IDSalida, string IDProducto, string IDUnidad)
        {
            try
            {
                decimal Existencia = 0;
                object[] Parametros = { IDProducto, IDUnidad, IDSalida };
                object Result = SqlHelper.ExecuteScalar(Conexion, "spCSLDB_Inventario_get_ExistenciaXIDProductoIDSalida", Parametros);
                if(Result != null)
                {
                    decimal.TryParse(Result.ToString(), out Existencia);
                }
                return Existencia;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public void ACSalidaAlmacenDetalle(SalidaAlmacenDetalleModels Datos)
        {
            try
            {
                object[] Parametros = {
                    Datos.NuevoRegistro,
                    Datos.IDSalidaDetalle ?? string.Empty,
                    Datos.IDSalida ?? string.Empty,
                    Datos.Producto.IDProductoAlmacen ?? string.Empty,
                    Datos.UnidadMedida.id_unidadProducto ?? string.Empty,
                    Datos.Cantidad,
                    Datos.Usuario ?? string.Empty };
                object Result = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Inventario_ac_SalidaAlmacenDetalle", Parametros);
                if(Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    Datos.Completado = (Resultado == 1);
                    Datos.Resultado = Resultado;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public SalidaAlmacenDetalleModels EliminarSalidaDetalle(string Conexion, string IDSalidaDetalle, string Usuario)
        {
            try
            {
                SalidaAlmacenDetalleModels Datos = new SalidaAlmacenDetalleModels();
                object[] Parametros = { IDSalidaDetalle, Usuario };
                object Result = SqlHelper.ExecuteScalar(Conexion, "spCSLDB_Inventario_del_SalidaAlmacenDetalle", Parametros);
                if(Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    Datos.Completado = (Resultado == 1);
                    Datos.Resultado = Resultado;
                }
                return Datos;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<SalidaAlmacenDetalleModels> ObtenerListaDetalleSalida(string Conexion, string IDSalida)
        {
            try
            {
                List<SalidaAlmacenDetalleModels> Lista = new List<SalidaAlmacenDetalleModels>();
                SalidaAlmacenDetalleModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Inventario_get_DetalleSalidaXID", IDSalida);
                while(Dr.Read())
                {
                    Item = new SalidaAlmacenDetalleModels();
                    Item.IDSalidaDetalle = !Dr.IsDBNull(Dr.GetOrdinal("IDSalidaDetalle")) ? Dr.GetString(Dr.GetOrdinal("IDSalidaDetalle")) : string.Empty;
                    Item.Producto.Nombre = !Dr.IsDBNull(Dr.GetOrdinal("Producto")) ? Dr.GetString(Dr.GetOrdinal("Producto")) : string.Empty;
                    Item.Cantidad = !Dr.IsDBNull(Dr.GetOrdinal("Cantidad")) ? Dr.GetDecimal(Dr.GetOrdinal("Cantidad")) : 0;
                    Item.UnidadMedida.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("UnidadMedida")) ? Dr.GetString(Dr.GetOrdinal("UnidadMedida")) : string.Empty;
                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public SalidaAlmacenDetalleViewModels ObtenerDatosSalidaDetalle(string Conexion, string IDSalidaDetalle)
        {
            try
            {
                SalidaAlmacenDetalleViewModels Result = new SalidaAlmacenDetalleViewModels();
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Inventario_get_DatosDetalleSalidaXID", IDSalidaDetalle);
                while (Dr.Read())
                {
                    Result.IDSalidaDetalle = !Dr.IsDBNull(Dr.GetOrdinal("IDSalidaDetalle")) ? Dr.GetString(Dr.GetOrdinal("IDSalidaDetalle")) : string.Empty;
                    Result.IDSalida = !Dr.IsDBNull(Dr.GetOrdinal("IDSalida")) ? Dr.GetString(Dr.GetOrdinal("IDSalida")) : string.Empty;
                    Result.IDProductoAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDProducto")) ? Dr.GetString(Dr.GetOrdinal("IDProducto")) : string.Empty;
                    Result.IDUnidadProducto = !Dr.IsDBNull(Dr.GetOrdinal("IDUnidad")) ? Dr.GetString(Dr.GetOrdinal("IDUnidad")) : string.Empty;
                    Result.Cantidad = !Dr.IsDBNull(Dr.GetOrdinal("Cantidad")) ? Dr.GetDecimal(Dr.GetOrdinal("Cantidad")) : 0;
                    break;
                }
                Dr.Close();
                return Result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public SalidaAlmacenModels ProcesarSalidaAlmacen(string Conexion, string IDSalida, string IDUsuario)
        {
            try
            {
                SalidaAlmacenModels Datos = new SalidaAlmacenModels();
                object[] Parametros = { IDSalida, IDUsuario };
                object Result = SqlHelper.ExecuteScalar(Conexion, "spCSLDB_Inventario_set_ProcesarSalida", Parametros);
                if(Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    Datos.Completado = (Resultado == 1);
                    Datos.Resultado = Resultado;
                }
                return Datos;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}