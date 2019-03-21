using CreativaSL.Web.Ganados.Filters;
using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/Account
        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                FormsAuthentication.SignOut();
                if (User.Identity.IsAuthenticated)
                {
                    UsuarioModels usuario = new UsuarioModels();
                    _Usuario_Datos UsuarioDatos = new _Usuario_Datos();
                    usuario.conexion = Conexion;
                    usuario.cuenta = User.Identity.Name;

                    int TipoUsario = UsuarioDatos.ObtenerTipoUsuarioByUserName(usuario);
                    if (TipoUsario == 1)
                    {
                        return RedirectToAction("Index", "HomeAdmin", new { Area = "Admin" });
                    }
                    //else if (id_tipoUsuario == "1")
                    //{
                    //    return RedirectToAction("Index", "HomeProfesor", new { Area = "Profesor" });
                    //}
                    else
                        return RedirectToAction("LogOff", "Account");
                }
                else
                    return View();
            }
            catch (Exception)
            {
                return RedirectToAction("LogOff", "Account");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index(UsuarioModels model, string returnUrl)
        {
            try
            {
                LoginDatos UD = new LoginDatos();
                model.conexion = Conexion;
                model = UD.ValidarUsuario(model);
                if (model.opcion == 1)
                {
                    FormsAuthentication.SignOut();
                    _Usuario_Datos usuario_datos = new _Usuario_Datos();
                    UsuarioModels usuario = new UsuarioModels();
                    usuario.conexion = Conexion;
                    usuario.cuenta = model.id_usuario;
                    int TipoUsario = usuario_datos.ObtenerTipoUsuarioByUserName(usuario);
                    System.Web.HttpContext.Current.Session["SessionTipoUsuario"] = TipoUsario;
                    FormsAuthentication.SetAuthCookie(model.id_usuario, model.RememberMe);
                    HttpCookie authCookie = FormsAuthentication.GetAuthCookie(model.id_usuario, model.RememberMe);
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    List<string> listaPermiso = new List<string>();
                    foreach (var item in model.ListaPermisos)
                    {
                        listaPermiso.Add(item.NombreUrl);
                    }
                    System.Web.HttpContext.Current.Session["SessionListaPermiso"] = listaPermiso;
                    System.Web.HttpContext.Current.Session["NombreUsuario"] = model.nombreCompleto;
                    if (TipoUsario == 1)
                    {
                        return RedirectToAction("Index", "HomeAdmin", new { Area = "Admin" });
                    }
                    //else if (id_tipoUsuario == "1")
                    //{
                    //    return RedirectToAction("Index", "HomeProfesor", new { Area = "Profesor" });
                    //}
                    else
                    {
                        ModelState.AddModelError("", "No tienes permisos");
                        Session.Abandon();
                        Session.Clear();
                        Session.RemoveAll();
                        return View(model);
                    }
                }
                else if (model.opcion == 2)
                {
                    ModelState.AddModelError("", "Usuario no existe");
                    Session.Abandon();
                    Session.Clear();
                    Session.RemoveAll();
                    return View(model);
                }
                else if (model.opcion == 3)
                {
                    ModelState.AddModelError("", "Error de Contraseña");
                    Session.Abandon();
                    Session.Clear();
                    Session.RemoveAll();
                    return View(model);
                }
                else if (model.opcion == 4)
                {
                    ModelState.AddModelError("", "El usuario tiene que ser de tipo. Administrador");
                    Session.Abandon();
                    Session.Clear();
                    Session.RemoveAll();
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("", "El usuario o contraseña son incorrectos!!.");
                    Session.Abandon();
                    Session.Clear();
                    Session.RemoveAll();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Contacte a soporte técnico. " + ex.Message);
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();
                return View(model);
            }
        }

        // GET: /Account/LogOff
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Account");
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult CheckUserAvailability(string cuenta, string id_usuario)
        {
            UsuarioModels usuario = new UsuarioModels();
            _Usuario_Datos usuario_datos = new _Usuario_Datos();
            usuario.conexion = Conexion;
            usuario.cuenta = cuenta;
            usuario.id_usuario = id_usuario;
            return Json(usuario_datos.CheckUserName(usuario), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult CheckEmailAvailability(string email, string id_usuario)
        {
            UsuarioModels usuario = new UsuarioModels();
            _Usuario_Datos usuario_datos = new _Usuario_Datos();
            usuario.conexion = Conexion;
            usuario.Correo = email;
            usuario.id_usuario = id_usuario;
            return Json(usuario_datos.CheckEmail(usuario), JsonRequestBehavior.AllowGet);
        }

        // GET: /Account/GetPassword
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetPassword()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return RedirectToAction("Index");
            }
        }
        // POST: /Account/GetPassword
        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetPassword(FormCollection collection)
        {
            try
            {
                UsuarioModels usuario = new UsuarioModels();
                _Usuario_Datos usuario_datos = new _Usuario_Datos();
                usuario.conexion = Conexion;
                usuario.email2 = collection["email2"];
                usuario = usuario_datos.ResetPassword(usuario);

                ConfiguracionModels Configuracion = new ConfiguracionModels();
                _Configuracion_Datos ConfiguracionDatos = new _Configuracion_Datos();
                Configuracion.Conexion = Conexion;
                Configuracion.idTicket = 1;
                Configuracion = ConfiguracionDatos.ObtenerTicket(Configuracion);

                if (usuario.activo == true)
                {
                    Comun.EnviarCorreo(
                    Configuracion.CorreoTxt
                   , Configuracion.Password
                   , usuario.email2
                   , "Password reset Grupo Ocampo"
                   , Comun.GenerarHtmlResetContraseña(usuario.cuenta, usuario.Password)
                   , false
                   , ""
                   , Configuracion.HtmlTxt
                   , Configuracion.HostTxt
                   , Convert.ToInt32(Configuracion.PortTxt)
                   , Configuracion.EnableSslTxt);
                    //Comun.EnviarCorreo(
                    // ConfigurationManager.AppSettings.Get("CorreoTxt")
                    //, ConfigurationManager.AppSettings.Get("PasswordTxt")
                    //, usuario.email2
                    //, "Password reset grupo ocampo"
                    //, Comun.GenerarHtmlResetContraseña(usuario.cuenta, usuario.Password)
                    //, false
                    //, ""
                    //, Convert.ToBoolean(ConfigurationManager.AppSettings.Get("HtmlTxt"))
                    //, ConfigurationManager.AppSettings.Get("HostTxt")
                    //, Convert.ToInt32(ConfigurationManager.AppSettings.Get("PortTxt"))
                    //, Convert.ToBoolean(ConfigurationManager.AppSettings.Get("EnableSslTxt")));
                    TempData["typemessage"] = "1";
                    TempData["message"] = "Password reseateada correctamente";
                    ModelState.AddModelError("", "Password reseateada correctamente");
                }
                else
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] = "Correo no existente";
                    ModelState.AddModelError("", "Correo no existente");
                }
                return RedirectToAction("GetPassword");
            }
            catch (Exception)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Correo no existente";
                return RedirectToAction("GetPassword");
            }
        }
    }
}