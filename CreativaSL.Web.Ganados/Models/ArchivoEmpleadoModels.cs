﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ArchivoEmpleadoModels
    {
        public int Id { get; set; }
        [Required]
        public string Id_empleado { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public string UrlArchivo { get; set; }
        [Required]
        public HttpPostedFileBase Archivo { get; set; }
        public string NombreArchivo { get; set; }
    }
}