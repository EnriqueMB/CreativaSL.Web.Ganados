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
        private CatSucursalesModels Sucursal;
        private _CatEmpresa_Datos EmpresaDatos;
        private _CatSucursal_Datos SucursalDatos;
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

                    if (Empresa.LogoEmpresaHttp != null)
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
            Empresa.IDEmpresa = IDCliente;
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

        [HttpGet]
        public ActionResult IndexSucursales(string id, string nombreEmpresa)
        {
            try
            {
                Sucursal = new CatSucursalesModels
                {
                    Conexion = Conexion,
                    NombreSucursalMatriz = nombreEmpresa,
                    IDEmpresa = id
                };
                SucursalDatos = new _CatSucursal_Datos();

                Sucursal.ListaSucursales = SucursalDatos.GetSucursalesXIDEmpresa(Sucursal);
                return View(Sucursal);
            }
            catch (Exception)
            {
                CatSucursalesModels Sucursal = new CatSucursalesModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Sucursal);
            }
        }
        [HttpGet]
        public ActionResult CreateSucursal(string id, string nombreEmpresa)
        {
            try
            {
                Sucursal = new CatSucursalesModels
                {
                    IDEmpresa = id,
                    NombreSucursalMatriz = nombreEmpresa
                };
                SucursalDatos = new _CatSucursal_Datos();
                Sucursal.Conexion = Conexion;
                return View(Sucursal);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista" + ex.ToString();
                return View(Sucursal);
            }
        }
        [HttpPost]
        public ActionResult CreateSucursal(CatSucursalesModels Sucursal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SucursalDatos = new _CatSucursal_Datos();
                    Sucursal.Conexion = Conexion;
                    Sucursal.Usuario = User.Identity.Name;
                    Sucursal = SucursalDatos.AC_Sucursal(Sucursal);

                    if (Sucursal.Completado == true)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("IndexSucursales", new { id = Sucursal.IDEmpresa, nombreEmpresa = Sucursal.NombreSucursalMatriz });
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        return View(Sucursal);
                    }
                }
                return View(Sucursal);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Se ha generado el siguiente error: " + ex.ToString();
                return View(Sucursal);
            }
        }
        [HttpGet]
        public ActionResult EditSucursal(string id, string nombreEmpresa)
        {
            {
                try
                {
                    Sucursal = new CatSucursalesModels
                    {
                        IDSucursal = id,
                        NombreSucursalMatriz = nombreEmpresa
                    };
                    SucursalDatos = new _CatSucursal_Datos();
                    Sucursal.Conexion = Conexion;
                    Sucursal = SucursalDatos.GetSucursalXIDSucursal(Sucursal);
                    return View(Sucursal);
                }
                catch (Exception ex)
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "No se puede cargar la vista" + ex.ToString();
                    return View(Sucursal);
                }
            }
        }
        [HttpPost]
        public ActionResult EditSucursal(CatSucursalesModels Sucursal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SucursalDatos = new _CatSucursal_Datos();
                    Sucursal.Conexion = Conexion;
                    Sucursal.Usuario = User.Identity.Name;
                    Sucursal = SucursalDatos.AC_Sucursal(Sucursal);

                    if (Sucursal.Completado == true)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Los datos se guardaron correctamente.";
                        return RedirectToAction("IndexSucursales", new { id = Sucursal.IDEmpresa, nombreEmpresa = Sucursal.NombreSucursalMatriz });
                    }
                    else
                    {
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                        return View(Sucursal);
                    }
                }
                return View(Sucursal);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Se ha generado el siguiente error: " + ex.ToString();
                return View(Sucursal);
            }
        }
    }
}