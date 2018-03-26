using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CompraGanadosModels
    {
        public CompraGanadosModels()
        {
            IDCompra = string.Empty;
            DifPeso = 0;
            IDGanados = string.Empty;
            Merma = 0;
            PesoInicial = 0;
            PesoFinal = 0;
            PesoPagado = 0;
            PrecioKilo = 0;
            Repeso = true;
        }

        public bool Repeso { get; set; }

        private decimal _DifPeso;
        private string _IDCompra;
        private string _IDGanados;
        private decimal _Merma;
        private decimal _PesoInicial;
        private decimal _PesoFinal;
        private decimal _PesoPagado;
        private decimal _PrecioKilo;

        public decimal DifPeso
        {
            get
            {
                _DifPeso = PesoInicial - PesoFinal;
                return _DifPeso;
            }
            set { _DifPeso = value; }
        }
        public string IDCompra
        {
            get { return _IDCompra; }
            set { _IDCompra = value; }
        }
        public string IDGanados
        {
            get { return _IDGanados; }
            set { _IDGanados = value; }
        }
        public decimal Merma
        {
            get { return _Merma; }
            set { _Merma = value; }
        }
        public decimal PesoInicial
        {
            get { return _PesoInicial; }
            set { _PesoInicial = value; }
        }
        public decimal PesoFinal
        {
            get { return _PesoFinal; }
            set { _PesoFinal = value; }
        }
        public decimal PesoPagado
        {
            get { return _PesoPagado; }
            set { _PesoPagado = value; }
        }
        public decimal PrecioKilo
        {
            get { return _PrecioKilo; }
            set { _PrecioKilo = value; }
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