using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatEmpresa
    {
        [Required]
        public string id_empresa { get; set; }

        [Required]
        [DisplayName("razón fiscal")]
        [StringLength(150, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 10)]
        public string RazonFiscal { get; set; }

        [DisplayName("direccion fiscal")]
        [StringLength(150, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 10)]
        public string DireccionFiscal { get; set; }

        [RFCAttribute]
        [StringLength(150, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 10)]
        public string RFC { get; set; }




        public string direccionComercial { get; set; }
    }
}