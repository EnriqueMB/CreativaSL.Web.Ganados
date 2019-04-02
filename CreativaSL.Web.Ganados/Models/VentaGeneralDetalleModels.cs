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
    }
}