using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CmbTipoFleteModels
    {
        public CmbTipoFleteModels()
        {
            id_tipoFlete = string.Empty;
            descripcion = string.Empty;
        }

        public string id_tipoFlete { get; set; }
        public string descripcion { get; set; }
    }
}