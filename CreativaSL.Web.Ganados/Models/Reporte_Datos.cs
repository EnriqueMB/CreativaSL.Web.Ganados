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

        public DatosEmpresaViewModels ObtenerDatosEmpresaTipo2(string Cadena)
        {
            try
            {
                DatosEmpresaViewModels Datos = new DatosEmpresaViewModels();
                SqlDataReader Dr = SqlHelper.ExecuteReader(Cadena, "spCSLDB_EMPRESA_get_CatEmpresasIDTIPO2");
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
                    //Datos.NombreSucursal = !Dr.IsDBNull(Dr.GetOrdinal("NombreSucursal")) ? Dr.GetString(Dr.GetOrdinal("NombreSucursal")) : string.Empty;
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

        public DatosEmpresaViewModels ObtenerDatosEmpresaTipoIDSucursal(string Cadena, string IDSucursal)
        {
            try
            {
                DatosEmpresaViewModels Datos = new DatosEmpresaViewModels();
                object[] parametros = { IDSucursal };
                SqlDataReader Dr = SqlHelper.ExecuteReader(Cadena, "[dbo].[spCSLDB_EMPRESA_get_CatEmpresasIDTIPO1_V2]", parametros);
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
                    break;
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
                object[] parametros = { Datos.FechaInicio, Datos.FechaFin, Datos.IdSucursal };
                List<RptProvedorVendioMasModels> lista = new List<RptProvedorVendioMasModels>();
                RptProvedorVendioMasModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Reporte_get_ProveedoresVendioMasGanado", parametros);
                while (dr.Read())
                {
                    item = new RptProvedorVendioMasModels();
                    item.IDProveedor = !dr.IsDBNull(dr.GetOrdinal("id_proveedor")) ? dr.GetString(dr.GetOrdinal("id_proveedor")) : string.Empty;
                    item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    item.NombreProveedor = !dr.IsDBNull(dr.GetOrdinal("nombreRazonSocial")) ? dr.GetString(dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                    item.NombreCompra = !dr.IsDBNull(dr.GetOrdinal("Compra")) ? dr.GetString(dr.GetOrdinal("Compra")) : string.Empty;
                    item.GanadoCompradoMachos = !dr.IsDBNull(dr.GetOrdinal("GanadoCompradoMacho")) ? dr.GetInt32(dr.GetOrdinal("GanadoCompradoMacho")) : 0;
                    item.KilosMachos = !dr.IsDBNull(dr.GetOrdinal("KilosMachos")) ? dr.GetInt32(dr.GetOrdinal("KilosMachos")) : 0;
                    item.GanadoCompradoHembra = !dr.IsDBNull(dr.GetOrdinal("GanadoCompradoHembras")) ? dr.GetInt32(dr.GetOrdinal("GanadoCompradoHembras")) : 0;
                    item.KilosHembra = !dr.IsDBNull(dr.GetOrdinal("KilosHembra")) ? dr.GetInt32(dr.GetOrdinal("KilosHembra")) : 0;
                    item.TotalGanado = !dr.IsDBNull(dr.GetOrdinal("TotalGanado")) ? dr.GetInt32(dr.GetOrdinal("TotalGanado")) : 0;
                    item.TotalKilos = !dr.IsDBNull(dr.GetOrdinal("TotalKilos")) ? dr.GetInt32(dr.GetOrdinal("TotalKilos")) : 0;
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
                object[] parametros = { Datos.FechaInicio, Datos.FechaFin, Datos.IdSucursal };
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
                dr.Close();
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RptMantenimientoVehiculoModels> ListaMantenimiento(RptMantenimientoVehiculoModels Datos)
        {
            try
            {
                object[] Parametros = { Datos.FechaInicio, Datos.FechaFin, Datos.IDSucursal };
                List<RptMantenimientoVehiculoModels> Lista = new List<RptMantenimientoVehiculoModels>();
                RptMantenimientoVehiculoModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Reporte_get_ManteniminetoVehiculo", Parametros);
                while (dr.Read())
                {
                    Item = new RptMantenimientoVehiculoModels();
                    Item.NomSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSuc")) ? dr.GetString(dr.GetOrdinal("NombreSuc")) : string.Empty;
                    Item.Fecha = !dr.IsDBNull(dr.GetOrdinal("Fecha")) ? dr.GetDateTime(dr.GetOrdinal("Fecha")) : DateTime.Today;
                    Item.Vehiculo = !dr.IsDBNull(dr.GetOrdinal("NomVehiculo")) ? dr.GetString(dr.GetOrdinal("NomVehiculo")) : string.Empty;
                    Item.NomProveedor = !dr.IsDBNull(dr.GetOrdinal("nombreRazonSocial")) ? dr.GetString(dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                    Item.Total = !dr.IsDBNull(dr.GetOrdinal("ImporteTotal")) ? dr.GetDecimal(dr.GetOrdinal("ImporteTotal")) : 0;
                    Item.Servicio = !dr.IsDBNull(dr.GetOrdinal("Servicios")) ? dr.GetString(dr.GetOrdinal("Servicios")) : string.Empty;
                    Lista.Add(Item);
                }
                dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<RptRendimientoVehiculoModels> ListaRendimiento(RptRendimientoVehiculoModels Datos)
        {
            try
            {
                object[] Parametros = { Datos.FechaFin, Datos.FechaFin, Datos.IDSucursal };
                List<RptRendimientoVehiculoModels> Lista = new List<RptRendimientoVehiculoModels>();
                RptRendimientoVehiculoModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Reporte_get_RendimientoVehiculo", Parametros);
                while (dr.Read())
                {
                    Item = new RptRendimientoVehiculoModels();
                    Item.NomSucursal = !dr.IsDBNull(dr.GetOrdinal("Sucursal")) ? dr.GetString(dr.GetOrdinal("Sucursal")) : string.Empty;
                    Item.NomVehiculo = !dr.IsDBNull(dr.GetOrdinal("Vehiculo")) ? dr.GetString(dr.GetOrdinal("Vehiculo")) : string.Empty;
                    Item.Fecha = !dr.IsDBNull(dr.GetOrdinal("Fecha")) ? dr.GetDateTime(dr.GetOrdinal("Fecha")) : DateTime.Today;
                    Item.NomProveedor = !dr.IsDBNull(dr.GetOrdinal("Proveedor")) ? dr.GetString(dr.GetOrdinal("Proveedor")) : string.Empty;
                    Item.NoTicket = !dr.IsDBNull(dr.GetOrdinal("NoTicket")) ? dr.GetString(dr.GetOrdinal("NoTicket")) : string.Empty;
                    Item.Litros = !dr.IsDBNull(dr.GetOrdinal("Litros")) ? dr.GetDecimal(dr.GetOrdinal("Litros")) : 0;
                    Item.KmInicial = !dr.IsDBNull(dr.GetOrdinal("KmInicial")) ? dr.GetInt32(dr.GetOrdinal("KmInicial")) : 0;
                    Item.KmFinal = !dr.IsDBNull(dr.GetOrdinal("KmFinal")) ? dr.GetInt32(dr.GetOrdinal("KmFinal")) : 0;
                    Item.Rendimiento = !dr.IsDBNull(dr.GetOrdinal("Rendimiento")) ? dr.GetDecimal(dr.GetOrdinal("Rendimiento")) : 0;
                    Item.PrecioLitro = !dr.IsDBNull(dr.GetOrdinal("PrecioLitro")) ? dr.GetDecimal(dr.GetOrdinal("PrecioLitro")) : 0;
                    Item.TotalCompra = !dr.IsDBNull(dr.GetOrdinal("Total")) ? dr.GetDecimal(dr.GetOrdinal("Total")) : 0;
                    Item.TipoCombustible = !dr.IsDBNull(dr.GetOrdinal("TipoCombustible")) ? dr.GetString(dr.GetOrdinal("TipoCombustible")) : string.Empty;
                }
                dr.Close();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }


        #region REPORTE DE ANALISIS DE VENTA

        public List<RptGandosModels> obtenerListaGanadosVendidos(RptGandosModels datos)
        {
            try
            {
                object[] parametros = { datos.FechaInicio, datos.FechaFin, datos.IDSucursal };
                List<RptGandosModels> lista = new List<RptGandosModels>();
                RptGandosModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Reporte_get_GanadosXVenta", parametros);
                while (dr.Read())
                {
                    item = new RptGandosModels();
                    item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NomSucursal")) ? dr.GetString(dr.GetOrdinal("NomSucursal")) : string.Empty;
                    item.FechahoraVenta = !dr.IsDBNull(dr.GetOrdinal("FechaVenta")) ? dr.GetDateTime(dr.GetOrdinal("FechaVenta")) : DateTime.Today;
                    item.Cliente = !dr.IsDBNull(dr.GetOrdinal("NomCliente")) ? dr.GetString(dr.GetOrdinal("NomCliente")) : string.Empty;
                    item.Folio = !dr.IsDBNull(dr.GetOrdinal("NumFolio")) ? dr.GetInt64(dr.GetOrdinal("NumFolio")) : 0;
                    item.GanadosMachos = !dr.IsDBNull(dr.GetOrdinal("CABEZAMACHOS")) ? dr.GetInt32(dr.GetOrdinal("CABEZAMACHOS")) : 0;
                    item.GanadosHembras = !dr.IsDBNull(dr.GetOrdinal("CABEZAHEMBRAS")) ? dr.GetInt32(dr.GetOrdinal("CABEZAHEMBRAS")) : 0;
                    item.GanadosTotal = !dr.IsDBNull(dr.GetOrdinal("CABEZASTOTAL")) ? dr.GetInt32(dr.GetOrdinal("CABEZASTOTAL")) : 0;
                    item.KilosMachos = !dr.IsDBNull(dr.GetOrdinal("PESOMACHOS")) ? dr.GetInt32(dr.GetOrdinal("PESOMACHOS")) : 0;
                    item.KiloHembra = !dr.IsDBNull(dr.GetOrdinal("PESOHEMBRAS")) ? dr.GetInt32(dr.GetOrdinal("PESOHEMBRAS")) : 0;
                    item.TotalKilos = !dr.IsDBNull(dr.GetOrdinal("TOTALPESO")) ? dr.GetInt32(dr.GetOrdinal("TOTALPESO")) : 0;
                    item.MontoTotal = !dr.IsDBNull(dr.GetOrdinal("montoTotal")) ? dr.GetDecimal(dr.GetOrdinal("montoTotal")) : 0;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RptGandosModels> ListaGanadosVendidosGraficaGeneroKG(RptGandosModels datos)
        {
            try
            {
                object[] parametros = { datos.FechaInicio, datos.FechaFin, datos.IDSucursal };
                List<RptGandosModels> lista = new List<RptGandosModels>();
                RptGandosModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Reporte_get_GanadosXVentaGraficaKGGenero", parametros);
                while (dr.Read())
                {
                    item = new RptGandosModels();
                    item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NomSucursal")) ? dr.GetString(dr.GetOrdinal("NomSucursal")) : string.Empty;
                    item.GanadosMachos = !dr.IsDBNull(dr.GetOrdinal("GANADOMACHOS")) ? dr.GetInt32(dr.GetOrdinal("GANADOMACHOS")) : 0;
                    item.GanadosHembras = !dr.IsDBNull(dr.GetOrdinal("GANADOHEMBRAS")) ? dr.GetInt32(dr.GetOrdinal("GANADOHEMBRAS")) : 0;
                    item.GanadosTotal = !dr.IsDBNull(dr.GetOrdinal("TOTALGENERO")) ? dr.GetInt32(dr.GetOrdinal("TOTALGENERO")) : 0;
                    item.KilosMachos = !dr.IsDBNull(dr.GetOrdinal("PESOMACHOS")) ? dr.GetInt32(dr.GetOrdinal("PESOMACHOS")) : 0;
                    item.KiloHembra = !dr.IsDBNull(dr.GetOrdinal("PESOHEMBRAS")) ? dr.GetInt32(dr.GetOrdinal("PESOHEMBRAS")) : 0;
                    item.TotalKilos = !dr.IsDBNull(dr.GetOrdinal("TOTALPESO")) ? dr.GetInt32(dr.GetOrdinal("TOTALPESO")) : 0;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public List<RptGanadosMtoVentaModels> obtenerListaGanadosMtoVenta(RptGanadosMtoVentaModels datos)
        {
            try
            {
                object[] Parametros = { datos.fechaInicio, datos.fechaFin, datos.IdSucursal };
                List<RptGanadosMtoVentaModels> lista = new List<RptGanadosMtoVentaModels>();
                RptGanadosMtoVentaModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Reporte_get_GanadosMtoVenta", Parametros);
                while(dr.Read())
                {
                    item = new RptGanadosMtoVentaModels();
                    item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NomSucursal")) ? dr.GetString(dr.GetOrdinal("NomSucursal")) : string.Empty;
                    item.genero = !dr.IsDBNull(dr.GetOrdinal("genero")) ? dr.GetString(dr.GetOrdinal("genero")) : string.Empty;
                    item.cliente = !dr.IsDBNull(dr.GetOrdinal("ProveedorOCliente")) ? dr.GetString(dr.GetOrdinal("ProveedorOCliente")) : string.Empty;
                    item.fechaHoraVenta = !dr.IsDBNull(dr.GetOrdinal("FechaVentaCompra")) ? dr.GetDateTime(dr.GetOrdinal("FechaVentaCompra")) : DateTime.Today;
                    item.FolioVC = !dr.IsDBNull(dr.GetOrdinal("Folio")) ? dr.GetString(dr.GetOrdinal("Folio")) : string.Empty;
                    item.numArete = !dr.IsDBNull(dr.GetOrdinal("numArete")) ? dr.GetString(dr.GetOrdinal("numArete")) : string.Empty;
                    item.KGCabeza = !dr.IsDBNull(dr.GetOrdinal("Peso")) ? dr.GetInt32(dr.GetOrdinal("Peso")) : 0;
                    item.TipoEvento = !dr.IsDBNull(dr.GetOrdinal("TipoEvento")) ? dr.GetString(dr.GetOrdinal("TipoEvento")) : string.Empty;
                    item.montoTotal = !dr.IsDBNull(dr.GetOrdinal("MontoEstimado")) ? dr.GetDecimal(dr.GetOrdinal("MontoEstimado")) : 0;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public List<RptGanadosMtoVentaModels> obtenerListaGanadosMtoVenta (RptGanadosMtoVentaModels datos)
        //{
        //    try
        //    {
        //        object[] parametros = { datos.fechaInicio, datos.fechaFin, datos.IdSucursal};
        //        List<RptGanadosMtoVentaModels> lista = new List<RptGanadosMtoVentaModels>();
        //        //List<RptGanadosMtoVentaModels> lista2 = new List<RptGanadosMtoVentaModels>();
        //        RptGanadosMtoVentaModels item;
        //        //RptGanadosMtoVentaModels item2;
        //        DataSet ds = null;
        //        ds = SqlHelper.ExecuteDataset(datos.Conexion, "spCSLDB_Reporte_get_GanadosMtoVenta", parametros);
        //        if (ds != null)
        //        {
        //            DataTableReader dr = ds.Tables[0].CreateDataReader();
        //            DataTableReader dr2 = ds.Tables[1].CreateDataReader();
        //            while (dr.Read())
        //            {
        //                item = new RptGanadosMtoVentaModels();
        //                item.cliente = !dr.IsDBNull(dr.GetOrdinal("nombreContacto")) ? dr.GetString(dr.GetOrdinal("nombreContacto")) : string.Empty;
        //                item.numArete = !dr.IsDBNull(dr.GetOrdinal("numArete")) ? dr.GetString(dr.GetOrdinal("numArete")) : string.Empty;
        //                item.genero = !dr.IsDBNull(dr.GetOrdinal("genero")) ? dr.GetString(dr.GetOrdinal("genero")) : string.Empty;
        //                item.folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetInt64(dr.GetOrdinal("folio")) : 0;
        //                item.fechaHoraVenta = !dr.IsDBNull(dr.GetOrdinal("fechaHoraVenta")) ? dr.GetDateTime(dr.GetOrdinal("fechaHoraVenta")) : DateTime.Today;
        //                item.montoTotal = !dr.IsDBNull(dr.GetOrdinal("montoTotal")) ? dr.GetDecimal(dr.GetOrdinal("montoTotal")) : 0;
        //                lista.Add(item);
        //            }

        //            //while (dr2.Read())
        //            //{
        //            //    item2 = new RptGanadosMtoVentaModels();
        //            //    item2.totalMachos = !dr2.IsDBNull(dr2.GetOrdinal("MACHOS")) ? dr2.GetInt32(dr2.GetOrdinal("MACHOS")) : 0;
        //            //    item2.totalHembras = !dr2.IsDBNull(dr2.GetOrdinal("HEMBRAS")) ? dr2.GetInt32(dr2.GetOrdinal("HEMBRAS")) : 0;
        //            //    item2.totalGanados = !dr2.IsDBNull(dr2.GetOrdinal("TOTAL")) ? dr2.GetInt32(dr2.GetOrdinal("TOTAL")) : 0;                        
        //            //    lista2.Add(item2);
        //            //}
        //            dr.Close();
        //           //dr2.Close();
        //        }
        //       // datos.ListaTotalGanado = lista2;
        //        return lista;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex; 
        //    }
        //}

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
                    item.cliente = !ds.IsDBNull(ds.GetOrdinal("Cliente")) ? ds.GetString(ds.GetOrdinal("Cliente")) : string.Empty;
                    item.machos = !ds.IsDBNull(ds.GetOrdinal("Machos")) ? ds.GetInt32(ds.GetOrdinal("Machos")) : 0;
                    item.hembras = !ds.IsDBNull(ds.GetOrdinal("Hembras")) ? ds.GetInt32(ds.GetOrdinal("Hembras")) : 0;
                    item.merma = !ds.IsDBNull(ds.GetOrdinal("merma")) ? ds.GetDecimal(ds.GetOrdinal("merma")) : 0;
                    item.pesoTotal = !ds.IsDBNull(ds.GetOrdinal("pesoTotalEnviado")) ? ds.GetDecimal(ds.GetOrdinal("pesoTotalEnviado")) : 0;
                    item.color = !ds.IsDBNull(ds.GetOrdinal("Color")) ? ds.GetString(ds.GetOrdinal("Color")) : string.Empty;
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
                ds.Close();
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
                ds.Close();
                return Lista;
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public List<RptFletesModels> ObtenerListaFletes(RptFletesModels datos)
        {
            try
            {
                object[] parametros = { datos.fechaInicio, datos.fechaFin,datos.id_sucursal };
                List<RptFletesModels> lista = new List<RptFletesModels>();
                RptFletesModels item;
                SqlDataReader ds = null;
                ds = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Reporte_get_ReporteFletes", parametros);
                while (ds.Read())
                {
                    item = new RptFletesModels();
                    item.chofer = !ds.IsDBNull(ds.GetOrdinal("Chofer")) ? ds.GetString(ds.GetOrdinal("Chofer")) : string.Empty;
                    //item.folio = !ds.IsDBNull(ds.GetOrdinal("Folio")) ? ds.GetInt64(ds.GetOrdinal("Folio")) : 0;
                    item.cliente = !ds.IsDBNull(ds.GetOrdinal("Cliente")) ? ds.GetString(ds.GetOrdinal("Cliente")) : string.Empty;
                    item.vehiculo = !ds.IsDBNull(ds.GetOrdinal("Vehiculo")) ? ds.GetString(ds.GetOrdinal("Vehiculo")) : string.Empty;
                    //item.modelo = !ds.IsDBNull(ds.GetOrdinal("Modelo")) ? ds.GetString(ds.GetOrdinal("Modelo")) : string.Empty;
                    //item.placas = !ds.IsDBNull(ds.GetOrdinal("Placas")) ? ds.GetString(ds.GetOrdinal("Placas")) : string.Empty;
                    //item.noSerie = !ds.IsDBNull(ds.GetOrdinal("noSerie")) ? ds.GetString(ds.GetOrdinal("noSerie")) : string.Empty;
                    //item.empresaOrigen = !ds.IsDBNull(ds.GetOrdinal("EmpresaOrigen")) ? ds.GetString(ds.GetOrdinal("EmpresaOrigen")) : string.Empty;
                    //item.empresaDestino = !ds.IsDBNull(ds.GetOrdinal("EmpresaDestino")) ? ds.GetString(ds.GetOrdinal("EmpresaDestino")) : string.Empty;
                    item.lugarOrigen = !ds.IsDBNull(ds.GetOrdinal("LugarOrigen")) ? ds.GetString(ds.GetOrdinal("LugarOrigen")) : string.Empty;
                    item.lugarDestino = !ds.IsDBNull(ds.GetOrdinal("LugarDestino")) ? ds.GetString(ds.GetOrdinal("LugarDestino")) : string.Empty;
                    //item.impuestoTrasladado = !ds.IsDBNull(ds.GetOrdinal("ImpuestoTrasladado")) ? ds.GetDecimal(ds.GetOrdinal("ImpuestoTrasladado")) : 0;
                    //item.impuestoRetenido = !ds.IsDBNull(ds.GetOrdinal("ImpuestoRetenido")) ? ds.GetDecimal(ds.GetOrdinal("ImpuestoRetenido")) : 0;
                    item.importeFlete = !ds.IsDBNull(ds.GetOrdinal("ImporteFlete")) ? ds.GetDecimal(ds.GetOrdinal("ImporteFlete")) : 0;
                    item.total = !ds.IsDBNull(ds.GetOrdinal("Total")) ? ds.GetDecimal(ds.GetOrdinal("Total")) : 0;
                    lista.Add(item);
                }
                ds.Close();
                return datos.listaFletes = lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<RptEstadoCuentaProveedorModels> ObtenerListaEstadoCuentaProveedor(RptEstadoCuentaProveedorModels datos)
        {
            try
            {
                List<RptEstadoCuentaProveedorModels> lista = new List<RptEstadoCuentaProveedorModels>();
                RptEstadoCuentaProveedorModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Reporte_get_EstadoCuentaProveedor", datos.fechaInicio, datos.fechaFin);
                if(dr != null)
                {
                    while(dr.Read())
                    {
                        item = new RptEstadoCuentaProveedorModels();
                        item.folio = !dr.IsDBNull(dr.GetOrdinal("Folio")) ? dr.GetInt64(dr.GetOrdinal("Folio")) : 0;
                        item.tipoProveedor = !dr.IsDBNull(dr.GetOrdinal("TipoProveedor")) ? dr.GetString(dr.GetOrdinal("TipoProveedor")) : string.Empty;
                        item.nombreProveedor = !dr.IsDBNull(dr.GetOrdinal("NombreProveedor")) ? dr.GetString(dr.GetOrdinal("NombreProveedor")) : string.Empty;
                        item.total = !dr.IsDBNull(dr.GetOrdinal("Total")) ? dr.GetDecimal(dr.GetOrdinal("Total")) : 0;
                        item.pagos = !dr.IsDBNull(dr.GetOrdinal("Pagos")) ? dr.GetDecimal(dr.GetOrdinal("Pagos")) : 0;
                        item.pendiente = !dr.IsDBNull(dr.GetOrdinal("Pendiente")) ? dr.GetDecimal(dr.GetOrdinal("Pendiente")) : 0;
                        lista.Add(item);
                    }
                    dr.Close();
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<RptCuentaEstadoProveedorActualizadoModels> ObtenerListaEstadoCuentaProveedorActualizado(RptCuentaEstadoProveedorActualizadoModels datos)
        {
            try
            {
                List<RptCuentaEstadoProveedorActualizadoModels> lista = new List<RptCuentaEstadoProveedorActualizadoModels>();
                RptCuentaEstadoProveedorActualizadoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Reporte_get_EstadoCuentaProveedorActualizado", datos.FechaInicio, datos.FechaFin);
                if (dr != null)
                {
                    decimal totalDescontado = 0;
                    int numFila = 0;
                    int filaAnterior = 0;
                    long folioAnterior = 0;
                    
                    while (dr.Read())
                    {
                        item = new RptCuentaEstadoProveedorActualizadoModels();
                        item.TipoProveedor = !dr.IsDBNull(dr.GetOrdinal("tipoProveedor")) ? dr.GetString(dr.GetOrdinal("tipoProveedor")) : string.Empty;
                        item.NombreProveedor = !dr.IsDBNull(dr.GetOrdinal("nombreProveedor")) ? dr.GetString(dr.GetOrdinal("nombreProveedor")) : string.Empty;
                        item.NumeroFolio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetInt64(dr.GetOrdinal("folio")) : 0;
                        item.Anticipo = !dr.IsDBNull(dr.GetOrdinal("anticipo")) ? dr.GetDecimal(dr.GetOrdinal("anticipo")) : 0;
                        item.GranTotal = !dr.IsDBNull(dr.GetOrdinal("granTotal")) ? dr.GetDecimal(dr.GetOrdinal("granTotal")) : 0;
                        item.NuevoTotal = item.GranTotal;
                        item.Finiquito = item.GranTotal - item.Anticipo;
                        item.FechaPago = !dr.IsDBNull(dr.GetOrdinal("fechaPago")) ? dr.GetDateTime(dr.GetOrdinal("fechaPago")) : DateTime.Today;

                        if (numFila > 0)
                        {
                            filaAnterior = numFila - 1;
                            folioAnterior = lista[filaAnterior].NumeroFolio;
                            if(folioAnterior == item.NumeroFolio)
                            {
                                item.NuevoTotal = item.GranTotal - lista[filaAnterior].Finiquito;
                                item.Finiquito = lista[filaAnterior].Finiquito - item.Anticipo;
                            }
                        }
                        lista.Add(item);
                        numFila += 1;
                    }
                    dr.Close();
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<RptEntradaModels> ObtenerEntradasV2(RptEntradaModels Datos)
        {
            try
            {
                object[] parametros = { Datos.FechaInicio, Datos.FechaFin };
                List<RptEntradaModels> Lista = new List<RptEntradaModels>();
                RptEntradaModels item;
                SqlDataReader ds = null;
                ds = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Reportes_get_ReporteEntradasV2", parametros);
                while (ds.Read())
                {
                    item = new RptEntradaModels();
                    item.Fecha = !ds.IsDBNull(ds.GetOrdinal("fecha")) ? ds.GetDateTime(ds.GetOrdinal("fecha")) : DateTime.Today;
                    item.Totalv2 = !ds.IsDBNull(ds.GetOrdinal("total")) ? ds.GetDecimal(ds.GetOrdinal("total")) : 0;
                    item.Descripcion = !ds.IsDBNull(ds.GetOrdinal("descripcion")) ? ds.GetString(ds.GetOrdinal("descripcion")) : string.Empty;
                    Lista.Add(item);
                }
                ds.Close();
                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}