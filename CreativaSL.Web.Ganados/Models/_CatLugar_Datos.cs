using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatLugar_Datos
    {
        public CatLugarModels AbcCatLugar(CatLugarModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.opcion,
                    datos.id_lugar ?? String.Empty,
                    datos.descripcion ?? string.Empty,
                    datos.latitud,
                    datos.longitud,
                    datos.ejido ?? string.Empty,
                    datos.id_sucursal ?? string.Empty,
                    datos.id_pais ?? string.Empty,
                    datos.id_estadoCodigo?? string.Empty,
                    datos.id_municipio,
                    datos.bascula,
                    datos.nombrePropietario ?? string.Empty,
                    datos.apellidoPaterno ?? string.Empty,
                    datos.apellidoMaterno ?? string.Empty,
                    datos.observaciones ?? string.Empty,
                    datos.user ?? string.Empty,
                    datos.Direccion ?? string.Empty
                    };
                object aux = SqlHelper.ExecuteScalar(datos.conexion, "spCSLDB_ac_CatLugar", parametros);
                datos.id_lugar = aux.ToString();
           
                if (!string.IsNullOrEmpty(datos.id_lugar))
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
        public List<CatLugarModels> obtenerCatLugar(CatLugarModels Datos)
        {
            try
            {
                List<CatLugarModels> lista = new List<CatLugarModels>();
                CatLugarModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "EM_spCSLDB_get_Lugar");
                while (dr.Read())
                {
                    item = new CatLugarModels();
                    item.id_lugar = dr["IDLugar"].ToString();
                    item.descripcion = dr["descripcion"].ToString();
                    item.municipio = dr["municipio"].ToString();
                    item.id_estadoCodigo = dr["nombreEstado"].ToString();
                    item.nombrePropietario = dr["nombrePropietario"].ToString();
                    //item.bascula = Convert.ToBoolean(dr["bascula"].ToString());
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public CatLugarModels ObtenerDetalleCatLugar(CatLugarModels datos)
        {
            try
            {
                object[] parametros = { datos.id_lugar };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.conexion, "spCSLDB_get_CatLugarXID", parametros);
                while (dr.Read())
                {
                    datos.id_lugar = dr["id_lugar"].ToString();
                    datos.descripcion = dr["descripcion"].ToString();
                    datos.ejido = dr["ejido"].ToString();
                    datos.lat = dr["gpsLatitud"].ToString();
                    datos.lng = dr["gpsLongitud"].ToString();
                    datos.id_municipio = Convert.ToInt32(dr["id_municipio"].ToString());
                    datos.id_estado = Convert.ToInt32(dr["id_estado"].ToString());
                    datos.id_pais = dr["id_pais"].ToString();
                    datos.id_estadoCodigo = dr["c_Estado"].ToString();
                    datos.nombrePropietario = dr["nombrePropietario"].ToString();
                    datos.apellidoMaterno = dr["apellidoMaterno"].ToString();
                    datos.apellidoPaterno = dr["apellidoPaterno"].ToString();
                    datos.observaciones = dr["observaciones"].ToString();
                    datos.nombreSucursal = dr["nombreSucursal"].ToString();
                    datos.bascula = Convert.ToBoolean(dr["bascula"].ToString());
                    datos.Direccion = dr["direccion"].ToString();
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatLugarModels EliminarLugar(CatLugarModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.id_lugar , datos.user
                };
                object aux = SqlHelper.ExecuteScalar(datos.conexion, "spCSLDB_Catalogo_del_CatLugar", parametros);
                datos.id_lugar = aux.ToString();
                if (!string.IsNullOrEmpty(datos.id_lugar))
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
        public List<CatSucursalesModels> obtenerListaSucursales(CatLugarModels Datos)
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
        public List<CatPaisModels> obtenerListaPaises(CatLugarModels Datos)
        {
            try
            {
                List<CatPaisModels> lista = new List<CatPaisModels>();
                CatPaisModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_get_ComboPaises");
                while (dr.Read())
                {
                    item = new CatPaisModels();
                    item.id_pais = dr["clave"].ToString();
                    item.descripcion = dr["descripcion"].ToString();
                    
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CatEstadoModels> obtenerListaEstados(CatLugarModels Datos)
        {
            try
            {
                List<CatEstadoModels> lista = new List<CatEstadoModels>();
                CatEstadoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_get_ComboEstado",Datos.id_pais);
                while (dr.Read())
                {
                    item = new CatEstadoModels();
                    item.id_estado = Convert.ToInt32(dr["id_estado"].ToString());
                    item.codigoEstado = dr["clave"].ToString();
                    item.descripcion = dr["descripcion"].ToString();

                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CatMunicipioModels> obtenerListaMunicipios(CatLugarModels Datos)
        {
            try
            {
                List<CatMunicipioModels> lista = new List<CatMunicipioModels>();
                CatMunicipioModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_get_ComboMunicipio2", Datos.id_pais,Datos.id_estadoCodigo,0);
                while (dr.Read())
                {
                    item = new CatMunicipioModels();
                    item.id_municipio = Convert.ToInt32(dr["id_municipio"].ToString());
                    item.codigoMunicipio = dr["clave"].ToString();
                    item.descripcion = dr["descripcion"].ToString();

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