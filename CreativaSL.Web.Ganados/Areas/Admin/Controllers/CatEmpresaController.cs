using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.App_Start;
using System.IO;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class CatEmpresaController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
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
                Token.SaveToken();
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
                if (Token.IsTokenValid())
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
                        Token.ResetToken();
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
                else
                {
                    return RedirectToAction("Index");
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
            Token.SaveToken();
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
                ModelState.Remove("Banco.ImagenB");
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        Empresa.Conexion = Conexion;
                        EmpresaDatos = new _CatEmpresa_Datos();
                        Empresa.IDUsuario = User.Identity.Name;
                        Empresa = EmpresaDatos.InsertUpdateCuentaBancaria(Empresa);
                        Token.ResetToken();
                        return Content(Empresa.RespuestaAjax.ToJSON(), "application/json");
                    }
                    else
                    {

                        Empresa.RespuestaAjax.Mensaje = "Verifique su formulario.";
                        Empresa.RespuestaAjax.Success = false;

                        return Content(Empresa.RespuestaAjax.ToJSON(), "application/json");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
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
                Token.SaveToken();
                Sucursal = new CatSucursalesModels
                {
                    IDEmpresa = id,
                    NombreSucursalMatriz = nombreEmpresa
                };
                SucursalDatos = new _CatSucursal_Datos();
                Sucursal.Conexion = Conexion;
                Sucursal.ListaLugares = SucursalDatos.GetLugares(Sucursal);
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
                if (Token.IsTokenValid())
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
                            TempData["message"] = Sucursal.Mensaje;
                            Token.ResetToken();
                            return RedirectToAction("IndexSucursales", new { id = Sucursal.IDEmpresa, nombreEmpresa = Sucursal.NombreSucursalMatriz });
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = Sucursal.Mensaje;
                            return RedirectToAction("CreateSucursal", new { id = Sucursal.IDEmpresa, nombreEmpresa = Sucursal.NombreSucursalMatriz });
                        }
                    }
                    return View(Sucursal);
                }
                else
                {
                    return RedirectToAction("IndexSucursales", new { id = Sucursal.IDEmpresa, nombreEmpresa = Sucursal.NombreSucursalMatriz });
                }
            
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
                    Token.SaveToken();
                    Sucursal = new CatSucursalesModels
                    {
                        IDSucursal = id,
                        NombreSucursalMatriz = nombreEmpresa
                    };
                    SucursalDatos = new _CatSucursal_Datos();
                    Sucursal.Conexion = Conexion;
                    Sucursal = SucursalDatos.GetSucursalXIDSucursal(Sucursal);
                    Sucursal.ListaLugares = SucursalDatos.GetLugares(Sucursal);
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
                if (Token.IsTokenValid())
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
                            TempData["message"] = Sucursal.Mensaje;
                            Token.ResetToken();
                            return RedirectToAction("IndexSucursales", new { id = Sucursal.IDEmpresa, nombreEmpresa = Sucursal.NombreSucursalMatriz });
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = Sucursal.Mensaje;
                            return RedirectToAction("EditSucursal", new { id = Sucursal.IDEmpresa, nombreEmpresa = Sucursal.NombreSucursalMatriz });
                        }
                    }
                    return View(Sucursal);
                }
                else
                {
                    return RedirectToAction("IndexSucursales", new { id = Sucursal.IDEmpresa, nombreEmpresa = Sucursal.NombreSucursalMatriz });
                }
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Se ha generado el siguiente error: " + ex.ToString();
                return View(Sucursal);
            }
        }
        [HttpPost]
        public ActionResult DeleteSucursal(string id)
        {
            try
            {
                Sucursal = new CatSucursalesModels();
                SucursalDatos = new _CatSucursal_Datos();
                Sucursal.Conexion = Conexion;
                Sucursal.Usuario = User.Identity.Name;
                Sucursal.IDSucursal = id;
                Sucursal = SucursalDatos.Del_Sucursal(Sucursal);
                Sucursal.Mensaje = "{\"Mensaje\": \"" + Sucursal.Mensaje + "\"}";
                return Content(Sucursal.Mensaje, "application/json");
            }
            catch (Exception ex)
            {
                Sucursal.Mensaje = "{\"Mensaje error:\": \"" + Sucursal.Mensaje + "\"}";
                return Content(Sucursal.Mensaje, "application/json");
            }
        }

        [HttpGet]
        public ActionResult AgregarArchivo(string idEmpresa)
        {
            try
            {
                if (string.IsNullOrEmpty(idEmpresa.Trim()))
                {
                    return RedirectToAction("Index");
                }

                ImagenEmpresaModels Imagen = new ImagenEmpresaModels();
                
                Imagen.Id_empresa = idEmpresa;

                return View(Imagen);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult AgregarArchivo(ImagenEmpresaModels Imagen)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index");
                }

                _CatEmpresa_Datos Datos = new _CatEmpresa_Datos();
                Imagen.UrlArchivo = Guid.NewGuid().ToString() + Path.GetExtension(Imagen.Archivo.FileName);
                RespuestaAjax respuesta = Datos.EMPRESA_ac_Archivo(Imagen, Conexion, User.Identity.Name, 1);

                if (respuesta.Success)
                {
                    Imagen.Archivo.SaveAs(Server.MapPath("~/ImagenEmpresa/" + Imagen.UrlArchivo));
                    TempData["typemessage"] = "1";
                }
                else
                {
                    TempData["typemessage"] = "2";
                }

                TempData["message"] = respuesta.Mensaje;

                return View(Imagen);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Verifique sus datos.";
                return RedirectToAction("Index");
            }
        }
    }
}