using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DeduccionModels 
    {
        /// <summary>
        /// Por si acaso se utiliza en otra vista, aqui seria el id de la compra, venta o flete
        /// </summary>
        public string IdGenerico { get; set; }

        [Required]
        public int IdDeduccion { get; set; }

        /// <summary>
        /// Id del documento, puede ser el del documento por pagar o cobrar
        /// </summary>
        public string IdDocumento { get; set; } 
        /// <summary>
        /// Id del detalle, puede ser el del documento por pagar o cobrar
        /// </summary>
        public string IdDetalleDocto { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un monto de la deducción")]
        public decimal Monto { get; set; }

        public int Id_conceptoDocumento { get; set; }
    }
}