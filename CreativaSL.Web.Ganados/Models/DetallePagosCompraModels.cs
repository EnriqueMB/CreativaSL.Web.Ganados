using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DetallePagosCompraModels
    {
        private string _IDDetalleDocCompra;

        public string IDDetalleDocCompra
        {
            get { return _IDDetalleDocCompra; }
            set { _IDDetalleDocCompra = value; }
        }

        private string _IDDocCompra;

        public string IDDocCompra
        {
            get { return _IDDocCompra; }
            set { _IDDocCompra = value; }
        }

        private string _IDFormaPago;

        public string IDFormaPago
        {
            get { return _IDFormaPago; }
            set { _IDFormaPago = value; }
        }

        private decimal _Monto;

        public decimal Monto
        {
            get { return _Monto; }
            set { _Monto = value; }
        }

        private string _Referencia;

        public string Referencia
        {
            get { return _Referencia; }
            set { _Referencia = value; }
        }

        private string _TipoEntradaSalida;

        public string TipoEntradaSalida
        {
            get { return _TipoEntradaSalida; }
            set { _TipoEntradaSalida = value; }
        }

        private DateTime _FechaRealizada;

        public DateTime FechaRealizada
        {
            get { return _FechaRealizada; }
            set { _FechaRealizada = value; }
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