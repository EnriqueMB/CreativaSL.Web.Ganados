using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class RptGandosModels
    {
        private int _GanadosMachos;

        public int GanadosMachos
        {
            get { return _GanadosMachos; }
            set { _GanadosMachos = value; }
        }
        private int _GandosHembras;

        public int GanadosHembras
        {
            get { return _GandosHembras; }
            set { _GandosHembras = value; }
        }
        private int _GanadosTotal;

        public int GanadosTotal
        {
            get { return _GanadosTotal; }
            set { _GanadosTotal = value; }
        }

    }
}