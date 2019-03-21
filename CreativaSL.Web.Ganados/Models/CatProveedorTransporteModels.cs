using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatProveedorTransporteModels
    {
        public string Id { get; set; }
        [Required]
        public string RazonFiscal { get; set; }
        public string DireccionFiscal { get; set; }
        public string RFC { get; set; }
        public string PsgEmpresa { get; set; }
    }
}