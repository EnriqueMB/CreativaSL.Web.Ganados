using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Filters
{
    public class TransferenciaGanadoAttribute : ActionFilterAttribute
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                _CatGanado_Datos ganado_Datos = new _CatGanado_Datos();
                IPrincipal user = HttpContext.Current.User;
                string id_persona = user.Identity.Name;
                bool puedeTransferirGanado = ganado_Datos.PuedeTransferirGanado(Conexion, id_persona);
                HttpContext.Current.Session["PuedeTransferirGanado"] = puedeTransferirGanado;
            }
        }
    }
}