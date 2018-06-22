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
    }
}