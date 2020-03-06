using CreativaSL.Web.Ganados.App_Start;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Models.Datatable;
using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models.System;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    
    public class TicketController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        #region Index
        public ActionResult Index()
        {
            try
            {
                ConfiguracionModels ticket = new ConfiguracionModels();
                _Configuracion_Datos datos = new _Configuracion_Datos();
                ticket.Conexion = Conexion;
                ticket.listaTicket = datos.ObtenerListaTicket(ticket);
                return View(ticket);
            }
            catch (Exception ex)
            {
                ConfiguracionModels ticket = new ConfiguracionModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(ticket);
            }
        } 
        #endregion

        #region Editar
        public ActionResult Edit(int id)
        {
            try
            {
                Token.SaveToken();
                ConfiguracionModels ticket = new ConfiguracionModels();
                _Configuracion_Datos datos = new _Configuracion_Datos();
                ticket.Conexion = Conexion;
                ticket.idTicket = id;
                ticket = datos.ObtenerTicket(ticket);

                return View(ticket);
            }
            catch (Exception ex)
            {
                ConfiguracionModels ticket = new ConfiguracionModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(ticket);
            }
        }
        [HttpPost]
        public ActionResult Edit(int id, ConfiguracionModels ticket)
        {
            try
            {
                if (Token.IsTokenValid())
                {
                    if (ModelState.IsValid)
                    {
                        _Configuracion_Datos datos = new _Configuracion_Datos();
                        ticket.Conexion = Conexion;
                        ticket.idTicket = id;
                        ticket.Usuario = User.Identity.Name;
                        ticket.textoTicket1 = "Uno";
                        ticket.textoTicket2 = "Dos";
                        ticket.textoTicket3 = "Tres";
                        ticket = datos.C_Configuracion(ticket);
                        if (ticket.Completado == true)
                        {
                            TempData["typemessage"] = "1";
                            TempData["message"] = "Los datos se guardarón correctamente.";
                            Token.ResetToken();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["typemessage"] = "2";
                            TempData["message"] = "Ocurrio un error al intentar guardar los datos. Intente más tarde.";
                            return View(ticket);
                        }
                    }
                    else
                    {
                        return View(ticket);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrio un error al intentar guardar los datos. Contacte a soporte técnico.";
                return View(ticket);
            }
        } 
        #endregion

        #region Asignar sucursal
        [HttpGet]
        public ActionResult AsignarSucursal()
        {
            var idSucursalAsignada = Auxiliar.IdSucursalAsignada;
            var datosSucursal = new _CatSucursal_Datos();
            ViewBag.IdSucursalAsignada = idSucursalAsignada;
            ViewBag.ListaSucursales = datosSucursal.GetSucursales(Conexion);

            return View();
        }

        [HttpPost]
        public ActionResult AsignarSucursal(string sucursales)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sucursales))
                {
                    TempData["typemessage"] = "2";
                    TempData["message"] =
                        "Verifique sus datos.";

                    return RedirectToAction("Index");
                }
                var webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                //Modifying the AppKey from AppValue to AppValue1
                webConfigApp.AppSettings.Settings["idSucursalAsignada"].Value = sucursales;
                //Save the Modified settings of AppSettings.
                webConfigApp.Save();
            }
            catch (Exception e)
            {
                TempData["typemessage"] = "2";
                TempData["message"] =
                    "Se ha producido un error al asignar el equipo, intentelo de nuevo más tarde o contacte con soporte técnico.";

                return RedirectToAction("Index");
            }
            TempData["typemessage"] = "1";
            TempData["message"] = "Equipo asiganado correctamente.";

            return RedirectToAction("Index");
        }
        #endregion

        #region Tablas base 64 a url
        [HttpGet]
        public ActionResult TablasBase64ToUrl()
        {
            return View();
        }
 
        public ActionResult RegistrosDeTablaBase64ToUrl(int id)
        {
            ViewBag.IdTabla = id;
            return View();
        }



        #endregion

        #region DataTable
        [HttpPost]
        public ActionResult JsonRegistrosDeTablaBase64ToUrl(DataTableAjaxPostModel dataTableAjaxPostModel, int idTabla)
        {
            var datos = new _Configuracion_Datos();
            var baseDir = string.Empty;
            switch (idTabla)
            {
                case 1:
                    baseDir = ProjectSettings.BaseDirCajaChicaChequeComprobante;
                    break;
            }

            var json = datos.ObtenerRegistrosTablaBase64ToUrl(idTabla, dataTableAjaxPostModel, baseDir);

            return Content(json, "application/json");
        }


        #endregion
    }
}