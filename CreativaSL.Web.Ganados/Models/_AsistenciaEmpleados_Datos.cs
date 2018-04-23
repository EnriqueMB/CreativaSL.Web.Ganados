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
        
        public int GenerarListaFaltas(AsistenciaEmpleadoModels datos)
        {
            try
            {
                DataSet dt = SqlHelper.ExecuteDataset(datos.conexion, CommandType.StoredProcedure, "spCSLDB_Faltas_setFaltasXFechaSistema",
                new SqlParameter("@IDFalta", datos.IDFalta),
                new SqlParameter("@TablaFalta", datos.tablaAsistencia),
                new SqlParameter("@Fecha",datos.fecha),
                new SqlParameter("@usuario", datos.user));
                return Convert.ToInt32(dt.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public List<CatEmpleadoModels> obtenerListaEmpleados(AsistenciaEmpleadoModels Datos)
        {
            try 
            {
                
                List<CatEmpleadoModels> lista = new List<CatEmpleadoModels>();
                CatEmpleadoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_Faltas_get_Empleados");
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