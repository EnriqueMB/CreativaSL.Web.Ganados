using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class Flete_ProductosServiciosModels
    {
        public string IDProducto { get; set; }
        public string IDFlete { get; set; }
        public string IDGanado { get; set; }
        public string ClaveProductoServicio { get; set; }
        public double Cantidad { get; set; }
        public int ID_UnidadMedida { get; set; }
        public float pesoAproximado { get; set; }
        public string Observacion { get; set; }

    }
}