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
    public class _EntradaAlmacen_Datos
    {
        public List<EntradaAlmacenModels> ObtenerEntradas(string Conexion, string Folio)
        {
            try
            {
                List<EntradaAlmacenModels> Lista = new List<EntradaAlmacenModels>();
                EntradaAlmacenModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Inventario_get_EntradasAlmacen", Folio ?? string.Empty);
                while (Dr.Read())
                {
                    Item = new EntradaAlmacenModels();
                    Item.IDEntradaAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDEntradaAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDEntradaAlmacen")) : string.Empty;
                    Item.Almacen = new CatAlmacenModels { Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Almacen")) ? Dr.GetString(Dr.GetOrdinal("Almacen")) : string.Empty };
                    Item.CompraAlmacen = new CompraAlmacenModels { NumFacturaNota = !Dr.IsDBNull(Dr.GetOrdinal("NumFacturaNota")) ? Dr.GetString(Dr.GetOrdinal("NumFacturaNota")) : string.Empty };
                    Item.FolioEntrada = !Dr.IsDBNull(Dr.GetOrdinal("FolioEntrada")) ? Dr.GetString(Dr.GetOrdinal("FolioEntrada")) : string.Empty;
                    Item.FechaEntrada = !Dr.IsDBNull(Dr.GetOrdinal("FechaEntrada")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaEntrada")) : DateTime.MinValue;
                    Item.Comentario = !Dr.IsDBNull(Dr.GetOrdinal("Comentarios")) ? Dr.GetString(Dr.GetOrdinal("Comentarios")) : string.Empty;
                    Item.Finalizado = !Dr.IsDBNull(Dr.GetOrdinal("Finalizado")) ? Dr.GetBoolean(Dr.GetOrdinal("Finalizado")) : false;
                    Item.Estatus = !Dr.IsDBNull(Dr.GetOrdinal("Estatus")) ? Dr.GetString(Dr.GetOrdinal("Estatus")) : string.Empty;
                    Item.CssEstatus = !Dr.IsDBNull(Dr.GetOrdinal("CssEstatus")) ? Dr.GetString(Dr.GetOrdinal("CssEstatus")) : string.Empty;
                    Item.IDEstatus = !Dr.IsDBNull(Dr.GetOrdinal("IDEstatus")) ? Dr.GetInt32(Dr.GetOrdinal("IDEstatus")) : 0;
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

        public List<CantidadEntregaViewModels> ObtenerDetalleEntradaXID(string Conexion, string IDEntraada)
        {
            try
            {
                List<CantidadEntregaViewModels> Lista = new List<CantidadEntregaViewModels>();
                CantidadEntregaViewModels Item = new CantidadEntregaViewModels();
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Inventario_get_DetalleEntregaXID", IDEntraada);
                while(Dr.Read())
                {
                    Item = new CantidadEntregaViewModels();
                    Item.IDEntradaAlmacenDetalle = !Dr.IsDBNull(Dr.GetOrdinal("IDEntradaAlmacenDetalle")) ? Dr.GetString(Dr.GetOrdinal("IDEntradaAlmacenDetalle")) : string.Empty;
                    Item.IDCompraAlmacenDetalle = !Dr.IsDBNull(Dr.GetOrdinal("IDCompraDetalle")) ? Dr.GetString(Dr.GetOrdinal("IDCompraDetalle")) : string.Empty;
                    Item.Producto = !Dr.IsDBNull(Dr.GetOrdinal("Producto")) ? Dr.GetString(Dr.GetOrdinal("Producto")) : string.Empty;
                    Item.CantidadCompra = !Dr.IsDBNull(Dr.GetOrdinal("Cantidad")) ? Dr.GetDecimal(Dr.GetOrdinal("Cantidad")) : 0;
                    Item.CantidadAsignada = !Dr.IsDBNull(Dr.GetOrdinal("CantidadAsignada")) ? Dr.GetDecimal(Dr.GetOrdinal("CantidadAsignada")) : 0;
                    Item.Cantidad = !Dr.IsDBNull(Dr.GetOrdinal("CantidadEntrega")) ? Dr.GetDecimal(Dr.GetOrdinal("CantidadEntrega")) : 0;
                    Item.UnidadMedida = !Dr.IsDBNull(Dr.GetOrdinal("UnidadMedida")) ? Dr.GetString(Dr.GetOrdinal("UnidadMedida")) : string.Empty;
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

        public EntradaAlmacenViewModels ObtenerDatosEntradaXID(string Conexion, string IDEntrada)
        {
            try
            {
                EntradaAlmacenViewModels Result = new EntradaAlmacenViewModels();
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Inventario_get_DatosXIDEntrada", IDEntrada);
                while(Dr.Read())
                {
                    Result.IDEntradaAlmacen = IDEntrada;
                    Result.IDCompraAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDCompraAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDCompraAlmacen")) : string.Empty;
                    Result.IDAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDAlmacen")) : string.Empty;
                    Result.FechaEntrada  = !Dr.IsDBNull(Dr.GetOrdinal("FechaEntrada")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaEntrada")) : DateTime.MinValue;
                    Result.Comentario = !Dr.IsDBNull(Dr.GetOrdinal("Comentarios")) ? Dr.GetString(Dr.GetOrdinal("Comentarios")) : string.Empty;
                }
                Dr.Close();
                return Result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public EntradaAlmacenModels ProcesarEntrada(string Conexion, string IDEntrada, string IDUsuario)
        {
            try
            {
                EntradaAlmacenModels Result = new EntradaAlmacenModels();
                object[] Parametros = { IDEntrada, IDUsuario };
                object Value = SqlHelper.ExecuteScalar(Conexion, "spCSLDB_Inventario_set_ProcesarEntrada", Parametros);
                if (Value != null)
                {
                    int Resultado = 0;
                    int.TryParse(Value.ToString(), out Resultado);
                    if (Resultado == 1)
                    {
                        Result.Completado = true;
                        Result.Resultado = Resultado;
                    }
                }
                return Result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public EntradaAlmacenModels EliminarEntrada(string Conexion, string IDEntrada, string IDUsuario)
        {
            try
            {
                EntradaAlmacenModels Result = new EntradaAlmacenModels();
                object[] Parametros = { IDEntrada, IDUsuario };
                object Value = SqlHelper.ExecuteScalar(Conexion, "spCSLDB_Inventario_set_EliminarEntrada", Parametros);
                if (Value != null)
                {
                    int Resultado = 0;
                    int.TryParse(Value.ToString(), out Resultado);
                    if (Resultado == 1)
                    {
                        Result.Completado = true;
                        Result.Resultado = Resultado;
                    }
                }
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ACEntradaAlmacen(EntradaAlmacenModels Datos)
        {
            try
            {
                object[] Parametros = 
                {   Datos.NuevoRegistro,
                    Datos.IDEntradaAlmacen ?? string.Empty,
                    Datos.CompraAlmacen.IDCompraAlmacen ?? string.Empty,
                    Datos.Almacen.IDAlmacen ?? string.Empty,
                    Datos.FechaEntrada,
                    Datos.Comentario ?? string.Empty,
                    Datos.Usuario
                };
                object Result = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Inventario_ac_EntradaAlmacen", Parametros);
                if (Result != null)
                {
                    if (!string.IsNullOrEmpty(Result.ToString().Trim()))
                    {
                        Datos.Completado = true;
                        Datos.IDEntradaAlmacen = Result.ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void ACEntradaAlmacenDetalle(EntradaAlmacenDetalleModels Datos)
        {
            try
            {                
                object Result = SqlHelper.ExecuteScalar(Datos.Conexion, CommandType.StoredProcedure, "spCSLDB_Inventario_ac_EntradaAlmacenDetalle", 
                                                        new SqlParameter("@IDEntrada", Datos.IDEntradaAlmacen),
                                                        new SqlParameter("@TablaDetalle", Datos.TablaDatos),
                                                        new SqlParameter("@IDUsuario", Datos.Usuario));
                if(Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    Datos.Resultado = Resultado;
                    if(Resultado == 1)
                    {
                        Datos.Completado = true;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}