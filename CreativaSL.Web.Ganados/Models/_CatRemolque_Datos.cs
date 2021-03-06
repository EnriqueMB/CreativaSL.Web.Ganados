﻿using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatRemolque_Datos
    {
        public CatRemolqueModels AcCatRemolque(CatRemolqueModels datos)
        {
            try
            {
                object[] parametros =
                {
                   datos.Opcion, datos.IDRemolque, datos.IDSucursal, datos.placa, datos.color, datos.capacidad, datos.Usuario, datos.IDEmpresa
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatRemolque", parametros);
                datos.IDRemolque = aux.ToString();

                if (!string.IsNullOrEmpty(datos.IDRemolque))
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
        public CatRemolqueModels ObtenerDetalleCatRemolque(CatRemolqueModels datos)
        {
            try
            {
                object[] parametros = { datos.IDRemolque };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatRemolqueXID", parametros);
                while (dr.Read())
                {
                    datos.IDRemolque = dr["id_remolque"].ToString();
                   
                    datos.IDSucursal = dr["id_sucursal"].ToString();
                    datos.placa = dr["placa"].ToString();
                    datos.color = dr["color"].ToString();
                    datos.capacidad = dr["capacidad"].ToString();
                    datos.IDEmpresa = dr["id_empresa"].ToString();
                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatRemolqueModels EliminarRemolque(CatRemolqueModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDRemolque, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatRemolque", parametros);
                datos.IDRemolque = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDRemolque))
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
        public List<CatRemolqueModels> ObtenerCatRemolque(CatRemolqueModels Datos)
        {
            try
            {
                List<CatRemolqueModels> lista = new List<CatRemolqueModels>();
                CatRemolqueModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatRemolque");
                while (dr.Read())
                {
                    item = new CatRemolqueModels();
                    item.IDRemolque = dr["id_remolque"].ToString();
                    item.placa = dr["placa"].ToString();
                    item.color = dr["color"].ToString();
                    item.nombreSucursal = dr["nombreSuc"].ToString();
                    item.capacidad = dr["capacidad"].ToString();
                    item.Estatus = Convert.ToBoolean(dr["estatus"].ToString());
                   
                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CatSucursalesModels> obtenerListaSucursales(CatRemolqueModels Datos)
        {
            try
            {
                List<CatSucursalesModels> lista = new List<CatSucursalesModels>();
                CatSucursalesModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatSucursal");
                while (dr.Read())
                {
                    item = new CatSucursalesModels();
                    item.IDSucursal = dr["IDSucursal"].ToString();
                    item.NombreSucursal = dr["NombreSucursal"].ToString();

                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CatEmpresaModels> ObtenerListaEmpresas(CatRemolqueModels Datos)
        {
            try
            {
                CatEmpresaModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatEmpresa");
                while (dr.Read())
                {
                    item = new CatEmpresaModels();
                    item.IDEmpresa = dr["IDEmpresa"].ToString();
                    item.RazonFiscal = dr["NombreEmpresa"].ToString();

                    Datos.ListaEmpresas.Add(item);
                }
                dr.Close();
                return Datos.ListaEmpresas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatSucursalesModels> ObtenerSucursalesXIDEmpresa(CatRemolqueModels Datos)
        {
            try
            {
                CatSucursalesModels item;
                SqlDataReader dr = null;
                object[] parametro =
                {
                    Datos.IDEmpresa
                };
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_SucursalesXIDEmpresa", parametro);
                while (dr.Read())
                {
                    item = new CatSucursalesModels();
                    item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    Datos.listaSucursales.Add(item);
                }
                dr.Close();
                return Datos.listaSucursales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}