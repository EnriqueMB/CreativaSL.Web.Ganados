using CreativaSL.Web.Ganados.Models;
using System;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Filters;
using System.Drawing;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class RptProveedorMermaAltaController : Controller
    {
         string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/RptProveedorMermaAlta
        public ActionResult Index()
        {

            try
            {
                RptProveedorMermaAltaModels reporte = new RptProveedorMermaAltaModels();
                _RptProveedorMermaAlta_Datos reporteDatos = new _RptProveedorMermaAlta_Datos();
                reporte.Conexion = Conexion;
                reporte.listaRptProveedorMerma = reporteDatos.obtenerListaProveedoresMermaAlta(reporte);
                return View(reporte);
            }
            catch (Exception ex)
            {
                RptProveedorMermaAltaModels Proveedor = new RptProveedorMermaAltaModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Proveedor);
            }
        }
        // GET: Admin/Reporte/Details/5
        public ActionResult Reporte()
        {
            try
            {
                RptProveedorMermaAltaModels reporte = new RptProveedorMermaAltaModels();
                _RptProveedorMermaAlta_Datos reporteDatos = new _RptProveedorMermaAlta_Datos();
                reporte.Conexion = Conexion;
                reporte.listaRptProveedorMerma = reporteDatos.obtenerListaProveedoresMermaAlta(reporte);
                return View(reporte);
            }
            catch (Exception ex)
            {
                RptProveedorMermaAltaModels Proveedor = new RptProveedorMermaAltaModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Proveedor);
            }
        }

        //GET: Admin/RptProveedorMermaAlta/Reporte/5
        public ActionResult Reporte2(string id)
        {
            try
            {
                RptProveedorMermaAltaModels reporte = new RptProveedorMermaAltaModels();
                _RptProveedorMermaAlta_Datos reporteDatos = new _RptProveedorMermaAlta_Datos();

                reporte.Conexion = Conexion;
                reporte.DatosEmpresa =  reporteDatos.ObtenerDatosEmpresaTipo1(Conexion);
                reporte.listaRptProveedorMerma = reporteDatos.obtenerListaProveedoresMermaAlta(reporte);
                Image LOGO =  LoadImage(reporte.DatosEmpresa.LogoEmpresa);
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
                    return RedirectToAction("Index", "RptProveedorMermaAlta");
                }
                string path2 = Path.Combine(Server.MapPath("~/Content/img"), "logo.png");
                ReportParameter[] Parametros = new ReportParameter[7];
                Parametros[0] = new ReportParameter("Empresa", reporte.DatosEmpresa.RazonFiscal);
                Parametros[1] = new ReportParameter("Direccion", reporte.DatosEmpresa.DireccionFiscal);
                Parametros[2] = new ReportParameter("RFC", reporte.DatosEmpresa.RFC);
                Parametros[3] = new ReportParameter("TelefonoCasa", reporte.DatosEmpresa.NumTelefonico1);
                Parametros[4] = new ReportParameter("TelefonoMovil", reporte.DatosEmpresa.NumTelefonico2);
                Parametros[5] = new ReportParameter("NombreSucursal", reporte.DatosEmpresa.NombreSucursal);
                Parametros[6] = new ReportParameter("UrlLogo", reporte.DatosEmpresa.LogoEmpresa);//new Uri(Path.Combine(Server.MapPath("~/Content/img"), "logo.png")).AbsoluteUri);
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
                throw ex;
            }
        }


        public Image LoadImage(string Images)
        {
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String(Images);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }

        // GET: Admin/RptProveedorMermaAlta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/RptProveedorMermaAlta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/RptProveedorMermaAlta/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/RptProveedorMermaAlta/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/RptProveedorMermaAlta/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/RptProveedorMermaAlta/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/RptProveedorMermaAlta/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
