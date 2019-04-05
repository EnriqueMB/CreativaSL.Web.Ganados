using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class VentaGeneralDetalleModels
    {
        public int Id { get; set; }
        public int Fk_id { get; set; }
        public string Id_producto { get; set; }
        public int Id_tipoProducto { get; set; }
        public string Id_documentoPorCobrar { get; set; }

        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public string Id_almacen { get; set; }
        public string Id_unidadProducto { get; set; }
    }
}