using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class VerificacionJaulaModels
    {
        public int Id_verificacionJaula { get; set; }
        public string Id_flete { get; set; }
        public string Id_vehiculo { get; set; }
        public bool LimpiezaCompleta { get; set; }
        public bool SoloPiso { get; set; }
        public bool Sucia { get; set; }
        public bool PuertasInternas { get; set; }
        public bool Focos { get; set; }
        public bool RiesgosPunzoCortantes { get; set; }
        public bool LlantaRefaccion { get; set; }
        public bool LlantasBuenEstado { get; set; }
        public bool PisoAntiadherente { get; set; }
    }
}