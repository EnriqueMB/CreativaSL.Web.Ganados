using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class EntregaCombistibleModels
    {
        private string _IDEntregaCombustible;

        public string IDEntregaCombustible
        {
            get { return _IDEntregaCombustible; }
            set { _IDEntregaCombustible = value; }
        }

        private string _IDVehiculo;

        public string IDVehiculo
        {
            get { return _IDVehiculo; }
            set { _IDVehiculo = value; }
        }

        private int _IDTipoCombustible;

        public int IDTipoCombustible
        {
            get { return _IDTipoCombustible; }
            set { _IDTipoCombustible = value; }
        }

        private DateTime _Fecha;

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private string _NoTicket;

        public string NoTicket
        {
            get { return _NoTicket; }
            set { _NoTicket = value; }
        }

        private int _KMInicial;

        public int KMInicial
        {
            get { return _KMInicial; }
            set { _KMInicial = value; }
        }

        private int _KMFinal;

        public int KMFinal
        {
            get { return _KMFinal; }
            set { _KMFinal = value; }
        }

        private decimal _Litros;

        public decimal Litros
        {
            get { return _Litros; }
            set { _Litros = value; }
        }

        private decimal _Precio;

        public decimal Precio
        {
            get { return _Precio; }
            set { _Precio = value; }
        }

        private decimal _Total;

        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        private decimal _Rendimento;

        public decimal Rendimiento
        {
            get { return _Rendimento; }
            set { _Rendimento = value; }
        }

        private string _ImgTicket;

        public string ImgTicket
        {
            get { return _ImgTicket; }
            set { _ImgTicket = value; }
        }

        private string _IDDocumentoXPagar;

        public string IDDocumentoXPagar
        {
            get { return _IDDocumentoXPagar; }
            set { _IDDocumentoXPagar = value; }
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