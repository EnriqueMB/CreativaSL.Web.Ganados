using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Compra_Datos
    {   
        /// <summary>
        /// Obtiene los datos a mostar en el modulo COMPRAS->INDEX
        /// </summary>
        /// <param name="CompraModels"></param>
        /// <returns></returns>
        public CompraModels ObtenerCompraIndex(CompraModels CompraModels)
            {
                try
                {
                    DataSet Ds = null;
                    Ds = SqlHelper.ExecuteDataset(CompraModels.Conexion, "spCSLDB_VENTAS_IndexVentas");
                    if (Ds != null)
                    {
                        if (Ds.Tables.Count > 0)
                        {
                            if (Ds.Tables[0] != null)
                            {
                            CompraModels.TablaCompra = Ds.Tables[0];
                            }
                        }
                    }
                    return CompraModels;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
       
        /// <summary>
        /// Obtiene un listado de todos los proveedores dados de alta
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<CatProveedorModels> ObtenerListadoProveedores(CompraModels Compra)
        {
            try
            {
                List<CatProveedorModels> ListaProveedores = new List<CatProveedorModels>();
                CatProveedorModels Proveedor;
                object[] parametros = {  };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Compra.Conexion, "spCSLDB_VENTAS_GetProveedores");
                while (dr.Read())
                {
                    Proveedor = new CatProveedorModels();
                    Proveedor.IDProveedor       = dr["id_proveedor"].ToString();
                    Proveedor.NombreRazonSocial = dr["NombreProveedor"].ToString();
                    Proveedor.IDTipoProveedor   = Int32.Parse(dr["id_tipoProveedor"].ToString());
                    ListaProveedores.Add(Proveedor);
                }
                return ListaProveedores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}