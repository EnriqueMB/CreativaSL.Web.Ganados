using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _ServicioMantenimiento_Datos
    {

        public ServiciosMantenimientoModels ObtenerDatosIndex(string Conexion, string IdSucursal)
        {
            try
            {
                ServiciosMantenimientoModels Result = new ServiciosMantenimientoModels();
                //Obtener el listado de vehículos
                
                DataSet Ds = SqlHelper.ExecuteDataset(Conexion, "", IdSucursal);
                if (Ds != null)
                {
                    if (Ds.Tables.Count == 2)
                    {
                        DataTableReader Dr01 = Ds.Tables[0].CreateDataReader();
                        List<CatVehiculoModels> ListaV = new List<CatVehiculoModels>();
                        CatVehiculoModels ItemV;
                        while (Dr01.Read())
                        {
                            ItemV = new CatVehiculoModels();
                            ItemV.IDVehiculo = !Dr01.IsDBNull(Dr01.GetOrdinal("")) ? Dr01.GetString(Dr01.GetOrdinal("")) : string.Empty;
                            ItemV.Modelo = !Dr01.IsDBNull(Dr01.GetOrdinal("")) ? Dr01.GetString(Dr01.GetOrdinal("")) : string.Empty;
                            ItemV.DateLastService = !Dr01.IsDBNull(Dr01.GetOrdinal("")) ? Dr01.GetDateTime(Dr01.GetOrdinal("")) : DateTime.MinValue;
                            ListaV.Add(ItemV);
                        }
                        //Obtener el listado de remolques
                        DataTableReader Dr02 = Ds.Tables[0].CreateDataReader();
                        List<CatRemolqueModels> ListaR = new List<CatRemolqueModels>();
                        CatRemolqueModels ItemR;
                        while (Dr01.Read())
                        {
                            ItemR = new CatRemolqueModels();
                            ItemR.IDRemolque = !Dr02.IsDBNull(Dr02.GetOrdinal("")) ? Dr02.GetString(Dr02.GetOrdinal("")) : string.Empty;
                            ItemR.placa = !Dr02.IsDBNull(Dr02.GetOrdinal("")) ? Dr02.GetString(Dr02.GetOrdinal("")) : string.Empty;
                            ItemR.DateLastService = !Dr02.IsDBNull(Dr02.GetOrdinal("")) ? Dr02.GetDateTime(Dr02.GetOrdinal("")) : DateTime.MinValue;
                            ListaR.Add(ItemR);
                        }
                        // Asignar listas a objeto principal y retornar
                        Result.ListaVehiculos = ListaV;
                        Result.ListaRemolques = ListaR;
                    }
                }
                return Result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}