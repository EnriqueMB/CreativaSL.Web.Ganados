using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CFDI_FormaPagoModels
    {
        public CFDI_FormaPagoModels()
        {
            _Clave = string.Empty;
            _Descripcion = string.Empty;
            _PatronCuentaOrdenante = string.Empty;
            _PatronCuentaBeneficiario = string.Empty;
            _FechaInicioVigencia = DateTime.Now;
            _FechaFinVigencia = DateTime.Now;
        }

        private string _Clave;

        public string Clave
        {
            get { return _Clave; }
            set { _Clave = value; }
        }

        private string _Descripcion;

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private int _Bancarizado;

        public int Bancarizado
        {
            get { return _Bancarizado; }
            set { _Bancarizado = value; }
        }

        private int _NumOperacion;

        public int NumOperacion
        {
            get { return _NumOperacion; }
            set { _NumOperacion = value; }
        }
        private int _RFCEmisorCuentaOrdenante;

        public int RFCEmisorCuentaOrdenante
        {
            get { return _RFCEmisorCuentaOrdenante; }
            set { _RFCEmisorCuentaOrdenante = value; }
        }

        private int _CuentaOrdenante;

        public int CuentaOrdenante
        {
            get { return _CuentaOrdenante; }
            set { _CuentaOrdenante = value; }
        }

        private string _PatronCuentaOrdenante;

        public string PatronCuentaOrdenante
        {
            get { return _PatronCuentaOrdenante; }
            set { _PatronCuentaOrdenante = value; }
        }

        private int _RFCEmisorCuentaBeneficiario;

        public int RFCEmisorCuentaBeneficiario
        {
            get { return _RFCEmisorCuentaBeneficiario; }
            set { _RFCEmisorCuentaBeneficiario = value; }
        }

        private int _CuentaBeneficiario;

        public int CuentaBeneficiario
        {
            get { return _CuentaBeneficiario; }
            set { _CuentaBeneficiario = value; }
        }
        private string _PatronCuentaBeneficiario;

        public string PatronCuentaBaneficiario
        {
            get { return _PatronCuentaBeneficiario; }
            set { _PatronCuentaBeneficiario = value; }
        }

        private int _TipoCadenaPago;

        public int TipoCadenaPago
        {
            get { return _TipoCadenaPago; }
            set { _TipoCadenaPago = value; }
        }

        private int _NombreBancoEmisor;

        public int NombreBancoEmisor
        {
            get { return _NombreBancoEmisor; }
            set { _NombreBancoEmisor = value; }
        }

        private DateTime _FechaInicioVigencia;

        public DateTime FechaInicioVigencia
        {
            get { return _FechaInicioVigencia; }
            set { _FechaInicioVigencia = value; }
        }

        private DateTime _FechaFinVigencia;

        public DateTime FechaFinVigencia
        {
            get { return _FechaFinVigencia; }
            set { _FechaFinVigencia = value; }
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