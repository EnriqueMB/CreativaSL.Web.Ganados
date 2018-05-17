using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CreativaSL.Web.Ganados.ViewModels;
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
        public CompraAlmacenDetalleModels ABCCompraAlmacenDetalle(CompraAlmacenDetalleModels datos)
        {
            try
            {
                object[] parametros = { datos.Opcion,
                    datos.IDCompraAlmacenDetalle,
                    datos.IDCompraAlmacen,
                    datos.Producto.IDProductoAlmacen,
                    datos.IDUnidadProducto,
                    datos.Cantidad,
                    datos.PrecioUnitario,
                    datos.Usuario
                };
                object resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_ac_compraAlmacenDetalle", parametros);
                if(resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro == 1)
                        {
                            datos.Completado = true;
                            datos.Resultado = IDRegistro;
                        }
                        if (IDRegistro == 2)
                        {
                            datos.Completado = false;
                            datos.Resultado = IDRegistro;
                        }
                        else if(IDRegistro > 2)
                        {
                            datos.Completado = false;
                            datos.Resultado = IDRegistro;
                        }
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CompraAlmacenModels ObtenerCompraAlmacen(CompraAlmacenModels Datos)
        {
            try
            {
                object[] parametros = { Datos.IDCompraAlmacen };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Almacen_get_Compra", parametros);
                while (dr.Read())
                {
                    Datos.Sucursal.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                    Datos.Fecha = !dr.IsDBNull(dr.GetOrdinal("fechaCompra")) ? dr.GetDateTime(dr.GetOrdinal("fechaCompra")) : DateTime.Now;
                    Datos.Proveedor.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("id_proveedorAlmacen")) ? dr.GetString(dr.GetOrdinal("id_proveedorAlmacen")) : string.Empty;
                    Datos.NumFacturaNota = !dr.IsDBNull(dr.GetOrdinal("numFacturaNota")) ? dr.GetString(dr.GetOrdinal("numFacturaNota")) : string.Empty;
                    Datos.MontoTotal = !dr.IsDBNull(dr.GetOrdinal("montoTotal")) ? dr.GetDecimal(dr.GetOrdinal("montoTotal")) : 0;
                    Datos.IDDocumentoXPagar = !dr.IsDBNull(dr.GetOrdinal("id_documentoXPagar")) ? dr.GetString(dr.GetOrdinal("id_documentoXPagar")) : string.Empty;
                }
                return Datos;
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
        public CompraAlmacenModels ACCompraAlmacen(CompraAlmacenModels Datos)
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
        public void ProcesarCompraAlmacen(CompraAlmacenModels Datos)
        {
            try
            {
                //PENDIENTE
                object[] parametros = { Datos.IDCompraAlmacen, Datos.Usuario };
                object resultado = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Almacen_ProcesarCompraXID", parametros);
                if (resultado != null)
                {
                    int Resultado = 0;
                    int.TryParse(resultado.ToString(), out Resultado);
                    Datos.Resultado = Resultado;
                    if (Resultado == 1)
                        Datos.Completado = true;
                }
                Datos.Completado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CompraAlmacenDetalleModels> ObtenerGridCompraDetalle(CompraAlmacenDetalleModels Datos)
        {
            try
            {
                List<CompraAlmacenDetalleModels> lista = new List<CompraAlmacenDetalleModels>();
                CompraAlmacenDetalleModels item;
                SqlDataReader dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_CompraDetalleXIDCompra",Datos.IDCompraAlmacen);
                while(dr.Read())
                {
                    item = new CompraAlmacenDetalleModels();
                    item.IDCompraAlmacen = !dr.IsDBNull(dr.GetOrdinal("id_compraAlmacenDetalle")) ? dr.GetString(dr.GetOrdinal("id_compraAlmacenDetalle")) : string.Empty;
                    item.IDCompraAlmacenDetalle = !dr.IsDBNull(dr.GetOrdinal("id_compraAlmacen")) ? dr.GetString(dr.GetOrdinal("id_compraAlmacen")) : string.Empty;
                    item.IDProductoAlmacen = !dr.IsDBNull(dr.GetOrdinal("id_productoAlmacen")) ? dr.GetString(dr.GetOrdinal("id_productoAlmacen")) : string.Empty;
                    item.IDUnidadProducto = !dr.IsDBNull(dr.GetOrdinal("id_unidadProducto")) ? dr.GetString(dr.GetOrdinal("id_unidadProducto")) : string.Empty;
                    item.Producto.Descripcion = !dr.IsDBNull(dr.GetOrdinal("producto")) ? dr.GetString(dr.GetOrdinal("producto")) : string.Empty;
                    item.UnidadMedida.Descripcion = !dr.IsDBNull(dr.GetOrdinal("unidad")) ? dr.GetString(dr.GetOrdinal("unidad")) : string.Empty;
                    item.PrecioUnitario = !dr.IsDBNull(dr.GetOrdinal("precioUnitario")) ? dr.GetDecimal(dr.GetOrdinal("precioUnitario")) : 0;
                    item.Cantidad = !dr.IsDBNull(dr.GetOrdinal("cantidad")) ? dr.GetDecimal(dr.GetOrdinal("cantidad")) : 0;
                    item.SubTotal = !dr.IsDBNull(dr.GetOrdinal("subtotal")) ? dr.GetDecimal(dr.GetOrdinal("subtotal")) : 0;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int ObtenerIdStatus(CompraAlmacenDetalleModels Datos)
        {
            try
            {
                object[] parametros = { Datos.IDCompraAlmacen };
                object resultado = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Almacen_get_IdStatus_CompraAlmacen", parametros);
                if(resultado != null)
                {
                    if(resultado.ToString() == "1")
                    {
                        Datos.id_status = Convert.ToInt32(resultado.ToString()); ;
                    }
                    else
                    {
                        Datos.id_status = Convert.ToInt32(resultado.ToString());
                    }
                }
                return Datos.id_status;
            }
            
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<CatProductosAlmacenModels> ObtenerComboProducto(CompraAlmacenDetallesViewModels Datos)
        {
            try
            {
                List<CatProductosAlmacenModels> lista = new List<CatProductosAlmacenModels>();
                CatProductosAlmacenModels item;
                SqlDataReader dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Almacen_Combo_get_CatProductoAlmacen");
                while(dr.Read())
                {
                    item = new CatProductosAlmacenModels();
                    item.IDProductoAlmacen = !dr.IsDBNull(dr.GetOrdinal("id_productoAlmacen")) ? dr.GetString(dr.GetOrdinal("id_productoAlmacen")) : string.Empty;
                    item.Nombre = !dr.IsDBNull(dr.GetOrdinal("nombre")) ? dr.GetString(dr.GetOrdinal("nombre")) : string.Empty;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<UnidadesProductosAlmacenModels> ObtenerComboUnidadMedida(CompraAlmacenDetallesViewModels Datos)
        {
            try
            {
                List<UnidadesProductosAlmacenModels> lista = new List<UnidadesProductosAlmacenModels>();
                UnidadesProductosAlmacenModels item;
                SqlDataReader dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Almacen_Combo_get_UnidadProductoAlmacen",Datos.IDProductoAlmacen);
                while(dr.Read())
                {
                    item = new UnidadesProductosAlmacenModels();
                    item.id_unidadProducto = !dr.IsDBNull(dr.GetOrdinal("IDUnidadMedida")) ? dr.GetString(dr.GetOrdinal("IDUnidadMedida")) : string.Empty;
                    item.NombreUnidad = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CompraAlmacenModels DeleteCompraAlmacen(CompraAlmacenModels Datos)
        {
            try
            {
                object resultado = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Almacen_del_CompraAlmacen", Datos.IDCompraAlmacen, Datos.Usuario);
                if(resultado != null)
                {
                    Datos.IDCompraAlmacen =resultado.ToString();
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
        public CompraAlmacenDetalleModels DeleteCompraAlmacenDetalle(CompraAlmacenDetalleModels Datos)
        {
            try
            {
                object[] parametros = { Datos.IDCompraAlmacenDetalle, Datos.IDCompraAlmacen, Datos.Usuario };
                object resultado = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Almacen_del_CompraAlmacenDetalle", parametros);
                if (resultado != null)
                {
                    int IDRegistro = Convert.ToInt32(resultado.ToString());
                        if (IDRegistro == 1)
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