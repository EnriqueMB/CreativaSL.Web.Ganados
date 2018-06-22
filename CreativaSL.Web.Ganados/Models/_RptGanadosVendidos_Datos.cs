using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _RptGanadosVendidos_Datos
    {
        public List<RptGandosModels> obtenerListaGanadosVendidos (RptGandosModels datos)
        {
            object[] parametros = { datos.FechaInicio, datos.FechaFin};
            List<RptGandosModels> lista = new List<RptGandosModels>();
            
        }
    }
}