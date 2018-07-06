using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatCategoriaPuesto_Datos
    {
        public CatCategoriaPuestoModels AcCatCategoriaPuestos(CatCategoriaPuestoModels datos)
        {
            try
            {
                object[] parametros =
                {
                   datos.Opcion,datos.id_categoria,datos.descripcion,datos.id_puesto,datos.sueldoBase,datos.Usuario
                    };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatCategoriaPuesto", parametros);
                datos.id_categoria = aux.ToString();

                if (!string.IsNullOrEmpty(datos.id_categoria))
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
        public List<CatCategoriaPuestoModels> obtenerCatCategoriaPuesto(CatCategoriaPuestoModels Datos)
        {
            try
            {
                List<CatCategoriaPuestoModels> lista = new List<CatCategoriaPuestoModels>();
                CatCategoriaPuestoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatCategoriaPuesto");
                while (dr.Read())
                {
                    item = new CatCategoriaPuestoModels();
                    item.id_categoria = dr["id_categoria"].ToString();
                    item.descripcion = dr["descripcion"].ToString();
                    item.puesto = dr["puesto"].ToString();
                    item.sueldoBase = Convert.ToDecimal(dr["sueldoBase"].ToString());
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
        public CatCategoriaPuestoModels ObtenerDetalleCatCategoriaPuesto(CatCategoriaPuestoModels datos)
        {
            try
            {
                object[] parametros = { datos.id_categoria };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatCategoriaPuestoXID", parametros);
                while (dr.Read())
                {
                    datos.id_categoria = dr["id_categoria"].ToString();
                    datos.descripcion = dr["descripcion"].ToString();
                    datos.id_puesto =Convert.ToInt32(dr["id_puesto"].ToString());
                    datos.sueldoBase = Convert.ToDecimal(dr["sueldoBase"].ToString());
                    
                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatCategoriaPuestoModels EliminarCategoriaPuesto(CatCategoriaPuestoModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.id_categoria , datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatCategoriaPuesto", parametros);
                datos.id_categoria = aux.ToString();
                if (!string.IsNullOrEmpty(datos.id_categoria))
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
        public List<CatPuestoModels> obtenerListaCategoriaPuesto(CatCategoriaPuestoModels Datos)
        {
            try
            {
                List<CatPuestoModels> lista = new List<CatPuestoModels>();
                CatPuestoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatPuestos");
                while (dr.Read())
                {
                    item = new CatPuestoModels();
                    item.IDPuesto = Convert.ToInt32(dr["IDPuesto"].ToString());
                    item.Descripcion = dr["Descripcion"].ToString();

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