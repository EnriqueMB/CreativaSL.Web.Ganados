using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatSucursal_Datos
    {
        public List<CatLugarModels> GetLugares(CatSucursalesModels Sucursal)
        {
            try
            {
                SqlDataReader dr = null;
                CatLugarModels lugar;
                List<CatLugarModels> listaLugares = new List<CatLugarModels>();

                dr = SqlHelper.ExecuteReader(Sucursal.Conexion, "spCSLDB_Combo_get_AllCatLugar");
                while (dr.Read())
                {
                    lugar = new CatLugarModels
                    {
                        id_lugar = !dr.IsDBNull(dr.GetOrdinal("IDLugar")) ? dr.GetString(dr.GetOrdinal("IDLugar")) : string.Empty,
                        descripcion = !dr.IsDBNull(dr.GetOrdinal("NombreLugar")) ? dr.GetString(dr.GetOrdinal("NombreLugar")) : string.Empty,
                        latitud = float.Parse(dr["GpsLatitud"].ToString()),
                        longitud = float.Parse(dr["GpsLongitud"].ToString())
                    };
                    listaLugares.Add(lugar);
                }
                dr.Close();
                return listaLugares;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<CatSucursalesModels> GetSucursalesXIDEmpresa(CatSucursalesModels Sucursal)
        {
            try
            {
                object[] parametros =
                {
                    Sucursal.IDEmpresa
                };
                SqlDataReader dr = null;
                CatSucursalesModels itemSucursal;

                dr = SqlHelper.ExecuteReader(Sucursal.Conexion, "spCSLDB_CATSUCURSAL_get_SucursalesXIDEmpresa", parametros);
                while (dr.Read())
                {
                    itemSucursal = new CatSucursalesModels
                    {
                        IDSucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty,
                        NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("nombreSuc")) ? dr.GetString(dr.GetOrdinal("nombreSuc")) : string.Empty,
                        Direccion = !dr.IsDBNull(dr.GetOrdinal("direccion")) ? dr.GetString(dr.GetOrdinal("direccion")) : string.Empty,
                        MermaPredeterminada = !dr.IsDBNull(dr.GetOrdinal("mermaPredeterminada")) ? dr.GetDecimal(dr.GetOrdinal("mermaPredeterminada")) : 0
                    };
                    Sucursal.ListaSucursales.Add(itemSucursal);
                }
                dr.Close();
                return Sucursal.ListaSucursales;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CatSucursalesModels AC_Sucursal(CatSucursalesModels Sucursal)
        {
            try
            {
                object[] parametros =
                {
                    Sucursal.IDSucursal,
                    Sucursal.IDEmpresa,
                    Sucursal.NombreSucursal,
                    Sucursal.Direccion,
                    Sucursal.MermaPredeterminada,
                    Sucursal.Usuario,
                    Sucursal.IDLugar
                };
                SqlDataReader dr = null;

                dr = SqlHelper.ExecuteReader(Sucursal.Conexion, "spCSLDB_CATSUCURSAL_ac_Sucursal", parametros);
                while (dr.Read())
                {
                    Sucursal.Completado = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                    Sucursal.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                dr.Close();
                return Sucursal;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CatSucursalesModels GetSucursalXIDSucursal(CatSucursalesModels Sucursal)
        {
            try
            {
                object[] parametros =
                {
                    Sucursal.IDSucursal
                };
                SqlDataReader dr = null;

                dr = SqlHelper.ExecuteReader(Sucursal.Conexion, "spCSLDB_CATSUCURSAL_get_SucursalXIDSucursal", parametros);
                while (dr.Read())
                {
                    Sucursal.IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("id_empresa")) ? dr.GetString(dr.GetOrdinal("id_empresa")) : string.Empty;
                    Sucursal.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("nombreSuc")) ? dr.GetString(dr.GetOrdinal("nombreSuc")) : string.Empty;
                    Sucursal.Direccion = !dr.IsDBNull(dr.GetOrdinal("direccion")) ? dr.GetString(dr.GetOrdinal("direccion")) : string.Empty;
                    Sucursal.MermaPredeterminada = !dr.IsDBNull(dr.GetOrdinal("mermaPredeterminada")) ? dr.GetDecimal(dr.GetOrdinal("mermaPredeterminada")) : 0;
                    Sucursal.IDLugar = !dr.IsDBNull(dr.GetOrdinal("id_lugar")) ? dr.GetString(dr.GetOrdinal("id_lugar")) : string.Empty;

                }
                dr.Close();
                return Sucursal;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CatSucursalesModels Del_Sucursal(CatSucursalesModels Sucursal)
        {
            try
            {
                object[] parametros =
                {
                    Sucursal.IDSucursal,
                    Sucursal.Usuario
                };
                SqlDataReader dr = null;

                dr = SqlHelper.ExecuteReader(Sucursal.Conexion, "spCSLDB_CATSUCURSAL_del_Sucursal", parametros);
                while (dr.Read())
                {
                    Sucursal.Completado = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : true;
                    Sucursal.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                dr.Close();
                return Sucursal;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
