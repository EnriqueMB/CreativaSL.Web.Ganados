using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CompraAlmacen_Datos
    {
        public List<CompraAlmacenModels> ObtenerGridCompras(CompraAlmacenModels Datos)
        {
            try
            {
                List<CompraAlmacenModels> lista = new List<CompraAlmacenModels>();
                CompraAlmacenModels item;
                SqlDataReader dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_CompraAlmcen");
                while (dr.Read())
                {
                    item = new CompraAlmacenModels();
                    item.IDCompraAlmacen = dr["id_compraAlmacen"].ToString();
                    //item.IDCompraAlmacen = !dr.IsDBNull(dr.GetOrdinal("id_compraAlmacen")) ? dr.GetString(dr.GetOrdinal("id_compraAlmacen")) : string.Empty;
                    item.NumFacturaNota = !dr.IsDBNull(dr.GetOrdinal("numFacturaNota")) ? dr.GetString(dr.GetOrdinal("numFacturaNota")) : string.Empty;
                    item.Sucursal.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("nombreSuc")) ? dr.GetString(dr.GetOrdinal("nombreSuc")) : string.Empty;
                    item.Proveedor.nombreProveedor = !dr.IsDBNull(dr.GetOrdinal("proveedor")) ? dr.GetString(dr.GetOrdinal("proveedor")) : string.Empty;
                    item.IDEstatusCompra = !dr.IsDBNull(dr.GetOrdinal("id_estatusCompra")) ? dr.GetInt16(dr.GetOrdinal("id_estatusCompra")) : 0;
                    item.StatusCompra = !dr.IsDBNull(dr.GetOrdinal("estatus")) ? dr.GetString(dr.GetOrdinal("estatus")) : string.Empty;
                    item.MontoTotal = !dr.IsDBNull(dr.GetOrdinal("montoTotal")) ? dr.GetDecimal(dr.GetOrdinal("montoTotal")) : 0;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<CatSucursalesModels> ObtenerComboSucursales(string Conexion)
        {
            try
            {
                List<CatSucursalesModels> lista = new List<CatSucursalesModels>();
                CatSucursalesModels item;
                SqlDataReader dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_get_ComboSucursalesAlmacen");
                while(dr.Read())
                {
                    item = new CatSucursalesModels();
                    item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                    item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("nombre")) ? dr.GetString(dr.GetOrdinal("nombre")) : string.Empty;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatProveedorModels> ObtenerComboProveedores(string Conexion)
        {
            try
            {
                List<CatProveedorModels> lista = new List<CatProveedorModels>();
                CatProveedorModels item;
                SqlDataReader dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_get_ComboProveedoresAlmacen");
                while (dr.Read())
                {
                    item = new CatProveedorModels();
                    item.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("id_proveedorAlmacen")) ? dr.GetString(dr.GetOrdinal("id_proveedorAlmacen")) : string.Empty;
                    item.nombreProveedor = !dr.IsDBNull(dr.GetOrdinal("nombre")) ? dr.GetString(dr.GetOrdinal("nombre")) : string.Empty;
                    lista.Add(item);
                }
                return lista;
            }
            catch ( Exception ex)
            {
                throw ex;
            }
        }
        public CompraAlmacenModels ABCCompraAlmacen(CompraAlmacenModels Datos)
        {
            try
            {
                object[] parametros =
                {
                    Datos.Opcion, Datos.IDCompraAlmacen, Datos.Sucursal.IDSucursal,Datos.Fecha,Datos.Proveedor.IDProveedor, Datos.NumFacturaNota, Datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Catalogo_ac_CompraAlmacen", parametros);
                if (Resultado != null)
                {
                    Datos.IDCompraAlmacen = Resultado.ToString();
                    if (!string.IsNullOrEmpty(Datos.IDCompraAlmacen))
                    {
                        Datos.Completado = true;
                    }
                    else
                    {
                        Datos.Completado = false;
                    }
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