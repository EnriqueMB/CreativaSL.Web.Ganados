using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DatosGeneralesGanados
    {
        public int TotalGanadoMachos { get; set; }
        public int TotalGanadoHembras { get; set; }
        public int TotalGanados { get; set; }

        public decimal TotalKilosGanadoMachos { get; set; }
        public decimal TotalKilosGanadoHembras { get; set; }
        public decimal TotalKilosGanados { get; set; }

        public decimal TotalMermaGanadoMachos { get; set; }
        public decimal TotalMermaGanadoHembras { get; set; }
        public decimal TotalMermaGanados { get; set; }

        public string StringTotalKilosGanadoMachos { get; set; }
        public string StringTotalKilosGanadoHembras { get; set; }
        public string StringTotalKilosGanados { get; set; }

        public string StringTotalMermaGanadoMachos { get; set; }
        public string StringTotalMermaGanadoHembras { get; set; }
        public string StringTotalMermaGanados { get; set; }
    }
}