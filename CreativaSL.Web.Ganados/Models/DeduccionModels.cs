using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DeduccionModels 
    {
        public string IdGenerico { get; set; } //por si acaso se utiliza en otra vista, aqui seria el id de la compra

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una deducción")]
        public int IdDeduccion { get; set; }

        public string IdDetalleDoctoPagar { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un monto de la deducción")]
        public decimal Monto { get; set; }
    }
}