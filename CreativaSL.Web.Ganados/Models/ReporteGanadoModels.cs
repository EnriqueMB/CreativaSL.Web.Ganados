using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ReporteGanadoModels
    {
        public string NumArete { get; set; }
        public string Genero { get; set; }
        public string ImagenBase64_1 { get; set; }
        public string ImagenBase64_2 { get; set; }
        public string ImagenBase64_3 { get; set; }
        public string NombreCorral { get; set; }
        public string CompradoA { get; set; }
        public string PesoInicial { get; set; }
        public string PesoFinal { get; set; }
        public string PesoPagado { get; set; }
        public string PrecioPorKilo { get; set; }
        public string Subtotal { get; set; }
        public string Merma { get; set; }
        public bool Repeso { get; set; }
    }
}