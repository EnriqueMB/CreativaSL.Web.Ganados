using CreativaSL.Web.Ganados.ViewModel.Venta;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml;
using CreativaSL.Web.Ganados.Models.Datatable;
using CreativaSL.Web.Ganados.Models.Dto;
using CreativaSL.Web.Ganados.Models.Dto.Base;
using CreativaSL.Web.Ganados.Models.Dto.Venta;
using Newtonsoft.Json;
namespace CreativaSL.Web.Ganados.Models
{
    public class _CostoExtra_Datos
    {
        public string DatatableIndex(VentaModels2 venta, DataTableAjaxPostModel dataTableAjaxPostModel)
        {
            try
            {

                using (SqlConnection sqlcon = new SqlConnection(venta.Conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("spCSLDB_Venta_get_Index", sqlcon))
                    {
                        //parametros de entrada
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Start", SqlDbType.Int).Value = dataTableAjaxPostModel.start;
                        cmd.Parameters.Add("@Length", SqlDbType.Int).Value = dataTableAjaxPostModel.length;
                        cmd.Parameters.Add("@SearchValue", SqlDbType.NVarChar).Value = dataTableAjaxPostModel.search.value;
                        cmd.Parameters.Add("@Draw", SqlDbType.Int).Value = dataTableAjaxPostModel.draw;
                        cmd.Parameters.Add("@ColumnNumber", SqlDbType.Int).Value = dataTableAjaxPostModel.order[0].column;
                        cmd.Parameters.Add("@ColumnDir", SqlDbType.NVarChar).Value = dataTableAjaxPostModel.order[0].dir;

                        // execute
                        sqlcon.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        //datatable = Auxiliar.SqlReaderToJson(reader);

                        var indexDatatableDto = new IndexDatatableDto();

                        if (reader.HasRows)
                        {
                            indexDatatableDto.Data = new List<object>();
                            bool firstData = true;
                            IFormatProvider culture = new CultureInfo("es-MX", true);

                            while (reader.Read())
                            {
                                if (firstData)
                                {
                                    indexDatatableDto.Draw = int.Parse(reader["Draw"].ToString()); ;
                                    indexDatatableDto.RecordsFiltered = int.Parse(reader["RecordsFiltered"].ToString());
                                    indexDatatableDto.RecordsTotal = int.Parse(reader["RecordsTotal"].ToString());
                                    firstData = false;
                                }

                                var indexVentaDto = new IndexVentaDto();

                                indexVentaDto.IdVenta = reader["id_venta"].ToString();
                                indexVentaDto.Folio = reader["folio"].ToString();

                                if (!string.IsNullOrEmpty(reader["fecha"].ToString()))
                                {
                                    indexVentaDto.Fecha = DateTime.ParseExact(reader["fecha"].ToString(), "dd/MM/yyyy hh:mm:ss tt",
                                        culture).ToString("dd/MM/yyyy HH:mm", culture);
                                }

                                indexVentaDto.NombreContacto = reader["nombreContacto"].ToString();
                                indexVentaDto.LugarDestino = reader["lugarDestino"].ToString();
                                indexVentaDto.TotalGanado = int.Parse(reader["totalGanado"].ToString());
                                //indexVentaDto.Total = decimal.Parse(reader["total"].ToString());
                                //indexVentaDto.Estatus = reader["estatus"].ToString();
                                indexVentaDto.Css = reader["css"].ToString();
                                indexVentaDto.IdEstatus = int.Parse(reader["id_estatus"].ToString());
                                indexVentaDto.CantidadGanadoMachos = int.Parse(reader["cantidadGanadoMachos"].ToString());
                                indexVentaDto.CantidadGanadoHembras = int.Parse(reader["cantidadGanadoHembras"].ToString());
                                indexVentaDto.KilosGanadoMachos = decimal.Parse(reader["kilosGanadoMachos"].ToString());
                                indexVentaDto.KilosGanadoHembras = decimal.Parse(reader["kilosGanadoHembras"].ToString());
                                indexVentaDto.KilosGanadoTotal = decimal.Parse(reader["kilosGanadoTotal"].ToString());
                                indexVentaDto.NombreSucursal = reader["nombreSucursal"].ToString();

                                indexDatatableDto.Data.Add(indexVentaDto);
                            }
                        }

                        var json = JsonConvert.SerializeObject(indexDatatableDto);

                        reader.Close();

                        return json;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}