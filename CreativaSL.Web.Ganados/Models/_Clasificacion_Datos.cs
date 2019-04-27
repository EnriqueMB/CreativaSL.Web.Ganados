using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace CreativaSL.Web.Ganados.Models
{
    public class _Clasificacion_Datos
    {
        public List<Clasificacion> ObtenerCatClasificacionGeneral()
        {
            try
            {
                List<Clasificacion> Lista = new List<Clasificacion>();
                SqlDataReader Dr = SqlHelper.ExecuteReader(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_CatClasificacionGeneral");
                Clasificacion Item;
                while (Dr.Read())
                {
                    Item = new Clasificacion
                    {
                        IdTipoClasificacion = !Dr.IsDBNull(Dr.GetOrdinal("IdTipoClasificacion")) ? Dr.GetInt16(Dr.GetOrdinal("IdTipoClasificacion")) : 0,
                        Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty,
                        SoloLectura = !Dr.IsDBNull(Dr.GetOrdinal("EsSoloLectura")) ? Dr.GetBoolean(Dr.GetOrdinal("EsSoloLectura")) : false,
                        Orden = !Dr.IsDBNull(Dr.GetOrdinal("Orden")) ? Dr.GetInt16(Dr.GetOrdinal("Orden")) : 0,
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

        public List<Clasificacion> ObtenerCatClasificacion(int ParentId)
        {
            try
            {
                List<Clasificacion> Lista = new List<Clasificacion>();
                SqlDataReader Dr = SqlHelper.ExecuteReader(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_CatClasificacion", ParentId);
                Clasificacion Item;
                while (Dr.Read())
                {
                    Item = new Clasificacion
                    {
                        IdTipoClasificacion = !Dr.IsDBNull(Dr.GetOrdinal("IdTipoClasificacion")) ? Dr.GetInt16(Dr.GetOrdinal("IdTipoClasificacion")) : 0,
                        Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty,
                        SoloLectura = !Dr.IsDBNull(Dr.GetOrdinal("EsSoloLectura")) ? Dr.GetBoolean(Dr.GetOrdinal("EsSoloLectura")) : false,
                        Orden = !Dr.IsDBNull(Dr.GetOrdinal("Orden")) ? Dr.GetInt16(Dr.GetOrdinal("Orden")) : 0,
                        ParentId = !Dr.IsDBNull(Dr.GetOrdinal("ParentId")) ? Dr.GetInt16(Dr.GetOrdinal("ParentId")) : 0,
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

        public int GuardarClasificacionGeneral(bool NuevoRegistro, Clasificacion Model, string IdUsuario)
        {
            try
            {
                object[] Parametros = { NuevoRegistro, Model.IdTipoClasificacion, Model.Descripcion, Model.Orden, IdUsuario };
                object Result = SqlHelper.ExecuteScalar(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_ac_ClasificacionGastoGeneral", Parametros);
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

        public int GuardarClasificacion(bool NuevoRegistro, Clasificacion Model, string IdUsuario)
        {
            try
            {
                object[] Parametros = { NuevoRegistro, Model.IdTipoClasificacion, Model.Descripcion, Model.Orden, Model.ParentId, IdUsuario };
                object Result = SqlHelper.ExecuteScalar(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_ac_ClasificacionGasto", Parametros);
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

        public int EliminarClasificacionGeneral(int IdClasificacion, string IdUsuario)
        {
            try
            {
                object[] Parametros = { IdClasificacion, IdUsuario };
                object Result = SqlHelper.ExecuteScalar(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_del_CategoriaGastoGeneral", Parametros);
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
                    clasificacion.Orden = !Dr.IsDBNull(Dr.GetOrdinal("Orden")) ? Dr.GetInt16(Dr.GetOrdinal("Orden")) : 0;
                    clasificacion.ParentId = !Dr.IsDBNull(Dr.GetOrdinal("ParentId")) ? Dr.GetInt16(Dr.GetOrdinal("ParentId")) : 0;
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

        public Clasificacion ObtenerDatosGeneral(int IdClasificacion)
        {
            try
            {
                SqlDataReader Dr = SqlHelper.ExecuteReader(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_get_ClasificacionGeneralXId", IdClasificacion);
                while (Dr.Read())
                {
                    Clasificacion clasificacion = new Clasificacion();
                    clasificacion.IdTipoClasificacion = !Dr.IsDBNull(Dr.GetOrdinal("IdClasificacion")) ? Dr.GetInt16(Dr.GetOrdinal("IdClasificacion")) : 0;
                    clasificacion.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
                    clasificacion.Orden = !Dr.IsDBNull(Dr.GetOrdinal("Orden")) ? Dr.GetInt16(Dr.GetOrdinal("Orden")) : 0;
                    return clasificacion;
                }
                Dr.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}