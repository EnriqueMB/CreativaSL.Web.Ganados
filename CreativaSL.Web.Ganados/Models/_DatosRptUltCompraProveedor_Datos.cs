using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DatosRptUltCompraProveedor_Datos
    {
        public List<RptUltCompraProveedorModels> GetListaProximaCompras(RptUltCompraProveedorModels reporte)
        {

            try
            {
                List<RptUltCompraProveedorModels> Lista = new List<RptUltCompraProveedorModels>();
                RptUltCompraProveedorModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(reporte.Conexion, "spCSLDB_Reporte_get_UltimasComprasProveedor", reporte.fechaStart, reporte.fechaEnd);
                while (dr.Read())
                {
                    Item = new RptUltCompraProveedorModels();
                    Item.NombreProveedor = !dr.IsDBNull(dr.GetOrdinal("nombreRazonSocial")) ? dr.GetString(dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                    DateTime FechaHoraProgramada = !dr.IsDBNull(dr.GetOrdinal("SiguienteCompra")) ? dr.GetDateTime(dr.GetOrdinal("SiguienteCompra")) : DateTime.Now;
                    Item.start = FechaHoraProgramada.ToString("yyyy-MM-dd");
                    Item.GanadosPactadoMachos = !dr.IsDBNull(dr.GetOrdinal("ganadoCompradoMachos")) ? dr.GetInt32(dr.GetOrdinal("ganadoCompradoMachos")) : 0;
                    Item.GanadosPactadoHembras = !dr.IsDBNull(dr.GetOrdinal("ganadoCompradoHembras")) ? dr.GetInt32(dr.GetOrdinal("ganadoCompradoHembras")) : 0;
                    Item.FolioCompra = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetInt32(dr.GetOrdinal("folio")) : 0;
                    Lista.Add(Item);
                }
                dr.Close();
                return Lista;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}