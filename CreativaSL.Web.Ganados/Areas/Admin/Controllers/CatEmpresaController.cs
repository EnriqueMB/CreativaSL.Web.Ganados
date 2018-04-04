using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatEmpresaController : Controller
    {
        private CatEmpresaModels Empresa;
        private _CatEmpresa_Datos EmpresaDatos;
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        // GET: Admin/CatEmpresa
        public ActionResult Index()
        {
            try
            {
                Empresa = new CatEmpresaModels
                {
                    Conexion = Conexion
                };
                EmpresaDatos = new _CatEmpresa_Datos();
                Empresa.ListaEmpresas = EmpresaDatos.GetListadoEmpresas(Empresa);
                return View(Empresa);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.ToString();
                return View(Empresa);
            }
        }

        [HttpGet]
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
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.ToString();
                return View(Empresa);
            }
        }

        [HttpPost]
        public ActionResult SaveEmpresa(CatEmpresaModels Empresa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmpresaDatos = new _CatEmpresa_Datos();

                    if(Empresa.LogoEmpresaHttp != null)
                        Empresa.LogoEmpresa = Auxiliar.ImageToBase64(Empresa.LogoEmpresaHttp);

                    if (Empresa.LogoRFCHttp != null)
                        Empresa.LogoRFC = Auxiliar.ImageToBase64(Empresa.LogoRFCHttp);

                    Empresa.Conexion = Conexion;
                    Empresa = EmpresaDatos.UpdateEmpresaXID(Empresa);
                    
                    return Content(Empresa.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                    Empresa.RespuestaAjax.Mensaje = "Verifique su formulario.";
                    Empresa.RespuestaAjax.Success = false;
                    Empresa.RespuestaAjax.Errores = ModelState.AllErrors();

                    return Content(Empresa.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                Empresa.RespuestaAjax.Mensaje = ex.ToString();
                Empresa.RespuestaAjax.Success = false;
                return Content(Empresa.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        [HttpPost]
        public ActionResult LoadTableCuentasBancarias(string IDEmpresa)
        {
            try
            {
                Empresa = new CatEmpresaModels
                {
                    Conexion = Conexion,
                    IDEmpresa = IDEmpresa
                };
                EmpresaDatos = new _CatEmpresa_Datos();
                Empresa.TablaCuentasBancarias = EmpresaDatos.GetCuentasBancarias(Empresa);

                return Content(Empresa.TablaCuentasBancarias, "application/json");
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Empresa.RespuestaAjax.Mensaje = ex.ToString();
                Empresa.RespuestaAjax.Success = false;
                return Content(Empresa.RespuestaAjax.ToJSON(), "application/json");
            }
        }
        
        [HttpPost]
        public ActionResult ModalCuentaBancaria(string IDCuentaBancaria, string IDCliente)
        {
            Empresa = new CatEmpresaModels();
            EmpresaDatos = new _CatEmpresa_Datos();
            Empresa.CuentaBancaria.IDDatosBancarios = IDCuentaBancaria;
            Empresa.CuentaBancaria.Cliente.IDCliente = IDCliente;
            Empresa.Conexion = Conexion;
            Empresa = EmpresaDatos.GetDatosBancariosXID(Empresa);
            Empresa.ListaBancos = EmpresaDatos.GetListaBancos(Empresa);

            return PartialView("ModalCuentaBancaria", Empresa);
        }

        [HttpPost]
        public ActionResult InsertUpdateCuentaBancaria(CatEmpresaModels Empresa)
        {
            try
            {
                //Deshabilitamos las validaciones de Empresa y dejamos los de Empresa.CuentaBancaria}
                ModelState.Remove("RazonFiscal");
                ModelState.Remove("DireccionFiscal");
                ModelState.Remove("RFC");
                ModelState.Remove("NumTelefonico1");
                ModelState.Remove("Email");
                ModelState.Remove("HorarioAtencion");
                ModelState.Remove("Representante");
                ModelState.Remove("LogoEmpresa");
                ModelState.Remove("LogoRFC");
                ModelState.Remove("LogoRFCHttp");
                ModelState.Remove("LogoEmpresaHttp");

                if (ModelState.IsValid)
                {
                    Empresa.Conexion = Conexion;
                    EmpresaDatos = new _CatEmpresa_Datos();
                    Empresa.IDUsuario = User.Identity.Name;
                    Empresa = EmpresaDatos.InsertUpdateCuentaBancaria(Empresa);

                    return Content(Empresa.RespuestaAjax.ToJSON(), "application/json");
                }
                else
                {
                   
                    Empresa.RespuestaAjax.Mensaje = "Verifique su formulario.";
                    Empresa.RespuestaAjax.Success = false;
                   
                    return Content(Empresa.RespuestaAjax.ToJSON(), "application/json");
                }
            }
            catch (Exception ex)
            {
                Empresa.RespuestaAjax.Mensaje = ex.ToString();
                Empresa.RespuestaAjax.Success = false;
                return Content(Empresa.RespuestaAjax.ToJSON(), "application/json");
            }
        }

        [HttpPost]
        public ActionResult DeleteCuentaBancaria(string id)
        {
            try
            {
                Empresa = new CatEmpresaModels();
                Empresa.CuentaBancaria.IDDatosBancarios = id;
                Empresa.IDUsuario = User.Identity.Name;
                Empresa.Conexion = Conexion;
                EmpresaDatos = new _CatEmpresa_Datos();
                Empresa = EmpresaDatos.DeleteCuentaBancaria(Empresa);

                return Content(Empresa.RespuestaAjax.ToJSON(), "application/json");
            }
            catch (Exception ex)
            {
                Empresa.RespuestaAjax.Mensaje = ex.ToString();
                Empresa.RespuestaAjax.Success = false;
                return Content(Empresa.RespuestaAjax.ToJSON(), "application/json");
            }
        }
    }
}