using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatConceptoDetalleDocumento_Datos
    {
        public List<CatConceptoDetalleDocumentosModels> ObtenerlistConceptosDocumentos(CatConceptoDetalleDocumentosModels Datos)
        {
            try
            {
                List<CatConceptoDetalleDocumentosModels> lista = new List<CatConceptoDetalleDocumentosModels>();
                CatConceptoDetalleDocumentosModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatConceptosDetalleDocumento");
                while (dr.Read())
                {
                    item = new CatConceptoDetalleDocumentosModels();
                    item.IDConceptosDocumento = dr["id_conceptoDocumento"].ToString();
                    item.IDTipoConciliacion = Convert.ToInt32(dr["id_tipoConciliacion"]);
                    item.Clave = dr["clave"].ToString();
                    item.Concepto = dr["concepto"].ToString();
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

        public List<CatTipoConciliacionModels> ObtenerComboTipoConciliacion(CatConceptoDetalleDocumentosModels Datos)
        {
            try
            {
                List<CatTipoConciliacionModels> lista = new List<CatTipoConciliacionModels>();
                CatTipoConciliacionModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_ComboTipoConciliacion");
                while (dr.Read())
                {
                    item = new CatTipoConciliacionModels();
                    item.IDTipoConciliacion = !dr.IsDBNull(dr.GetOrdinal("id_tipoConciliacion")) ? dr.GetInt32(dr.GetOrdinal("id_tipoConciliacion")) : -1;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CatConceptoDetalleDocumentosModels DACatConceptoDocumento(CatConceptoDetalleDocumentosModels Datos)
        {
            try
            {
                object[] parametros =
                {
                    Datos.Opcion, Datos.IDConceptosDocumento, Datos.IDTipoConciliacion, Datos.Clave, Datos.Descripcion, Datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Catalogo_ac_CatConceptoDetalleDocumento", parametros);
                if (Resultado != null)
                {
                    Datos.IDConceptosDocumento = Resultado.ToString();
                    if (!string.IsNullOrEmpty(Datos.IDConceptosDocumento))
                    {
                        Datos.Completado = true;
                    }
                    else
                    {
                        Datos.Completado = false;
                    }
                }
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatConceptoDetalleDocumentosModels ObternerCatConceptoDocumento(CatConceptoDetalleDocumentosModels Datos)
        {
            try
            {
                object[] parametros = { Datos.IDConceptosDocumento };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatConceptosDetalleDocumentoXID", parametros);
                while (dr.Read())
                {
                    Datos.IDConceptosDocumento = dr["id_conceptoDocumento"].ToString();
                    Datos.IDTipoConciliacion = Convert.ToInt32(dr["id_tipoConciliacion"].ToString());
                    Datos.Clave = dr["clave"].ToString();
                    Datos.Descripcion = dr["descripcion"].ToString();
                }
                return Datos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CatConceptoDetalleDocumentosModels EliminarCatConceptoDocumento(CatConceptoDetalleDocumentosModels Datos)
        {
            try
            {
                object[] parametros =
                {
                    Datos.IDConceptosDocumento, Datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Catalogo_del_CatConceptoDocumento", parametros);
                Datos.IDConceptosDocumento = Resultado.ToString();
                if (!string.IsNullOrEmpty(Datos.IDConceptosDocumento))
                {
                    Datos.Completado = true;
                }
                else
                {
                    Datos.Completado = false;
                }
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}