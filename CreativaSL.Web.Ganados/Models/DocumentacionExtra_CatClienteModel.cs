using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentacionExtra_CatClienteModel
    {
        public string IdDocumentacionExtra { get; set; }

        public int IdTipoDocumentacionExtra { get; set; }

        public string IdCliente { get; set; }

        public string Archivo { get; set; }
    }
}