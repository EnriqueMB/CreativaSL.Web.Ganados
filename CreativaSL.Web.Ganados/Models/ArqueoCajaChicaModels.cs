using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ArqueoCajaChicaModels
    {
        public int IdDenominacion { get; set; }
        public string IdTipo { get; set; }
        public string Descripcion { get; set; }
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "Ingrese la cantidad en la fila correspondiente.")]
        [Display(Name = "Cantidad")]
        [EnteroAttribute(ErrorMessage = "Ingrese un dato válido")]
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
    }
}