using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _DocumentoXCobrar_Datos
    {
        public List<CFDI_FormaPagoModels> GetListadoCFDIFormaPago(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarDetallePagos)
        {
            try
            {
                CFDI_FormaPagoModels item;
                List<CFDI_FormaPagoModels> lista = new List<CFDI_FormaPagoModels>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_Combo_get_CFDIFormaPago");
                while (dr.Read())
                {
                    item = new CFDI_FormaPagoModels
                    {
                        Clave = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetInt16(dr.GetOrdinal("ID")) : 0,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty,
                        Bancarizado = !dr.IsDBNull(dr.GetOrdinal("Bancarizado")) ? dr.GetInt32(dr.GetOrdinal("Bancarizado")) : 0,
                    };
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ListaGenerica> GetListadoAsignar(DocumentosPorCobrarDetallePagosModels DocumentoPorCobrarDetallePagos)
        {
            try
            {
                object[] parametros = 
                {
                    1,
                    DocumentoPorCobrarDetallePagos.Id_documentoPorCobrar
                };
                ListaGenerica item;
                List<ListaGenerica> lista = new List<ListaGenerica>();
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(DocumentoPorCobrarDetallePagos.Conexion, "spCSLDB_DocumentoPorCobrar_get_IDDocPorAsignar", parametros);
                while (dr.Read())
                {
                    item = new ListaGenerica
                    {
                        Id = !dr.IsDBNull(dr.GetOrdinal("id_documento")) ? dr.GetString(dr.GetOrdinal("id_documento")) : string.Empty,
                        Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty,
                    };
                    if(!string.IsNullOrEmpty(item.Id.Trim()))
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