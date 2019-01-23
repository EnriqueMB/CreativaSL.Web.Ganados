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
                reporte.listaProveedores = reporteDatos.ListaProveedoresMermaAltaGrafica(reporte);
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
                Rtp.DataSources.Add(new ReportDataSource("ListaGrafica", reporte.listaProveedores));
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

        public ActionResult RptProveedorVendioMas(string id, string id2, string id3)
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
                ReporteVMas.DatosEmpresa = RDatos.ObtenerDatosEmpresaTipo1(Conexion);
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

        public ActionResult RptGanadosVendidos(string id, string id2, string id3)
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
                reporte.datosEmpresa = R.ObtenerDatosEmpresaTipo1(Conexion);
                reporte.listaGanadosVendidos = R.obtenerListaGanadosVendidos(reporte);
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
                Rtp.DataSources.Add(new ReportDataSource("ListaGanadosTotal", reporte.listaGanadosTotal));
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

        public ActionResult RptGanadosMtoVenta(string id, string id2, string id3)
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
                reporte.Conexion = Conexion;
                reporte.datosEmpresa = R.ObtenerDatosEmpresaTipo1(Conexion);
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
                Rtp.DataSources.Add(new ReportDataSource("ListaTotalGanado", reporte.ListaTotalGanado));
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

        public ActionResult RptSocios(string id, string id2, string id3)
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
                Socios.Conexion = Conexion;
                Socios.DatosEmpresa = RSocios.ObtenerDatosEmpresaTipo1(Conexion);
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

        public ActionResult RptFletes (string id, string id2, string id3)
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

        public ActionResult RptEntradasV2(string id, string id2, string id3)
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
    }
}