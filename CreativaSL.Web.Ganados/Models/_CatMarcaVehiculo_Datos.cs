using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatMarcaVehiculo_Datos
    {
        public CatMarcaVehiculoModels ObtenerListaMarcas(CatMarcaVehiculoModels datos)
        {
            try
            {
                List<CatMarcaVehiculoModels> Lista = new List<CatMarcaVehiculoModels>();
                CatMarcaVehiculoModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatMarcaVehiculo");
                while (dr.Read())
                {
                    Item = new CatMarcaVehiculoModels(); 
                    Item.IDMarca = !dr.IsDBNull(dr.GetOrdinal("IDMarca")) ? dr.GetInt16(dr.GetOrdinal("IDMarca")): 0;
                    Item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    Lista.Add(Item);
                }
                datos.ListaMarcas = Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatMarcaVehiculoModels AbcCatMarcaVehiculo(CatMarcaVehiculoModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Opcion, datos.IDMarca, datos.Descripcion, datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatMarcaVehiculo", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            datos.Completado = true;
                            datos.IDMarca = IDRegistro;
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

        public CatMarcaVehiculoModels EliminarMarca(CatMarcaVehiculoModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDMarca, datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatMarcaVehiculo", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            datos.Completado = true;
                            datos.IDMarca = IDRegistro;
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

        public CatMarcaVehiculoModels ObtenerDetalleCatMarcaVehiculo(CatMarcaVehiculoModels datos)
        {
            try
            {
                object[] parametros = { datos.IDMarca };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatMarcaVehiculoXID", parametros);
                while (dr.Read())
                {
                    datos.IDMarca = !dr.IsDBNull(dr.GetOrdinal("IDMarca")) ? dr.GetInt16(dr.GetOrdinal("IDMarca")) : 0;
                    datos.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
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