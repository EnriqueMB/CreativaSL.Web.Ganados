using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class NominaConceptosFijosModels
    {
        public NominaConceptosFijosModels()
        {
            _IDConceptosFijo = string.Empty;
            _IDEmpleado = string.Empty;
            _Simbolo = string.Empty;
            Conexion = string.Empty;
            Usuario = string.Empty;
            _NombreConcepto = string.Empty;
            _NombreEmpleado = string.Empty;
        }

        private string _IDConceptosFijo;

        public string IDConceptosFijo
        {
            get { return _IDConceptosFijo; }
            set { _IDConceptosFijo = value; }
        }

        private int _IDConcepto;

        public int IDConcepto
        {
            get { return _IDConcepto; }
            set { _IDConcepto = value; }
        }

        private string _IDEmpleado;

        public string IDEmpleado
        {
            get { return _IDEmpleado; }
            set { _IDEmpleado = value; }
        }

        private string _NombreConcepto;

        public string NombreConcepto
        {
            get { return _NombreConcepto; }
            set { _NombreConcepto = value; }
        }

        private string _NombreEmpleado;

        public string NombreEmpleado
        {
            get { return _NombreEmpleado; }
            set { _NombreEmpleado = value; }
        }

        private decimal _Monto;

        public decimal Monto
        {
            get { return _Monto; }
            set { _Monto = value; }
        }

        private string _Simbolo;

        public string Simbolo
        {
            get { return _Simbolo; }
            set { _Simbolo = value; }
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