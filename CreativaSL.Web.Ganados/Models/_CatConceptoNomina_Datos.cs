using System;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatConceptoNomina_Datos
    {
        public CatConceptosNominaModels ObtenerConceptosNomina(CatConceptosNominaModels datos)
        {
            try
            {
                List<CatConceptosNominaModels> Lista = new List<CatConceptosNominaModels>();
                CatConceptosNominaModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatConceptoNomina");
                while (dr.Read())
                {
                    item = new CatConceptosNominaModels();
                    item.IDConceptoNomina = !dr.IsDBNull(dr.GetOrdinal("IDConceptoNomina")) ? dr.GetInt32(dr.GetOrdinal("IDConceptoNomina")) : 0;//Convert.ToInt32(dr["IDConceptoNomina"]);
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    item.Calculado= !dr.IsDBNull(dr.GetOrdinal("calculado")) ? dr.GetBoolean(dr.GetOrdinal("calculado")) : false;
                    item.SumaResta = !dr.IsDBNull(dr.GetOrdinal("sumaResta")) ? dr.GetBoolean(dr.GetOrdinal("sumaResta")) : false;
                    item.SoloLectura = !dr.IsDBNull(dr.GetOrdinal("soloLectura")) ? dr.GetBoolean(dr.GetOrdinal("soloLectura")) : false;
                    Lista.Add(item);
                }
                dr.Close();
                datos.LIstaConceptoNomina = Lista;
                return datos;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CatConceptosNominaModels ObtenerDetalleConceptoNomina(CatConceptosNominaModels datos)
        {
            try
            {
                object[] parametros = { datos.IDConceptoNomina };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatConceptoNominaXID", parametros);
                while (dr.Read())
                {
                    datos.IDConceptoNomina = !dr.IsDBNull(dr.GetOrdinal("IDConceptoNomina")) ? dr.GetInt32(dr.GetOrdinal("IDConceptoNomina")) : 0;//Convert.ToInt32(dr["IDConceptoNomina"]);
                    datos.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                    datos.Calculado = !dr.IsDBNull(dr.GetOrdinal("calculado")) ? dr.GetBoolean(dr.GetOrdinal("calculado")) : false;
                    datos.SumaResta = !dr.IsDBNull(dr.GetOrdinal("sumaResta")) ? dr.GetBoolean(dr.GetOrdinal("sumaResta")) : false;
                    datos.SoloLectura = !dr.IsDBNull(dr.GetOrdinal("soloLectura")) ? dr.GetBoolean(dr.GetOrdinal("soloLectura")) : false;
                    
                }
                dr.Close();
                return datos;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CatConceptosNominaModels AbcCatConceptosNomina(CatConceptosNominaModels conceptos)
        {
            try
            {
                object[] parametros =
                {
                    conceptos.Opcion,
                    conceptos.IDConceptoNomina,
                    conceptos.Descripcion ?? string.Empty,
                    conceptos.Calculado,
                    conceptos.SumaResta,
                    conceptos.SoloLectura,
                    conceptos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(conceptos.Conexion, "spCSLDB_Catalogo_ac_CatConceptoNomina", parametros);
                if (aux!=null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(aux.ToString(), out IDRegistro))
                    {
                        if (IDRegistro>0)
                        {
                            conceptos.Completado = true;
                            conceptos.IDConceptoNomina = IDRegistro;
                        }
                    }
                }
                return conceptos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatConceptosNominaModels EliminarConcetoNomina(CatConceptosNominaModels conceptos)
        {
            try
            {
                object[] parametros = { conceptos.IDConceptoNomina, conceptos.Usuario };
                object aux = SqlHelper.ExecuteScalar(conceptos.Conexion, "spCSLDB_Catalogo_del_CatConceptoNomina", parametros);
                if (aux != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(aux.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            conceptos.Completado = true;
                            conceptos.IDConceptoNomina = IDRegistro;
                        }
                    }
                }
                return conceptos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}