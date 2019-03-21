using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ConfiguracionModels
    {
        public ConfiguracionModels ()
        {
            _listaTicket = new List<ConfiguracionModels>();
            _idTicket = 0;
            _pagosDiasDomingos = 0;
            _pagosDiasFestivos = 0;
            _pagosDiasVacaciones = 0;
            _retardoFaltas = 0;
            _textoTicket1 = string.Empty;
            _textoTicket2 = string.Empty;
            _textoTicket3 = string.Empty;
            Conexion = string.Empty;
            _CorreoTxt = string.Empty;
            _Password = string.Empty;
            _HtmlTxt = true;
            _PortTxt = string.Empty;
            _EnableSslTxt = true;
        }
        private List<ConfiguracionModels> _listaTicket;

        public List<ConfiguracionModels> listaTicket
        {
            get { return _listaTicket; }
            set { _listaTicket = value; }
        }
        private int _idTicket;

        public int idTicket
        {
            get { return _idTicket; }
            set { _idTicket = value; }
        }
        private Decimal _pagosDiasFestivos;

        public Decimal pagosDiasFestivos
        {
            get { return _pagosDiasFestivos; }
            set { _pagosDiasFestivos = value; }
        }
        private Decimal _pagosDiasVacaciones;

        public Decimal pagosDiasVacaciones
        {
            get { return _pagosDiasVacaciones; }
            set { _pagosDiasVacaciones = value; }
        }
        private Decimal _pagosDiasDomingos;

        public Decimal pagosDiasDomingos
        {
            get { return _pagosDiasDomingos; }
            set { _pagosDiasDomingos = value; }
        }
        private string _textoTicket1;

        public string textoTicket1
        {
            get { return _textoTicket1; }
            set { _textoTicket1 = value; }
        }
        private string _textoTicket2;

        public string textoTicket2
        {
            get { return _textoTicket2; }
            set { _textoTicket2 = value; }
        }

        private string _textoTicket3;

        public string textoTicket3
        {
            get { return _textoTicket3; }
            set { _textoTicket3 = value; }
        }
        private int _retardoFaltas;

        public int retardosFaltas
        {
            get { return _retardoFaltas; }
            set { _retardoFaltas = value; }
        }

        private string _CorreoTxt;

        public string CorreoTxt
        {
            get { return _CorreoTxt; }
            set { _CorreoTxt = value; }
        }

        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private bool _HtmlTxt;

        public bool HtmlTxt
        {
            get { return _HtmlTxt; }
            set { _HtmlTxt = value; }
        }

        private string _HostTxt;

        public string HostTxt
        {
            get { return _HostTxt; }
            set { _HostTxt = value; }
        }

        private string _PortTxt;

        public string PortTxt
        {
            get { return _PortTxt; }
            set { _PortTxt = value; }
        }

        private bool _EnableSslTxt;

        public bool EnableSslTxt
        {
            get { return _EnableSslTxt; }
            set { _EnableSslTxt = value; }
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