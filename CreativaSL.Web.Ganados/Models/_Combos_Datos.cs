﻿using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Combos_Datos
    {
        public List<CatVehiculoModels> ObtenerComboVehiculos(string Conexion, string IdSucursal)
        {
            try
            {
                List<CatVehiculoModels> Lista = new List<CatVehiculoModels>();
                CatVehiculoModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatVehiculo", IdSucursal);
                while(Dr.Read())
                {
                    Item = new CatVehiculoModels();
                    Item.IDVehiculo = !Dr.IsDBNull(Dr.GetOrdinal("id_vehiculo")) ? Dr.GetString(Dr.GetOrdinal("id_vehiculo")) : string.Empty;
                    Item.Modelo = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
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

        public List<CatVehiculoModels> ObtenerComboVehiculosPrincp(string Conexion)
        {
            try
            {
                List<CatVehiculoModels> Lista = new List<CatVehiculoModels>();
                CatVehiculoModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatVehiculoEntregaCombustible");
                while (Dr.Read())
                {
                    Item = new CatVehiculoModels();
                    Item.IDVehiculo = !Dr.IsDBNull(Dr.GetOrdinal("id_vehiculo")) ? Dr.GetString(Dr.GetOrdinal("id_vehiculo")) : string.Empty;
                    Item.Modelo = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
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

        public List<CatTipoCombustibleModels> ObtenerComboTiposCombustible(string Conexion)
        {
            try
            {
                List<CatTipoCombustibleModels> Lista = new List<CatTipoCombustibleModels>();
                CatTipoCombustibleModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatTipoCombustible");
                while (Dr.Read())
                {
                    Item = new CatTipoCombustibleModels();
                    Item.IDTipoCombustible = !Dr.IsDBNull(Dr.GetOrdinal("id_tipoCombustible")) ? Dr.GetInt32(Dr.GetOrdinal("id_tipoCombustible")) : 0;
                    Item.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
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

        public List<CatSucursalesModels> ObtenerComboSucursales(string Conexion)
        {
            try
            {
                List<CatSucursalesModels> Lista = new List<CatSucursalesModels>();
                CatSucursalesModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatSucursal");
                while (Dr.Read())
                {
                    Item = new CatSucursalesModels();
                    Item.IDSucursal = !Dr.IsDBNull(Dr.GetOrdinal("IDSucursal")) ? Dr.GetString(Dr.GetOrdinal("IDSucursal")) : string.Empty;
                    Item.NombreSucursal = !Dr.IsDBNull(Dr.GetOrdinal("NombreSucursal")) ? Dr.GetString(Dr.GetOrdinal("NombreSucursal")) : string.Empty;
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

        public List<CatTipoServicioModels> ObtenerComboTipoServicio(string Conexion)
        {
            try
            {
                List<CatTipoServicioModels> Lista = new List<CatTipoServicioModels>();
                CatTipoServicioModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Mantenimiento_get_ComboTipoServicios");
                while(Dr.Read())
                {
                    Item = new CatTipoServicioModels();
                    Item.IDTipoServicio = !Dr.IsDBNull(Dr.GetOrdinal("IDTipoServicio")) ? Dr.GetString(Dr.GetOrdinal("IDTipoServicio")) : string.Empty;
                    Item.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
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

        public List<CompraAlmacenModels> ObtenerComprasProcesadas(string Conexion)
        {
            try
            {
                List<CompraAlmacenModels> Lista = new List<CompraAlmacenModels>();
                CompraAlmacenModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Inventario_get_ComboComprasProcesadas");
                while(Dr.Read())
                {
                    Item = new CompraAlmacenModels();
                    Item.IDCompraAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDCompraAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDCompraAlmacen")) : string.Empty;
                    Item.NumFacturaNota = !Dr.IsDBNull(Dr.GetOrdinal("NumFacturaNota")) ? Dr.GetString(Dr.GetOrdinal("NumFacturaNota")) : string.Empty;
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

        public List<CatAlmacenModels> ObtenerAlmacenes(string Conexion)
        {
            try
            {
                List<CatAlmacenModels> Lista = new List<CatAlmacenModels>();
                CatAlmacenModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatAlmacen");
                while(Dr.Read())
                {
                    Item = new CatAlmacenModels();
                    Item.IDAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDAlmacen")) : string.Empty;
                    Item.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
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

        public List<CatAlmacenModels> ObtenerAlmacenesXIDSucursal(string Conexion, string IDSucursal)
        {
            try
            {
                List<CatAlmacenModels> Lista = new List<CatAlmacenModels>();
                CatAlmacenModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatAlmacenXIDSucursal", IDSucursal);
                while (Dr.Read())
                {
                    Item = new CatAlmacenModels();
                    Item.IDAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDAlmacen")) : string.Empty;
                    Item.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
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

        public List<CatAlmacenModels> ObtenerAlmacenesXIDCompra(string Conexion, string IDCompra)
        {
            try
            {
                List<CatAlmacenModels> Lista = new List<CatAlmacenModels>();
                CatAlmacenModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatAlmacenXIDCompra", IDCompra);
                while (Dr.Read())
                {
                    Item = new CatAlmacenModels();
                    Item.IDAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDAlmacen")) : string.Empty;
                    Item.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
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

        public List<CatProveedorModels> ObtenerComboProveedoresCombustible(string Conexion, string IDSucursal)
        {
            try
            {
                List<CatProveedorModels> Lista = new List<CatProveedorModels>();
                CatProveedorModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatProveedorCombustible", IDSucursal);
                while(Dr.Read())
                {
                    Item = new CatProveedorModels();
                    Item.IDProveedor = !Dr.IsDBNull(Dr.GetOrdinal("IDProveedor")) ? Dr.GetString(Dr.GetOrdinal("IDProveedor")) : string.Empty;
                    Item.NombreRazonSocial = !Dr.IsDBNull(Dr.GetOrdinal("NombreRazonSocial")) ? Dr.GetString(Dr.GetOrdinal("NombreRazonSocial")) : string.Empty;
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

        public List<CatProveedorModels> ObtenerComboProveedoresMantenimiento(string Conexion, string IDSucursal)
        {
            try
            {
                List<CatProveedorModels> Lista = new List<CatProveedorModels>();
                CatProveedorModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatProveedorMantenimiento", IDSucursal);
                while (Dr.Read())
                {
                    Item = new CatProveedorModels();
                    Item.IDProveedor = !Dr.IsDBNull(Dr.GetOrdinal("IDProveedor")) ? Dr.GetString(Dr.GetOrdinal("IDProveedor")) : string.Empty;
                    Item.NombreRazonSocial = !Dr.IsDBNull(Dr.GetOrdinal("NombreRazonSocial")) ? Dr.GetString(Dr.GetOrdinal("NombreRazonSocial")) : string.Empty;
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

        public List<CatEmpleadoModels> ObtenerComboEmpleadosSalidaAlmacen(string Conexion, string IDSucursal)
        {
            try
            {
                List<CatEmpleadoModels> Lista = new List<CatEmpleadoModels>();
                CatEmpleadoModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_EmpleadosSalidaAlmacen", IDSucursal);
                while(Dr.Read())
                {
                    Item = new CatEmpleadoModels();
                    Item.IDEmpleado = !Dr.IsDBNull(Dr.GetOrdinal("IDEmpleado")) ? Dr.GetString(Dr.GetOrdinal("IDEmpleado")) : string.Empty;
                    Item.NombreCompleto = !Dr.IsDBNull(Dr.GetOrdinal("NombreEmpleado")) ? Dr.GetString(Dr.GetOrdinal("NombreEmpleado")) : string.Empty;
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

        public List<CatProductosAlmacenModels> ObtenerComboProductosAlmacenXIDSalida(string Conexion, string IDSalida)
        {
            try
            {
                List<CatProductosAlmacenModels> Lista = new List<CatProductosAlmacenModels>();
                CatProductosAlmacenModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_ProductosAlmacenXIDSalida", IDSalida);
                while (Dr.Read())
                {
                    Item = new CatProductosAlmacenModels();
                    Item.IDProductoAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDProductoAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDProductoAlmacen")) : string.Empty;
                    Item.Nombre = !Dr.IsDBNull(Dr.GetOrdinal("NombreProducto")) ? Dr.GetString(Dr.GetOrdinal("NombreProducto")) : string.Empty;
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
        
        public List<CatProductosAlmacenModels> ObtenerComboProductosAlmacenXIDConciliacion(string Conexion, string IDConciliacion)
        {
            try
            {
                List<CatProductosAlmacenModels> Lista = new List<CatProductosAlmacenModels>();
                CatProductosAlmacenModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_ProductosAlmacenXIDConciliacion", IDConciliacion);
                while (Dr.Read())
                {
                    Item = new CatProductosAlmacenModels();
                    Item.IDProductoAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDProductoAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDProductoAlmacen")) : string.Empty;
                    Item.Nombre = !Dr.IsDBNull(Dr.GetOrdinal("NombreProducto")) ? Dr.GetString(Dr.GetOrdinal("NombreProducto")) : string.Empty;
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
        
        public List<CatProductosAlmacenModels> ObtenerComboProductosAlmacenXIDAlmacen(string Conexion, string IDAlmacen)
        {
            try
            {
                List<CatProductosAlmacenModels> Lista = new List<CatProductosAlmacenModels>();
                CatProductosAlmacenModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_ProductosAlmacenXIDAlmacen", IDAlmacen);
                while (Dr.Read())
                {
                    Item = new CatProductosAlmacenModels();
                    Item.IDProductoAlmacen = !Dr.IsDBNull(Dr.GetOrdinal("IDProductoAlmacen")) ? Dr.GetString(Dr.GetOrdinal("IDProductoAlmacen")) : string.Empty;
                    Item.Nombre = !Dr.IsDBNull(Dr.GetOrdinal("NombreProducto")) ? Dr.GetString(Dr.GetOrdinal("NombreProducto")) : string.Empty;
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
        
        public List<UnidadesProductosAlmacenModels> ObtenerComboUnidadesXIDProducto(string Conexion, string IDProducto)
        {
            try
            {
                List<UnidadesProductosAlmacenModels> Lista = new List<UnidadesProductosAlmacenModels>();
                UnidadesProductosAlmacenModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_UnidadesXIDProducto", IDProducto);
                while(Dr.Read())
                {
                    Item = new UnidadesProductosAlmacenModels();
                    Item.id_unidadProducto = !Dr.IsDBNull(Dr.GetOrdinal("IDUnidad")) ? Dr.GetString(Dr.GetOrdinal("IDUnidad")) : string.Empty;
                    Item.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("UnidadMedida")) ? Dr.GetString(Dr.GetOrdinal("UnidadMedida")) : string.Empty;
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

        public List<CatTipoConciliacionModels> ObtenerComboTipoConciliacion(string Conexion)
        {
            try
            {
                List<CatTipoConciliacionModels> Lista = new List<CatTipoConciliacionModels>();
                CatTipoConciliacionModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatTipoConciliacion");
                while (Dr.Read())
                {
                    Item = new CatTipoConciliacionModels();
                    Item.IDTipoConciliacion = !Dr.IsDBNull(Dr.GetOrdinal("IDTipoConciliacion")) ? Dr.GetInt32(Dr.GetOrdinal("IDTipoConciliacion")) : 0;
                    Item.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
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

        public List<CatTipoClasificacionModels> ObtenerComboClasificacionGastos(string Conexion)
        {
            try
            {
                List<CatTipoClasificacionModels> Lista = new List<CatTipoClasificacionModels>();
                CatTipoClasificacionModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatTipoClasificacion");
                while(Dr.Read())
                {
                    Item = new CatTipoClasificacionModels();
                    Item.IDTipoClasificacionGasto = !Dr.IsDBNull(Dr.GetOrdinal("IDTipoClasificacion")) ? Dr.GetInt32(Dr.GetOrdinal("IDTipoClasificacion")) : 0;
                    Item.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
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

        public List<CatTipoClasificacionModels> ObtenerComboClasificacionGastosXTipoFormulario(string Conexion, int opcion)
        {
            try
            {
                object[] parametro =
                {
                    opcion
                };
                List<CatTipoClasificacionModels> Lista = new List<CatTipoClasificacionModels>();
                CatTipoClasificacionModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatTipoClasificacionXTipoFormulario", parametro);
                while (Dr.Read())
                {
                    Item = new CatTipoClasificacionModels();
                    Item.IDTipoClasificacionGasto = !Dr.IsDBNull(Dr.GetOrdinal("IDTipoClasificacion")) ? Dr.GetInt32(Dr.GetOrdinal("IDTipoClasificacion")) : 0;
                    Item.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
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

    }
}