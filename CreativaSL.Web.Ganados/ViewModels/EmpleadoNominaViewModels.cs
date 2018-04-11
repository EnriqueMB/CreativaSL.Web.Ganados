using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class EmpleadoNominaViewModels
    {
        public EmpleadoNominaViewModels()
        {
            _AbrirCaja = false;
            _IDEmpleado = string.Empty;
        }

        private bool _AbrirCaja;
        /// <summary>
        /// Ver si el Empleado Abre caja 
        /// </summary>
        public bool AbrirCaja
        {
            get { return _AbrirCaja; }
            set { _AbrirCaja = value; }
        }


        private string _IDEmpleado;
        /// <summary>
        /// Identificador unico del empleado 
        /// </summary>
        public string IDEmpleado
        {
            get { return _IDEmpleado; }
            set { _IDEmpleado = value; }
        }

    }
}