using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatTipoClasificacionCobroModels
    {
        public int Id_tipoClasificacionCobro { get; set; }
        public string Descripcion { get; set; }
        public bool Sistema { get; set; }
        public bool Inventario { get; set; }
    }
}