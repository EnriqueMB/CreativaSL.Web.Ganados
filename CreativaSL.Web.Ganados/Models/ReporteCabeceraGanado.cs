using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ReporteCabeceraGanado
    {
        public string NombreChofer { get; set; }
        public string UnidadVehiculo { get; set; }
        public string ModeloVehiculo { get; set; }
        public string MarcaVehiculo { get; set; }
        public string ColorVehiculo { get; set; }
        public string CapacidadVehiculo { get; set; }
        public string GPS { get; set; }
        public string FechaHoraSalida { get; set; }
        public string FechaHoraEmbarque { get; set; }
        public string LugarOrigen { get; set; }
        public string LugarDestino { get; set; }
        public string PSGOrigen { get; set; }
        public string PSGDestino { get; set; }
        public int TotalGanadoMachos { get; set; }
        public int TotalGanadoHembras { get; set; }
        public int TotalGanado { get; set; }
        public decimal TotalKilosGanado { get; set; }
        public string PlacaTracto { get; set; }
        public string PlacaJaula { get; set; }
        public decimal TotalKilosGanadoMachos { get; set; }
        public decimal TotalKilosGanadoHembras { get; set; }

    }
}