using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptVentaCompraDetalles
    {
        public string IdSucursalVenta { get; set; }
        public string NombreSucursal { get; set; }
        public string IdVenta { get; set; }
        public long FolioVenta { get; set; }
        public decimal Compra { get; set; }
        public decimal Venta { get; set; }
        public decimal Utilidad { get; set; }
    }
}