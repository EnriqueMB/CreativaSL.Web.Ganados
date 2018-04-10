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
                
                DataSet Ds = SqlHelper.ExecuteDataset(Conexion, "spCSLDB_Mantenimiento_get_IndexServicios", IdSucursal);
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
                            ItemV.IDVehiculo = !Dr01.IsDBNull(Dr01.GetOrdinal("IDVehiculo")) ? Dr01.GetString(Dr01.GetOrdinal("IDVehiculo")) : string.Empty;
                            ItemV.Modelo = !Dr01.IsDBNull(Dr01.GetOrdinal("Descripcion")) ? Dr01.GetString(Dr01.GetOrdinal("Descripcion")) : string.Empty;
                            ItemV.DateLastService = !Dr01.IsDBNull(Dr01.GetOrdinal("LastDate")) ? Dr01.GetDateTime(Dr01.GetOrdinal("LastDate")) : DateTime.MinValue;
                            ListaV.Add(ItemV);
                        }
                        //Obtener el listado de remolques
                        DataTableReader Dr02 = Ds.Tables[1].CreateDataReader();
                        List<CatRemolqueModels> ListaR = new List<CatRemolqueModels>();
                        CatRemolqueModels ItemR;
                        while (Dr02.Read())
                        {
                            ItemR = new CatRemolqueModels();
                            ItemR.IDRemolque = !Dr02.IsDBNull(Dr02.GetOrdinal("IDRemolque")) ? Dr02.GetString(Dr02.GetOrdinal("IDRemolque")) : string.Empty;
                            ItemR.placa = !Dr02.IsDBNull(Dr02.GetOrdinal("Descripcion")) ? Dr02.GetString(Dr02.GetOrdinal("Descripcion")) : string.Empty;
                            ItemR.DateLastService = !Dr02.IsDBNull(Dr02.GetOrdinal("LastDate")) ? Dr02.GetDateTime(Dr02.GetOrdinal("LastDate")) : DateTime.MinValue;
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

        public List<ServiciosMantenimientoModels> ObtenerServiciosXIDVehiculo(string Conexion, string IDVehiculo)
        {
            try
            {
                List<ServiciosMantenimientoModels> Lista = new List<ServiciosMantenimientoModels>();
                ServiciosMantenimientoModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Mantenimiento_get_ServiciosXIDVehiculo", IDVehiculo);
                while (Dr.Read())
                {
                    Item = new ServiciosMantenimientoModels();
                    Item.IDServicio = !Dr.IsDBNull(Dr.GetOrdinal("IDServicio")) ? Dr.GetString(Dr.GetOrdinal("IDServicio")) : string.Empty;
                    Item.Fecha = !Dr.IsDBNull(Dr.GetOrdinal("Fecha")) ? Dr.GetDateTime(Dr.GetOrdinal("Fecha")) : DateTime.MinValue; ;
                    Item.Sucursal.NombreSucursal = !Dr.IsDBNull(Dr.GetOrdinal("Sucursal")) ? Dr.GetString(Dr.GetOrdinal("Sucursal")) : string.Empty;
                    Item.ImporteTotal = !Dr.IsDBNull(Dr.GetOrdinal("ImporteTotal")) ? Dr.GetDecimal(Dr.GetOrdinal("ImporteTotal")) : 0;
                    Item.ServiciosRealizados = !Dr.IsDBNull(Dr.GetOrdinal("Servicios")) ? Dr.GetString(Dr.GetOrdinal("Servicios")) : string.Empty;
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}