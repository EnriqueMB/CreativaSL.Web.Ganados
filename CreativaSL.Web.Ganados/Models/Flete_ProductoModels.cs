using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class Flete_ProductoModels
    {
        public string ID_FleteProducto { get; set; }
        public string ID_Flete { get; set; }
        public int ID_UnidadMedida { get; set; }
        public string ID_EstatusProducto { get; set; }
        public double PesoAproximado { get; set; }
        public double Cantidad { get; set; }
        public bool Propio { get; set; }
        public string Observacion { get; set; }

        public Flete_ProductoPropioModels Flete_ProductoPropio { get; set; }

        public string Conexion { get; set; }
        public string Usuario { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }
    }
}