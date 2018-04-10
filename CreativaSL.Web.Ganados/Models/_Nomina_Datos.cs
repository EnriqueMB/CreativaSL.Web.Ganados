using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class Nomina_Datos
    {
        public List<NominaModels> ObtenerListaNomina(NominaModels Datos)
        {
            try
            {
                List<NominaModels> Lista = new List<NominaModels>();
                NominaModels Item;
                object[] parametros = {
                    Datos.EsBusqueda,
                    Datos.BandBusqClave,
                    Datos.BandIDSucursal,
                    Datos.BandBusqFechas,
                    Datos.ClaveNomina ?? string.Empty,
                    Datos.FechaInicio != null ? Datos.FechaInicio : DateTime.Today,
                    Datos.FechaFin != null ? Datos.FechaFin : DateTime.Today,
                    Datos.IDSucursal ?? string.Empty
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Nomina_get_ObtenerNominas", parametros);
                while (dr.Read())
                {
                    Item = new NominaModels();
                    Item.IDNomina = !dr.IsDBNull(dr.GetOrdinal("IDNomina")) ? dr.GetString(dr.GetOrdinal("IDNomina")) : string.Empty;
                    Item.ClaveNomina = !dr.IsDBNull(dr.GetOrdinal("ClaveNomina")) ? dr.GetString(dr.GetOrdinal("ClaveNomina")) : string.Empty;
                    Item.FechaInicio = !dr.IsDBNull(dr.GetOrdinal("FechaInicio")) ? dr.GetDateTime(dr.GetOrdinal("FechaInicio")) : DateTime.Today;
                    Item.FechaFin = !dr.IsDBNull(dr.GetOrdinal("FechaFin")) ? dr.GetDateTime(dr.GetOrdinal("FechaFin")) : DateTime.Today;
                    Item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<NominaModels> ObtenerListaNominaEmpleado(NominaModels Datos)
        {
            try
            {
                List<NominaModels> Lista = new List<NominaModels>();
                NominaModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Nomina_get_Empleados", Datos.IDSucursal);
                while (dr.Read())
                {
                    Item = new NominaModels();
                    Item.IDEmpleado = !dr.IsDBNull(dr.GetOrdinal("IDEmpleado")) ? dr.GetString(dr.GetOrdinal("IDEmpleado")) : string.Empty;
                    Item.CodigoUsuario = !dr.IsDBNull(dr.GetOrdinal("Clave")) ? dr.GetString(dr.GetOrdinal("Clave")) : string.Empty;
                    Item.NombreEmpleado = !dr.IsDBNull(dr.GetOrdinal("Nombre")) ? dr.GetString(dr.GetOrdinal("Nombre")) : string.Empty;
                    Item.Puesto = !dr.IsDBNull(dr.GetOrdinal("Puesto")) ? dr.GetString(dr.GetOrdinal("Puesto")) : string.Empty;
                    Item.CategoriaPuesto = !dr.IsDBNull(dr.GetOrdinal("Categoria")) ? dr.GetString(dr.GetOrdinal("Puesto")) : string.Empty;
                    Item.Sueldo = !dr.IsDBNull(dr.GetOrdinal("Sueldo")) ? dr.GetDecimal(dr.GetOrdinal("Sueldo")) : 0;
                    Item.Percepciones = !dr.IsDBNull(dr.GetOrdinal("Percepciones")) ? dr.GetDecimal(dr.GetOrdinal("Percepciones")) : 0;
                    Item.Deducciones = !dr.IsDBNull(dr.GetOrdinal("Deducciones")) ? dr.GetDecimal(dr.GetOrdinal("Deducciones")) : 0;
                    Lista.Add(Item);
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}