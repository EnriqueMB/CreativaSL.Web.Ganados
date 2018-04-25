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
                    item.IDAlmacen = dr["id_almacen"].ToString();
                    item.nombreAlmacen = dr["nombreSuc"].ToString();
                    item.ClaveAlmacen = dr["clave"].ToString();
                    item.Descripcion = dr["descripcion"].ToString();

                    Lista.Add(item);
                }
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
                    item.ID_almacen = dr["id_unidadProducto"].ToString();
                    item.Clave = dr["clave"].ToString();
                    item.Nombre_producto = dr["producto"].ToString();
                    item.Existencia = dr["existencia"].ToString();
                    item.Unidad_medida = dr["unidadMedida"].ToString();
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