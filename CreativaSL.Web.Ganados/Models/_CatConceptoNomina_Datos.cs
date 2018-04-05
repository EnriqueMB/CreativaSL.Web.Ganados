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
                    item.IDConceptoNomina = !dr.IsDBNull(dr.GetOrdinal("IDConceptoNomina")) ? dr.GetString(dr.GetOrdinal("IDConceptoNomina")) : string.Empty;
                    item.Calculado= !dr.IsDBNull(dr.GetOrdinal("calculado")) ? dr.GetBoolean(dr.GetOrdinal("calculado")) : false;
                    item.SumaResta = !dr.IsDBNull(dr.GetOrdinal("sumaResta")) ? dr.GetBoolean(dr.GetOrdinal("sumaResta")) : false;
                    item.SoloLectura = !dr.IsDBNull(dr.GetOrdinal("soloLectura")) ? dr.GetBoolean(dr.GetOrdinal("soloLectura")) : false;
                    Lista.Add(item);
                }
                datos.LIstaConceptoNomina = Lista;
                return datos;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        

    }
}