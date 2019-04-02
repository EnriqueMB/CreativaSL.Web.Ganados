using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class VentaGeneralModels
    {
        public int Id { get; set; }
        public string Id_sucursal { get; set; }
        public string Id_cliente { get; set; }
        public string Id_documentoPorCobrar { get; set; }
        public DateTime FechaFinalizacion { get; set; }

        public List<VentaGeneralDetalleModels> ListaDetalles { get; set; }
    }
}