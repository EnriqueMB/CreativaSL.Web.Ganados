using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CalendarioServiciosModels
    {
        public string Servicio { get; set; }
        public string Encargado { get; set; }
        public string Vehiculo { get; set; }
        public string Sucursal { get; set; }
        public string Proveedor { get; set; }
        public string FechaUltServicio { get; set; }
        public string FechaProxServicio { get; set; }
        public string GastoTotal { get; set; }
        public string Estatus { get; set; }
        public string ColorEstatus { get; set; }
        public string Letras { get; set; }

        #region Datos
        public string Conexion { get; set; }
        public DateTime fechaStart { get; set; }
        public DateTime fechaEnd { get; set; }
        public string start { get; set; }
        public string title { get; set; }
        #endregion
    }
}