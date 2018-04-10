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
            _Fecha = DateTime.Now;
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


        private DateTime _Fecha;
        /// <summary>
        /// Fecha en que se realiza la carga de combustible
        /// </summary>
        public DateTime Fecha
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
        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion

    }
}