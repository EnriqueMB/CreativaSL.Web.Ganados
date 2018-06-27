using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptSociosModels
    {
        public RptSociosModels()
        {
            _NombreCompleto = string.Empty;
            _Procentaje = 0;
            _ListaSocio = new List<RptSociosModels>();
        }

        private string _NombreCompleto;

        public string NombreCompleto
        {
            get { return _NombreCompleto; }
            set { _NombreCompleto = value; }
        }

        private int _Procentaje;

        public int Porcentaje
        {
            get { return _Procentaje; }
            set { _Procentaje = value; }
        }

        private List<RptSociosModels> _ListaSocio;

        public List<RptSociosModels> ListaSocios
        {
            get { return _ListaSocio; }
            set { _ListaSocio = value; }
        }

    }
}