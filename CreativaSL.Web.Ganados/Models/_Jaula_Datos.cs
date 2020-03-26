using CreativaSL.Web.Ganados.Models.Datatable;
using CreativaSL.Web.Ganados.Models.Dto.Base;
using CreativaSL.Web.Ganados.Models.Dto.Jaula;
using CreativaSL.Web.Ganados.Models.System;
using Microsoft.ApplicationBlocks.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Jaula_Datos:BaseSQL
    {
        #region Choferes
        public List<CatChoferModels> GetChoferes(JaulaModels Jaula)
        {
            try
            {
                
                CatChoferModels Chofer;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Jaula.Conexion, "spCSLDB_Combo_get_AllCatChoferes");
                while (dr.Read())
                {
                    Chofer = new CatChoferModels
                    {
                        IDChofer = !dr.IsDBNull(dr.GetOrdinal("IDChofer")) ? dr.GetString(dr.GetOrdinal("IDChofer")) : string.Empty,
                        Nombre = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty,
                    };

                    Jaula.ListaChoferes.Add(Chofer);
                }
                dr.Close();
                return Jaula.ListaChoferes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Vehiculos
        public List<CatVehiculoModels> GetVehiculos(JaulaModels Jaula)
        {
            try
            {
                
                CatVehiculoModels Vehiculo;

                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Jaula.Conexion, "spCSLDB_Combo_get_CatVehiculosAll");
                while (dr.Read())
                {
                    Vehiculo = new CatVehiculoModels
                    {
                        IDVehiculo = !dr.IsDBNull(dr.GetOrdinal("IDVehiculo")) ? dr.GetString(dr.GetOrdinal("IDVehiculo")) : string.Empty,
                        nombreMarca = !dr.IsDBNull(dr.GetOrdinal("NombreVehiculo")) ? dr.GetString(dr.GetOrdinal("NombreVehiculo")) : string.Empty,
                        Modelo = !dr.IsDBNull(dr.GetOrdinal("Tipo")) ? dr.GetString(dr.GetOrdinal("Tipo")) : string.Empty
                    };

                    Jaula.ListaVehiculos.Add(Vehiculo);
                }
                dr.Close();
                return Jaula.ListaVehiculos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Ventas
        public List<JaulaVentas> ObtenerVentas()
        {
            var lista = new List<JaulaVentas>();

            using (var sqlcon = new SqlConnection(ConexionSql))
            {
                using (var cmd = new SqlCommand("spCIDDB_ObtenerVentas",
                    sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    sqlcon.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var item = new JaulaVentas();
                            item.Id_venta = reader["Id_venta"].ToString();
                            item.Folio = reader["Folio"].ToString();
                            item.Sucursal = reader["Sucursal"].ToString();
                            item.CantTotal = string.IsNullOrEmpty(reader["CantTotal"].ToString())
                                        ? 0
                                        : int.Parse(reader["CantTotal"].ToString());
                            item.PesoTotal = string.IsNullOrEmpty(reader["PesoTotal"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["PesoTotal"].ToString());
                            item.CbzMacho= string.IsNullOrEmpty(reader["CbzMacho"].ToString())
                                        ? 0
                                        : int.Parse(reader["CbzMacho"].ToString());
                            item.CbzHembra= string.IsNullOrEmpty(reader["CbzHembra"].ToString())
                                        ? 0
                                        : int.Parse(reader["CbzHembra"].ToString());
                            item.KgHembra= string.IsNullOrEmpty(reader["KgHembra"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["KgHembra"].ToString());
                            item.KgMacho= string.IsNullOrEmpty(reader["KgMacho"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["KgMacho"].ToString());
                            item.ImporteMacho= string.IsNullOrEmpty(reader["ImporteMacho"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["ImporteMacho"].ToString());
                            item.ImporteHembra= string.IsNullOrEmpty(reader["ImporteHembra"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["ImporteHembra"].ToString());
                            item.ImporteTotal = string.IsNullOrEmpty(reader["ImporteTotal"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["ImporteTotal"].ToString());
                            item.Percepcion = string.IsNullOrEmpty(reader["Percepcion"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["Percepcion"].ToString());
                            item.Deduccion = string.IsNullOrEmpty(reader["Deduccion"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["Deduccion"].ToString());
                            item.CostoExtra = string.IsNullOrEmpty(reader["CostoExtra"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["CostoExtra"].ToString());
                            item.Pago = string.IsNullOrEmpty(reader["Pago"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["Pago"].ToString());
                            item.Pendiente = string.IsNullOrEmpty(reader["Pendiente"].ToString())
                                        ? 0
                                        : decimal.Parse(reader["Pendiente"].ToString());
                            item.Total = string.IsNullOrEmpty(reader["Total"].ToString())
                                       ? 0
                                       : decimal.Parse(reader["Total"].ToString());

                            lista.Add(item);
                        }
                    }
                    reader.Close();
                }
            }

            return lista;
        }
        #endregion
        #region Vehiculo
        public CatVehiculoModels ObtenerDetalleCatVehiculo(CatVehiculoModels datos)
        {
            try
            {
                object[] parametros = { datos.IDVehiculo };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Jaula_get_CatVehiculoXID", parametros);
                while (dr.Read())
                {                                                                                
                    datos.nombreMarca = !dr.IsDBNull(dr.GetOrdinal("Marca")) ? dr.GetString(dr.GetOrdinal("Marca")) : string.Empty;                   
                    datos.Modelo = !dr.IsDBNull(dr.GetOrdinal("modelo")) ? dr.GetString(dr.GetOrdinal("modelo")) : string.Empty;
                    datos.Color = !dr.IsDBNull(dr.GetOrdinal("color")) ? dr.GetString(dr.GetOrdinal("color")) : string.Empty;
                    datos.Placas = !dr.IsDBNull(dr.GetOrdinal("placas")) ? dr.GetString(dr.GetOrdinal("placas")) : string.Empty;                                        
                    datos.PlacaJaula = !dr.IsDBNull(dr.GetOrdinal("placaJaula")) ? dr.GetString(dr.GetOrdinal("placaJaula")) : string.Empty;                    
                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<string> GetIDsVentas(string IDJaula)
        {
            try
            {
                List<string> IDVentas = new List<string>();
                using (var sqlcon=new SqlConnection(ConexionSql))
                {
                    using(var cmd=new SqlCommand("spCSLDB_Jaula_get_VentasXIDJaula", sqlcon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IDJaula", SqlDbType.Char).Value = IDJaula;
                        sqlcon.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                
                                IDVentas.Add(reader["id_venta"].ToString());
                            }
                        }
                        reader.Close();
                    }
                }
                return IDVentas;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JaulaModels GetJaulaID(string IDJaula)
        {
            try
            {
                JaulaModels item = new JaulaModels();
                using (var sqlcon=new SqlConnection(ConexionSql))
                {
                    using(var cmd = new SqlCommand("spCSLDB_Jaula_get_InfoXID", sqlcon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IDJaula", SqlDbType.Char).Value = IDJaula;
                        sqlcon.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                item.IDJaula = reader["id_jaula"].ToString();
                                item.IDChofer = reader["id_chofer"].ToString();
                                item.IDVehiculo = reader["id_vehiculo"].ToString();
                                item.Folio = reader["folio"].ToString();
                                item.FechaSalida = Convert.ToDateTime( reader["fechaSalida"].ToString());
                                item.HoraSalida =TimeSpan.Parse(reader["horaSalida"].ToString());
                                item.Fechallegada= Convert.ToDateTime(reader["fechaLlegada"].ToString());
                                item.Horallegada= TimeSpan.Parse(reader["horaLlegada"].ToString());
                                item.Observaciones= reader["nota"].ToString();
                            }
                        }
                        reader.Close();
                    }
                }
                return item;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JaulaModels ACJaula(List<string> IDsVentas,JaulaModels model)
        {
            try
            {                
                using (var sqlcon = new SqlConnection(ConexionSql))
                {
                    using (var cmd = new SqlCommand("spCIDDB_Jaula_ACJaula",
                        sqlcon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable dataTable;
                        dataTable = new DataTable();

                        dataTable.Columns.Add("Id", typeof(string));
                        if (IDsVentas != null)
                        {
                            foreach (var item in IDsVentas)
                            {
                                dataTable.Rows.Add(item);
                            }
                        }
                       
                        cmd.Parameters.Add("@Opcion", SqlDbType.Int).Value = model.Opcion;
                        cmd.Parameters.Add("@IdJaula", SqlDbType.Char).Value = model.IDJaula;
                        cmd.Parameters.Add("@IdChofer", SqlDbType.Char).Value = model.IDChofer;
                        cmd.Parameters.Add("@IdVehiculo", SqlDbType.Char).Value = model.IDVehiculo;                       
                        cmd.Parameters.Add("@FechaSalida", SqlDbType.Date).Value = model.FechaSalida;
                        cmd.Parameters.Add("@HoraSalida", SqlDbType.Time).Value = model.HoraSalida;
                        cmd.Parameters.Add("@FechaLlegada", SqlDbType.Date).Value =model.Fechallegada;
                        cmd.Parameters.Add("@HoraLlegada", SqlDbType.Time).Value = model.Horallegada;
                        cmd.Parameters.Add("@Nota", SqlDbType.NVarChar).Value = model.Observaciones;
                        cmd.Parameters.Add("@IdUsuario", SqlDbType.Char).Value = model.IDUsuario;
                        cmd.Parameters.Add("@UDTT_Ventas", SqlDbType.Structured).Value = dataTable;
                        sqlcon.Open();

                        var reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.IDJaula =  reader["id_jaula"].ToString();
                                if (!string.IsNullOrEmpty(model.IDJaula))
                                {
                                    model.Completado = true;
                                }
                                else
                                {
                                    model.Completado = false;
                                }

                            }
                        }
                        reader.Close();
                    }
                }
                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public string DatatableIndex(JaulaModels jaula, DataTableAjaxPostModel dataTableAjaxPostModel)
        {
            try
            {

                using (SqlConnection sqlcon = new SqlConnection(jaula.Conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("spCSLDB_Jaula_get_Index", sqlcon))
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

                                var indexJaulaDto = new IndexJaulaDto();

                                indexJaulaDto.IdJaula = reader["id_jaula"].ToString();
                                indexJaulaDto.Folio = reader["folio"].ToString();

                                if (!string.IsNullOrEmpty(reader["fecha"].ToString()))
                                {
                                    indexJaulaDto.Fecha = DateTime.ParseExact(reader["fecha"].ToString(), "dd/MM/yyyy hh:mm:ss tt",
                                        culture).ToString("dd/MM/yyyy HH:mm", culture);
                                }

                                /*indexJaulaDto.NombreContacto = reader["nombreContacto"].ToString();
                                indexJaulaDto.LugarDestino = reader["lugarDestino"].ToString();                                
                                //indexVentaDto.Total = decimal.Parse(reader["total"].ToString());
                                //indexVentaDto.Estatus = reader["estatus"].ToString();
                                indexJaulaDto.Css = reader["css"].ToString();
                                indexJaulaDto.IdEstatus = int.Parse(reader["id_estatus"].ToString());*/
                                indexJaulaDto.TotalGanado = int.Parse(reader["totalGanado"].ToString());
                                indexJaulaDto.CantidadGanadoMachos = int.Parse(reader["cantidadGanadoMachos"].ToString());
                                indexJaulaDto.CantidadGanadoHembras = int.Parse(reader["cantidadGanadoHembras"].ToString());
                                indexJaulaDto.KilosGanadoMachos = decimal.Parse(reader["kilosGanadoMachos"].ToString());
                                indexJaulaDto.KilosGanadoHembras = decimal.Parse(reader["kilosGanadoHembras"].ToString());
                                indexJaulaDto.KilosGanadoTotal = decimal.Parse(reader["kilosGanadoTotal"].ToString());
                                indexJaulaDto.NombreSucursal = reader["nombreSucursal"].ToString();
                                indexDatatableDto.Data.Add(indexJaulaDto);
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
        #endregion
    }
}