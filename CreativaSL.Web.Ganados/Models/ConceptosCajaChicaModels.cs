using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ConceptosCajaChicaModels
    {
        public int IdConcepto { get; set; }
        public string Descripcion { get; set; }
        public decimal Importe { get; set; }

        public string ImporteString
        {
            get { return string.Format("{0:c}", Importe); }
        }

    }
}