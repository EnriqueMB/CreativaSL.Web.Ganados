using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class TransaccionesModels
    {
        /// <summary>
        /// Id generico del modulo (el modulo, puede ser compra, venta, flete, pesajeGanado o venta de activo)
        /// </summary>
        public string IdModuloGenerico { get; set; }

        public string Id_documentoCobrar { get; set; }

        public decimal TotalPercepciones { get; set; }
        public decimal TotalDeducciones { get; set; }
        public decimal Total { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Pagos { get; set; }
        public decimal Pendiente { get; set; }
        public RespuestaAjax Respuesta { get; set; }
    }
}