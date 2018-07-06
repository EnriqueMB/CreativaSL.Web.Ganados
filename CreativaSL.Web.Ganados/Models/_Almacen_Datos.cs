using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Almacen_Datos
    {
        public List<CatAlmacenModels> ObtenerGridAlmacen(CatAlmacenModels Datos)
        {
            try
            {
                List<CatAlmacenModels> Lista = new List<CatAlmacenModels>();
                CatAlmacenModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatAlmacen");
                while (dr.Read())
                {
                    item = new CatAlmacenModels();
                    
                    item.IDAlmacen = !dr.IsDBNull(dr.GetOrdinal("id_almacen")) ? dr.GetString(dr.GetOrdinal("id_almacen")) : string.Empty;
                    item.nombreAlmacen = !dr.IsDBNull(dr.GetOrdinal("nombreSuc")) ? dr.GetString(dr.GetOrdinal("nombreSuc")) : string.Empty;
                    item.ClaveAlmacen = !dr.IsDBNull(dr.GetOrdinal("clave")) ? dr.GetString(dr.GetOrdinal("clave")) : string.Empty;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;

                    Lista.Add(item);
                }
                dr.Close();
                return Lista;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<VerAlmacenInventarioModels> ObtenerListaInventario(CatAlmacenModels Datos)
        {
            try
            {
                List<VerAlmacenInventarioModels> lista = new List<VerAlmacenInventarioModels>();
                VerAlmacenInventarioModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Almacen_get_AlmacenInventario", Datos.IDAlmacen);
                while (dr.Read())
                {
                    item = new VerAlmacenInventarioModels();
                    item.ID_almacen = !dr.IsDBNull(dr.GetOrdinal("id_unidadProducto")) ? dr.GetString(dr.GetOrdinal("id_unidadProducto")) : string.Empty;
                    item.Clave = !dr.IsDBNull(dr.GetOrdinal("clave")) ? dr.GetString(dr.GetOrdinal("clave")) : string.Empty;
                    item.Nombre_producto = !dr.IsDBNull(dr.GetOrdinal("producto")) ? dr.GetString(dr.GetOrdinal("producto")) : string.Empty;
                    item.Existencia = !dr.IsDBNull(dr.GetOrdinal("existencia")) ? dr.GetDecimal(dr.GetOrdinal("existencia")) : 0;
                    item.Unidad_medida = !dr.IsDBNull(dr.GetOrdinal("unidadMedida")) ? dr.GetString(dr.GetOrdinal("unidadMedida")) : string.Empty;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}