using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.ViewModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class ReportesController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/Reportes
        public ActionResult Index()
        {
            try
            {
                RptProveedorMermaAltaModels Reporte = new RptProveedorMermaAltaModels();
                _Combos_Datos Combos = new _Combos_Datos();
                Reporte.ListaCmbSucursal = Combos.ObtenerComboSucursales(Conexion);
                return View(Reporte);
            }
            catch (Exception)
            {
                RptProveedorMermaAltaModels Reporte = new RptProveedorMermaAltaModels();
                Reporte.ListaCmbSucursal = new List<CatSucursalesModels>();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Reporte);
            }          
        }

        public ActionResult RptMermaAlta(string id, string id2, string id3, string id4)
        {
            try
            {

                Reporte_Datos R = new Reporte_Datos();
                RptProveedorMermaAltaModels reporte = new RptProveedorMermaAltaModels();
                _RptProveedorMermaAlta_Datos reporteDatos = new _RptProveedorMermaAlta_Datos();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                reporte.FechaInicio = Fecha1;
                reporte.FechaFin = Fecha2;
                reporte.IDSucursalActual = id4;
                reporte.Conexion = Conexion;
                reporte.DatosEmpresa = R.ObtenerDatosEmpresaTipoIDSucursal(Conexion, id4);
                reporte.listaRptProveedorMerma = reporteDatos.obtenerListaProveedoresMermaAlta(reporte);
                //reporte.listaProveedores = reporteDatos.ListaProveedoresMermaAltaGrafica(reporte);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteMermaAlta.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", reporte.DatosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", reporte.DatosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", reporte.DatosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", reporte.DatosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", reporte.DatosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", reporte.DatosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", reporte.DatosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaMerma", reporte.listaRptProveedorMerma));
               // Rtp.DataSources.Add(new ReportDataSource("ListaGrafica", reporte.listaProveedores));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                //"  <PageWidth>8.5in</PageWidth>" +
                //"  <PageHeight>11in</PageHeight>" +
                //"  <MarginTop>0.5in</MarginTop>" +
                //"  <MarginLeft>1in</MarginLeft>" +
                //"  <MarginRight>1in</MarginRight>" +
                //"  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        public ActionResult RptProveedorVendioMas(string id, string id2, string id3, string id4)
        {
            try
            {
                Reporte_Datos RDatos = new Reporte_Datos();
                RptProvedorVendioMasModels ReporteVMas = new RptProvedorVendioMasModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                ReporteVMas.FechaInicio = Fecha1;
                ReporteVMas.FechaFin = Fecha2;
                ReporteVMas.Conexion = Conexion;
                ReporteVMas.IdSucursal = id4;
                ReporteVMas.DatosEmpresa = RDatos.ObtenerDatosEmpresaTipoIDSucursal(ReporteVMas.Conexion, ReporteVMas.IdSucursal);
                ReporteVMas.ListaProveedorVendioMas = RDatos.obtenerListaProveedoresMermaAlta(ReporteVMas);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteProveedorVendioMasG.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", ReporteVMas.DatosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", ReporteVMas.DatosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", ReporteVMas.DatosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", ReporteVMas.DatosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", ReporteVMas.DatosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", ReporteVMas.DatosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", ReporteVMas.DatosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaMasVendido", ReporteVMas.ListaProveedorVendioMas));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        public ActionResult RptSalidas(string id, string id2, string id3)
        {
            try
            {
                ReportViewer Rtp = new ReportViewer();
                Rtp.ProcessingMode = ProcessingMode.Local;
                //Rtp.SizeToReportContent = true;
                Rtp.Width = Unit.Percentage(100);
                Rtp.Height = Unit.Percentage(100);
                Reporte_Datos RSalidas = new Reporte_Datos();
                RptSalidaModels ReporteSalida = new RptSalidaModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                ReporteSalida.FechaInicio = Fecha1;
                ReporteSalida.FechaFin = Fecha2;
                ReporteSalida.Conexion = Conexion;
                ReporteSalida.DatosEmpresa = RSalidas.ObtenerDatosEmpresaTipo1(Conexion);
                ReporteSalida.ListaSalidas = RSalidas.obtenerListaSalidas(ReporteSalida);
                //LocalReport Rtp = new LocalReport();
                Rtp.LocalReport.EnableExternalImages = true;
                Rtp.LocalReport.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "RptSalidas.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.LocalReport.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", ReporteSalida.DatosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", ReporteSalida.DatosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", ReporteSalida.DatosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", ReporteSalida.DatosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", ReporteSalida.DatosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", ReporteSalida.DatosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", ReporteSalida.DatosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.LocalReport.SetParameters(Parametros);
                Rtp.LocalReport.DataSources.Add(new ReportDataSource("ListaSalidas", ReporteSalida.ListaSalidas));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";
                
                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.LocalReport.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        public ActionResult RptGanadosVendidos(string id, string id2, string id3, string id4)
        {
            try
            {

                Reporte_Datos R = new Reporte_Datos();
                RptGandosModels reporte = new RptGandosModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                reporte.FechaInicio = Fecha1;
                reporte.FechaFin = Fecha2;
                reporte.Conexion = Conexion;
                reporte.IDSucursal = id4;
                reporte.datosEmpresa = R.ObtenerDatosEmpresaTipoIDSucursal(Conexion, reporte.IDSucursal);
                reporte.listaGanadosVendidos = R.obtenerListaGanadosVendidos(reporte);
                reporte.listaGanadosTotal = R.ListaGanadosVendidosGraficaGeneroKG(reporte);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteGanadosVendidos.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", reporte.datosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", reporte.datosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", reporte.datosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", reporte.datosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", reporte.datosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", reporte.datosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", reporte.datosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaGanadosVendidos", reporte.listaGanadosVendidos));
                Rtp.DataSources.Add(new ReportDataSource("ListaGraficaKG", reporte.listaGanadosTotal));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        public ActionResult RptGanadosMtoVenta(string id, string id2, string id3, string id4)
        {
            try
            {

                Reporte_Datos R = new Reporte_Datos();
                RptGanadosMtoVentaModels reporte = new RptGanadosMtoVentaModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                reporte.fechaInicio = Fecha1;
                reporte.fechaFin = Fecha2;
                reporte.IdSucursal = id4;
                reporte.Conexion = Conexion;
                reporte.datosEmpresa = R.ObtenerDatosEmpresaTipoIDSucursal(Conexion, reporte.IdSucursal);
                reporte.listaGanadosMtoVenta = R.obtenerListaGanadosMtoVenta(reporte);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteGanadosMuertoVenta.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", reporte.datosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", reporte.datosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", reporte.datosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", reporte.datosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", reporte.datosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", reporte.datosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", reporte.datosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaGanadosMtoVenta", reporte.listaGanadosMtoVenta));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }
        
        public ActionResult RptSocios(string id, string id2, string id3, string id4)
        {
            try
            {
                ReportViewer Rtp = new ReportViewer();
                Rtp.ProcessingMode = ProcessingMode.Local;
                //Rtp.SizeToReportContent = true;
                Rtp.Width = Unit.Percentage(100);
                Rtp.Height = Unit.Percentage(100);
                Reporte_Datos RSocios = new Reporte_Datos();
                RptSociosModels Socios = new RptSociosModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                Socios.FechaInicio = Fecha1;
                Socios.FechaFin = Fecha2;
                Socios.IdSucursal = id4;
                Socios.Conexion = Conexion;
                Socios.DatosEmpresa = RSocios.ObtenerDatosEmpresaTipoIDSucursal(Conexion, Socios.IdSucursal);
                Socios.ListaSocios = RSocios.ObtenerSocios(Socios);
                Rtp.LocalReport.EnableExternalImages = true;
                Rtp.LocalReport.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "RptSocios.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.LocalReport.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", Socios.DatosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", Socios.DatosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", Socios.DatosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", Socios.DatosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", Socios.DatosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", Socios.DatosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", Socios.DatosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.LocalReport.SetParameters(Parametros);
                Rtp.LocalReport.DataSources.Add(new ReportDataSource("ListaSocios", Socios.ListaSocios));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.LocalReport.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        public ActionResult RptEntradasV2(string id, string id2, string id3, string id4)
        {
            try
            {
                ReportViewer Rtp = new ReportViewer();
                Rtp.ProcessingMode = ProcessingMode.Local;
                //Rtp.SizeToReportContent = true;
                Rtp.Width = Unit.Percentage(100);
                Rtp.Height = Unit.Percentage(100);
                Reporte_Datos RDEntra = new Reporte_Datos();
                RptEntradaModels REntradas = new RptEntradaModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                REntradas.FechaInicio = Fecha1;
                REntradas.FechaFin = Fecha2;
                REntradas.Conexion = Conexion;
                REntradas.IdSucursal = id4;
                REntradas.DatosEmpresa = RDEntra.ObtenerDatosEmpresaTipoIDSucursal(Conexion, REntradas.IdSucursal);
                REntradas.ListaEntradas = RDEntra.ObtenerEntradasV2(REntradas);
                Rtp.LocalReport.EnableExternalImages = true;
                Rtp.LocalReport.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteEntradasV2.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.LocalReport.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[5];
                Parametros[0] = new ReportParameter("Empresa", REntradas.DatosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("NombreSucursal", REntradas.DatosEmpresa.NombreSucursal);
                Parametros[2] = new ReportParameter("UrlLogo", REntradas.DatosEmpresa.LogoEmpresa);
                Parametros[3] = new ReportParameter("FechaInicio", id2);
                Parametros[4] = new ReportParameter("FechaFin", id3);
                Rtp.LocalReport.SetParameters(Parametros);
                Rtp.LocalReport.DataSources.Add(new ReportDataSource("ListaEntradasV2", REntradas.ListaEntradas));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.LocalReport.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        public ActionResult RptFletes (string id, string id2, string id3, string id4)
        {
            try
            {
                Reporte_Datos R = new Reporte_Datos();
                RptFletesModels reporte = new RptFletesModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                reporte.fechaInicio = Fecha1;
                reporte.fechaFin = Fecha2;
                reporte.id_sucursal = id4;
                reporte.Conexion = Conexion;
                reporte.datosEmpresa = R.ObtenerDatosEmpresaTipo2(Conexion);
                reporte.listaFletes = R.ObtenerListaFletes(reporte);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteFletes.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", reporte.datosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", reporte.datosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", reporte.datosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", reporte.datosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", reporte.datosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", reporte.datosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", reporte.datosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaFletes", reporte.listaFletes));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        public ActionResult RptMttVehiculo(string id, string id2, string id3, string id4)
        {
            try
            {
                Reporte_Datos R = new Reporte_Datos();
                RptMantenimientoVehiculoModels rtpMtt = new RptMantenimientoVehiculoModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                rtpMtt.FechaInicio = Fecha1;
                rtpMtt.FechaFin = Fecha2;
                rtpMtt.IDSucursal = id4;
                rtpMtt.Conexion = Conexion;
                rtpMtt.datosEmpresa = R.ObtenerDatosEmpresaTipoIDSucursal(Conexion, id4);
                rtpMtt.ListaMantemiento = R.ListaMantenimiento(rtpMtt);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteMantenimientoVehiculo.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", rtpMtt.datosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", rtpMtt.datosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", rtpMtt.datosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", rtpMtt.datosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", rtpMtt.datosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", rtpMtt.datosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", rtpMtt.datosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaMantenimiento", rtpMtt.ListaMantemiento));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        public ActionResult RptRendimientoVehiculo(string id, string id2, string id3, string id4)
        {
            try
            {
                Reporte_Datos R = new Reporte_Datos();
                RptRendimientoVehiculoModels Rendimiento = new RptRendimientoVehiculoModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                Rendimiento.FechaInicio = Fecha1;
                Rendimiento.FechaFin = Fecha2;
                Rendimiento.IDSucursal = id4;
                Rendimiento.Conexion = Conexion;
                Rendimiento.DatosEmpresa = R.ObtenerDatosEmpresaTipoIDSucursal(Conexion, id4);
                Rendimiento.ListaRendimiento = R.ListaRendimiento(Rendimiento);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteRendimientoVehiculo.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", Rendimiento.DatosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", Rendimiento.DatosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", Rendimiento.DatosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", Rendimiento.DatosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", Rendimiento.DatosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", Rendimiento.DatosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", Rendimiento.DatosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaRendimiento", Rendimiento.ListaRendimiento));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }
        


        public ActionResult RptAlmacen(string id, string id2, string id3, string id4)
        {
            try
            {
                Reporte_Datos R = new Reporte_Datos();
                RptAlmacenModels Almacen = new RptAlmacenModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                Almacen.FechaInicio = Fecha1;
                Almacen.FechaFin = Fecha2;
                Almacen.IDSucursal = id4;
                Almacen.Conexion = Conexion;
                Almacen.DatosEmpresa = R.ObtenerDatosEmpresaTipoIDSucursal(Conexion, id4);
                Almacen.ListaAlmacen = R.ListaEntradaSalidaAlmacen(Almacen);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteAlmacen.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", Almacen.DatosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", Almacen.DatosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", Almacen.DatosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", Almacen.DatosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", Almacen.DatosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", Almacen.DatosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", Almacen.DatosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaAlmacen", Almacen.ListaAlmacen));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        public ActionResult RptEntradaAlmacen(string id, string id2, string id3, string id4)
        {
            try
            {
                Reporte_Datos R = new Reporte_Datos();
                RptEntadaAlmacenModels Entrada = new RptEntadaAlmacenModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                Entrada.FechaInicio = Fecha1;
                Entrada.FechaFin = Fecha2;
                Entrada.IDSucursal = id4;
                Entrada.Conexion = Conexion;
                Entrada.DatosEmpresa = R.ObtenerDatosEmpresaTipoIDSucursal(Conexion, id4);
                Entrada.ListaEntradaA = R.ListaEntradaAlmacen(Entrada);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteEntradaAlmacen.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", Entrada.DatosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", Entrada.DatosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", Entrada.DatosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", Entrada.DatosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", Entrada.DatosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", Entrada.DatosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", Entrada.DatosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaEntradaAlmacen", Entrada.ListaEntradaA));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Reporte de entrada y salida de almacen
        public ActionResult RptEntradaSalidaAlmacen(string id, string id2, string id3, string id4)
        {
            try
            {
                Reporte_Datos R = new Reporte_Datos();
                RptEntadaAlmacenModels Entrada = new RptEntadaAlmacenModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                Entrada.FechaInicio = Fecha1;
                Entrada.FechaFin = Fecha2;
                Entrada.IDSucursal = id4;
                Entrada.Conexion = Conexion;
                Entrada.DatosEmpresa = R.ObtenerDatosEmpresaTipoIDSucursal(Conexion, id4);
                Entrada.ListaEntradaA = R.ListaEntradaAlmacen(Entrada);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteAlmacen.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", Entrada.DatosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", Entrada.DatosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", Entrada.DatosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", Entrada.DatosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", Entrada.DatosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", Entrada.DatosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", Entrada.DatosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaAlmacen", Entrada.ListaEntradaA));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult RptSalidaAlmacen(string id, string id2, string id3, string id4)
        {
            try
            {
                Reporte_Datos R = new Reporte_Datos();
                RptSalidaAlmacenModels Salidas = new RptSalidaAlmacenModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                Salidas.FechaInicio = Fecha1;
                Salidas.FechaFin = Fecha2;
                Salidas.IDSucursal = id4;
                Salidas.Conexion = Conexion;
                Salidas.DatosEmpresa = R.ObtenerDatosEmpresaTipoIDSucursal(Conexion, id4);
                Salidas.ListaSalidaA = R.ListaSalidaAlmacen(Salidas);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteSalidasAlmacen.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", Salidas.DatosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", Salidas.DatosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", Salidas.DatosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", Salidas.DatosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", Salidas.DatosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", Salidas.DatosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", Salidas.DatosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaSalidaAlmacen", Salidas.ListaSalidaA));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult RptVentaMerma(string id, string id2, string id3, string id4)
        {
            try
            {
                Reporte_Datos R = new Reporte_Datos();
                RptVentasMermasModels VentaMerma = new RptVentasMermasModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                VentaMerma.FechaInicio = Fecha1;
                VentaMerma.FechaFin = Fecha2;
                VentaMerma.IDSucursal = id4;
                VentaMerma.Conexion = Conexion;
                VentaMerma.DatosEmpresa = R.ObtenerDatosEmpresaTipoIDSucursal(Conexion, id4);
                VentaMerma.ListaVentaMerma = R.ListaVentaMerma(VentaMerma);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteVentaMermaDestino.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", VentaMerma.DatosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", VentaMerma.DatosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", VentaMerma.DatosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", VentaMerma.DatosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", VentaMerma.DatosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", VentaMerma.DatosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", VentaMerma.DatosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaVentaMerma", VentaMerma.ListaVentaMerma));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region REPORTES OCULTADO DEL EN EL INDEX CONSULTAS.

        public ActionResult RptEntrada(string id, string id2, string id3)
        {
            try
            {
                ReportViewer Rtp = new ReportViewer();
                Rtp.ProcessingMode = ProcessingMode.Local;
                //Rtp.SizeToReportContent = true;
                Rtp.Width = Unit.Percentage(100);
                Rtp.Height = Unit.Percentage(100);
                Reporte_Datos RDEntra = new Reporte_Datos();
                RptEntradaModels REntradas = new RptEntradaModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                REntradas.FechaInicio = Fecha1;
                REntradas.FechaFin = Fecha2;
                REntradas.Conexion = Conexion;
                REntradas.DatosEmpresa = RDEntra.ObtenerDatosEmpresaTipo1(Conexion);
                REntradas.ListaEntradas = RDEntra.ObtenerEntradas(REntradas);
                Rtp.LocalReport.EnableExternalImages = true;
                Rtp.LocalReport.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteEntradas.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.LocalReport.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", REntradas.DatosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", REntradas.DatosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", REntradas.DatosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", REntradas.DatosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", REntradas.DatosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", REntradas.DatosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", REntradas.DatosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.LocalReport.SetParameters(Parametros);
                Rtp.LocalReport.DataSources.Add(new ReportDataSource("ListaEntradas", REntradas.ListaEntradas));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.LocalReport.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        public ActionResult RptGanadosMtoCompra(string id, string id2, string id3)
        {
            try
            {

                Reporte_Datos R = new Reporte_Datos();
                RptGanadosMtoCompraModels reporteCpra = new RptGanadosMtoCompraModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                reporteCpra.fechaInicio = Fecha1;
                reporteCpra.fechaFin = Fecha2;
                reporteCpra.Conexion = Conexion;
                reporteCpra.datosEmpresa = R.ObtenerDatosEmpresaTipo1(Conexion);
                reporteCpra.listaGanadosMtoCompra = R.obtenerListaGanadosMtoCompra(reporteCpra);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteGanadosMuertoCompra.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", reporteCpra.datosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", reporteCpra.datosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", reporteCpra.datosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", reporteCpra.datosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", reporteCpra.datosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", reporteCpra.datosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", reporteCpra.datosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaGanadosMtoCompra", reporteCpra.listaGanadosMtoCompra));
                Rtp.DataSources.Add(new ReportDataSource("ListaTotalGanados", reporteCpra.listaTotalGanado));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        public ActionResult RptJaulasXVenta(string id, string id2, string id3)
        {
            try
            {

                Reporte_Datos R = new Reporte_Datos();
                RptJaulasXVentaModels reporte = new RptJaulasXVentaModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                reporte.fechaInicio = Fecha1;
                reporte.fechaFin = Fecha2;
                reporte.Conexion = Conexion;
                reporte.datosEmpresa = R.ObtenerDatosEmpresaTipo1(Conexion);
                reporte.listaJaulas = R.obtenerListaJaulasXVenta(reporte);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteJaulasVenta.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", reporte.datosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", reporte.datosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", reporte.datosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", reporte.datosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", reporte.datosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", reporte.datosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", reporte.datosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaJaulasXVenta", reporte.listaJaulas));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        public ActionResult RptCuentaEstadoProveedor(string id, string id2, string id3)
        {
            try
            {
                RptEstadoCuentaProveedorModels reporte = new RptEstadoCuentaProveedorModels();
                Reporte_Datos R = new Reporte_Datos();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                reporte.fechaInicio = Fecha1;
                reporte.fechaFin = Fecha2;
                reporte.Conexion = Conexion;
                reporte.datosEmpresa = R.ObtenerDatosEmpresaTipo1(Conexion);
                reporte.listEstadoCuentaProveedor = R.ObtenerListaEstadoCuentaProveedor(reporte);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteEstadoCuentaProveedor.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", reporte.datosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", reporte.datosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", reporte.datosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", reporte.datosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", reporte.datosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", reporte.datosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", reporte.datosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaEstadoCuentaProveedor", reporte.listEstadoCuentaProveedor));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        public ActionResult RptCuentaEstadoProveedorActualizado(string id, string id2, string id3)
        {
            try
            {
                RptCuentaEstadoProveedorActualizadoModels reporte = new RptCuentaEstadoProveedorActualizadoModels();
                Reporte_Datos R = new Reporte_Datos();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                reporte.FechaInicio = Fecha1;
                reporte.FechaFin = Fecha2;
                reporte.Conexion = Conexion;
                reporte.DatosEmpresa = new DatosEmpresaViewModels();

                reporte.DatosEmpresa = R.ObtenerDatosEmpresaTipo1(Conexion);
                List<RptCuentaEstadoProveedorActualizadoModels> lista = new List<RptCuentaEstadoProveedorActualizadoModels>();
                lista = R.ObtenerListaEstadoCuentaProveedorActualizado(reporte);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteEstadoCuentaProveedorActualizado.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[4];
                //Parametros[0] = new ReportParameter("Empresa", reporte.DatosEmpresa.RazonFiscal);
                //Parametros[1] = new ReportParameter("Direccion", reporte.DatosEmpresa.DireccionFiscal);
                //Parametros[2] = new ReportParameter("RFC", reporte.DatosEmpresa.RFC);
                //Parametros[3] = new ReportParameter("TelefonoCasa", reporte.DatosEmpresa.NumTelefonico1);
                //Parametros[4] = new ReportParameter("TelefonoMovil", reporte.DatosEmpresa.NumTelefonico2);
                Parametros[0] = new ReportParameter("NombreSucursal", reporte.DatosEmpresa.NombreSucursal);
                Parametros[1] = new ReportParameter("UrlLogo", reporte.DatosEmpresa.LogoEmpresa);
                Parametros[2] = new ReportParameter("FechaInicio", id2);
                Parametros[3] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaEstadoCuentaProveedorActualizado", lista));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        public ActionResult RptCorrales(string id, string id2, string id3)
        {
            try
            {
                ReportViewer Rtp = new ReportViewer();
                Rtp.ProcessingMode = ProcessingMode.Local;
                //Rtp.SizeToReportContent = true;
                Rtp.Width = Unit.Percentage(100);
                Rtp.Height = Unit.Percentage(100);
                Reporte_Datos RDCorral = new Reporte_Datos();
                RptCorralesModels RCorrales = new RptCorralesModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                RCorrales.FechaInicio = Fecha1;
                RCorrales.FechaFin = Fecha2;
                RCorrales.Conexion = Conexion;
                RCorrales.DatosEmpresa = RDCorral.ObtenerDatosEmpresaTipo1(Conexion);
                RCorrales.ListaCorrales = RDCorral.ObetenerListaCorrales(RCorrales);
                Rtp.LocalReport.EnableExternalImages = true;
                Rtp.LocalReport.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "RptCorral.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.LocalReport.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", RCorrales.DatosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", RCorrales.DatosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", RCorrales.DatosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", RCorrales.DatosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", RCorrales.DatosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", RCorrales.DatosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", RCorrales.DatosEmpresa.LogoEmpresa);
                Parametros[7] = new ReportParameter("FechaInicio", id2);
                Parametros[8] = new ReportParameter("FechaFin", id3);
                Rtp.LocalReport.SetParameters(Parametros);
                Rtp.LocalReport.DataSources.Add(new ReportDataSource("ListaCorrales", RCorrales.ListaCorrales));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.LocalReport.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }
        //-------------------------------------INICIO----------------------------------------------------------------------------
        public ActionResult RptCombustibles(string id, string id2, string id3, string id4)
        {
            try
            {
                Reporte_Datos R = new Reporte_Datos();//DONDE HACE REFERENCIA AL sp
                RPTCombustibleModels Rendimiento = new RPTCombustibleModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                Rendimiento.FechaInic = Fecha1;
                Rendimiento.FechaFin = Fecha2;
                Rendimiento.IdSucursal = id4;
                Rendimiento.Conexion = Conexion;
                Rendimiento.datosEmpresa = R.ObtenerDatosEmpresaTipoIDSucursal(Conexion, id4);//en obtenerRPtCombustible es el  metodo donde esta el sp
                Rendimiento.ListaEntradas = R.ListaObtenerCombustible(Rendimiento);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteCombustible.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", Rendimiento.datosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", Rendimiento.datosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", Rendimiento.datosEmpresa.RFC); 
                Parametros[3] = new ReportParameter("NombreSucursal", Rendimiento.datosEmpresa.NombreSucursal);
                Parametros[4] = new ReportParameter("UrlLogo", Rendimiento.datosEmpresa.LogoEmpresa);
                Parametros[5] = new ReportParameter("FechaInicio", id2);
                Parametros[6] = new ReportParameter("FechaFin", id3);
                Parametros[7] = new ReportParameter("TelefonoCasa", Rendimiento.datosEmpresa.NumTelefonico1);
                Parametros[8] = new ReportParameter("NombreSucursal2", Rendimiento.datosEmpresa.NombreSucursal);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaCombustible", Rendimiento.ListaEntradas));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        //-------------------------------------FINAL    ---------------------------------------------------------------------------



        //-------------------------------------INICIO----------rtpPagosProveedor------------------------------------------------------------------
        public ActionResult RptPagosProveedor(string id, string id2, string id3, string id4)
        {
            try
            {
                Reporte_Datos R = new Reporte_Datos();//DONDE HACE REFERENCIA AL sp
                RPTPagosProveedorModels proveedor = new RPTPagosProveedorModels();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(id2.ToString(), out Fecha1);
                DateTime.TryParse(id3.ToString(), out Fecha2);
                proveedor.FechaInic = Fecha1;
                proveedor.FechaFin = Fecha2;
                proveedor.IdSucursal = id4;
                proveedor.Conexion = Conexion;
                proveedor.datosEmpresa = R.ObtenerDatosEmpresaTipoIDSucursal(Conexion, id4);//en obtenerRPtCombustible es el  metodo donde esta el sp
                proveedor.ListaProveedor = R.ListaObtenerPagoProveedores(proveedor);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReportePagosProveedor.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "Reportes");
                }
                ReportParameter[] Parametros = new ReportParameter[7];
                Parametros[0] = new ReportParameter("Empresa", proveedor.datosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", proveedor.datosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", proveedor.datosEmpresa.RFC);
                Parametros[3] = new ReportParameter("NombreSucursal", proveedor.datosEmpresa.NombreSucursal);
                Parametros[4] = new ReportParameter("UrlLogo", proveedor.datosEmpresa.LogoEmpresa);
                Parametros[5] = new ReportParameter("FechaInicio", id2);
                Parametros[6] = new ReportParameter("FechaFin", id3);
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaProveedor", proveedor.ListaProveedor));
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = Rtp.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Reportes");
            }
        }

        //-------------------------------------FINAL    ---------------------------------------------------------------------------


        #endregion
    }
}