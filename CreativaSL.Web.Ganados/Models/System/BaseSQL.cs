using System.Configuration;
using System.Web;

namespace CreativaSL.Web.Ganados.Models.System
{
    public class BaseSQL
    {
        protected string ConexionSql { get; set; }
        protected string UsuarioActual { get; set; }

        public BaseSQL()
        {
            ConexionSql = ConfigurationManager.AppSettings.Get("strConnection");
            UsuarioActual = HttpContext.Current.User.Identity.Name;
        }
    }
}