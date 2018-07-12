using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _AsistenciaEmpleados_Datos
    {
        public AsistenciaEmpleadoModels ActualizarListaFaltas(AsistenciaEmpleadoModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDFalta,
                    datos.user

                };
                object Resultado = SqlHelper.ExecuteScalar(datos.conexion, "spCSLDB_Faltas_ActualizarFaltasXFechaSistema", parametros);
                datos.IDFalta = Resultado.ToString();
                if (!string.IsNullOrEmpty(datos.IDFalta))
                {
                    datos.Completado = true;
                }
                else
                {
                    datos.Completado = false;
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsistenciaEmpleadoModels> GenerarListaFaltas(AsistenciaEmpleadoModels datos)
        {
            try
            {
                DataSet dt = SqlHelper.ExecuteDataset(datos.conexion, CommandType.StoredProcedure, "spCSLDB_Faltas_setFaltasXFechaSistema",
                new SqlParameter("@IDFalta", datos.IDFalta),
                new SqlParameter("@TablaFalta", datos.tablaAsistencia),
                new SqlParameter("@Fecha",datos.fecha),
                new SqlParameter("@usuario", datos.user));

                DataTableReader Dr2 = dt.Tables[0].CreateDataReader();
                List<AsistenciaEmpleadoModels> Lista = new List<AsistenciaEmpleadoModels>();
                AsistenciaEmpleadoModels Item;
                while (Dr2.Read())
                {
                    Item = new AsistenciaEmpleadoModels();
                    Item.IDFalta = Dr2.GetString(Dr2.GetOrdinal("id_falta"));
                    Item.IDEmpleados = Dr2.GetString(Dr2.GetOrdinal("nombreCompleto"));
                    Item.IDSucursal = Dr2.GetString(Dr2.GetOrdinal("nombreSuc"));
                    Item.fecha = Convert.ToDateTime(Dr2.GetString(Dr2.GetOrdinal("fecha")));
                    Lista.Add(Item);
                }
                

                
                datos.listaAsistencia = Lista;
                if (datos.listaAsistencia.Count>0)
                {
                    datos.Completado = true;
                }
                else
                {
                    datos.Completado = false;
                }
                Dr2.Close();
                return datos.listaAsistencia;
            }
            catch (Exception ex)
            {
                return datos.listaAsistencia;
            }
        }
        public List<CatEmpleadoModels> obtenerListaEmpleados(AsistenciaEmpleadoModels Datos)
        {
            try 
            {
                
                List<CatEmpleadoModels> lista = new List<CatEmpleadoModels>();
                CatEmpleadoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_Faltas_get_Empleados", Datos.IDSucursal);
                while (dr.Read())
                {
                    item = new CatEmpleadoModels();
                    item.IDEmpleado = !dr.IsDBNull(dr.GetOrdinal("IDEmpleado")) ? dr.GetString(dr.GetOrdinal("IDEmpleado")) : string.Empty;
                    item.CodigoUsuario = !dr.IsDBNull(dr.GetOrdinal("CodigoUsuario")) ? dr.GetString(dr.GetOrdinal("CodigoUsuario")) : string.Empty;
                    item.NombreCompleto = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty;
                    item.IDSucursalActual = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    item.FechaInacistencia = !dr.IsDBNull(dr.GetOrdinal("Fecha")) ? dr.GetDateTime(dr.GetOrdinal("Fecha")) : DateTime.Today;
                    item.Asistencia = !dr.IsDBNull(dr.GetOrdinal("Asitencia")) ? dr.GetString(dr.GetOrdinal("Asitencia")) : string.Empty;
                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}