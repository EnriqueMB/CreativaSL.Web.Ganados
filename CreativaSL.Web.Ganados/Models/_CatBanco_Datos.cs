using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatBanco_Datos
    {
        public List<CatBancoModels> ObtenerlistaBancos(CatBancoModels Datos)
        {
            try
            {
                List<CatBancoModels> lista = new List<CatBancoModels>();
                CatBancoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_Bancos");
                while (dr.Read())
                {
                    item = new CatBancoModels();
                    item.IDBanco = Convert.ToInt32(dr["id_banco"]);
                    item.Descripcion = dr["descripcion"].ToString();
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CatBancoModels DaCatBancos(CatBancoModels Datos)
        {
            try
            {
                object[] parametros =
                {
                    Datos.Opcion, Datos.IDBanco, Datos.Descripcion, Datos.Imagen, Datos.Usuario, Datos.BandImg
                };
                object Resultado = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Catalogo_ac_CatBancos", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            Datos.Completado = true;
                            Datos.IDBanco = IDRegistro;
                        }
                    }
                }
                return Datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public CatBancoModels ObternerDetalleCatBanco(CatBancoModels Datos)
        {
            try
            {
                object[] parametros = { Datos.IDBanco };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_BancosXID", parametros);
                while (dr.Read())
                {
                    Datos.IDBanco = Convert.ToInt32(dr["id_banco"].ToString());
                    Datos.Descripcion = dr["descripcion"].ToString();
                    Datos.Imagen = dr["imagen"].ToString();
                }
                return Datos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CatBancoModels EliminarCatBanco(CatBancoModels Datos)
        {
            try
            {
                object[] parametros =
                {
                    Datos.IDBanco, Datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Catalogo_del_CatBanco", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            Datos.Completado = true;
                            Datos.IDBanco = IDRegistro;
                        }
                    }
                }
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}