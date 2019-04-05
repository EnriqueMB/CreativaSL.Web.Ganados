using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _VentaGeneral_Datos
    {
        public string VentaGeneral_spCIDDB_index(string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "ventaGeneral.spCIDDB_index");
                string datatable = Auxiliar.SqlReaderToJson(dr);

                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RespuestaAjax VentaGeneral_spCIDDB_ac(VentaGeneralModels oVentaGeneral, string conexion, string usuario, int opcion)
        {
            try
            {
                RespuestaAjax oRespuesta = new RespuestaAjax();
                // construct sql connection and sql command objects.
                using (SqlConnection sqlcon = new SqlConnection(conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("ventaGeneral.spCIDDB_ac", sqlcon))
                    {
                        //parametros de entrada
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable dataTable;
                        dataTable = new DataTable("Items");

                        dataTable.Columns.Add("id", typeof(int));
                        dataTable.Columns.Add("fk_id", typeof(int));
                        dataTable.Columns.Add("id_producto", typeof(string));
                        dataTable.Columns.Add("id_tipoProducto", typeof(int));
                        dataTable.Columns.Add("id_documentoPorCobrarDetalle", typeof(string));
                        dataTable.Columns.Add("cantidad", typeof(decimal));
                        dataTable.Columns.Add("precioUnitario", typeof(decimal));
                        dataTable.Columns.Add("id_almacen", typeof(string));
                        dataTable.Columns.Add("id_unidadProducto", typeof(string));

                        foreach (var item in oVentaGeneral.ListaDetalles)
                        {
                            dataTable.Rows.Add(
                                item.Id, 
                                item.Fk_id, 
                                item.Id_producto, 
                                item.Id_tipoProducto,
                                item.Id_documentoPorCobrar, 
                                item.Cantidad, 
                                item.PrecioUnitario,
                                item.Id_almacen, 
                                item.Id_unidadProducto
                            );
                        }

                        cmd.Parameters.Add("@id", SqlDbType.Char).Value = oVentaGeneral.Id;
                        cmd.Parameters.Add("@id_sucursal", SqlDbType.Char).Value = oVentaGeneral.Id_sucursal;
                        cmd.Parameters.Add("@id_cliente", SqlDbType.Char).Value = oVentaGeneral.Id_cliente;
                        cmd.Parameters.Add("@tbl_productosDetalle", SqlDbType.Structured).Value = dataTable;
                        cmd.Parameters.Add("@usuario", SqlDbType.Char).Value = usuario;
                        cmd.Parameters.Add("@opcion", SqlDbType.SmallInt).Value = opcion;

                        //parametros de salida
                        cmd.Parameters.Add(new SqlParameter("@mensaje", SqlDbType.NVarChar, -1)); //-1 para tipo MAX
                        cmd.Parameters["@mensaje"].Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new SqlParameter("@success", SqlDbType.Bit));
                        cmd.Parameters["@success"].Direction = ParameterDirection.Output;
                        // execute
                        sqlcon.Open();

                        cmd.ExecuteNonQuery();
                        oRespuesta.Mensaje = cmd.Parameters["@mensaje"].Value.ToString();

                        bool success;

                        if (Boolean.TryParse(cmd.Parameters["@success"].Value.ToString(), out success))
                        {
                            oRespuesta.Success = success;
                        }
                    }
                }

                return oRespuesta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}