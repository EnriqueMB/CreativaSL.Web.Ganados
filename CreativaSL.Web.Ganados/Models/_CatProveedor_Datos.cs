using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatProveedor_Datos
    {
        public CatProveedorModels AcCatProveedor(CatProveedorModels datos)
        {
            try
            {
                object[] parametros =
                {
                   datos.Opcion,
                    datos.IDProveedor ?? string.Empty,
                    datos.IDTipoProveedor,
                    datos.IDSucursal ?? string.Empty,
                    datos.NombreRazonSocial ?? string.Empty,
                    datos.RFC ?? string.Empty,
                    datos.ImgINE ?? string.Empty,
                    datos.ImgManifestacionFierro ?? string.Empty,
                    datos.BandINE,
                    datos.BandMF,
                    datos.Direccion ?? string.Empty,
                    datos.telefonoCelular ?? string.Empty,
                    datos.telefonoCasa ?? string.Empty,
                    datos.correo ?? string.Empty,
                    datos.sexo,
                    datos.FechaIngreso != null ? datos.FechaIngreso : DateTime.Today,
                    datos.EsEmpresa,
                    datos.Tolerancia,
                    datos.Observaciones ?? string.Empty,
                    datos.Usuario ?? string.Empty
                    };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatProveedor", parametros);
                datos.IDProveedor = aux.ToString();

                if (!string.IsNullOrEmpty(datos.IDProveedor))
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
        public CatProveedorModels EliminarProveedor(CatProveedorModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDProveedor, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatProveedor", parametros);
                datos.IDProveedor = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDProveedor))
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
        public CatProveedorModels ObtenerDetalleCatProveedor(CatProveedorModels datos)
        {
            try
            {
                object[] parametros = { datos.IDProveedor };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorXID", parametros);
                while (dr.Read())
                {
                    datos.IDProveedor = dr["id_proveedor"].ToString();
                    datos.IDTipoProveedor =Convert.ToInt32(dr["id_tipoProveedor"].ToString());
                    datos.IDSucursal = dr["id_sucursal"].ToString();
                    datos.NombreRazonSocial = dr["nombreRazonSocial"].ToString();
                    datos.RFC = dr["rfc"].ToString();
                    datos.ImgINE = dr["imgINE"].ToString();
                    datos.ImgManifestacionFierro = dr["imgManifestacionFierro"].ToString();
                    datos.Direccion = dr["direccion"].ToString();
                    datos.correo = dr["correo"].ToString();
                    datos.telefonoCasa = dr["telefonoCasa"].ToString();
                    datos.telefonoCelular = dr["telefonoCelular"].ToString();
                    datos.sexo = Convert.ToInt32(dr["sexo"].ToString());
                    datos.FechaIngreso = !dr.IsDBNull(dr.GetOrdinal("FechaIngreso")) ? dr.GetDateTime(dr.GetOrdinal("FechaIngreso")) : DateTime.Now;
                    datos.EsEmpresa = !dr.IsDBNull(dr.GetOrdinal("esEmpresa")) ? dr.GetBoolean(dr.GetOrdinal("esEmpresa")) : false;
                    datos.Tolerancia = !dr.IsDBNull(dr.GetOrdinal("tolerancia")) ? dr.GetInt32(dr.GetOrdinal("tolerancia")) : 0;
                    datos.Observaciones = !dr.IsDBNull(dr.GetOrdinal("observaciones")) ? dr.GetString(dr.GetOrdinal("observaciones")) : string.Empty;
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        //LISTA DE PROVEEDORES MOSTRADA EN EL INDEX DE 'CatProveedor'
        public List<CatProveedorModels> ObtenerCatProveedores(CatProveedorModels Datos)
        {
            try
            {
                List<CatProveedorModels> lista = new List<CatProveedorModels>();
                CatProveedorModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatProveedores");
                while (dr.Read())
                {
                    item = new CatProveedorModels();
                    item.IDProveedor = dr["id_proveedor"].ToString();
                    item.NombreRazonSocial = dr["nombreRazonSocial"].ToString();
                    item.RFC = dr["rfc"].ToString();
                    item.nombreProveedor = dr["tipoProveedor"].ToString();
                    item.nombreSucursal = dr["sucursal"].ToString();
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CatSucursalesModels> obtenerListaSucursales(CatProveedorModels Datos)
        {
            try
            {
                List<CatSucursalesModels> lista = new List<CatSucursalesModels>();
                CatSucursalesModels item;
                SqlDataReader dr = null;
                lista.Add(new CatSucursalesModels { IDSucursal = string.Empty, NombreSucursal = " - Seleccione -" });
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_ComboSucursales");
                while (dr.Read())
                {
                    item = new CatSucursalesModels();
                    item.IDSucursal = dr["id_sucursal"].ToString();
                    item.NombreSucursal = dr["nombre"].ToString();

                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CatTipoProveedorModels> obtenerListaTipoProveedor(CatProveedorModels Datos)
        {
            try
            {
                List<CatTipoProveedorModels> lista = new List<CatTipoProveedorModels>();
                CatTipoProveedorModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_TipoProveedor");
                while (dr.Read())
                {
                    item = new CatTipoProveedorModels();
                    item.IDTipoProveedor = Convert.ToInt32(dr["id_tipoProveedor"].ToString());
                    item.Descripcion = dr["nombre"].ToString();

                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CatGeneroModels> ObteneComboCatGenero(CatProveedorModels Datos)
        {
            try
            {
                List<CatGeneroModels> lista = new List<CatGeneroModels>();
                CatGeneroModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatGenero");
                // lista.Add(new CatGeneroModels { IDGenero = string.Empty, NombreSucursal = " - Seleccione -" });
                while (dr.Read())
                {
                    item = new CatGeneroModels();
                    item.IDGenero = !dr.IsDBNull(dr.GetOrdinal("IDGenero")) ? dr.GetInt32(dr.GetOrdinal("IDGenero")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}