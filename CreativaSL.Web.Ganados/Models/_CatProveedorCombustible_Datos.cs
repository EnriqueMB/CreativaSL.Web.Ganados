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
    }
}