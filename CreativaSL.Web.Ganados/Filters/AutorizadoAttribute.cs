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
    public class AutorizadoAttribute : ActionFilterAttribute
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.Session["SessionTipoUsuario"] == null)
                {
                    UsuarioModels Usuario = new UsuarioModels();
                    _Usuario_Datos usuario_datos = new _Usuario_Datos();
                    Usuario.conexion = Conexion;
                    IPrincipal user = HttpContext.Current.User;
                    Usuario.cuenta = user.Identity.Name;
                    int TipoUsario = usuario_datos.ObtenerTipoUsuarioByUserName(Usuario);
                    HttpContext.Current.Session["SessionTipoUsuario"] = TipoUsario;
                }
                else
                {
                    int TipoUsuario = (int)HttpContext.Current.Session["SessionTipoUsuario"];
                    HttpContext.Current.Session["SessionTipoUsuario"] = TipoUsuario;
                }
                if (HttpContext.Current.Session["SessionListaPermiso"] == null)
                {
                    UsuarioModels Usuario = new UsuarioModels();

                    LoginDatos LoginD = new LoginDatos();
                    Usuario.conexion = Conexion;
                    IPrincipal user = HttpContext.Current.User;
                    Usuario.cuenta = user.Identity.Name;
                    Usuario = LoginD.ObtenerPermisos(Usuario);
                    List<string> ListaPermiso = new List<string>();
                    foreach (var item in Usuario.ListaPermisos)
                    {
                        ListaPermiso.Add(item.NombreUrl);
                    }
                    HttpContext.Current.Session["SessionListaPermiso"] = ListaPermiso;
                }
                else
                {
                    
                    List<string> ListaPermiso = new List<string>();
                    ListaPermiso = (List<string>)HttpContext.Current.Session["SessionListaPermiso"];
                    HttpContext.Current.Session["SessionListaPermiso"] = ListaPermiso;
                }
                if (HttpContext.Current.Session["NombreUsuario"] == null)
                {
                    UsuarioModels CuentaUsuario = new UsuarioModels();
                    CuentaUsuario.conexion = Conexion;
                    IPrincipal user = HttpContext.Current.User;
                    CuentaUsuario.id_usuario = user.Identity.Name;
                    int TipoUsuario = (int)HttpContext.Current.Session["SessionTipoUsuario"];
                    CuentaUsuario.id_tipoUsuario = TipoUsuario;
                    CuentaUsuario = _Usuario_Datos.ObtenerUsuario(CuentaUsuario);
                    HttpContext.Current.Session["NombreUsuario"] = CuentaUsuario.tablaUsuario.Rows[0]["Nombre"];
                }
                else
                {
                    string NombreUsuario = (string)HttpContext.Current.Session["NombreUsuario"];
                    HttpContext.Current.Session["NombreUsuario"] = NombreUsuario;
                }
            }
        }
    }
}