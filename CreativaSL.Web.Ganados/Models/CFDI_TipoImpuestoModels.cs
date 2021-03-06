﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CFDI_TipoImpuestoModels
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleccione un Tipo de Impuesto.")]
        public int Clave { get; set; }
        public string Descripcion { get; set; }
    }
}