using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatRangoPesoVentaModels
    {
        public int Id_rango { get; set; }
        [Required]
        public int Id_tipoCliente { get; set; }

        public bool EsMacho { get; set; }

        [Required(ErrorMessage = "El peso minimo es obligatorio")]
        [Display(Name = "peso minimo")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduzca un número mayor a 0")]
        [Peso(ErrorMessage = "Solo números enteros")]
        public decimal PesoMinimo { get; set; }

        [Required(ErrorMessage = "El peso maximo es obligatorio")]
        [Display(Name = "peso maximo")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduzca un número mayor a 0")]
        [Peso(ErrorMessage = "Solo números enteros")]
        public decimal PesoMaximo { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Display(Name = "precio")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduzca un número mayor a 0")]
        [Precio(ErrorMessage = "Solo números y decimales")]
        public decimal Precio { get; set; }

        public string NombreRango { get; set; }
    }
}