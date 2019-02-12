using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class GanadoParaVender
    {
        public string Id_ganado { get; set; }
        public decimal MermaExtra { get; set; }
        public decimal Subtotal { get; set; }
    }

  
}