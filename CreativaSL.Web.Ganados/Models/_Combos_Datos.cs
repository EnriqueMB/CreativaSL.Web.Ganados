using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Combos_Datos
    {
        public List<CatVehiculoModels> ObtenerComboVehiculos(string Conexion, string IdSucursal)
        {
            try
            {
                List<CatVehiculoModels> Lista = new List<CatVehiculoModels>();
                CatVehiculoModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatVehiculo", IdSucursal);
                while(Dr.Read())
                {
                    Item = new CatVehiculoModels();
                    Item.IDVehiculo = !Dr.IsDBNull(Dr.GetOrdinal("id_vehiculo")) ? Dr.GetString(Dr.GetOrdinal("id_vehiculo")) : string.Empty;
                    Item.Modelo = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<CatVehiculoModels> ObtenerComboVehiculosPrincp(string Conexion)
        {
            try
            {
                List<CatVehiculoModels> Lista = new List<CatVehiculoModels>();
                CatVehiculoModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatVehiculoEntregaCombustible");
                while (Dr.Read())
                {
                    Item = new CatVehiculoModels();
                    Item.IDVehiculo = !Dr.IsDBNull(Dr.GetOrdinal("id_vehiculo")) ? Dr.GetString(Dr.GetOrdinal("id_vehiculo")) : string.Empty;
                    Item.Modelo = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatTipoCombustibleModels> ObtenerComboTiposCombustible(string Conexion)
        {
            try
            {
                List<CatTipoCombustibleModels> Lista = new List<CatTipoCombustibleModels>();
                CatTipoCombustibleModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatTipoCombustible");
                while (Dr.Read())
                {
                    Item = new CatTipoCombustibleModels();
                    Item.IDTipoCombustible = !Dr.IsDBNull(Dr.GetOrdinal("id_tipoCombustible")) ? Dr.GetInt32(Dr.GetOrdinal("id_tipoCombustible")) : 0;
                    Item.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatSucursalesModels> ObtenerComboSucursales(string Conexion)
        {
            try
            {
                List<CatSucursalesModels> Lista = new List<CatSucursalesModels>();
                CatSucursalesModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Combo_get_CatSucursal");
                while (Dr.Read())
                {
                    Item = new CatSucursalesModels();
                    Item.IDSucursal = !Dr.IsDBNull(Dr.GetOrdinal("IDSucursal")) ? Dr.GetString(Dr.GetOrdinal("IDSucursal")) : string.Empty;
                    Item.NombreSucursal = !Dr.IsDBNull(Dr.GetOrdinal("NombreSucursal")) ? Dr.GetString(Dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatTipoServicioModels> ObtenerComboTipoServicio(string Conexion)
        {
            try
            {
                List<CatTipoServicioModels> Lista = new List<CatTipoServicioModels>();
                CatTipoServicioModels Item;
                SqlDataReader Dr = SqlHelper.ExecuteReader(Conexion, "spCSLDB_Mantenimiento_get_ComboTipoServicios");
                while(Dr.Read())
                {
                    Item = new CatTipoServicioModels();
                    Item.IDTipoServicio = !Dr.IsDBNull(Dr.GetOrdinal("IDTipoServicio")) ? Dr.GetString(Dr.GetOrdinal("IDTipoServicio")) : string.Empty;
                    Item.Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty;
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