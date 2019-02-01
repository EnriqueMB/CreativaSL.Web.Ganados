using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatRangoPesoVenta_Datos
    {
        public string RangoPesoVenta_index_RangoPesoVenta(string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_RangoPesoVenta_index_RangoPesoVenta");
                string datatable = Auxiliar.SqlReaderToJson(dr);

                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<CatTipoClienteModels> ObetenerListaTipoProveedor(CatRangoPesoCompraModels Datos)
        //{
        //    try
        //    {
        //        List<CatTipoProveedorModels> lista = new List<CatTipoProveedorModels>();
        //        CatTipoProveedorModels item;
        //        SqlDataReader dr = null;
        //        dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatTipoProveedor");
        //        while (dr.Read())
        //        {
        //            item = new CatTipoProveedorModels();
        //            item.IDTipoProveedor = !dr.IsDBNull(dr.GetOrdinal("id_tipoProveedor")) ? dr.GetInt32(dr.GetOrdinal("id_tipoProveedor")) : 0;
        //            item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
        //            lista.Add(item);
        //        }
        //        dr.Close();
        //        return lista;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
    }
}