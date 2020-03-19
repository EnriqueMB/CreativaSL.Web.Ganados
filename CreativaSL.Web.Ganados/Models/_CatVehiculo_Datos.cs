using CreativaSL.Web.Ganados.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CreativaSL.Web.Ganados.Models.System;

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
                    Item.nombreVehiculo = !dr.IsDBNull(dr.GetOrdinal("nombreVehiculo")) ? dr.GetString(dr.GetOrdinal("nombreVehiculo")) : string.Empty;
                    Lista.Add(Item);
                }
                dr.Close();
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
                    datos.Opcion,
                    datos.IDVehiculo ?? string.Empty,
                    datos.IDSucursal ?? string.Empty,
                    datos.IDTipoVehiculo,
                    datos.IDMarca,
                    datos.EsPropio,
                    datos.Gps,
                    datos.Capacidad ?? string.Empty,
                    datos.Modelo ?? string.Empty,
                    datos.Color ?? string.Empty,
                    datos.Placas ?? string.Empty,
                    datos.NoSerie ?? string.Empty,
                    datos.Estatus,
                    datos.numeroUnidad ?? string.Empty,
                    datos.img64 ?? string.Empty,
                    datos.BandImg,                    
                    datos.tarjetaCirculacion ?? string.Empty,
                    datos.fechaIngreso != null ? datos.fechaIngreso : DateTime.Today,
                    datos.Usuario ?? string.Empty,
                    datos.IDEmpresa ?? string.Empty,
                    datos.PlacaJaula ?? string.Empty,
                    datos.EsJaula,
                    datos.ColorJaula
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
                    var imagen = Resultado.ToString();

                    if (!string.Equals(imagen, "0"))
                    {
                        datos.Completado = true;
                        datos.img64 = imagen;
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
                    datos.Gps = !dr.IsDBNull(dr.GetOrdinal("gps")) ? dr.GetString(dr.GetOrdinal("gps")) : string.Empty;
                    datos.Capacidad = !dr.IsDBNull(dr.GetOrdinal("capacidad")) ? dr.GetString(dr.GetOrdinal("capacidad")) : string.Empty;
                    datos.Modelo = !dr.IsDBNull(dr.GetOrdinal("modelo")) ? dr.GetString(dr.GetOrdinal("modelo")) : string.Empty;
                    datos.Color = !dr.IsDBNull(dr.GetOrdinal("color")) ? dr.GetString(dr.GetOrdinal("color")) : string.Empty;
                    datos.Placas = !dr.IsDBNull(dr.GetOrdinal("placas")) ? dr.GetString(dr.GetOrdinal("placas")) : string.Empty;
                    datos.numeroUnidad = !dr.IsDBNull(dr.GetOrdinal("numeroUnidad")) ? dr.GetString(dr.GetOrdinal("numeroUnidad")) : string.Empty;
                    datos.NoSerie = !dr.IsDBNull(dr.GetOrdinal("noSerie")) ? dr.GetString(dr.GetOrdinal("noSerie")) : string.Empty;
                    datos.Estatus = !dr.IsDBNull(dr.GetOrdinal("estatus")) ? dr.GetBoolean(dr.GetOrdinal("estatus")) : false;
                    datos.img64 = !dr.IsDBNull(dr.GetOrdinal("imagen")) ? dr.GetString(dr.GetOrdinal("imagen")) : string.Empty;                    
                    datos.tarjetaCirculacion = !dr.IsDBNull(dr.GetOrdinal("tarjetaCirculacion")) ? dr.GetString(dr.GetOrdinal("tarjetaCirculacion")) : string.Empty;
                    datos.fechaIngreso = !dr.IsDBNull(dr.GetOrdinal("fechaIngreso")) ? dr.GetDateTime(dr.GetOrdinal("fechaIngreso")) : DateTime.Now;
                    datos.IDEmpresa = !dr.IsDBNull(dr.GetOrdinal("id_empresa")) ? dr.GetString(dr.GetOrdinal("id_empresa")) : string.Empty;
                    datos.PlacaJaula = !dr.IsDBNull(dr.GetOrdinal("placaJaula")) ? dr.GetString(dr.GetOrdinal("placaJaula")) : string.Empty;
                    datos.ColorJaula = !dr.IsDBNull(dr.GetOrdinal("colorJaula")) ? dr.GetString(dr.GetOrdinal("colorJaula")) : string.Empty;
                    datos.EsJaula = !dr.IsDBNull(dr.GetOrdinal("esJaula")) ? dr.GetBoolean(dr.GetOrdinal("esJaula")) : false;

                    datos.img64 = Auxiliar.ValidImageFormServer(datos.img64, ProjectSettings.BaseDirCatVehiculo);
                }
                dr.Close();
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
                dr.Close();
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
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CatEmpresaModels> obtenerListaEmpresas(CatVehiculoModels Datos)
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
        public List<CatSucursalesModels> ObtenerSucursalesXIDEmpresa(CatVehiculoModels Datos)
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
                    Datos.listaSucursal.Add(item);
                }
                dr.Close();
                return Datos.listaSucursal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Archivos
        public string VEHICULO_index_Archivo(string conexion, string id_vehiculo)
        {
            try
            {
                object[] parametros = { id_vehiculo };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_VEHICULO_index_Archivo", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);

                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RespuestaAjax VEHICULO_ac_Archivo(ArchivoVehiculoModels item, string conexion, string usuario, int opcion)
        {
            try
            {
                object[] parametros =
                {
                    item.Id,
                    item.Id_vehiculo,
                    item.Descripcion,
                    item.UrlArchivo,
                    item.NombreArchivo,
                    usuario,
                    opcion
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_VEHICULO_ac_Archivo", parametros);
                RespuestaAjax respuesta = new RespuestaAjax();
                respuesta.Success = false;
                respuesta.Mensaje = "";
                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    respuesta.Href = !dr.IsDBNull(dr.GetOrdinal("nombreVehiculo")) ? dr.GetString(dr.GetOrdinal("nombreVehiculo")) : string.Empty;
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RespuestaAjax VEHICULO_del_Archivo(string conexion, int id)
        {
            try
            {
                object[] parametros = { id };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_VEHICULO_del_Archivo", parametros);
                RespuestaAjax respuesta = new RespuestaAjax();
                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}