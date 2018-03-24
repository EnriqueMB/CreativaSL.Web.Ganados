using CreativaSL.Web.Ganados.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatVehiculo_Datos
    {
        public CatVehiculoModels ObtenerListaVehiculos(CatVehiculoModels datos)
        {
            try
            {
                List<CatVehiculoModels> Lista = new List<CatVehiculoModels>();
                CatVehiculoModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatVehiculo");
                while (dr.Read())
                {
                    Item = new CatVehiculoModels();
                    Item.IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("id_vehiculo")) ? dr.GetString(dr.GetOrdinal("id_vehiculo")) : string.Empty;
                    Item.nombreTipoVehiculo = !dr.IsDBNull(dr.GetOrdinal("tipoVehiculo")) ? dr.GetString(dr.GetOrdinal("tipoVehiculo")) : string.Empty;
                    Item.nombreMarca = !dr.IsDBNull(dr.GetOrdinal("marca")) ? dr.GetString(dr.GetOrdinal("marca")) : string.Empty;
                    Item.nombreSucursal = !dr.IsDBNull(dr.GetOrdinal("nombreSuc")) ? dr.GetString(dr.GetOrdinal("nombreSuc")) : string.Empty;
                    Item.EsPropio = !dr.IsDBNull(dr.GetOrdinal("propio")) ? dr.GetBoolean(dr.GetOrdinal("propio")) : false;
                    Item.NoSerie = !dr.IsDBNull(dr.GetOrdinal("noSerie")) ? dr.GetString(dr.GetOrdinal("noSerie")) : string.Empty;
                    Item.Estatus = !dr.IsDBNull(dr.GetOrdinal("estatus")) ? dr.GetBoolean(dr.GetOrdinal("estatus")) : false;
                    Lista.Add(Item);
                }
                datos.listaVehiculos = Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatVehiculoModels AcCatVehiculo(CatVehiculoModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Opcion, datos.IDVehiculo, datos.IDSucursal,datos.IDTipoVehiculo,datos.IDMarca,datos.EsPropio,datos.Capacidad,datos.Modelo,datos.Color,datos.Placas,datos.Remolque,datos.NoSerie,datos.Estatus,
                    datos.colorRemolque,datos.placaRemolque,datos.tarjetaCirculacion,datos.fechaIngreso,datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatVehiculo", parametros);
                datos.IDVehiculo = Resultado.ToString();
                if (!string.IsNullOrEmpty(datos.IDVehiculo))
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
        public CatVehiculoModels EliminarVehiculo(CatVehiculoModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDVehiculo, datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatVehiculo", parametros);
                if (Resultado != null)
                {
                    if (!string.IsNullOrEmpty(datos.IDVehiculo))
                    {
                        datos.Completado = true;
                    }
                    else
                    {
                        datos.Completado = false;
                    }
                    return datos;
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatVehiculoModels ObtenerDetalleCatVehiculo(CatVehiculoModels datos)
        {
            try
            {
                object[] parametros = { datos.IDVehiculo };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatVehiculoXID", parametros);
                while (dr.Read())
                {
                    //  datos.IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("IDTipoVehiculo")) ? dr.GetInt16(dr.GetOrdinal("IDTipoVehiculo")) : 0;
                    datos.IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("id_vehiculo")) ? dr.GetString(dr.GetOrdinal("id_vehiculo")) : string.Empty;
                    datos.IDTipoVehiculo = !dr.IsDBNull(dr.GetOrdinal("id_tipoVehiculo")) ? dr.GetInt16(dr.GetOrdinal("id_tipoVehiculo")) : 0;
                    datos.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("id_sucursal")) ? dr.GetString(dr.GetOrdinal("id_sucursal")) : string.Empty;
                    datos.IDMarca = !dr.IsDBNull(dr.GetOrdinal("id_marca")) ? dr.GetInt16(dr.GetOrdinal("id_marca")) : 0;
                    datos.EsPropio = !dr.IsDBNull(dr.GetOrdinal("propio")) ? dr.GetBoolean(dr.GetOrdinal("propio")) : false;
                    datos.Capacidad = !dr.IsDBNull(dr.GetOrdinal("capacidad")) ? dr.GetString(dr.GetOrdinal("capacidad")) : string.Empty;
                    datos.Modelo = !dr.IsDBNull(dr.GetOrdinal("modelo")) ? dr.GetString(dr.GetOrdinal("modelo")) : string.Empty;
                    datos.Color = !dr.IsDBNull(dr.GetOrdinal("color")) ? dr.GetString(dr.GetOrdinal("color")) : string.Empty;
                    datos.Placas = !dr.IsDBNull(dr.GetOrdinal("placas")) ? dr.GetString(dr.GetOrdinal("placas")) : string.Empty;
                    datos.Remolque = !dr.IsDBNull(dr.GetOrdinal("remolque")) ? dr.GetString(dr.GetOrdinal("remolque")) : string.Empty;
                    datos.NoSerie = !dr.IsDBNull(dr.GetOrdinal("noSerie")) ? dr.GetString(dr.GetOrdinal("noSerie")) : string.Empty;
                    datos.Estatus = !dr.IsDBNull(dr.GetOrdinal("estatus")) ? dr.GetBoolean(dr.GetOrdinal("estatus")) : false;
                    datos.colorRemolque = !dr.IsDBNull(dr.GetOrdinal("colorRemolque")) ? dr.GetString(dr.GetOrdinal("colorRemolque")) : string.Empty;
                    datos.placaRemolque = !dr.IsDBNull(dr.GetOrdinal("placaRemolque")) ? dr.GetString(dr.GetOrdinal("placaRemolque")) : string.Empty;
                    datos.tarjetaCirculacion = !dr.IsDBNull(dr.GetOrdinal("tarjetaCirculacion")) ? dr.GetString(dr.GetOrdinal("tarjetaCirculacion")) : string.Empty;
                    datos.fechaIngreso = !dr.IsDBNull(dr.GetOrdinal("fechaIngreso")) ? dr.GetDateTime(dr.GetOrdinal("fechaIngreso")) : DateTime.Now;
                   

                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatTipoVehiculoModels> obtenerListaTipoVehiculo(CatVehiculoModels Datos)
        {
            try
            {
                List<CatTipoVehiculoModels> lista = new List<CatTipoVehiculoModels>();
                CatTipoVehiculoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_ComboTipoVehiculo");
                while (dr.Read())
                {
                    item = new CatTipoVehiculoModels();
                    item.IDTipoVehiculo = Convert.ToInt32(dr["id_tipoVehiculo"].ToString());
                    item.Descripcion = dr["descripcion"].ToString();

                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CatSucursalesModels> obtenerListaSucursales(CatVehiculoModels Datos)
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
        public List<CatMarcaVehiculoModels> obtenerListaMarcas(CatVehiculoModels Datos)
        {
            try
            {
                List<CatMarcaVehiculoModels> lista = new List<CatMarcaVehiculoModels>();
                CatMarcaVehiculoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_ComboMarcaVehiculo");
                while (dr.Read())
                {
                    item = new CatMarcaVehiculoModels();
                    item.IDMarca = Convert.ToInt32(dr["id_marca"].ToString());
                    item.Descripcion = dr["descripcion"].ToString();

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