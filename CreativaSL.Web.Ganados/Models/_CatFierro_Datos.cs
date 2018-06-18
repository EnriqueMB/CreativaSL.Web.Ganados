using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatFierro_Datos
    {

        public SqlDataReader DatatableIndex(CatFierroModels fierro)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(fierro.Conexion, "spCSLDB_Catalogo_get_CatFierro");
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Obtener la lista de todos los fierros activos y dibujarlos 
        /// </summary>
        /// <param name="datos">Recibe la cadena de conexion</param>
        /// <returns>Retornar la lista de los fierros</returns>
        public CatFierroModels ObtenerListaFierros(CatFierroModels datos)
        {
            try
            {
                List<CatFierroModels> Lista = new List<CatFierroModels>();
                CatFierroModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatFierro");
                while (dr.Read())
                {
                    Item = new CatFierroModels();
                    Item.IDFierro = !dr.IsDBNull(dr.GetOrdinal("IDFierro")) ? dr.GetString(dr.GetOrdinal("IDFierro")) : string.Empty;
                    Item.NombreFierro = !dr.IsDBNull(dr.GetOrdinal("NombreFierro")) ? dr.GetString(dr.GetOrdinal("NombreFierro")) : string.Empty;
                    Item.ImgFierro = !dr.IsDBNull(dr.GetOrdinal("ImgFierro")) ? dr.GetString(dr.GetOrdinal("ImgFierro")) : string.Empty;
                    Item.Observaciones = !dr.IsDBNull(dr.GetOrdinal("Observaciones")) ? dr.GetString(dr.GetOrdinal("Observaciones")) : string.Empty;

                    Bitmap bmpFromString = Item.ImgFierro.Base64StringToBitmap();
                    Item.ImagenContruida = bmpFromString.ToBase64ImageTag(ImageFormat.Png);
                    Lista.Add(Item);
                }
                datos.ListaFierro = Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// AB del altas y cambios para los Fierros 
        /// </summary>
        /// <param name="datos">Recibe los datos para la alta y las modificaciones</param>
        /// <returns>Retona si fue completado con exito o un error</returns>
        public CatFierroModels AbcCatFierro(CatFierroModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Opcion, datos.IDFierro, datos.NombreFierro, datos.ImgFierro, datos.Observaciones, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatFierro", parametros);
                datos.IDFierro = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDFierro))
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
        /// <summary>
        /// Eliminar un registro de Fierro
        /// </summary>
        /// <param name="datos">Recibe el IDFierro y el Usuario logeado</param>
        /// <returns>Retorna Si se completo con exito o un error</returns>
        public CatFierroModels EliminarFierro(CatFierroModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDFierro, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatFierro", parametros);
                datos.IDFierro = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDFierro))
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
        public CatFierroModels ObtenerDetalleCatFierro(CatFierroModels datos)
        {
            try
            {
                object[] parametros = { datos.IDFierro };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatFierroXID", parametros);
                while (dr.Read())
                {
                    datos.IDFierro = !dr.IsDBNull(dr.GetOrdinal("IDFierro")) ? dr.GetString(dr.GetOrdinal("IDFierro")) : string.Empty;
                    datos.NombreFierro = !dr.IsDBNull(dr.GetOrdinal("NombreFierro")) ? dr.GetString(dr.GetOrdinal("NombreFierro")) : string.Empty;
                    datos.Observaciones = !dr.IsDBNull(dr.GetOrdinal("Observaciones")) ? dr.GetString(dr.GetOrdinal("Observaciones")) : string.Empty;
                    datos.ImgFierro = !dr.IsDBNull(dr.GetOrdinal("ImgFierro")) ? dr.GetString(dr.GetOrdinal("ImgFierro")) : string.Empty;
                    Bitmap bmpFromString = datos.ImgFierro.Base64StringToBitmap();
                    datos.ImagenContruida = bmpFromString.ToBase64ImageTag(ImageFormat.Png);
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}