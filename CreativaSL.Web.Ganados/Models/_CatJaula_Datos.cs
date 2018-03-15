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
                   datos.opcion,datos.IDJaula,datos.IDSucursal,datos.Matricula,datos.Estatus,datos.user
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
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_get_ComboSucursales");
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
    }
}