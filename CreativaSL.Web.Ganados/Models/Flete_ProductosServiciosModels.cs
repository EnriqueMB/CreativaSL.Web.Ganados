using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class Flete_ProductosServiciosModels
    {
        public string IDProductoFlete { get; set; }
        public string IDFlete { get; set; }
        public string IDProductoSeleccionado { get; set; }
        public double Cantidad { get; set; }
        public int ID_UnidadMedida { get; set; }
        public float PesoAproximado { get; set; }
        public string Observacion { get; set; }

        public string Conexion { get; set; }
        public string Usuario { get; set; }
        public string ListaStringIDProductoSeleccionado { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }
    }
}