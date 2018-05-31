using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Venta2_Datos
    {
        public List<ListaGenerica> GetDocumentosDataTable(FleteModels Flete)
        {
            object[] parametros =
            {
                Flete.id_flete
            };

            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Flete.Conexion, "spCSLDB_Flete_get_DocumentosXIDFlete", parametros);
                ListaGenerica item;
                List<ListaGenerica> lista = new List<ListaGenerica>();
                while (dr.Read())
                {
                    item = new ListaGenerica();
                    item.Id = !dr.IsDBNull(dr.GetOrdinal("id_documento")) ? dr.GetString(dr.GetOrdinal("id_documento")) : string.Empty;
                    item.Nombre = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    item.Imagen = Auxiliar.ImageBase64ToImage(!dr.IsDBNull(dr.GetOrdinal("imagen")) ? dr.GetString(dr.GetOrdinal("imagen")) : string.Empty);
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