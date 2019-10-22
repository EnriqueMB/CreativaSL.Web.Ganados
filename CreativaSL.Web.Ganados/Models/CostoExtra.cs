using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CostoExtra
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string IdVenta { get; set; }
        [Required]
        [StringLength(300)]
        public string NombreProducto { get; set; }
        [Required]
        [StringLength(100)]
        public string NombreUnidadMedida { get; set; }
        [Required]
        public decimal Cantidad { get; set; }
        [Required]
        public decimal PrecioUnitario { get; set; }
        [Required]
        public decimal Subtotal { get; set; }
        public string Observacion { get; set; }
    }
}