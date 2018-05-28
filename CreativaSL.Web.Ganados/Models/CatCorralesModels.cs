using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatCorralesModels
    {
        public int Id_corral { get; set; }
        public string Descripcion { get; set; }
        public int Rango_inferior { get; set; }
        public int Rango_superior { get; set; }
        public string Genero { get; set; }
    }
}
