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
                   datos.Opcion, datos.IDProveedor, datos.IDTipoProveedor, datos.IDSucursal, datos.NombreRazonSocial, datos.RFC, datos.ImgINE, datos.ImgManifestacionFierro,
                    datos.BandINE, datos.BandMF,datos.direccion,datos.telefonoCelular,datos.telefonoCasa,datos.correo,datos.sexo, datos.Usuario
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
                    datos.direccion = dr["direccion"].ToString();
                    datos.correo = dr["correo"].ToString();
                    datos.telefonoCasa = dr["telefonoCasa"].ToString();
                    datos.telefonoCelular = dr["telefonoCelular"].ToString();
                    datos.sexo = Convert.ToInt32(dr["sexo"].ToString());
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