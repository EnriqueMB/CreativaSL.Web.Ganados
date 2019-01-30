using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ImagenEmpresaModels
    {
        public int Id { get; set; }
        public string Id_empresa { get; set; }
        public string Descripcion { get; set; }
        public string UrlArchivo { get; set; }
        public HttpPostedFileBase Archivo { get; set; }

     
    }
}