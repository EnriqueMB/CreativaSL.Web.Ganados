using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CalendarioModels
    {

        public CalendarioModels()
        {
            _IDProveedor = string.Empty;
            _listaCalendario = new List<CalendarioModels>();
            _GuiaTransito = string.Empty;
            _FechaHoraProgramada = DateTime.Now;
            _Estatus = 0;
            _estatusDesc = string.Empty;
            _GanadosPactadoHembras = 0;
            _GanadosPactadoMachos = 0;
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Opcion = 0;
            Usuario = string.Empty;
        }

        private string _IDProveedor;

        public string IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }
        private List<CalendarioModels> _listaCalendario;

        public List<CalendarioModels> listaCompra
        {
            get { return _listaCalendario; }
            set { _listaCalendario = value; }
        }
        private string _GuiaTransito;

        public string GuiaTransito
        {
            get { return _GuiaTransito; }
            set { _GuiaTransito = value; }
        }
        private DateTime _FechaHoraProgramada;

        public DateTime FechaHoraProgramada
        {
            get { return _FechaHoraProgramada; }
            set { _FechaHoraProgramada = value; }
        }

        private int _Estatus;

        public int Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        private int _GanadosPactadoMachos;

        public int GanadosPactadoMachos
        {
            get { return _GanadosPactadoMachos; }
            set { _GanadosPactadoMachos = value; }
        }
        private int _GanadosPactadoHembras;

        public int GanadosPactadoHembras
        {
            get { return _GanadosPactadoHembras; }
            set { _GanadosPactadoHembras = value; }
        }
        private string _estatusDesc;

        public string estatusDesc
        {
            get { return _estatusDesc; }
            set { _estatusDesc = value; }
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