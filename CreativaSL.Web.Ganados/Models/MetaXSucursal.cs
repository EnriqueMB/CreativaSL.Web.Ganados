using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class MetaXSucursal : RespuestaAjax
    { 
        public string Id { get; set; }
        public string Id_sucursal { get; set; }
        public decimal CantidadKilo { get; set; }
        public decimal CantidadGanado { get; set; }
    }
}