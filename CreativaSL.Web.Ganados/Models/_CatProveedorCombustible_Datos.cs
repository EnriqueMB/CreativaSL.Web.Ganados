using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatProveedorCombustible_Datos
    {
        public List<CatProveedorCombustibleModels> ObtenerCatProveedores(CatProveedorCombustibleModels Datos)
        {
            try
            {
                List<CatProveedorCombustibleModels> lista = new List<CatProveedorCombustibleModels>();
                CatProveedorCombustibleModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorCombustible");
                while (dr.Read())
                {
                    item = new CatProveedorCombustibleModels();
                    item.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("IDProveedorCombustible")) ? dr.GetString(dr.GetOrdinal("IDProveedorCombustible")) : string.Empty;
                    item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    item.nombreSucursal = !dr.IsDBNull(dr.GetOrdinal("nombreSucursal")) ? dr.GetString(dr.GetOrdinal("nombreSucursal")) : string.Empty;
                    item.NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("RazonSocial")) ? dr.GetString(dr.GetOrdinal("RazonSocial")) : string.Empty;
                    item.RFC = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty;
                    item.Direccion = !dr.IsDBNull(dr.GetOrdinal("Direccion")) ? dr.GetString(dr.GetOrdinal("Direccion")) : string.Empty;
                    item.telefonoCelular = !dr.IsDBNull(dr.GetOrdinal("TelefonoCelular")) ? dr.GetString(dr.GetOrdinal("TelefonoCelular")) : string.Empty;
                    item.telefonoCasa = !dr.IsDBNull(dr.GetOrdinal("TelefonoCasa")) ? dr.GetString(dr.GetOrdinal("TelefonoCasa")) : string.Empty;
                    item.correo = !dr.IsDBNull(dr.GetOrdinal("CorreoElectronico")) ? dr.GetString(dr.GetOrdinal("CorreoElectronico")) : string.Empty;
                    item.fechaIngreso = !dr.IsDBNull(dr.GetOrdinal("FechaIngreso")) ? dr.GetDateTime(dr.GetOrdinal("FechaIngreso")) : DateTime.Now;
                    item.observaciones = !dr.IsDBNull(dr.GetOrdinal("Observaciones")) ? dr.GetString(dr.GetOrdinal("Observaciones")) : string.Empty;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public CatProveedorCombustibleModels acCatProveedorCombustible(CatProveedorCombustibleModels datos)
        {
            try
            {
                object[] parametros =
                {
                   datos.Opcion,
                   datos.IDProveedor ?? string.Empty,

                   datos.IDSucursal ?? string.Empty,

                   datos.NombreRazonSocial?? string.Empty,
                   datos.Direccion?? string.Empty,
                   datos.RFC ?? string.Empty,
                   datos.correo ?? string.Empty,
                   datos.telefonoCasa ?? string.Empty,
                   datos.telefonoCelular ?? string.Empty,
                   datos.observaciones ?? string.Empty,
                   
                    datos.fechaIngreso !=null? datos.fechaIngreso:DateTime.Now,
                   datos.Usuario ?? string.Empty

                    };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatProveedorCombustible", parametros);
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
        public CatProveedorCombustibleModels ObtenerDetalleCatProveedor(CatProveedorCombustibleModels datos)
        {
            try
            {
                object[] parametros = { datos.IDProveedor };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorCombustibleXID", parametros);
                while (dr.Read())
                {
                    datos.IDProveedor = dr["id_proveedorCombustible"].ToString();
                   
                    datos.IDSucursal = dr["id_sucursal"].ToString();
                    datos.NombreRazonSocial = dr["nombreRazonSocial"].ToString();
                    datos.RFC = dr["rfc"].ToString();
                   
                    datos.Direccion = dr["direccion"].ToString();
                    datos.correo = dr["correo"].ToString();
                    datos.telefonoCasa = dr["telefonoCasa"].ToString();
                    datos.telefonoCelular = dr["telefonoCelular"].ToString();
                    
                    datos.fechaIngreso = !dr.IsDBNull(dr.GetOrdinal("FechaIngreso")) ? dr.GetDateTime(dr.GetOrdinal("FechaIngreso")) : DateTime.Now;
                    
                    datos.observaciones = !dr.IsDBNull(dr.GetOrdinal("observaciones")) ? dr.GetString(dr.GetOrdinal("observaciones")) : string.Empty;
                    // datos.merma= !dr.IsDBNull(dr.GetOrdinal("merma")) ? dr.GetInt32(dr.GetOrdinal("merma")) : 0;
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}