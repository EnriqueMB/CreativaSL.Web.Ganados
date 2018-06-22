using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;
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
            return View();
        }

        public ActionResult RptMermaAlta(string id, string id2, string id3)
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
                reporte.Conexion = Conexion;
                reporte.DatosEmpresa = R.ObtenerDatosEmpresaTipo1(Conexion);
                reporte.listaRptProveedorMerma = reporteDatos.obtenerListaProveedoresMermaAlta(reporte);
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
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
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
                return View();
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
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
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

                throw ex;
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

                throw;
            }
        }

        public ActionResult RptGanadosVendidos(string id, string id2, string id3)
        {
            try
            {

                Reporte_Datos R = new Reporte_Datos();
                RptGandosModels reporte = new RptGandosModels();
                // _RptGanadosVendidos_Datos reporteDatos = new _RptGanadosVendidos_Datos();
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
                string reportType = id;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + id + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
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
                return View();
            }
        }
    }
}