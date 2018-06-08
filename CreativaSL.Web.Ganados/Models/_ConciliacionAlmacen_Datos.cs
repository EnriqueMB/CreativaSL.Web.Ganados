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
    public class _ConciliacionAlmacen_Datos
    {
        public List<ConciliacionAlmacenModels> ObtenerConciliaciones(string Conexion)
        {
            try
            {
                List<ConciliacionAlmacenModels> Lista = new List<ConciliacionAlmacenModels>();
                ConciliacionAlmacenModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Inventario_get_Conciliaciones");
                while(Dr.Read())
                {
                    Item = new ConciliacionAlmacenModels();
                    Item.IDConciliacionAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDConciliacion")) ? Dr.GetString(Dr.GetOrdinal("IDConciliacion")) : string.Empty;
                    Item.FolioConciliacion = !Dr.IsDBNull(Dr.GetOrdinal("FolioConciliacion")) ? Dr.GetString(Dr.GetOrdinal("FolioConciliacion")) : string.Empty;
                    Item.Estatus = !Dr.IsDBNull(Dr.GetOrdinal("Estatus")) ? Dr.GetString(Dr.GetOrdinal("Estatus")) : string.Empty;
                    Item.CssClass = !Dr.IsDBNull(Dr.GetOrdinal("CssClass")) ? Dr.GetString(Dr.GetOrdinal("CssClass")) : string.Empty;
                    Item.IDEstatus = !Dr.IsDBNull(Dr.GetOrdinal("IDEstatus")) ? Dr.GetInt16(Dr.GetOrdinal("IDEstatus")) : 0;
                    Item.Sucursal.NombreSucursal = !Dr.IsDBNull(Dr.GetOrdinal("Sucursal")) ? Dr.GetString(Dr.GetOrdinal("Sucursal")) : string.Empty;
                    Item.Almacen.nombreAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("Almacen")) ? Dr.GetString(Dr.GetOrdinal("Almacen")) : string.Empty;
                    Item.TipoConciliacion.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("TipoConciliacion")) ? Dr.GetString(Dr.GetOrdinal("TipoConciliacion")) : string.Empty;
                    Item.FechaConciliacion = !Dr.IsDBNull(Dr.GetOrdinal("FechaConciliacion")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaConciliacion")) : DateTime.MinValue;
                    Item.Comentario = !Dr.IsDBNull(Dr.GetOrdinal("Comentarios")) ? Dr.GetString(Dr.GetOrdinal("Comentarios")) : string.Empty;                    
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

        public ConciliacionAlmacenModels ObtenerDetalleConciliaciones(string Conexion, string IDConciliacion)
        {
            try
            {
                ConciliacionAlmacenModels Result = new ConciliacionAlmacenModels();

                List<ConciliacionAlmacenDetalleModel> Lista = new List<ConciliacionAlmacenDetalleModel>();
                ConciliacionAlmacenDetalleModel Item;
                DataSet Ds = SqlHelper.ExecuteDataset(Conexion, "spCSLDB_Inventario_get_DetalleConciliacionXID", IDConciliacion);
                if(Ds!= null)
                {
                    if(Ds.Tables.Count == 2)
                    {

                        if(Ds.Tables[0].Rows.Count > 0)
                        {
                            int IDTipoConciliacion = 0;
                            int.TryParse(Ds.Tables[0].Rows[0][0].ToString(), out IDTipoConciliacion);
                            Result.TipoConciliacion.IDTipoConciliacion = IDTipoConciliacion;
                        }

                        DataTableReader Dr = Ds.Tables[1].CreateDataReader();
                        while (Dr.Read())
                        {
                            Item = new ConciliacionAlmacenDetalleModel();
                            Item.IDConciliacionAlmacenDetalle = !Dr.IsDBNull(Dr.GetOrdinal("IDConciliacionDetalle")) ? Dr.GetString(Dr.GetOrdinal("IDConciliacionDetalle")) : string.Empty;
                            Item.Producto.Nombre = !Dr.IsDBNull(Dr.GetOrdinal("Producto")) ? Dr.GetString(Dr.GetOrdinal("Producto")) : string.Empty;
                            Item.Cantidad = !Dr.IsDBNull(Dr.GetOrdinal("Cantidad")) ? Dr.GetDecimal(Dr.GetOrdinal("Cantidad")) : 0;
                            Item.UnidadMedida.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("UnidadMedida")) ? Dr.GetString(Dr.GetOrdinal("UnidadMedida")) : string.Empty;
                            Item.Precio = !Dr.IsDBNull(Dr.GetOrdinal("Precio")) ? Dr.GetDecimal(Dr.GetOrdinal("Precio")) : 0;
                            Lista.Add(Item);
                        }
                        Dr.Close();
                        Result.ListaDetalle = Lista;
                    }
                }
                return Result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ConciliacionAlmacenViewModels ObtenerDatosDetalleConciliacion(string Conexion, string IDConciliacion)
        {
            try
            {
                ConciliacionAlmacenViewModels Datos = new ConciliacionAlmacenViewModels();
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Inventario_get_DatosDetalleConciliacionXID", IDConciliacion);
                while (Dr.Read())
                {
                    Datos.IDConciliacion = !Dr.IsDBNull(Dr.GetOrdinal("IDConciliacion")) ? Dr.GetString(Dr.GetOrdinal("IDConciliacion")) : string.Empty;
                    Datos.IDSucursal = !Dr.IsDBNull(Dr.GetOrdinal("IDSucursal")) ? Dr.GetString(Dr.GetOrdinal("IDSucursal")) : string.Empty;
                    Datos.IDAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDAlmacen")) : string.Empty;
                    Datos.IDTipoConciliacion = !Dr.IsDBNull(Dr.GetOrdinal("IDTipoConciliacion")) ? Dr.GetInt16(Dr.GetOrdinal("IDTipoConciliacion")) : 0;
                    Datos.FechaConciliacion = !Dr.IsDBNull(Dr.GetOrdinal("FechaConciliacion")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaConciliacion")) : DateTime.MinValue;
                    Datos.Comentarios = !Dr.IsDBNull(Dr.GetOrdinal("Comentarios")) ? Dr.GetString(Dr.GetOrdinal("Comentarios")) : string.Empty;
                    break;
                }
                Dr.Close();
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConciliacionAlmacenDetalleViewModels ObtenerDatosConciliacionDetalle(string Conexion, string IDConciliacionDetalle)
        {
            try
            {
                ConciliacionAlmacenDetalleViewModels Datos = new ConciliacionAlmacenDetalleViewModels();
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Inventario_get_DatosConciliacionDetalleXID", IDConciliacionDetalle);
                while (Dr.Read())
                {
                    Datos.IDConciliacionDetalle = !Dr.IsDBNull(Dr.GetOrdinal("IDConciliacionDetalle")) ? Dr.GetString(Dr.GetOrdinal("IDConciliacionDetalle")) : string.Empty;
                    Datos.IDConciliacion = !Dr.IsDBNull(Dr.GetOrdinal("IDConciliacion")) ? Dr.GetString(Dr.GetOrdinal("IDConciliacion")) : string.Empty;
                    Datos.IDProductoAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDProducto")) ? Dr.GetString(Dr.GetOrdinal("IDProducto")) : string.Empty;
                    Datos.IDUnidadProducto = !Dr.IsDBNull(Dr.GetOrdinal("IDUnidadProducto")) ? Dr.GetString(Dr.GetOrdinal("IDUnidadProducto")) : string.Empty;
                    Datos.Cantidad = !Dr.IsDBNull(Dr.GetOrdinal("Cantidad")) ? Dr.GetDecimal(Dr.GetOrdinal("Cantidad")) : 0;
                    Datos.Precio = !Dr.IsDBNull(Dr.GetOrdinal("Precio")) ? Dr.GetDecimal(Dr.GetOrdinal("Precio")) : 0;
                }
                Dr.Close();
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ACConciliacionAlmacen(ConciliacionAlmacenModels Datos)
        {
            try
            {
                object [] Parametros = {
                        Datos.NuevoRegistro,
                        Datos.IDConciliacionAlmacen ?? string.Empty,
                        Datos.Sucursal.IDSucursal ?? string.Empty,
                        Datos.Almacen.IDAlmacen ?? string.Empty,
                        Datos.FechaConciliacion,
                        Datos.TipoConciliacion.IDTipoConciliacion,
                        Datos.Comentario ?? string.Empty,
                        Datos.Usuario ?? string.Empty
                    };
                SqlDataReader Dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Inventario_ac_Conciliacion", Parametros);
                while(Dr.Read())
                {
                    int Resultado = !Dr.IsDBNull(Dr.GetOrdinal("Resultado")) ? Dr.GetInt32(Dr.GetOrdinal("Resultado")) : -1;
                    if(Resultado == 1)
                    {
                        Datos.Completado = true;
                        Datos.IDConciliacionAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDConciliacion")) ? Dr.GetString(Dr.GetOrdinal("IDConciliacion")) : string.Empty;
                    }
                    break;
                }
                Dr.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public decimal ObtenerExistenciaXIDProductoIDUnidadIDConciliacion(string Conexion, string IDConciliacion, string IDProducto, string IDUnidad)
        {
            try
            {
                decimal Existencia = 0;
                object[] Parametros = { IDProducto, IDUnidad, IDConciliacion };
                object Result = SqlHelper.ExecuteScalar(Conexion, "spCSLDB_Inventario_get_ExistenciaXIDProductoIDConciliacion", Parametros);
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

        public void ACConciliacionAlmacenDetallePos(ConciliacionAlmacenDetalleModel Datos)
        {
            try
            {
                object[] Parametros = { Datos.NuevoRegistro,
                                        Datos.IDConciliacionAlmacenDetalle ?? string.Empty,
                                        Datos.IDConciliacionAlmacen ?? string.Empty,
                                        Datos.Producto.IDProductoAlmacen ?? string.Empty,
                                        Datos.UnidadMedida.id_unidadProducto ?? string.Empty,
                                        Datos.Cantidad,
                                        Datos.Precio,
                                        Datos.Usuario ?? string.Empty };
                object Result = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Inventario_ac_ConciliacionDetallePos", Parametros);
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

        public void ACConciliacionAlmacenDetalleNeg(ConciliacionAlmacenDetalleModel Datos)
        {
            try
            {
                object[] Parametros = { Datos.NuevoRegistro,
                                        Datos.IDConciliacionAlmacenDetalle ?? string.Empty,
                                        Datos.IDConciliacionAlmacen ?? string.Empty,
                                        Datos.Producto.IDProductoAlmacen ?? string.Empty,
                                        Datos.UnidadMedida.id_unidadProducto ?? string.Empty,
                                        Datos.Cantidad,
                                        Datos.Usuario ?? string.Empty };
                object Result = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Inventario_ac_ConciliacionDetalleNeg", Parametros);
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

        public ConciliacionAlmacenDetalleModel EliminarConciliacionDetalle(string Conexion, string IDConciliacionDetalle, string IDUsuario)
        {
            try
            {
                ConciliacionAlmacenDetalleModel Datos = new ConciliacionAlmacenDetalleModel();
                object[] Parametros = new object [] { IDConciliacionDetalle, IDUsuario };
                object Result = SqlHelper.ExecuteScalar(Conexion, "spCSLDB_Inventario_del_ConciliacionDetalle", Parametros);
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

        public ConciliacionAlmacenModels EliminarConciliacion(string Conexion, string IDConciliacion, string IDUsuario)
        {
            try
            {
                ConciliacionAlmacenModels Datos = new ConciliacionAlmacenModels();
                object[] Parametros = new object[] { IDConciliacion, IDUsuario };
                object Result = SqlHelper.ExecuteScalar(Conexion, "spCSLDB_Inventario_del_Conciliacion", Parametros);
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

        public ConciliacionAlmacenModels ProcesarConciliacion(string Conexion, string IDConciliacion, string IDUsuario)
        {
            try
            {
                ConciliacionAlmacenModels Datos = new ConciliacionAlmacenModels();
                object[] Parametros = new object[] { IDConciliacion, IDUsuario };
                object Result = SqlHelper.ExecuteScalar(Conexion, "spCSLDB_Inventario_set_ProcesarConciliacion", Parametros);
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