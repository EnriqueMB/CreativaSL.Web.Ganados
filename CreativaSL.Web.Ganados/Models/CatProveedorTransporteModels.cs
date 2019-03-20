using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatProveedorTransporteModels
    {
        public int Id { get; set; }
        [Required]
        public string NombreRazonSocial { get; set; }
        public string RFC { get; set; }
        public string DomicilioFiscal { get; set; }
        public bool TodasLasSucursales { get; set; }
    }
}