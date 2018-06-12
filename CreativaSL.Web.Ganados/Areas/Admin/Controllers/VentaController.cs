using CreativaSL.Web.Ganados.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using CreativaSL.Web.Ganados.Models;
using System.Configuration;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    public class VentaController : Controller
    {
        private TokenProcessor Token = TokenProcessor.GetInstance();
        private string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/Venta
        public ActionResult Index()
        {
            return View();
        }

        #region Vista Ganado
        [HttpGet]
        public ActionResult VentaGanado()
        {
            try
            {
                Token.SaveToken();
                return View();
            }
            catch (Exception ex)
            {
                TempData["typemessage"] = "2";
                TempData["message"] = "No se puede cargar la vista, error: " + ex.ToString();
                throw;
            }
        }
        #endregion
   

        #region Modal evento
        [HttpPost]
        public ActionResult ModalEvento()
        {
            return PartialView("ModalEvento");
        }
        #endregion
    }
}
