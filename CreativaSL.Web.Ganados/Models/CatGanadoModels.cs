using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatGanadoModels
    {
        public string IdSucursal { get; set; }
        public int IdCorral { get; set; }

        public CatGanadoModels()
        {
            _IDGanado = string.Empty;
            _ListaEventoEnvio = new List<CatTipoEventoEnvioModels>();
            _NumArete = string.Empty;
            _Genero = string.Empty;
            _Corral = string.Empty;
            _Observacion = string.Empty;
            _Status = string.Empty;
            _ListaGanados = new List<CatGanadoModels>();
            _PesoGanado = 0;
            _listaSucursal = new List<CatSucursalesModels>();
            _ListaCorral = new List<CatCorralModels>();
        }

        private List<CatTipoEventoEnvioModels> _ListaEventoEnvio;
        /// <summary>
        /// Lista para el combo del status
        /// </summary>
        public List<CatTipoEventoEnvioModels> ListaEventoEnvio
        {
            get { return _ListaEventoEnvio; }
            set { _ListaEventoEnvio = value; }
        }

        private string _IDGanado;
        /// <summary>
        /// Identificador del ganado
        /// </summary>
        public string IDGanado
        {
            get { return _IDGanado; }
            set { _IDGanado = value; }
        }

        private string _NumArete;
        /// <summary>
        /// Numero de arete del ganado
        /// </summary>
        public string NumArete
        {
            get { return _NumArete; }
            set { _NumArete = value; }
        }

        private string _Genero;
        /// <summary>
        /// Genero del ganado
        /// </summary>
        public string Genero
        {
            get { return _Genero; }
            set { _Genero = value; }
        }

        private string _Corral;
        /// <summary>
        /// Corral en el que esta el ganado
        /// </summary>
        public string Corral
        {
            get { return _Corral; }
            set { _Corral = value; }
        }

        private string _Observacion;
        /// <summary>
        /// Observaciones hacia el ganado
        /// </summary>
        [Required(ErrorMessage = "Debe ingresar las observaciones del ganado")]
        [Display(Name = "Observaciones")]
        public string Observacion
        {
            get { return _Observacion; }
            set { _Observacion = value; }
        }

        private string _Status;
        /// <summary>
        /// Estado del ganado
        /// </summary>
        public string Staus
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private int _IDTipoEventoEnvio;
        /// <summary>
        /// identificador del estado del ganado
        /// </summary>
        [Required(ErrorMessage = "Seleccione una Status")]
        [Display(Name = "Status")]
        public int IDTipoEventoEnvio
        {
            get { return _IDTipoEventoEnvio; }
            set { _IDTipoEventoEnvio = value; }
        }

        private List<CatGanadoModels> _ListaGanados;
        /// <summary>
        /// Lista para recuperar todos los ganados
        /// </summary>
        public List<CatGanadoModels> ListaGanados
        {
            get { return _ListaGanados; }
            set { _ListaGanados = value; }
        }

        private int _PesoGanado;

        public int PesoGanado
        {
            get { return _PesoGanado; }
            set { _PesoGanado = value; }
        }

        private DateTime _FechaCompra;

        public DateTime FechaCompra
        {
            get { return _FechaCompra; }
            set { _FechaCompra = value; }
        }

        private string _Proveedor;

        public string Proveedor
        {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }
        private List<CatSucursalesModels> _listaSucursal;
        public List<CatSucursalesModels> listaSucursal
        {
            get { return _listaSucursal; }
            set { _listaSucursal = value; }
        }

        private List<CatCorralModels> _ListaCorral;

        public List<CatCorralModels> ListaCorral
        {
            get { return _ListaCorral; }
            set { _ListaCorral = value; }
        }

        public RespuestaAjax RespuestaAjax { get; set; }
        public decimal Dinero { get; set; }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}