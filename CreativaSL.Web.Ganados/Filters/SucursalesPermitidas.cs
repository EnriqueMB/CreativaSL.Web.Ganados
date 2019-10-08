using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using System.Security.Principal;
using CreativaSL.Web.Ganados.Models;

namespace CreativaSL.Web.Ganados.Filters
{
    
    public class SucursalesPermitidas : ActionFilterAttribute
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.Session["lista_id_sucursales"] == null)
                {
                    _CatSucursal_Datos sucursal_Datos = new _CatSucursal_Datos();
                    IPrincipal user = HttpContext.Current.User;
                    string id_persona = user.Identity.Name;
                    List<CatSucursalesModels> lista_sucursales = new List<CatSucursalesModels>();
                    List<string> lista_id_sucursales = new List<string>();
                    lista_sucursales = sucursal_Datos.GetSucursalesXPersona(Conexion, id_persona);
                    if(lista_sucursales != null || lista_sucursales.Count == 0)
                    {
                        foreach (var sucursal in lista_sucursales)
                        {
                            lista_id_sucursales.Add(sucursal.IDSucursal);
                        }
                    }
                    
                    HttpContext.Current.Session["lista_id_sucursales"] = lista_id_sucursales;
                }
                else
                {

                    List<string> lista_id_sucursales = new List<string>();
                    lista_id_sucursales = (List<string>)HttpContext.Current.Session["lista_id_sucursales"];
                    HttpContext.Current.Session["lista_id_sucursales"] = lista_id_sucursales;
                }
            }
        }
    }

}