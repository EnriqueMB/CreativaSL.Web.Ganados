using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class PesajeGanadoModels
    {
        public int Id { get; set; }

        [Required]
        public string Id_sucursal { get; set; }

        public string Id_cliente { get; set; }
        public string Id_documentPorCobrar { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor, ingrese un número mayor a: {1}")]
        public decimal PesoTotal { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Por favor, ingrese un número mayor a: {1}")]
        public decimal MontoPorCobrar { get; set; }

        public bool Finalizado { get; set; }

        public RespuestaAjax Respuesta { get; set; }
    }
}