using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Filters;
using System.Configuration;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class HomeAdminController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                CalendarioModels Compra = new CalendarioModels();
                _Compra_Datos CompraDatos = new _Compra_Datos();
                Compra.Conexion = Conexion;
                //Compra.listaCompra = CompraDatos.GetListaComprasNofinalizadas(Compra);
                return View(Compra);
            }
            catch (Exception)
            {
                CompraModels Compra = new CompraModels();
                Compra.listaCompra = new List<CompraModels>();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Compra);
            }
        }
        [HttpPost]
        public ActionResult Eventos(string start,string end)
        {
            try
            {
                CalendarioModels Compra = new CalendarioModels();
                _Compra_Datos CompraDatos = new _Compra_Datos();
                Compra.fechaStart = Convert.ToDateTime(start);
                Compra.fechaEnd = Convert.ToDateTime(end);
                Compra.Conexion = Conexion;
                Compra.listaCalendario = CompraDatos.GetListaComprasNofinalizadas(Compra);


                return Json(Compra.listaCalendario, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al cargar las actividades. Por favor contacte a soporte técnico.";
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EventosFlete(string start, string end)
        {
            try
            {
                CalendarioFleteModels Item = new CalendarioFleteModels();
                _Compra_Datos CompraDatos = new _Compra_Datos();
                Item.fechaStart = Convert.ToDateTime(start);
                Item.fechaEnd = Convert.ToDateTime(end);
                Item.Conexion = Conexion;

                List<CalendarioFleteModels> Lista = new List<CalendarioFleteModels>();
                Lista = CompraDatos.GetListaFletesNofinalizadas(Item);

                return Json(Lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "Ocurrió un error al cargar las actividades. Por favor contacte a soporte técnico.";
                ex.Message.ToString();
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LoadMenu()
        {
            try
            {
                MenuModels Menu = new MenuModels();
                Menu_Datos MenuD = new Menu_Datos();
                Menu.Conexion = Conexion;
                Menu.User = User.Identity.Name;
                Menu.TipoMenu = 1;
                Menu = MenuD.ObtenerAllPermisoUsuario(Menu);
                return View(Menu);
            }
            catch (Exception)
            {
                MenuModels Menu = new MenuModels();
                return View(Menu);
            }
        }
    }
}
