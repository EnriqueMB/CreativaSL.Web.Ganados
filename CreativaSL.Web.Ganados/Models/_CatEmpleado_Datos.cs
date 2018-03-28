using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatEmpleado_Datos
    {
        public List<CatEmpleadoModels> ObtenerCatEmpleado(CatEmpleadoModels datos)
        {
            try
            {
                List<CatEmpleadoModels> Lista = new List<CatEmpleadoModels>();
                CatEmpleadoModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatEmpleado");
                while (dr.Read())
                {
                    Item = new CatEmpleadoModels();
                    Item.IDEmpleado = !dr.IsDBNull(dr.GetOrdinal("IDEmpleado")) ? dr.GetString(dr.GetOrdinal("IDEmpleado")) : string.Empty;
                    Item.CodigoUsuario = !dr.IsDBNull(dr.GetOrdinal("CodigoUsuario")) ? dr.GetString(dr.GetOrdinal("CodigoUsuario")) : string.Empty;
                    Item.NombreCompleto = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty;
                    Item.AltaNominal = !dr.IsDBNull(dr.GetOrdinal("AltaNomina")) ? dr.GetBoolean(dr.GetOrdinal("AltaNomina")) : false;
                    Item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    Item.NombrePuesto = !dr.IsDBNull(dr.GetOrdinal("NombrePuesto")) ? dr.GetString(dr.GetOrdinal("NombrePuesto")) : string.Empty;
                    Item.NombreCategoriaP = !dr.IsDBNull(dr.GetOrdinal("CategoriaPuesto")) ? dr.GetString(dr.GetOrdinal("CategoriaPuesto")) : string.Empty;
                    Item.GrupoSanguineo = !dr.IsDBNull(dr.GetOrdinal("GrupoSanguineo")) ? dr.GetString(dr.GetOrdinal("GrupoSanguineo")) : string.Empty;
                    Item.DirCalle = !dr.IsDBNull(dr.GetOrdinal("Direccion")) ? dr.GetString(dr.GetOrdinal("Direccion")) : string.Empty;
                    Lista.Add(Item);
                }
                return Lista;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatEmpleadoModels AbcCatEmpleado(CatEmpleadoModels datos)
        {
            try
            {
                object[] parametros = {
                    datos.Opcion,
                    datos.IDEmpleado ?? string.Empty,
                    datos.IDTipoUsuario,
                    datos.AbrirCaja,
                    datos.CodigoUsuario ?? string.Empty,
                    datos.Nombre ?? string.Empty,
                    datos.ApellidoPat ?? string.Empty,
                    datos.ApellidoMat ?? string.Empty,
                    datos.AltaNominal,
                    datos.IDPuesto,
                    datos.IDCategoriaPuesto ?? string.Empty,
                    datos.IDSucursalActual ?? string.Empty,
                    datos.DirCalle ?? string.Empty,
                    datos.DirColonia ?? string.Empty,
                    datos.DirNumero ?? string.Empty,
                    datos.Telefono ?? string.Empty,
                    datos.IDGrupoSanguineo,
                    datos.Usuario ?? string.Empty
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatEmpleado", parametros);
                datos.IDEmpleado = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDEmpleado))
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

        public CatEmpleadoModels ObtenerDetalleCatEmpleado(CatEmpleadoModels datos)
        {
            try
            {
                object[] parametros = { datos.IDEmpleado };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatEmpleadoXID", parametros);
                while (dr.Read())
                {
                    datos.IDEmpleado = !dr.IsDBNull(dr.GetOrdinal("IDEmpleado")) ? dr.GetString(dr.GetOrdinal("IDEmpleado")) : string.Empty;
                    datos.IDTipoUsuario = !dr.IsDBNull(dr.GetOrdinal("IDTipoUsuario")) ? dr.GetInt16(dr.GetOrdinal("IDTipoUsuario")) : 0;
                    datos.AbrirCaja = !dr.IsDBNull(dr.GetOrdinal("AbrirCaja")) ? dr.GetBoolean(dr.GetOrdinal("AbrirCaja")) : false;
                    datos.CodigoUsuario = !dr.IsDBNull(dr.GetOrdinal("CodigoUsuario")) ? dr.GetString(dr.GetOrdinal("CodigoUsuario")) : string.Empty;
                    datos.Nombre = !dr.IsDBNull(dr.GetOrdinal("Nombre")) ? dr.GetString(dr.GetOrdinal("Nombre")) : string.Empty;
                    datos.ApellidoPat = !dr.IsDBNull(dr.GetOrdinal("ApellidoPat")) ? dr.GetString(dr.GetOrdinal("ApellidoPat")) : string.Empty;
                    datos.ApellidoMat = !dr.IsDBNull(dr.GetOrdinal("ApellidoMat")) ? dr.GetString(dr.GetOrdinal("ApellidoMat")) : string.Empty;
                    datos.AltaNominal = !dr.IsDBNull(dr.GetOrdinal("AltaNomina")) ? dr.GetBoolean(dr.GetOrdinal("AltaNomina")) : false;
                    datos.IDPuesto = !dr.IsDBNull(dr.GetOrdinal("IDPuesto")) ? dr.GetInt32(dr.GetOrdinal("IDPuesto")) : 0;
                    datos.IDCategoriaPuesto = !dr.IsDBNull(dr.GetOrdinal("IDCategoriaPuesto")) ? dr.GetString(dr.GetOrdinal("IDCategoriaPuesto")) : string.Empty;
                    datos.IDSucursalActual = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    datos.DirCalle = !dr.IsDBNull(dr.GetOrdinal("DirCalle")) ? dr.GetString(dr.GetOrdinal("DirCalle")) : string.Empty;
                    datos.DirColonia = !dr.IsDBNull(dr.GetOrdinal("DirColonia")) ? dr.GetString(dr.GetOrdinal("DirColonia")) : string.Empty;
                    datos.DirNumero = !dr.IsDBNull(dr.GetOrdinal("DirNumero")) ? dr.GetString(dr.GetOrdinal("DirNumero")) : string.Empty;
                    datos.Telefono = !dr.IsDBNull(dr.GetOrdinal("Telefono")) ? dr.GetString(dr.GetOrdinal("Telefono")) : string.Empty;
                    datos.IDGrupoSanguineo = !dr.IsDBNull(dr.GetOrdinal("IDGrupoSanguineo")) ? dr.GetInt32(dr.GetOrdinal("IDGrupoSanguineo")) : 0;
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatSucursalesModels> ObteneComboCatSucursal(CatEmpleadoModels Datos)
        {
            try
            {
                List<CatSucursalesModels> lista = new List<CatSucursalesModels>();
                CatSucursalesModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatSucursal");
                lista.Add(new CatSucursalesModels { IDSucursal = string.Empty, NombreSucursal = " - Seleccione -" });
                while (dr.Read())
                {
                    item = new CatSucursalesModels();
                    item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatGrupoSanguineoModels> ObteneComboCatGrupoSanguineo(CatEmpleadoModels Datos)
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

        public List<CatPuestoModels> obtenerComboCatPuesto(CatEmpleadoModels Datos)
        {
            try
            {
                List<CatPuestoModels> lista = new List<CatPuestoModels>();
                CatPuestoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatPuestos");
                while (dr.Read())
                {
                    item = new CatPuestoModels();
                    item.IDPuesto = Convert.ToInt32(dr["IDPuesto"].ToString());
                    item.Descripcion = dr["Descripcion"].ToString();
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<CatCategoriaPuestoModels> ObteneComboCatCategoriaPuesto(CatEmpleadoModels Datos)
        {
            try
            {
                List<CatCategoriaPuestoModels> lista = new List<CatCategoriaPuestoModels>();
                CatCategoriaPuestoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatCategoriaPuesto", Datos.IDPuesto);
                // lista.Add(new CatGeneroModels { IDGenero = string.Empty, NombreSucursal = " - Seleccione -" });
                while (dr.Read())
                {
                    item = new CatCategoriaPuestoModels();
                    item.id_categoria = !dr.IsDBNull(dr.GetOrdinal("IDCategoria")) ? dr.GetString(dr.GetOrdinal("IDCategoria")) : string.Empty;
                    item.descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
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