using CreativaSL.Web.Ganados.ViewModels;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _ReporteTipoFlete_Datos
    {
        public List<ReporteTipoFleteModels> obtenerListaReporteTipoFlete(ReporteTipoFleteModels Datos)
        {
            try
            {
                List<ReporteTipoFleteModels> Lista = new List<ReporteTipoFleteModels>();
                object[] parametros = { Datos.id_tipoFlete, Datos.id_sucursal, Datos.FechaInicio, Datos.FechaFin, Datos.Usuario };
                ReporteTipoFleteModels item;
                SqlDataReader Dr = null;
                Dr = SqlHelper.ExecuteReader(Datos.Conexion, "[dbo].[spCLSDB_Reporte_Flete]", parametros);
                while (Dr.Read())
                {
                    item = new ReporteTipoFleteModels();
                    item.FechaHora = !Dr.IsDBNull(Dr.GetOrdinal("FechaHora")) ? Convert.ToDateTime(Dr.GetDateTime(Dr.GetOrdinal("FechaHora"))) : DateTime.Today;
                    item.TipoFlete = !Dr.IsDBNull(Dr.GetOrdinal("TipoFlete")) ? Convert.ToString(Dr.GetInt16(Dr.GetOrdinal("TipoFlete"))) : string.Empty;
                    item.NombreTipoFlete = !Dr.IsDBNull(Dr.GetOrdinal("NombreTipoFlete")) ? Dr.GetString(Dr.GetOrdinal("NombreTipoFlete")) : string.Empty;
                    item.Folio = !Dr.IsDBNull(Dr.GetOrdinal("FolioFlete")) ? Dr.GetInt64(Dr.GetOrdinal("FolioFlete")) : 0;
                    item.Proveedor = !Dr.IsDBNull(Dr.GetOrdinal("Proveedor")) ? Dr.GetString(Dr.GetOrdinal("Proveedor")) : "Sin dato";
                    item.LugarOrigen = !Dr.IsDBNull(Dr.GetOrdinal("OrigenFlete")) ? Dr.GetString(Dr.GetOrdinal("OrigenFlete")) : string.Empty;
                    item.LugarDestino = !Dr.IsDBNull(Dr.GetOrdinal("DestinoFlete")) ? Dr.GetString(Dr.GetOrdinal("DestinoFlete")) : string.Empty;
                    item.Chofer = !Dr.IsDBNull(Dr.GetOrdinal("Chofer")) ? Dr.GetString(Dr.GetOrdinal("Chofer")) : string.Empty;
                    item.Unidad = !Dr.IsDBNull(Dr.GetOrdinal("Unidad")) ? Dr.GetString(Dr.GetOrdinal("Unidad")) : string.Empty;
                    item.Observacion = !Dr.IsDBNull(Dr.GetOrdinal("Observacion")) ? Dr.GetString(Dr.GetOrdinal("Observacion")) : string.Empty;
                    item.Importe = !Dr.IsDBNull(Dr.GetOrdinal("Importe")) ? Dr.GetDecimal(Dr.GetOrdinal("Importe")) : 0;
                    Lista.Add(item);
                }
                Dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}