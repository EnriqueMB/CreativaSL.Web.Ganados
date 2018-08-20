using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatDocumentos_Datos
    {
        public string DatatableIndex(CatTipoDocumentoModels Documento)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_CatTipoDocumento_get_Index");
                string datatable = Auxiliar.SqlReaderToJson(dr);
                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RespuestaAjax AC_Documentos(CatTipoDocumentoModels Documento)
        {
            try
            {
                object[] parametros =
                {
                    Documento.IDTipoDocumento,       Documento.Descripcion,
                    Documento.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_Catalogo_ac_CatTipoDocumento", parametros);
                RespuestaAjax RespuestaAjax = new RespuestaAjax();

                while (dr.Read())
                {
                    RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("Success")) ? dr.GetBoolean(dr.GetOrdinal("Success")) : false;
                    RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje")) ? dr.GetString(dr.GetOrdinal("Mensaje")) : string.Empty;
                }

                dr.Close();
                return RespuestaAjax;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatTipoDocumentoModels GET_DocumentoXID(CatTipoDocumentoModels Documento)
        {
            try
            {
                object[] parametros =
                {
                    Documento.IDTipoDocumento
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_CatDocumento_get_DocumentoXID", parametros);
                Documento.RespuestaAjax = new RespuestaAjax();

                while (dr.Read())
                {
                    Documento.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    if (Documento.RespuestaAjax.Success)
                    {
                        Documento.IDTipoDocumento = !dr.IsDBNull(dr.GetOrdinal("id_tipoDocumento")) ? dr.GetInt16(dr.GetOrdinal("id_tipoDocumento")) : 0;
                        Documento.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    }
                    else
                    {
                        Documento.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    }
                }

                dr.Close();
                return Documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatTipoDocumentoModels DEL_DocumentoXID(CatTipoDocumentoModels Documento)
        {
            try
            {
                object[] parametros =
                {
                    Documento.IDTipoDocumento,   Documento.Usuario
                };

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Documento.Conexion, "spCSLDB_CatDocumento_del_DocumentoXID", parametros);
                Documento.RespuestaAjax = new RespuestaAjax();

                while (dr.Read())
                {
                    Documento.RespuestaAjax.Success = !dr.IsDBNull(dr.GetOrdinal("Success")) ? dr.GetBoolean(dr.GetOrdinal("Success")) : false;
                    Documento.RespuestaAjax.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje")) ? dr.GetString(dr.GetOrdinal("Mensaje")) : string.Empty;
                }

                dr.Close();
                return Documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}