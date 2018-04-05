using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatChofer_Datos
    {
        public CatChoferModels EliminarChofer(CatChoferModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDChofer, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatChofer", parametros);
                datos.IDChofer = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDChofer))
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
        public CatChoferModels AbcCatChofer(CatChoferModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Opcion ,
                    datos.IDChofer ?? string.Empty,
                    datos.Nombre ?? string.Empty,
                    datos.ApPaterno ?? string.Empty,
                    datos.ApMaterno ?? string.Empty,
                    datos.Licencia ,datos.numLicencia ?? string.Empty,
                    datos.vigencia != null ? datos.FechaIngreso : DateTime.Today,
                    datos.Ife ?? string.Empty,
                    datos.idgruposanguineo ,
                    datos.IDGenero,
                    datos.NumSeguroSocial ?? string.Empty,
                    datos.AvisoAccidente ?? string.Empty,
                    datos.TelefonoAccidente ?? string.Empty,
                    datos.Telefono ?? string.Empty,
                    datos.Movil ?? string.Empty,
                    datos.FechaNacimiento != null ? datos.FechaNacimiento : DateTime.Today,
                    datos.FechaIngreso != null ? datos.FechaIngreso : DateTime.Today,
                    datos.Usuario ?? string.Empty
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "EM_spCSLDB_abc_Chofer", parametros);
                datos.IDChofer = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDChofer))
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

        public List<CatChoferModels> ObtenerCatChofer(CatChoferModels Datos)
        {
            try
            {
                List<CatChoferModels> lista = new List<CatChoferModels>();
                CatChoferModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "EM_spCSLDB_get_Chofer");
                while (dr.Read())
                {
                    item = new CatChoferModels();
                    item.IDChofer = dr["IDChofer"].ToString();
                    item.Nombre = dr["NombreChofer"].ToString();
                    item.Licencia = Convert.ToBoolean(dr["Licencia"].ToString());
                    item.Estatus = Convert.ToBoolean(dr["Estatus"].ToString());
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public CatChoferModels ObtenerDetalleCatChofer(CatChoferModels datos)
        {
            try
            {
                object[] parametros = { datos.IDChofer };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "EM_spCSLDB_get_ChoferXID", parametros);
                while (dr.Read())
                {
                    datos.IDChofer = dr["IDChofer"].ToString();
                    datos.Nombre = dr["Nombre"].ToString();
                    datos.ApPaterno = dr["ApPaterno"].ToString();
                    datos.ApMaterno = dr["ApMaterno"].ToString();
                    datos.Licencia = dr.GetBoolean(dr.GetOrdinal("Licencia"));
                    datos.numLicencia = dr["numLicencia"].ToString();
                    datos.vigencia = dr.GetDateTime(dr.GetOrdinal("vigencia"));
                    datos.Ife= dr["ife"].ToString();
                    datos.idgruposanguineo = Convert.ToInt32(dr["id_grupoSanguineo"].ToString());
                    datos.IDGenero = Convert.ToInt32(dr["id_genero"].ToString());
                    datos.NumSeguroSocial= dr["numSeguroSocial"].ToString();
                    datos.AvisoAccidente= dr["avisoAccidente"].ToString();
                    datos.TelefonoAccidente = dr["telefonoAccidente"].ToString();
                    datos.Telefono = dr["telefono"].ToString();
                    datos.Movil = dr["movil"].ToString();
                    datos.FechaIngreso = dr.GetDateTime(dr.GetOrdinal("fechaIngreso"));
                    datos.FechaNacimiento = dr.GetDateTime(dr.GetOrdinal("fechaNacimiento"));
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatGeneroModels> ObteneComboCatGenero(CatChoferModels Datos)
        {
            try
            {
                List<CatGeneroModels> lista = new List<CatGeneroModels>();
                CatGeneroModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatGenero");
               // lista.Add(new CatGeneroModels { IDGenero = string.Empty, NombreSucursal = " - Seleccione -" });
                while (dr.Read())
                {
                    item = new CatGeneroModels();
                    item.IDGenero = !dr.IsDBNull(dr.GetOrdinal("IDGenero")) ? dr.GetInt32(dr.GetOrdinal("IDGenero")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatGrupoSanguineoModels> ObteneComboCatGrupoSanguineo(CatChoferModels Datos)
        {
            try
            {
                List<CatGrupoSanguineoModels> lista = new List<CatGrupoSanguineoModels>();
                CatGrupoSanguineoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatGrupoSanguineo");
                // lista.Add(new CatGeneroModels { IDGenero = string.Empty, NombreSucursal = " - Seleccione -" });
                while (dr.Read())
                {
                    item = new CatGrupoSanguineoModels();
                    item.IDGrupoSanguineo = !dr.IsDBNull(dr.GetOrdinal("id_grupoSanguineo")) ? dr.GetInt32(dr.GetOrdinal("id_grupoSanguineo")) : 0;
                    item.descripcion = !dr.IsDBNull(dr.GetOrdinal("TipoSanguineo")) ? dr.GetString(dr.GetOrdinal("TipoSanguineo")) : string.Empty;
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