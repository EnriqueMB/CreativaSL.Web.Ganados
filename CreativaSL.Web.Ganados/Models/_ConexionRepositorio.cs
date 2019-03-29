using System.Configuration;
namespace CreativaSL.Web.Ganados.Models
{
    public  class _ConexionRepositorio
    {
        
        public static string CadenaConexion
        {
            get { return ConfigurationManager.AppSettings.Get("strConnection"); ; }
        }

    }
}