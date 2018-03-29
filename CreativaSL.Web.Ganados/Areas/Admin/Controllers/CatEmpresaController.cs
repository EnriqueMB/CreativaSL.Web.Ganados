using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using System.Configuration;
using System.IO;
using System;
using System.Net;
using System.Web;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class CatEmpresaController : Controller
    {
        private CatEmpresaModels Empresa;
        private _CatEmpresa_Datos EmpresaDatos;
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        // GET: Admin/CatEmpresa
        public ActionResult Index()
        {
            Empresa = new CatEmpresaModels
            {
                Conexion = Conexion
            };
            EmpresaDatos = new _CatEmpresa_Datos();
            Empresa.ListaEmpresas = EmpresaDatos.GetListadoEmpresas(Empresa);
            return View(Empresa);
        }

        // GET: Admin/CatEmpresa/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CatEmpresa/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                Empresa = new CatEmpresaModels
                {
                    Conexion = Conexion,
                    IDEmpresa = id
                };
                EmpresaDatos = new _CatEmpresa_Datos();
                Empresa = EmpresaDatos.GetEmpresaXID(Empresa);

                return View(Empresa);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public ActionResult Edit(CatEmpresaModels Empresa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmpresaDatos = new _CatEmpresa_Datos();
                    Empresa.LogoRFC = Empresa.ImageToBase64(Empresa.LogoRFCHttp);
                    Empresa.LogoEmpresa = Empresa.ImageToBase64(Empresa.LogoEmpresaHttp);
                    Empresa.Conexion = Conexion;
                    Empresa = EmpresaDatos.UpdateEmpresaXID(Empresa);
                    return Json(new { success = true, responseText = Empresa.MensajeJson, error = Empresa.Error }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Empresa.MensajeJson = "Verifique su formulario.";
                    return Content(Empresa.GenerarMensajeJson(), "application/json");
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Empresa.MensajeJson = ex.ToString();
                return Content(Empresa.GenerarMensajeJson(), "application/json");
            }
        }

        public ContentResult SaveEmpresa(CatEmpresaModels Empresa)
        {
                return Content(Empresa.GenerarMensajeJson(), "application/json");

        }
    }
}
