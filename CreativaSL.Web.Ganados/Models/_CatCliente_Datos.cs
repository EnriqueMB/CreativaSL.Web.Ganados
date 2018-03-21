using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatCliente_Datos
    {
        public CatClienteModels ObtenerClientes(CatClienteModels datos)
        {
            try
            {
                List<CatClienteModels> Lista = new List<CatClienteModels>();
                CatClienteModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatCliente");
                while (dr.Read())
                {
                    Item = new CatClienteModels();
                    Item.IDCliente = !dr.IsDBNull(dr.GetOrdinal("IDCliente")) ? dr.GetString(dr.GetOrdinal("IDCliente")) : string.Empty;
                    Item.NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("RazonSocial")) ? dr.GetString(dr.GetOrdinal("RazonSocial")) : string.Empty;
                    Item.EsPersonaFisica = !dr.IsDBNull(dr.GetOrdinal("EsPersona")) ? dr.GetBoolean(dr.GetOrdinal("EsPersona")) : false;
                    Item.RFC = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty;
                    Item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    Item.NombreRegimenFiscal = !dr.IsDBNull(dr.GetOrdinal("NombreRegimenFiscal")) ? dr.GetString(dr.GetOrdinal("NombreRegimenFiscal")) : string.Empty;
                    Item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    Lista.Add(Item);
                }
                datos.ListaClientes = Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatClienteModels AbcCatClientes(CatClienteModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Opcion, datos.IDCliente, datos.IDSucursal, datos.RFC, datos.NombreRazonSocial, datos.EsPersonaFisica, datos.IDRegimenFiscal, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatCliente", parametros);
                datos.IDCliente = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDCliente))
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

        public CatClienteModels EliminarCliente(CatClienteModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDCliente, datos.IDSucursal, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatCliente", parametros);
                datos.IDCliente = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDCliente))
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

        public List<CatSucursalesModels> ObteneComboCatSucursal(CatClienteModels Datos)
        {
            try
            {
                List<CatSucursalesModels> lista = new List<CatSucursalesModels>();
                CatSucursalesModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatSucursal");
                lista.Add(new CatSucursalesModels { IDSucursal = string.Empty, NombreSucursal=" - Seleccione -" });
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

        public List<CFDI_RegimenFiscalModels> ObtenerComboRegimenFiscal(CatClienteModels Datos)
        {
            try
            {
                List<CFDI_RegimenFiscalModels> lista = new List<CFDI_RegimenFiscalModels>();
                CFDI_RegimenFiscalModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_cfdiRegimenFiscal");
                while (dr.Read())
                {
                    item = new CFDI_RegimenFiscalModels();
                    item.Clave = !dr.IsDBNull(dr.GetOrdinal("Clave")) ? dr.GetString(dr.GetOrdinal("Clave")) : string.Empty;
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

        public CatClienteModels ObtenerDetalleCatCliente(CatClienteModels datos)
        {
            try
            {
                object[] parametros = { datos.IDCliente };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatClienteXID", parametros);
                while (dr.Read())
                {
                    datos.IDCliente = !dr.IsDBNull(dr.GetOrdinal("IDCliente")) ? dr.GetString(dr.GetOrdinal("IDCliente")) : string.Empty;
                    datos.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    datos.RFC = !dr.IsDBNull(dr.GetOrdinal("RFC")) ? dr.GetString(dr.GetOrdinal("RFC")) : string.Empty;
                    datos.NombreRazonSocial = !dr.IsDBNull(dr.GetOrdinal("RazonSocial")) ? dr.GetString(dr.GetOrdinal("RazonSocial")) : string.Empty;
                    datos.EsPersonaFisica = !dr.IsDBNull(dr.GetOrdinal("EsPersona")) ? dr.GetBoolean(dr.GetOrdinal("EsPersona")) : false;
                    datos.IDRegimenFiscal = !dr.IsDBNull(dr.GetOrdinal("IDRegimenFiscal")) ? dr.GetString(dr.GetOrdinal("IDRegimenFiscal")) : string.Empty;
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CFDI_RegimenFiscalModels> ObtenerDatosRegimen(CatClienteModels datos)
        {
            try
            {
                List<CFDI_RegimenFiscalModels> lista = new List<CFDI_RegimenFiscalModels>();
                CFDI_RegimenFiscalModels item;
                SqlDataReader dr = null;

                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_DatosRegimenFiscal", datos.IDRegimenFiscal);
                while (dr.Read())
                {
                    item = new CFDI_RegimenFiscalModels();
                    item.Fisica = !dr.IsDBNull(dr.GetOrdinal("Fisica")) ? dr.GetBoolean(dr.GetOrdinal("Fisica")) : false;
                    item.Moral = !dr.IsDBNull(dr.GetOrdinal("Moral")) ? dr.GetBoolean(dr.GetOrdinal("Moral")) : false;
                    item.FechaInicio = !dr.IsDBNull(dr.GetOrdinal("FechaInicioVigencia")) ? dr.GetString(dr.GetOrdinal("FechaInicioVigencia")) : string.Empty;
                    item.FechaFin = !dr.IsDBNull(dr.GetOrdinal("FechaFinVigencia")) ? dr.GetString(dr.GetOrdinal("FechaFinVigencia")) : string.Empty;
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