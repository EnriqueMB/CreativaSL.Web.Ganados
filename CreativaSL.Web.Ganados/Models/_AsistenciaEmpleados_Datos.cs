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
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_Faltas_get_Empleados",Datos.IDSucursal);
                while (dr.Read())
                {
                    item = new CatEmpleadoModels();
                    item.IDEmpleado = dr["IDEmpleado"].ToString();
                    item.CodigoUsuario = dr["CodigoUsuario"].ToString();
                    item.NombreCompleto = dr["NombreCompleto"].ToString();
                    item.IDSucursalActual = dr["IDSucursal"].ToString();
                    item.NombreSucursal = dr["NombreSucursal"].ToString();
                    //item.bascula = Convert.ToBoolean(dr["bascula"].ToString());
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