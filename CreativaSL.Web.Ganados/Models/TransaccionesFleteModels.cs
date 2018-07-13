using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class TransaccionesFleteModels
    {
        public string Id_flete { get; set; }
        public string NombreCliente { get; set; }
        public string RFC_Cliente { get; set; }
        public string NombreConductor { get; set; }
        public string Vehiculo { get; set; }
        public string PlacaVehiculo { get; set; }
        public DocumentosPorCobrarModels DocumentosPorCobrar { get; set; }
        public string Conexion { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }
    }
}