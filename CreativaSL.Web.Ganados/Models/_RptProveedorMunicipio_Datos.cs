using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _RptProveedorMunicipio_Datos
    {
        public List<RptProveedorMunicipioModels> obtenerListaProveedoresFecha(RptProveedorMunicipioModels Datos)
        {
            try
            {
                List<RptProveedorMunicipioModels> lista = new List<RptProveedorMunicipioModels>();
                RptProveedorMunicipioModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_Reporte_get_ProveedorMunicipio",Datos.fechaInicio,Datos.fechaFin,Datos.IDEstado,Datos.IDMunicipio);
                while (dr.Read())
                {
                    item = new RptProveedorMunicipioModels();
                    item.ganadoPactadoMacho = !dr.IsDBNull(dr.GetOrdinal("GanadoPactadoMachos")) ? dr.GetInt32(dr.GetOrdinal(" GanadoPactadoMachos")) : 0;
                    item.ganadoPactadoHembra = !dr.IsDBNull(dr.GetOrdinal("GanadoPactadoHembras")) ? dr.GetInt32(dr.GetOrdinal("GanadoPactadoHembras")) : 0;
                    item.ganadoCompradoMacho = !dr.IsDBNull(dr.GetOrdinal("GanadoCompradoMacho")) ? dr.GetInt32(dr.GetOrdinal(" GanadoCompradoMacho")) : 0;
                    item.ganadoCompradoHembra = !dr.IsDBNull(dr.GetOrdinal("GanadoCompradoHembra")) ? dr.GetInt32(dr.GetOrdinal("GanadoCompradoHembra")) : 0;
                    item.fechaProgramada= !dr.IsDBNull(dr.GetOrdinal("FechaHoraProgamada")) ? dr.GetDateTime(dr.GetOrdinal("FechaHoraProgamada")) : DateTime.Now;
                    item.Estado = !dr.IsDBNull(dr.GetOrdinal("nombreEstado")) ? dr.GetString(dr.GetOrdinal("nombreEstado")) : string.Empty;
                    item.totalPactado = !dr.IsDBNull(dr.GetOrdinal("GanadoPactatoTotal")) ? dr.GetInt32(dr.GetOrdinal("GanadoPactatoTotal")) : 0;
                    item.totalComprado = !dr.IsDBNull(dr.GetOrdinal("GanadoCompradoTotal")) ? dr.GetInt32(dr.GetOrdinal("GanadoCompradoTotal")) : 0;
                    item.municipio = !dr.IsDBNull(dr.GetOrdinal("NombreMunicipio")) ? dr.GetDecimal(dr.GetOrdinal("NombreMunicipio")) : 0;
                    lista.Add(item);
                }
                
                Datos.listaProveedor = lista;
                return Datos.listaProveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatEstadoModels> obtenerListaEstados(RptProveedorMunicipioModels Datos)
        {
            try
            {
                List<CatEstadoModels> lista = new List<CatEstadoModels>();
                CatEstadoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_get_ComboEstado","Mex");
                while (dr.Read())
                {
                    item = new CatEstadoModels();
                    item.id_estado = Convert.ToInt32(dr["id_estado"].ToString());
                    item.codigoEstado = dr["clave"].ToString();
                    item.descripcion = dr["descripcion"].ToString();

                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CatMunicipioModels> obtenerListaMunicipios(RptProveedorMunicipioModels Datos)
        {
            try
            {
                List<CatMunicipioModels> lista = new List<CatMunicipioModels>();
                CatMunicipioModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_get_ComboMunicipio", "Mex", Datos.IDEstado,1);
                while (dr.Read())
                {
                    item = new CatMunicipioModels();
                    item.id_municipio = Convert.ToInt32(dr["id_municipio"].ToString());
                    item.codigoMunicipio = dr["clave"].ToString();
                    item.descripcion = dr["descripcion"].ToString();

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