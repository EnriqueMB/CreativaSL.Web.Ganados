using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace CreativaSL.Web.Ganados.Models
{
    public class _Clasificacion_Datos
    {
        public List<Clasificacion> ObtenerCatClasificacion()
        {
            try
            {
                List<Clasificacion> Lista = new List<Clasificacion>();
                SqlDataReader Dr = SqlHelper.ExecuteReader(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_CatClasificacion");
                Clasificacion Item;
                while (Dr.Read())
                {
                    Item = new Clasificacion
                    {
                        IdTipoClasificacion = !Dr.IsDBNull(Dr.GetOrdinal("IdTipoClasificacion")) ? Dr.GetInt16(Dr.GetOrdinal("IdTipoClasificacion")) : 0,
                        Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty,
                        SoloLectura = !Dr.IsDBNull(Dr.GetOrdinal("EsSoloLectura")) ? Dr.GetBoolean(Dr.GetOrdinal("EsSoloLectura")) : false
                    };
                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GuardarClasificacion(bool NuevoRegistro, Clasificacion Model, string IdUsuario)
        {
            try
            {
                object[] Parametros = { NuevoRegistro, Model.IdTipoClasificacion, Model.Descripcion, IdUsuario };
                object Result = SqlHelper.ExecuteScalar(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_ac_ClasificacionGasto", Parametros);
                if(Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    return Resultado;
                }
                return -1;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int EliminarClasificacion(int IdClasificacion, string IdUsuario)
        {
            try
            {
                object[] Parametros = { IdClasificacion, IdUsuario };
                object Result = SqlHelper.ExecuteScalar(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_del_CategoriaGasto", Parametros);
                if (Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    return Resultado;
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Clasificacion ObtenerDatos(int IdClasificacion)
        {
            try
            {
                SqlDataReader Dr = SqlHelper.ExecuteReader(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_get_ClasificacionXId", IdClasificacion);
                while(Dr.Read())
                {
                    Clasificacion clasificacion = new Clasificacion();

                    clasificacion.IdTipoClasificacion = !Dr.IsDBNull(Dr.GetOrdinal("IdClasificacion")) ? Dr.GetInt16(Dr.GetOrdinal("IdClasificacion")) : 0;
                    clasificacion.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;

                    return clasificacion;
                }
                Dr.Close();
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}