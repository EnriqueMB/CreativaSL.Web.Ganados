using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class NominaDetalleModels
    {
        public NominaDetalleModels()
        {
            _IDNomina = string.Empty;
            _IDEmpleado = string.Empty;
            _NombreEmpleado = string.Empty;
            _diasLaborados = 0;
            Sueldo = 0;
            CategoriaPuesto = string.Empty;
            _puesto = string.Empty;
        }

        private string _IDNomina;

        public string IDNomina
        {
            get { return _IDNomina; }
            set { _IDNomina = value; }
        }

        private string _NombreEmpleado;

        public string NombreEmpleado
        {
            get { return _NombreEmpleado; }
            set { _NombreEmpleado = value; }
        }


        private string _IDEmpleado;

        public string IDEmpleado
        {
            get { return _IDEmpleado; }
            set { _IDEmpleado = value; }
        }

        private decimal _Percepciones;

        public decimal Percepciones
        {
            get { return _Percepciones; }
            set { _Percepciones = value; }
        }

        private decimal _Deducciones;

        public decimal Deducciones
        {
            get { return _Deducciones; }
            set { _Deducciones = value; }
        }

        private decimal _Total;

        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        public decimal Sueldo { get; set; }
        public string CategoriaPuesto { get; set; }

        private string _puesto;

        public string Puesto
        {
            get { return _puesto; }
            set { _puesto = value; }
        }


        private bool _Pagado;

        public bool Pagado
        {
            get { return _Pagado; }
            set { _Pagado = value; }
        }
        private int _diasLaborados;

        public int DiasLaborados
        {
            get { return _diasLaborados; }
            set { _diasLaborados = value; }
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