using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatEstatusDocumentoModels
    {
        public CatEstatusDocumentoModels()
        {
            id_estatusDocumento = 0;
            descripcion = string.Empty;
        }
        public int id_estatusDocumento { get; set; }
        public string descripcion { get; set; }
    }
}