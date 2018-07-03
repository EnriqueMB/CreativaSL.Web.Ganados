using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatTipoVehiculos_Datos
    {
        public CatTipoVehiculoModels ObtenerListaTipoVehiculos(CatTipoVehiculoModels datos)
        {
            try
            {
                List<CatTipoVehiculoModels> Lista = new List<CatTipoVehiculoModels>();
                CatTipoVehiculoModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatTipoVehiculo");
                while (dr.Read())
                {
                    Item = new CatTipoVehiculoModels();
                    Item.IDTipoVehiculo = !dr.IsDBNull(dr.GetOrdinal("IDTipoVehiculo")) ? dr.GetInt16(dr.GetOrdinal("IDTipoVehiculo")) : 0;
                    Item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    Lista.Add(Item);
                }
                datos.listaTipoVehiculos = Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatTipoVehiculoModels AcCatTipoVehiculo(CatTipoVehiculoModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Opcion, datos.IDTipoVehiculo, datos.Descripcion, datos.Usuario, datos.esJaula
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatTipoVehiculo", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            datos.Completado = true;
                            datos.IDTipoVehiculo = IDRegistro;
                        }
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatTipoVehiculoModels EliminarTipoVehiculo(CatTipoVehiculoModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDTipoVehiculo, datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatTipoVehiculo", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            datos.Completado = true;
                            datos.IDTipoVehiculo = IDRegistro;
                        }
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatTipoVehiculoModels ObtenerDetalleCatMarcaVehiculo(CatTipoVehiculoModels datos)
        {
            try
            {
                object[] parametros = { datos.IDTipoVehiculo };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatTipoVehiculoXID", parametros);
                while (dr.Read())
                {
                    datos.IDTipoVehiculo = !dr.IsDBNull(dr.GetOrdinal("IDTipoVehiculo")) ? dr.GetInt16(dr.GetOrdinal("IDTipoVehiculo")) : 0;
                    datos.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    datos.esJaula = !dr.IsDBNull(dr.GetOrdinal("EsJaula")) ? dr.GetBoolean(dr.GetOrdinal("EsJaula")) : false;
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