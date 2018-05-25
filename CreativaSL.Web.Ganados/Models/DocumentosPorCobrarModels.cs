﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentosPorCobrarModels
    {
        public string Id_documentoCobrar { get; set; }
        public string Id_sucursal { get; set; }
        public string Id_metodoPago { get; set; }
        public DateTime Fecha { get; set; }
        public bool EsSistema { get; set; }
        public decimal Impuesto_retenido { get; set; }
        public decimal Impuesto_trasladado { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }
        public decimal Cambio { get; set; }
        public decimal Pagos { get; set; }
        public decimal Pendiente { get; set; }
    }
}