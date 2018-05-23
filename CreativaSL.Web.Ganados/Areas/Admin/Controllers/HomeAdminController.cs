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
                Compra.listaCompra = CompraDatos.GetListaComprasNofinalizadas(Compra);
                return View(Compra);
            }
            catch (Exception)
            {
                CompraModels Compra = new CompraModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista";
                return View(Compra);
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
