using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class CuentaEmpleadoViewModels
    {
        public string IdEmpleado { get; set; }
        public int IdTipoUsuario { get; set; }
        public string Cuenta { get; set; }
        public string Password { get; set; }
        public string ConfirmaPassword { get; set; }
        public string Correo { get; set; }
    }
}