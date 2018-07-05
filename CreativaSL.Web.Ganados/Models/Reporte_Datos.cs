using CreativaSL.Web.Ganados.ViewModels;
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
                Dr.Close();
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
                dr.Close();
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
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RptSociosModels> ObtenerSocios(RptSociosModels Datos)
        {
            try
            {
                object[] parametros = { Datos.FechaInicio, Datos.FechaFin };
                List<RptSociosModels> Lista = new List<RptSociosModels>();
                RptSociosModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Reporte_get_Socios", parametros);
                while (dr.Read())
                {
                    item = new RptSociosModels();
                    item.NombreCompleto = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty;
                    item.Porcentaje = !dr.IsDBNull(dr.GetOrdinal("Porcentaje")) ? dr.GetInt32(dr.GetOrdinal("Porcentaje")) : 0;
                    item.Total = !dr.IsDBNull(dr.GetOrdinal("Total")) ? dr.GetDecimal(dr.GetOrdinal("Total")) : 0;
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
                List<RptGandosModels> lista2 = new List<RptGandosModels>();
                RptGandosModels item;
                RptGandosModels item2;
                DataSet ds = null;
                ds = SqlHelper.ExecuteDataset(datos.Conexion, "spCSLDB_Reporte_get_GanadosXVenta", parametros);
                if (ds != null)
                {
                    DataTableReader dr = ds.Tables[0].CreateDataReader();
                    DataTableReader dr1 = ds.Tables[1].CreateDataReader();
                    while (dr.Read())
                    {
                        item = new RptGandosModels();
                        item.NoArete = !dr.IsDBNull(dr.GetOrdinal("numArete")) ? dr.GetString(dr.GetOrdinal("numArete")) : string.Empty;
                        item.Cliente = !dr.IsDBNull(dr.GetOrdinal("nombreContacto")) ? dr.GetString(dr.GetOrdinal("nombreContacto")) : string.Empty;
                        item.Genero = !dr.IsDBNull(dr.GetOrdinal("genero")) ? dr.GetString(dr.GetOrdinal("genero")) : string.Empty;
                        item.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetInt64(dr.GetOrdinal("folio")) : 0;
                        item.FechahoraVenta = !dr.IsDBNull(dr.GetOrdinal("fechaHoraVenta")) ? dr.GetDateTime(dr.GetOrdinal("fechaHoraVenta")) : DateTime.Today;
                        item.MontoTotal = !dr.IsDBNull(dr.GetOrdinal("montoTotal")) ? dr.GetDecimal(dr.GetOrdinal("montoTotal")) : 0;
                        lista.Add(item);
                    }

                    while (dr1.Read())
                    {
                        item2 = new RptGandosModels();
                        item2.GanadosMachos = !dr1.IsDBNull(dr1.GetOrdinal("MACHOS")) ? dr1.GetInt32(dr1.GetOrdinal("MACHOS")) : 0;
                        item2.GanadosHembras = !dr1.IsDBNull(dr1.GetOrdinal("HEMBRAS")) ? dr1.GetInt32(dr1.GetOrdinal("HEMBRAS")) : 0;
                        item2.GanadosTotal = !dr1.IsDBNull(dr1.GetOrdinal("TOTAL")) ? dr1.GetInt32(dr1.GetOrdinal("TOTAL")) : 0;
                        lista2.Add(item2);
                        break;
                    }
                    dr.Close();
                    dr1.Close();
                    
                }
                datos.listaGanadosTotal = lista2;
                return datos.listaGanadosVendidos = lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<RptGanadosMtoVentaModels> obtenerListaGanadosMtoVenta (RptGanadosMtoVentaModels datos)
        {
            try
            {
                object[] parametros = { datos.fechaInicio, datos.fechaFin };
                List<RptGanadosMtoVentaModels> lista = new List<RptGanadosMtoVentaModels>();
                List<RptGanadosMtoVentaModels> lista2 = new List<RptGanadosMtoVentaModels>();
                RptGanadosMtoVentaModels item;
                RptGanadosMtoVentaModels item2;
                DataSet ds = null;
                ds = SqlHelper.ExecuteDataset(datos.Conexion, "spCSLDB_Reporte_get_GanadosMtoVenta", parametros);
                if (ds != null)
                {
                    DataTableReader dr = ds.Tables[0].CreateDataReader();
                    DataTableReader dr2 = ds.Tables[1].CreateDataReader();
                    while (dr.Read())
                    {
                        item = new RptGanadosMtoVentaModels();
                        item.cliente = !dr.IsDBNull(dr.GetOrdinal("nombreContacto")) ? dr.GetString(dr.GetOrdinal("nombreContacto")) : string.Empty;
                        item.numArete = !dr.IsDBNull(dr.GetOrdinal("numArete")) ? dr.GetString(dr.GetOrdinal("numArete")) : string.Empty;
                        item.genero = !dr.IsDBNull(dr.GetOrdinal("genero")) ? dr.GetString(dr.GetOrdinal("genero")) : string.Empty;
                        item.folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetInt64(dr.GetOrdinal("folio")) : 0;
                        item.fechaHoraVenta = !dr.IsDBNull(dr.GetOrdinal("fechaHoraVenta")) ? dr.GetDateTime(dr.GetOrdinal("fechaHoraVenta")) : DateTime.Today;
                        item.montoTotal = !dr.IsDBNull(dr.GetOrdinal("montoTotal")) ? dr.GetDecimal(dr.GetOrdinal("montoTotal")) : 0;
                        lista.Add(item);
                    }

                    while (dr2.Read())
                    {
                        item2 = new RptGanadosMtoVentaModels();
                        item2.totalMachos = !dr2.IsDBNull(dr2.GetOrdinal("MACHOS")) ? dr2.GetInt32(dr2.GetOrdinal("MACHOS")) : 0;
                        item2.totalHembras = !dr2.IsDBNull(dr2.GetOrdinal("HEMBRAS")) ? dr2.GetInt32(dr2.GetOrdinal("HEMBRAS")) : 0;
                        item2.totalGanados = !dr2.IsDBNull(dr2.GetOrdinal("TOTAL")) ? dr2.GetInt32(dr2.GetOrdinal("TOTAL")) : 0;                        
                        lista2.Add(item2);
                    }
                    dr.Close();
                    dr2.Close();
                }
                datos.ListaTotalGanado = lista2;
                return datos.listaGanadosMtoVenta = lista;
            }
            catch (Exception ex)
            {

                throw ex; 
            }
        }

        public List<RptGanadosMtoCompraModels> obtenerListaGanadosMtoCompra(RptGanadosMtoCompraModels datos)
        {
            try
            {
                object[] parametros = { datos.fechaInicio, datos.fechaFin };
                List<RptGanadosMtoCompraModels> lista = new List<RptGanadosMtoCompraModels>();
                List<RptGanadosMtoCompraModels> lista2 = new List<RptGanadosMtoCompraModels>();
                RptGanadosMtoCompraModels item;
                RptGanadosMtoCompraModels item2;
                DataSet ds = null;
                ds = SqlHelper.ExecuteDataset(datos.Conexion, "spCSLDB_Reporte_get_GanadosMtoCompra", parametros);
                if (ds != null)
                {
                    DataTableReader dr = ds.Tables[0].CreateDataReader();
                    DataTableReader dr2 = ds.Tables[1].CreateDataReader();
                    while (dr.Read())
                    {
                        item = new RptGanadosMtoCompraModels();
                        item.proveedor = !dr.IsDBNull(dr.GetOrdinal("proveedor")) ? dr.GetString(dr.GetOrdinal("proveedor")) : string.Empty;
                        item.numArete = !dr.IsDBNull(dr.GetOrdinal("numArete")) ? dr.GetString(dr.GetOrdinal("numArete")) : string.Empty;
                        item.genero = !dr.IsDBNull(dr.GetOrdinal("genero")) ? dr.GetString(dr.GetOrdinal("genero")) : string.Empty;
                        item.folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetInt64(dr.GetOrdinal("folio")) : 0;
                        item.fechaHoraTerminada = !dr.IsDBNull(dr.GetOrdinal("fechaHoraTerminada")) ? dr.GetDateTime(dr.GetOrdinal("fechaHoraTerminada")) : DateTime.Today;
                        //item.fechaHoraProgramada = !dr.IsDBNull(dr.GetOrdinal("fechaHoraProgramada")) ? dr.GetDateTime(dr.GetOrdinal("fechaHoraProgramada")) : DateTime.Today;
                        item.montoTotal = !dr.IsDBNull(dr.GetOrdinal("montoTotalGanado")) ? dr.GetDecimal(dr.GetOrdinal("montoTotalGanado")) : 0;
                        lista.Add(item);
                    }
                    while (dr2.Read())
                    {
                        item2 = new RptGanadosMtoCompraModels();
                        item2.totalMachos = !dr2.IsDBNull(dr2.GetOrdinal("MACHOS")) ? dr2.GetInt32(dr2.GetOrdinal("MACHOS")) : 0;
                        item2.totalHembras = !dr2.IsDBNull(dr2.GetOrdinal("HEMBRAS")) ? dr2.GetInt32(dr2.GetOrdinal("HEMBRAS")) : 0;
                        item2.totalGanados = !dr2.IsDBNull(dr2.GetOrdinal("TOTAL")) ? dr2.GetInt32(dr2.GetOrdinal("TOTAL")) : 0;
                        lista2.Add(item2);
                        break;
                    }
                    dr.Close();
                    dr2.Close();
                }
                datos.listaTotalGanado = lista2;
                return datos.listaGanadosMtoCompra = lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<RptJaulasXVentaModels> obtenerListaJaulasXVenta(RptJaulasXVentaModels datos)
        {
            try
            {
                object[] parametros = { datos.fechaInicio, datos.fechaFin };
                List<RptJaulasXVentaModels> lista = new List<RptJaulasXVentaModels>();
                RptJaulasXVentaModels item;
                SqlDataReader ds = null;
                ds = SqlHelper.ExecuteReader(datos.Conexion,"spCSLDB_Reporte_get_JaulaXVenta", parametros);
                while (ds.Read())
                {
                    item = new RptJaulasXVentaModels();
                    item.chofer = !ds.IsDBNull(ds.GetOrdinal("nombreChofer")) ? ds.GetString(ds.GetOrdinal("nombreChofer")) : string.Empty;
                    item.folio = !ds.IsDBNull(ds.GetOrdinal("folioFlete")) ? ds.GetInt64(ds.GetOrdinal("folioFlete")) : 0;
                    item.descripcion = !ds.IsDBNull(ds.GetOrdinal("Descripcion")) ? ds.GetString(ds.GetOrdinal("Descripcion")) : string.Empty;
                    item.placas = !ds.IsDBNull(ds.GetOrdinal("Placas")) ? ds.GetString(ds.GetOrdinal("Placas")) : string.Empty;
                    item.modelo = !ds.IsDBNull(ds.GetOrdinal("Modelo")) ? ds.GetString(ds.GetOrdinal("Modelo")) : string.Empty;
                    item.noSerie = !ds.IsDBNull(ds.GetOrdinal("NoSerie")) ? ds.GetString(ds.GetOrdinal("NoSerie")) : string.Empty;
                    item.montoTotal = !ds.IsDBNull(ds.GetOrdinal("MontoTotal")) ? ds.GetDecimal(ds.GetOrdinal("MontoTotal")) : 0;
                    item.capacidad = !ds.IsDBNull(ds.GetOrdinal("Capacidad")) ? ds.GetString(ds.GetOrdinal("Capacidad")) : string.Empty;
                    item.totalGanados = !ds.IsDBNull(ds.GetOrdinal("TotalGanado")) ? ds.GetInt32(ds.GetOrdinal("TotalGanado")) : 0;
                    lista.Add(item);
                }
                ds.Close();  
                return datos.listaJaulas = lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<RptEntradaModels> ObtenerEntradas(RptEntradaModels Datos)
        {
            try
            {
                object[] parametros = { Datos.FechaInicio, Datos.FechaFin };
                List<RptEntradaModels> Lista = new List<RptEntradaModels>();
                RptEntradaModels item;
                SqlDataReader ds = null;
                ds = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Reportes_get_ReporteEntradas", parametros);
                while (ds.Read())
                {
                    item = new RptEntradaModels();
                    item.NombreCliente = !ds.IsDBNull(ds.GetOrdinal("NombreCliente")) ? ds.GetString(ds.GetOrdinal("NombreCliente")) : string.Empty;
                    item.Fecha = !ds.IsDBNull(ds.GetOrdinal("Fecha")) ? ds.GetDateTime(ds.GetOrdinal("Fecha")) : DateTime.Today;
                    item.Total = !ds.IsDBNull(ds.GetOrdinal("Total")) ? ds.GetInt32(ds.GetOrdinal("Total")) : 0;
                    item.Descripcion = !ds.IsDBNull(ds.GetOrdinal("Descripcion")) ? ds.GetString(ds.GetOrdinal("Descripcion")) : string.Empty;
                    item.Merma = !ds.IsDBNull(ds.GetOrdinal("Merma")) ? ds.GetDecimal(ds.GetOrdinal("Merma")) : 0;
                    item.Folio = !ds.IsDBNull(ds.GetOrdinal("Folio")) ? ds.GetInt64(ds.GetOrdinal("Folio")) : 0;
                    item.Observaciones = !ds.IsDBNull(ds.GetOrdinal("Observaciones")) ? ds.GetString(ds.GetOrdinal("Observaciones")) : string.Empty;
                    item.ImporteGanado = !ds.IsDBNull(ds.GetOrdinal("ImporteGanado")) ? ds.GetDecimal(ds.GetOrdinal("ImporteGanado")) : 0;
                    item.CostoFlete = !ds.IsDBNull(ds.GetOrdinal("CostoFlete")) ? ds.GetDecimal(ds.GetOrdinal("CostoFlete")) : 0;
                    item.AplicaCobro = !ds.IsDBNull(ds.GetOrdinal("CobroFlete")) ? ds.GetString(ds.GetOrdinal("CobroFlete")) : string.Empty;
                    Lista.Add(item);
                }
                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<RptCorralesModels> ObetenerListaCorrales(RptCorralesModels Datos)
        {
            try
            {
                object[] parametros = { Datos.FechaInicio, Datos.FechaFin };
                List<RptCorralesModels> Lista = new List<RptCorralesModels>();
                RptCorralesModels item;
                SqlDataReader ds = null;
                ds = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Reportes_get_Corrales");
                while (ds.Read())
                {
                    item = new RptCorralesModels();
                    item.Corral = !ds.IsDBNull(ds.GetOrdinal("Corral")) ? ds.GetString(ds.GetOrdinal("Corral")) : string.Empty;
                    item.NumeroFilas = !ds.IsDBNull(ds.GetOrdinal("TotalFilas")) ? ds.GetInt32(ds.GetOrdinal("TotalFilas")) : 0;
                    item.NumeroSerie = !ds.IsDBNull(ds.GetOrdinal("NumeroSerie")) ? ds.GetString(ds.GetOrdinal("NumeroSerie")) : string.Empty;
                    item.NumeroGrande = !ds.IsDBNull(ds.GetOrdinal("NumeroGrande")) ? ds.GetString(ds.GetOrdinal("NumeroGrande")) : string.Empty;
                    item.PesoInicial = !ds.IsDBNull(ds.GetOrdinal("PesoInicial")) ? ds.GetDecimal(ds.GetOrdinal("PesoInicial")) : 0;
                    item.Repeso = !ds.IsDBNull(ds.GetOrdinal("Repeso")) ? ds.GetDecimal(ds.GetOrdinal("Repeso")) : 0;
                    item.PesoPagado = !ds.IsDBNull(ds.GetOrdinal("PesoPagado")) ? ds.GetDecimal(ds.GetOrdinal("PesoPagado")) : 0;
                    item.DiferenciaKG = !ds.IsDBNull(ds.GetOrdinal("DiferenciaKG")) ? ds.GetDecimal(ds.GetOrdinal("DiferenciaKG")) : 0;
                    item.Merma = !ds.IsDBNull(ds.GetOrdinal("Merma")) ? ds.GetDecimal(ds.GetOrdinal("Merma")) : 0;
                    Lista.Add(item);
                }
                return Lista;
            }
            catch (Exception)
            { 
                throw;
            }
        }
    }
}