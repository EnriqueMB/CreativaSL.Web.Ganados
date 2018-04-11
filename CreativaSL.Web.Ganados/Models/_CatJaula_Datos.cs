using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatJaula_Datos
    {
        public CatJaulaModels AbcCatJaula(CatJaulaModels datos)
        {
            try
            {
                object[] parametros =
                {
                   datos.opcion,datos.IDJaula,datos.IDSucursal,datos.Matricula,datos.Estatus,datos.user, datos.IDEmpresa
                };
                object aux = SqlHelper.ExecuteScalar(datos.conexion, "spCSLDB_Catalogo_ac_CatJaula", parametros);
                datos.IDJaula = aux.ToString();

                if (!string.IsNullOrEmpty(datos.IDJaula))
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
        public CatJaulaModels EliminarJaula(CatJaulaModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDJaula, datos.user
                };
                object aux = SqlHelper.ExecuteScalar(datos.conexion, "spCSLDB_Catalogo_del_CatJaula", parametros);
                datos.IDJaula = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDJaula))
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
        public CatJaulaModels ObtenerDetalleCatJaula(CatJaulaModels datos)
        {
            try
            {
                object[] parametros = { datos.IDJaula };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.conexion, "spCSLDB_Catalogo_get_CatJaulaXID", parametros);
                while (dr.Read())
                {
                    datos.IDJaula = dr["id_jaula"].ToString();
                    datos.IDSucursal = dr["id_sucursal"].ToString();
                    datos.Matricula = dr["matricula"].ToString();
                    datos.Estatus = Convert.ToBoolean(dr["estatus"].ToString());
                   datos.IDEmpresa = dr["id_empresa"].ToString();
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatJaulaModels> obtenerCatJaula(CatJaulaModels Datos)
        {
            try
            {
                List<CatJaulaModels> lista = new List<CatJaulaModels>();
                CatJaulaModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_Catalogo_get_CatJaula");
                while (dr.Read())
                {
                    item = new CatJaulaModels();
                    item.IDJaula = dr["id_jaula"].ToString();
                    item.nombreSucursal = dr["nombreSuc"].ToString();
                    item.Matricula = dr["matricula"].ToString();
                    item.Estatus = Convert.ToBoolean(dr["estatus"].ToString());
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CatSucursalesModels> obtenerListaSucursales(CatJaulaModels Datos)
        {
            try
            {
                List<CatSucursalesModels> lista = new List<CatSucursalesModels>();
                CatSucursalesModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_Combo_get_CatSucursal");
                while (dr.Read())
                {
                    item = new CatSucursalesModels();
                    item.IDSucursal = dr["IDSucursal"].ToString();
                    item.NombreSucursal = dr["NombreSucursal"].ToString();
                   
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatEmpresaModels> ObteneComboCatEmpresa(CatJaulaModels Datos)
        {
            try
            {
                CatEmpresaModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_Combo_get_CatEmpresa");
                while (dr.Read())
                {
                    item = new CatEmpresaModels();
                    item.IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("IDEmpresa")) ? dr.GetString(dr.GetOrdinal("IDEmpresa")) : string.Empty;
                    item.RazonFiscal = !dr.IsDBNull(dr.GetOrdinal("NombreEmpresa")) ? dr.GetString(dr.GetOrdinal("NombreEmpresa")) : string.Empty;
                    Datos.ListaEmpresas.Add(item);
                }
                return Datos.ListaEmpresas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatSucursalesModels> ObtenerSucursalesXIDEmpresa(CatJaulaModels Datos)
        {
            try
            {
                CatSucursalesModels item;
                SqlDataReader dr = null;
                object[] parametro =
                {
                    Datos.IDEmpresa
                };
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_Combo_get_SucursalesXIDEmpresa", parametro);
                while (dr.Read())
                {
                    item = new CatSucursalesModels();
                    item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    Datos.listaSucursales.Add(item);
                }
                return Datos.listaSucursales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}