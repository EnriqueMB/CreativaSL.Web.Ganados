using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentoXPagar_Datos
    {
        public List<DocumentoPorPagarModels> ObtenerListaDocumentosPagar(DocumentoPorPagarModels datos)
        {
            try
            {
                List<DocumentoPorPagarModels> Lista = new List<DocumentoPorPagarModels>();
                DocumentoPorPagarModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Documento_get_DocumentoXPagar");
                while (dr.Read())
                {
                    Item = new DocumentoPorPagarModels();
                    Item.IDTipoDocumento = !dr.IsDBNull(dr.GetOrdinal("IDTipo")) ? dr.GetInt32(dr.GetOrdinal("IDTipo")) : 0;
                    Item.IDDocumentoPagar = !dr.IsDBNull(dr.GetOrdinal("IDDocuemntoPagar")) ? dr.GetString(dr.GetOrdinal("IDDocuemntoPagar")) : string.Empty;
                    Item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    Item.Fecha = !dr.IsDBNull(dr.GetOrdinal("Fecha")) ? dr.GetDateTime(dr.GetOrdinal("Fecha")) : DateTime.Today;
                    Item.IDEstatus = !dr.IsDBNull(dr.GetOrdinal("IDEstatus")) ? dr.GetInt16(dr.GetOrdinal("IDEstatus")) : 1;
                    Item.EsSistema = !dr.IsDBNull(dr.GetOrdinal("EsSistema")) ? dr.GetBoolean(dr.GetOrdinal("EsSistema")) : false;
                    Item.Total = !dr.IsDBNull(dr.GetOrdinal("Total")) ? dr.GetDecimal(dr.GetOrdinal("Total")) : 0;
                    Item.Pagos = !dr.IsDBNull(dr.GetOrdinal("Pagos")) ? dr.GetDecimal(dr.GetOrdinal("Pagos")) : 0;
                    Item.Pendientes = !dr.IsDBNull(dr.GetOrdinal("Pendiente")) ? dr.GetDecimal(dr.GetOrdinal("Pendiente")) : 0;
                    Item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    Item.EstatusNombre = !dr.IsDBNull(dr.GetOrdinal("EstatusDocumento")) ? dr.GetString(dr.GetOrdinal("EstatusDocumento")) : string.Empty;
                    Lista.Add(Item);
                }
                return Lista;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}