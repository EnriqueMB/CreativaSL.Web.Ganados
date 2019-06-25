using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.ViewModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class ReporteTipoFleteController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/ReporteFlete
        public ActionResult Index()
        {
            try
            {
                ReporteTipoFleteModels Reporte = new ReporteTipoFleteModels();
                _Combos_Datos Combos = new _Combos_Datos();
                Reporte.ListaTipoFlete = new List<CmbTipoFleteModels>(){
                new CmbTipoFleteModels(){ id_tipoFlete = "", descripcion = "--Seleccione--" },
                new CmbTipoFleteModels(){ id_tipoFlete = "1", descripcion = "Compra" },
                new CmbTipoFleteModels(){ id_tipoFlete = "2", descripcion = "Venta" },
                new CmbTipoFleteModels(){ id_tipoFlete = "3", descripcion = "Flete" },
                new CmbTipoFleteModels(){ id_tipoFlete = "4", descripcion = "Todos" }};
                Reporte.ListaCmbSucursal = Combos.ObtenerComboSucursales(Conexion);
                return View(Reporte);
            }
            catch (Exception)
            {
                ReporteTipoFleteModels Reporte = new ReporteTipoFleteModels();
                Reporte.ListaCmbSucursal = new List<CatSucursalesModels>();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Reporte);
            }
        }

        //idS Id_sucursal - fI Fecha Inicios - fF Fecha Final
        [HttpPost]
        public ActionResult ReporteFlete(ReporteTipoFleteModels datos)
        { 
            try
            {
                Reporte_Datos R = new Reporte_Datos();
                ReporteTipoFleteModels reporte = new ReporteTipoFleteModels();
                _ReporteTipoFlete_Datos reporteDatos = new _ReporteTipoFlete_Datos();
                DateTime Fecha1 = DateTime.Today;
                DateTime Fecha2 = DateTime.Today;
                DateTime.TryParse(datos.FechaInicio.ToString(), out Fecha1);
                DateTime.TryParse(datos.FechaFin.ToString(), out Fecha2);
                reporte.FechaInicio = Fecha1;
                reporte.FechaFin = Fecha2;
                reporte.id_sucursal = datos.id_sucursal;
                reporte.Conexion = Conexion;
                reporte.Usuario = User.Identity.Name;
                reporte.id_tipoFlete = datos.id_tipoFlete;
                reporte.datosEmpresa = R.ObtenerDatosEmpresaTipoIDSucursal(Conexion, datos.id_sucursal);
                reporte.ListaReporteFlete = reporteDatos.obtenerListaReporteTipoFlete(reporte);
                LocalReport Rtp = new LocalReport();
                Rtp.EnableExternalImages = true;
                Rtp.DataSources.Clear();
                string path = Path.Combine(Server.MapPath("~/Reports"), "ReporteTipoFlete.rdlc");
                if (System.IO.File.Exists(path))
                {
                    Rtp.ReportPath = path;
                }
                else
                {
                    return RedirectToAction("Index", "HomeAdmin");
                }
                ReportParameter[] Parametros = new ReportParameter[9];
                Parametros[0] = new ReportParameter("Empresa", reporte.datosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", reporte.datosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", reporte.datosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoMovil", reporte.datosEmpresa.NumTelefonico2);
                Parametros[4] = new ReportParameter("TelefonoCasa", reporte.datosEmpresa.NumTelefonico1);
                Parametros[5] = new ReportParameter("UrlLogo", reporte.datosEmpresa.LogoEmpresa);
                Parametros[6] = new ReportParameter("NombreSucursal", reporte.datosEmpresa.NombreSucursal);
                Parametros[7] = new ReportParameter("FechaInicio", datos.FechaInicioFormat.ToString());
                Parametros[8] = new ReportParameter("FechaFin", datos.FechaFin.ToString());
                Rtp.SetParameters(Parametros);
                Rtp.DataSources.Add(new ReportDataSource("ListaFletes", reporte.ListaReporteFlete));
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + "PDF" + "</OutputFormat>" +
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
    }
}