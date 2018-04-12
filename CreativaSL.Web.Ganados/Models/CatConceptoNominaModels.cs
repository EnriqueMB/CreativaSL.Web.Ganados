using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatConceptoNominaModels
    {
        private int _IDConceptoNomina;

        public int IDConceptoNomina
        {
            get { return _IDConceptoNomina; }
            set { _IDConceptoNomina = value; }
        }

        private string _Descripcion;

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private bool _Calculado;

        public bool Calculado
        {
            get { return _Calculado; }
            set { _Calculado = value; }
        }

        private bool _SumaResta;

        public bool SumaResta
        {
            get { return _SumaResta; }
            set { _SumaResta = value; }
        }

        private bool _SoloLectura;

        public bool SoloLectura
        {
            get { return _SoloLectura; }
            set { _SoloLectura = value; }
        }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}