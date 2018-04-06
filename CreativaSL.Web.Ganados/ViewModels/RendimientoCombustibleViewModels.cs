using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class RendimientoCombustibleViewModels
    {
        public RendimientoCombustibleViewModels()
        {
            _IDEntregaCombustible = string.Empty;
            _Fecha = string.Empty;
            _NoTicket = string.Empty;
            _KMInicial = 0;
            _KMFinal = 0;
            _Litros = 0;
            _Rendimiento = 0;
        }

        private string _IDEntregaCombustible;
        /// <summary>
        /// Identificador de la entrega de combustible
        /// </summary>
        public string IDEntregaCombustible
        {
            get { return _IDEntregaCombustible; }
            set { _IDEntregaCombustible = value; }
        }


        private string _Fecha;
        /// <summary>
        /// Fecha en que se realiza la carga de combustible
        /// </summary>
        public string Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private string _NoTicket;
        /// <summary>
        /// Número de ticket de compra de combustible
        /// </summary>
        public string NoTicket
        {
            get { return _NoTicket; }
            set { _NoTicket = value; }
        }

        private int _KMInicial;
        /// <summary>
        /// Kilometraje inicial 
        /// </summary>
        public int KMInicial
        {
            get { return _KMInicial; }
            set { _KMInicial = value; }
        }

        private int _KMFinal;
        /// <summary>
        /// Kilometraje final
        /// </summary>
        public int KMFinal
        {
            get { return _KMFinal; }
            set { _KMFinal = value; }
        }

        private decimal _Litros;
        /// <summary>
        /// Cantidad de litros ingresados
        /// </summary>
        public decimal Litros
        {
            get { return _Litros; }
            set { _Litros = value; }
        }

        private decimal _Rendimiento;

        public decimal Rendimiento
        {
            get { return _Rendimiento; }
            set { _Rendimiento = value; }
        }

    }
}