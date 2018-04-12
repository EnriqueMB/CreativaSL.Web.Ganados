using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class NominaResumenDetalleModels
    {

        public NominaResumenDetalleModels()
        {
            _IDResumenNominaDetalle = string.Empty;
            _IDNomina = string.Empty;
            _IDEmpleado = string.Empty;
        }

        private string _IDResumenNominaDetalle;

        public string IDResumenNominaDetalle
        {
            get { return _IDResumenNominaDetalle; }
            set { _IDResumenNominaDetalle = value; }
        }

        private string _IDNomina;

        public string IDNomina
        {
            get { return _IDNomina; }
            set { _IDNomina = value; }
        }

        private string _IDEmpleado;

        public string IDEmpleado
        {
            get { return _IDEmpleado; }
            set { _IDEmpleado = value; }
        }

        private string _NombreEmpleado;

        public string NombreEmpleado
        {
            get { return _NombreEmpleado; }
            set { _NombreEmpleado = value; }
        }


        private int _DiasPeriodo;

        public int DiasPeriodo
        {
            get { return _DiasPeriodo; }
            set { _DiasPeriodo = value; }
        }

        private int _DiasLaborados;

        public int DiasLaborados
        {
            get { return _DiasLaborados; }
            set { _DiasLaborados = value; }
        }

        private int _DiasDescanso;

        public int DiasDescanso
        {
            get { return _DiasDescanso; }
            set { _DiasDescanso = value; }
        }

        private int _Faltas;

        public int Faltas
        {
            get { return _Faltas; }
            set { _Faltas = value; }
        }

        private int _Retardos;

        public int Retardos
        {
            get { return _Retardos; }
            set { _Retardos = value; }
        }

        private int _FaltasRetardo;

        public int FaltasRetardo
        {
            get { return _FaltasRetardo; }
            set { _FaltasRetardo = value; }
        }

        private int _DiasFestivos;

        public int DiasFestivos
        {
            get { return _DiasFestivos; }
            set { _DiasFestivos = value; }
        }

        private int _DiasVacaciones;

        public int DiasVacaciones
        {
            get { return _DiasVacaciones; }
            set { _DiasVacaciones = value; }
        }

        private int _DiasDescuentos;

        public int DiasDescuentos
        {
            get { return _DiasDescuentos; }
            set { _DiasDescuentos = value; }
        }

        private int _DiasDomingo;

        public int DiasDomingo
        {
            get { return _DiasDomingo; }
            set { _DiasDomingo = value; }
        }

        private int _DiasDescuentoFaltas;

        public int DiasDescuentoFaltas
        {
            get { return _DiasDescuentoFaltas; }
            set { _DiasDescuentoFaltas = value; }
        }

        private int _DiasDescuentoRetardos;

        public int DiasDescuentoRetardos
        {
            get { return _DiasDescuentoRetardos; }
            set { _DiasDescuentoRetardos = value; }
        }

        private int _DiasDescuentoTotales;

        public int DiasDescuentoTotales
        {
            get { return _DiasDescuentoTotales; }
            set { _DiasDescuentoTotales = value; }
        }

    }
}