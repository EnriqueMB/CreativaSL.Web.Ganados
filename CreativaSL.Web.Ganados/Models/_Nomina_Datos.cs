using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
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
                    Item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty; 
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

        public void ANomina(NominaModels Datos)
        {
            try
            {
                object Resultado = SqlHelper.ExecuteScalar(Datos.Conexion, CommandType.StoredProcedure, "spCSLDB_Nomina_a_Nomina",
                     new SqlParameter("@IDSucursal", Datos.IDSucursal),
                     new SqlParameter("@FechaInicio", Datos.FechaInicio),
                     new SqlParameter("@FechaFin", Datos.FechaFin),
                     new SqlParameter("@TablaEmpleado", Datos.TablaEmpladoNomina),
                     new SqlParameter("@IDUsuario", Datos.Usuario)
                     );
                if (Resultado != null)
                {
                    if (!string.IsNullOrEmpty(Resultado.ToString()))
                    {
                        Datos.Completado = true;
                        Datos.IDNomina = Resultado.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NominaModels> ObtenerListaDetalleNomina(NominaModels Datos)
        {
            try
            {
                List<NominaModels> Lista = new List<NominaModels>();
                NominaModels Item;
                object[] parametros = {
                    Datos.IDNomina ?? string.Empty,
                    Datos.IDSucursal ?? string.Empty
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Nomina_get_NominaEmpleadosXID", parametros);
                while (dr.Read())
                {
                    Item = new NominaModels();
                    Item.IDNomina = !dr.IsDBNull(dr.GetOrdinal("IDNomina")) ? dr.GetString(dr.GetOrdinal("IDNomina")) : string.Empty;
                    Item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    Item.ClaveNomina = !dr.IsDBNull(dr.GetOrdinal("ClaveNomina")) ? dr.GetString(dr.GetOrdinal("ClaveNomina")) : string.Empty;
                    Item.CodigoUsuario = !dr.IsDBNull(dr.GetOrdinal("Clave")) ? dr.GetString(dr.GetOrdinal("Clave")) : string.Empty;
                    Item.NombreEmpleado = !dr.IsDBNull(dr.GetOrdinal("Nombre")) ? dr.GetString(dr.GetOrdinal("Nombre")) : string.Empty;
                    Item.Puesto = !dr.IsDBNull(dr.GetOrdinal("Puesto")) ? dr.GetString(dr.GetOrdinal("Puesto")) : string.Empty;
                    Item.CategoriaPuesto = !dr.IsDBNull(dr.GetOrdinal("Categoria")) ? dr.GetString(dr.GetOrdinal("Categoria")) : string.Empty;
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
        
        public NominaModels AgregarConceptoNomina(NominaModels Datos)
        {
            try
            {
                object[] parametros =
                {
                    Datos.EsFijo, Datos.IDEmpleado, Datos.IDConcepto, Datos.Sueldo, Datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Nomina_set_AgregarConcepto", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro == 1)
                        {
                            Datos.Completado = true;
                        }
                        Datos.Resultado = IDRegistro;
                    }
                }
                return Datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<CatConceptoNominaModels> ObtenerConceptosNomina(NominaModels Datos)
        {
            try
            {
                List<CatConceptoNominaModels> lista = new List<CatConceptoNominaModels>();
                CatConceptoNominaModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Nomina_Combo_get_CatConceptoNomina");
                while (dr.Read())
                {
                    item = new CatConceptoNominaModels();
                    item.IDConceptoNomina = !dr.IsDBNull(dr.GetOrdinal("IDConcepto")) ? dr.GetInt32(dr.GetOrdinal("IDConcepto")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
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