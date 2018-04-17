using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
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
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Inventario_get_EntradasAlmacen", Folio);
                while (Dr.Read())
                {
                    Item = new EntradaAlmacenModels();
                    Item.IDEntradaAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDEntradaAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDEntradaAlmacen")) : string.Empty;
                    Item.Almacen = new CatAlmacenModels { Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Almacen")) ? Dr.GetString(Dr.GetOrdinal("Almacen")) : string.Empty };
                    Item.CompraAlmacen = new CompraAlmacenModels { NumFacturaNota = !Dr.IsDBNull(Dr.GetOrdinal("NumFacturaNota")) ? Dr.GetString(Dr.GetOrdinal("NumFacturaNota")) : string.Empty };
                    Item.FolioEntrada = !Dr.IsDBNull(Dr.GetOrdinal("FolioEntrada")) ? Dr.GetString(Dr.GetOrdinal("FolioEntrada")) : string.Empty;
                    Item.FechaEntrada = !Dr.IsDBNull(Dr.GetOrdinal("FechaEntrada")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaEntrada")) : DateTime.MinValue;
                    Item.Comentario = !Dr.IsDBNull(Dr.GetOrdinal("Comentarios")) ? Dr.GetString(Dr.GetOrdinal("Comentarios")) : string.Empty;
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<EntradaAlmacenDetalleModels> ObtenerDetalleEntradaXID(string Conexion, string IDEntraada)
        {
            try
            {
                List<EntradaAlmacenDetalleModels> Lista = new List<EntradaAlmacenDetalleModels>();
                EntradaAlmacenDetalleModels Item = new EntradaAlmacenDetalleModels();
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Inventario_get_DetalleEntregaXID", IDEntraada);
                while(Dr.Read())
                {
                    Item = new EntradaAlmacenDetalleModels();
                    Item.IDEntradaAlmacenDetalle = !Dr.IsDBNull(Dr.GetOrdinal("IDEntradaAlmacenDetalle")) ? Dr.GetString(Dr.GetOrdinal("IDEntradaAlmacenDetalle")) : string.Empty;
                    Item.CompraDetalle = new CompraAlmacenDetalleModels { IDCompraAlmacenDetalle =  !Dr.IsDBNull(Dr.GetOrdinal("IDCompraDetalle")) ? Dr.GetString(Dr.GetOrdinal("IDCompraDetalle")) : string.Empty };
                    Item.ProductoAlmacen = new CatProductosAlmacenModels { Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Producto")) ? Dr.GetString(Dr.GetOrdinal("Producto")) : string.Empty };
                    Item.CompraDetalle.Cantidad = !Dr.IsDBNull(Dr.GetOrdinal("Cantidad")) ? Dr.GetDecimal(Dr.GetOrdinal("Cantidad")) : 0;
                    Item.CompraDetalle.CantidadAsignada = !Dr.IsDBNull(Dr.GetOrdinal("CantidadAsignada")) ? Dr.GetDecimal(Dr.GetOrdinal("CantidadAsignada")) : 0;
                    Item.Cantidad = !Dr.IsDBNull(Dr.GetOrdinal("CantidadEntrega")) ? Dr.GetDecimal(Dr.GetOrdinal("CantidadEntrega")) : 0;
                    Item.UnidadMedida = new CatUnidadMedidaModels { Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("UnidadMedida")) ? Dr.GetString(Dr.GetOrdinal("UnidadMedida")) : string.Empty };
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch(Exception ex)
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
    }
}