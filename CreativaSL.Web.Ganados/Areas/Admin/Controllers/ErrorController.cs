using CreativaSL.Web.Ganados.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class ErrorController : Controller
    {
        // GET: Error
        //[Compress]
        //[RequireHttps]
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Client)]
        public ActionResult Index(int error = 0)
        {
            switch (error)
            {
                case 500:
                    ViewBag.Num = error;
                    ViewBag.Title = "Se excedió la longitud de solicitud máxima.";
                    ViewBag.Description = "El limite permitido para subir es de 8MB.";
                    break;

                case 505:
                    ViewBag.Num = error;
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Esto es muy vergonzoso, esperemos que no vuelva a pasar ..";
                    break;

                case 404:
                    ViewBag.Num = error;
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "La URL que está intentando ingresar no existe";
                    Response.TrySkipIisCustomErrors = true;
                    Response.StatusCode = 404;
                    break;

                default:
                    ViewBag.Num = error;
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "Algo salio muy mal :( ..";
                    break;
            }
            return View();
            //return RedirectToAction("PageNotFound", "Error");
            //return View("~/Area/Admin/Views/Error/PageNotFound.cshtml");
        }

        public ActionResult PageNotFound()
        {
            return View();
        }
    }
}