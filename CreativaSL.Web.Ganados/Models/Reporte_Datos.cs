﻿using CreativaSL.Web.Ganados.ViewModels;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class Reporte_Datos
    {
        public DatosEmpresaViewModels ObtenerDatosEmpresaTipo1(string Cadena)
        {
            try
            {
                DatosEmpresaViewModels Datos = new DatosEmpresaViewModels();
                SqlDataReader Dr = SqlHelper.ExecuteReader(Cadena, "spCSLDB_EMPRESA_get_CatEmpresasIDTIPO1");
                while (Dr.Read())
                {
                    Datos.LogoEmpresa = !Dr.IsDBNull(Dr.GetOrdinal("LogoEmpresa")) ? Dr.GetString(Dr.GetOrdinal("LogoEmpresa")) : string.Empty;
                    Datos.RazonFiscal = !Dr.IsDBNull(Dr.GetOrdinal("RazonFiscal")) ? Dr.GetString(Dr.GetOrdinal("RazonFiscal")) : string.Empty;
                    Datos.DireccionFiscal = !Dr.IsDBNull(Dr.GetOrdinal("DireccionFiscal")) ? Dr.GetString(Dr.GetOrdinal("DireccionFiscal")) : string.Empty;
                    Datos.RFC = !Dr.IsDBNull(Dr.GetOrdinal("RFC")) ? Dr.GetString(Dr.GetOrdinal("RFC")) : string.Empty;
                    Datos.Representante = !Dr.IsDBNull(Dr.GetOrdinal("Representante")) ? Dr.GetString(Dr.GetOrdinal("Representante")) : string.Empty;
                    Datos.NumTelefonico1 = !Dr.IsDBNull(Dr.GetOrdinal("NumTelefono1")) ? Dr.GetString(Dr.GetOrdinal("NumTelefono1")) : string.Empty;
                    Datos.NumTelefonico2 = !Dr.IsDBNull(Dr.GetOrdinal("NumTelefono2")) ? Dr.GetString(Dr.GetOrdinal("NumTelefono2")) : string.Empty;
                    Datos.Email = !Dr.IsDBNull(Dr.GetOrdinal("Correo")) ? Dr.GetString(Dr.GetOrdinal("Correo")) : string.Empty;
                    Datos.HorarioAtencion = !Dr.IsDBNull(Dr.GetOrdinal("HorarioAtencion")) ? Dr.GetString(Dr.GetOrdinal("HorarioAtencion")) : string.Empty;
                    Datos.NombreSucursal = !Dr.IsDBNull(Dr.GetOrdinal("NombreSucursal")) ? Dr.GetString(Dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    //Bitmap bmpFromString = Datos.DatosEmpresa.LogoEmpresa.Base64StringToBitmap();
                    //Datos.DatosEmpresa.ImagenContruida = bmpFromString.ToBase64ImageReport(ImageFormat.Png);
                }
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RptProvedorVendioMasModels> obtenerListaProveedoresMermaAlta(RptProvedorVendioMasModels Datos)
        {
            try
            {
                object[] parametros = { Datos.FechaInicio, Datos.FechaFin };
                List<RptProvedorVendioMasModels> lista = new List<RptProvedorVendioMasModels>();
                RptProvedorVendioMasModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Reporte_get_ProveedoresVendioMasGanado", parametros);
                while (dr.Read())
                {
                    item = new RptProvedorVendioMasModels();
                    item.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("id_proveedor")) ? dr.GetString(dr.GetOrdinal("id_proveedor")) : string.Empty;
                    item.NombreProveedor = !dr.IsDBNull(dr.GetOrdinal("nombreRazonSocial")) ? dr.GetString(dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                    item.NombreCompra = !dr.IsDBNull(dr.GetOrdinal("Compra")) ? dr.GetString(dr.GetOrdinal("Compra")) : string.Empty;
                    item.GanadoCompradoMachos = !dr.IsDBNull(dr.GetOrdinal("GanadoCompradoMacho")) ? dr.GetInt32(dr.GetOrdinal("GanadoCompradoMacho")) : 0;
                    item.GanadoCompradoHembra = !dr.IsDBNull(dr.GetOrdinal("GanadoCompradoHembras")) ? dr.GetInt32(dr.GetOrdinal("GanadoCompradoHembras")) : 0;
                    item.TotalGanado = !dr.IsDBNull(dr.GetOrdinal("TotalGanado")) ? dr.GetInt32(dr.GetOrdinal("TotalGanado")) : 0;
                    item.PrecioGanado = !dr.IsDBNull(dr.GetOrdinal("PrecioGanado")) ? dr.GetDecimal(dr.GetOrdinal("PrecioGanado")) : 0;
                    lista.Add(item);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RptSalidaModels> obtenerListaSalidas(RptSalidaModels Datos)
        {
            try
            {
                object[] parametros = { Datos.FechaInicio, Datos.FechaFin };
                List<RptSalidaModels> lista = new List<RptSalidaModels>();
                RptSalidaModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Reportes_get_ReporteSalidas", parametros);
                while (dr.Read())
                {
                    item = new RptSalidaModels();
                    item.Categoria = !dr.IsDBNull(dr.GetOrdinal("Categoria")) ? dr.GetString(dr.GetOrdinal("Categoria")) : string.Empty;
                    item.SubCategoria = !dr.IsDBNull(dr.GetOrdinal("SubCategoria")) ? dr.GetString(dr.GetOrdinal("SubCategoria")) : string.Empty;
                    item.Fecha = !dr.IsDBNull(dr.GetOrdinal("fecha")) ? dr.GetDateTime(dr.GetOrdinal("fecha")) : DateTime.Today;
                    item.Total = !dr.IsDBNull(dr.GetOrdinal("Total")) ? dr.GetDecimal(dr.GetOrdinal("Total")) : 0;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RptSociosModels> ObtenerSocios(RptSalidaModels Datos)
        {
            try
            {
                List<RptSociosModels> Lista = new List<RptSociosModels>();
                RptSociosModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Reporte_get_Socios");
                while (dr.Read())
                {
                    item = new RptSociosModels();
                    item.NombreCompleto = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty;
                    item.Porcentaje = !dr.IsDBNull(dr.GetOrdinal("Porcentaje")) ? dr.GetInt32(dr.GetOrdinal("Porcentaje")) : 0;
                    item.Total = !dr.IsDBNull(dr.GetOrdinal("Total")) ? dr.GetInt32(dr.GetOrdinal("Total")) : 0;
                    Lista.Add(item);
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public List<RptGandosModels> obtenerListaGanadosVendidos(RptGandosModels datos)
        {
            try
            {
                object[] parametros = { datos.FechaInicio, datos.FechaFin };
                List<RptGandosModels> lista = new List<RptGandosModels>();
                RptGandosModels item;
                DataSet ds = null;
                ds = SqlHelper.ExecuteDataset(datos.Conexion, "spCSLDB_Reporte_get_GanadosXVenta", parametros);
                if (ds != null)
                {
                    bool ban = false;
                    DataTableReader dr = ds.Tables[0].CreateDataReader();
                    DataTableReader dr1 = ds.Tables[1].CreateDataReader();
                    while (dr.Read())
                    {
                        item = new RptGandosModels();
                        item.NoArete = !dr.IsDBNull(dr.GetOrdinal("numArete")) ? dr.GetString(dr.GetOrdinal("numArete")) : string.Empty;
                        item.Genero = !dr.IsDBNull(dr.GetOrdinal("genero")) ? dr.GetString(dr.GetOrdinal("genero")) : string.Empty;
                        item.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetInt64(dr.GetOrdinal("folio")) : 0;
                        item.FechahoraVenta = !dr.IsDBNull(dr.GetOrdinal("fechaHoraVenta")) ? dr.GetDateTime(dr.GetOrdinal("fechaHoraVenta")) : DateTime.Today;
                        item.MontoTotal = !dr.IsDBNull(dr.GetOrdinal("montoTotal")) ? dr.GetDecimal(dr.GetOrdinal("montoTotal")) : 0;
                        if(ban == false)
                        {
                            while(dr1.Read())
                            {
                                item.GanadosMachos = !dr1.IsDBNull(dr1.GetOrdinal("MACHOS")) ? dr1.GetInt32(dr1.GetOrdinal("MACHOS")) : 0;
                                item.GanadosHembras = !dr1.IsDBNull(dr1.GetOrdinal("HEMBRAS")) ? dr1.GetInt32(dr1.GetOrdinal("HEMBRAS")) : 0;
                                item.GanadosTotal = !dr1.IsDBNull(dr1.GetOrdinal("TOTAL")) ? dr1.GetInt32(dr1.GetOrdinal("TOTAL")) : 0;
                                break;
                            }
                            ban = true;
                        }
                        lista.Add(item);
                    }
                    dr.Close();
                    dr1.Close();
                    //while (dr1.Read())
                    //{
                    //    datos.GanadosMachos = !dr1.IsDBNull(dr1.GetOrdinal("MACHOS")) ? dr1.GetInt32(dr1.GetOrdinal("MACHOS")) : 0;
                    //    datos.GanadosHembras = !dr1.IsDBNull(dr1.GetOrdinal("HEMBRAS")) ? dr1.GetInt32(dr1.GetOrdinal("HEMBRAS")) : 0;
                    //    datos.GanadosTotal = !dr1.IsDBNull(dr1.GetOrdinal("TOTAL")) ? dr1.GetInt32(dr1.GetOrdinal("TOTAL")) : 0;
                    //    //lista.Add(datos);
                    //    break;
                    //}
                   // datos.listaGanadosVendidos = lista;
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}