using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatEstatusGanadoModels
    {
        public CatEstatusGanadoModels()
        {
            id_estatusGanado = 0;
            descripcion = string.Empty;
        }

        public int id_estatusGanado { get; set; }
        public string descripcion { get; set; }

    }
}