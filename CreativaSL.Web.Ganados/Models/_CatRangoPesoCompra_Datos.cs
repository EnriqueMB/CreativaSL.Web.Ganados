﻿using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatRangoPesoCompra_Datos
    {
        public CatRangoPesoCompraModels ObtenerListaRangoPeso(CatRangoPesoCompraModels datos)
        {
            try
            {
                List<CatRangoPesoCompraModels> Lista = new List<CatRangoPesoCompraModels>();
                CatRangoPesoCompraModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatRangoPesoCompra");
                while (dr.Read())
                {
                    Item = new CatRangoPesoCompraModels();
                    Item.IDRango = !dr.IsDBNull(dr.GetOrdinal("IDRango")) ? dr.GetInt16(dr.GetOrdinal("IDRango")) : 0;
                    Item.EsMacho = !dr.IsDBNull(dr.GetOrdinal("EsMacho")) ? dr.GetBoolean(dr.GetOrdinal("EsMacho")) : false;
                    Item.PesoMinimo = !dr.IsDBNull(dr.GetOrdinal("PesoMinimo")) ? dr.GetDecimal(dr.GetOrdinal("PesoMinimo")) : 0;
                    Item.PesoMaximo = !dr.IsDBNull(dr.GetOrdinal("PesoMaximo")) ? dr.GetDecimal(dr.GetOrdinal("PesoMaximo")) : 0;
                    Item.Precio = !dr.IsDBNull(dr.GetOrdinal("Precio")) ? dr.GetDecimal(dr.GetOrdinal("Precio")) : 0;
                    Item.TipoProveedor = !dr.IsDBNull(dr.GetOrdinal("TipoProveedor")) ? dr.GetString(dr.GetOrdinal("TipoProveedor")) : string.Empty;
                    Item.NombreRango = !dr.IsDBNull(dr.GetOrdinal("NombreRango")) ? dr.GetString(dr.GetOrdinal("NombreRango")) : string.Empty;
                    Lista.Add(Item);
                }
                dr.Close();
                datos.ListaRangoPeso = Lista;
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatRangoPesoCompraModels AbcCatRangoPesoCompra(CatRangoPesoCompraModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.Opcion, datos.IDRango, datos.EsMacho, datos.PesoMinimo, datos.PesoMaximo, datos.Precio, datos.IDTipoProveedor, datos.Usuario, datos.NombreRango ?? string.Empty
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatRangoPesoCompra", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            datos.Completado = true;
                            datos.IDRango = IDRegistro;
                        }
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatRangoPesoCompraModels EliminarRangoPesoCompra(CatRangoPesoCompraModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDRango, datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatRangoPesoCompra", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            datos.Completado = true;
                            datos.IDRango = IDRegistro;
                        }
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatRangoPesoCompraModels ObtenerDetalleCatRangoPesoCompra(CatRangoPesoCompraModels datos)
        {
            try
            {
                object[] parametros = { datos.IDRango };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatRangoPesoCompraXID", parametros);
                while (dr.Read())
                {
                    datos.IDRango = !dr.IsDBNull(dr.GetOrdinal("IDRango")) ? dr.GetInt16(dr.GetOrdinal("IDRango")) : 0;
                    datos.EsMacho = !dr.IsDBNull(dr.GetOrdinal("EsMacho")) ? dr.GetBoolean(dr.GetOrdinal("EsMacho")) : false;
                    datos.PesoMinimo = !dr.IsDBNull(dr.GetOrdinal("PesoMinimo")) ? dr.GetDecimal(dr.GetOrdinal("PesoMinimo")) : 0;
                    datos.PesoMaximo = !dr.IsDBNull(dr.GetOrdinal("PesoMaximo")) ? dr.GetDecimal(dr.GetOrdinal("PesoMaximo")) : 0;
                    datos.Precio = !dr.IsDBNull(dr.GetOrdinal("Precio")) ? dr.GetDecimal(dr.GetOrdinal("Precio")) : 0;
                    datos.IDTipoProveedor = !dr.IsDBNull(dr.GetOrdinal("IDTipoProveedor")) ? dr.GetInt32(dr.GetOrdinal("IDTipoProveedor")) : 0;
                    datos.NombreRango = !dr.IsDBNull(dr.GetOrdinal("NombreRango")) ? dr.GetString(dr.GetOrdinal("NombreRango")) : string.Empty;
                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatTipoProveedorModels> ObetenerListaTipoProveedor(CatRangoPesoCompraModels Datos)
        {
            try
            {
                List<CatTipoProveedorModels> lista = new List<CatTipoProveedorModels>();
                CatTipoProveedorModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatTipoProveedor");
                while (dr.Read())
                {
                    item = new CatTipoProveedorModels();
                    item.IDTipoProveedor = !dr.IsDBNull(dr.GetOrdinal("id_tipoProveedor")) ? dr.GetInt32(dr.GetOrdinal("id_tipoProveedor")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
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