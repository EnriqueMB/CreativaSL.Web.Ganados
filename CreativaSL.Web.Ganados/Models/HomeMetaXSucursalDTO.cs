using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class HomeMetaXSucursalDTO
    {
        public string NombreSucursal { get; set; }
        public decimal SucursalCantidadKilo { get; set; }
        public int  SucursalCantidadGanado { get; set; }
        public decimal MetaCantidadKilo { get; set; }
        public int MetaCantidadGanado { get; set; }
    }
}