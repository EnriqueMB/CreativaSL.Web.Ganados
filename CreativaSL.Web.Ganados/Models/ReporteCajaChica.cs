using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ReporteCajaChica
    {
        public List<MovimientosCajaChicaModels> ListaMovimientos { get; set; }

        public List<MovimientosCajaChicaModels> ListaMovimientosCheque { get; set; }

        public List<ArqueoCajaChicaModels> ListaDenominaciones { get; set; }

        public List<ConceptosCajaChicaModels> ListaConceptos { get; set; }
    }
}