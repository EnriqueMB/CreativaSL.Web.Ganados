using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class NominaConceptosEmpModels
    {
        public NominaConceptosEmpModels()
        {
            _IDConceptoEmpleado = string.Empty;
            _IDEmpleado = string.Empty;
            _IDNomina = string.Empty;
            Conexion = string.Empty;
            Usuario = string.Empty;
            _NombreConcepto = string.Empty;
            _Simbolo = string.Empty;
        }

        private string _IDConceptoEmpleado;

        public string IDConceptoEmpleado
        {
            get { return _IDConceptoEmpleado; }
            set { _IDConceptoEmpleado = value; }
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

        private decimal _Monto;

        public decimal Monto
        {
            get { return _Monto; }
            set { _Monto = value; }
        }

        private string _IDNomina;

        public string IDNomina
        {
            get { return _IDNomina; }
            set { _IDNomina = value; }
        }

        private string _NombreConcepto;

        public string NombreConcepto
        {
            get { return _NombreConcepto; }
            set { _NombreConcepto = value; }
        }
        private string _Simbolo;

        public string Simbolo
        {
            get { return _Simbolo; }
            set { _Simbolo = value; }
        }
        private bool _Revisado;

        public bool Revisado
        {
            get { return _Revisado; }
            set { _Revisado = value; }
        }

        private bool _EsFijo;

        public bool EsFijo
        {
            get { return _EsFijo; }
            set { _EsFijo = value; }
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