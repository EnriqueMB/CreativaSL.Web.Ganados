using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class EntregaCombistibleModels
    {
        public EntregaCombistibleModels()
        {
            _IDEntregaCombustible = string.Empty;
            _Vehiculo = new CatVehiculoModels();
            _TipoCombustible = new CatTipoCombustibleModels();
            _Fecha = DateTime.Today;
            _NoTicket = string.Empty;
            _KMInicial = 0;
            _KMFinal = 0;
            _Litros = 0;
            _Precio = 0;
            _Total = 0;
            _Rendimento = 0;
            _ImgTicket = string.Empty;
            _Documento = new DocumentoPorPagarDetalleModels();
            _ListaEntregas = new List<EntregaCombistibleModels>();
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Usuario = string.Empty;
            Opcion = 0;
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

        private CatSucursalesModels _Sucursal;
        /// <summary>
        /// Objeto que contiene los datos de la sucursal a la que pertenece la Entrega
        /// </summary>
        public CatSucursalesModels Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }


        private CatVehiculoModels _Vehiculo;
        /// <summary>
        /// Objeto que contiene los datos del vehículo al que se le cargará combustible
        /// </summary>
        public CatVehiculoModels Vehiculo
        {
            get { return _Vehiculo; }
            set { _Vehiculo = value; }
        }
        
        private CatTipoCombustibleModels _TipoCombustible;
        /// <summary>
        /// Objeto que contiene los datos del tipo de combustible a cargar
        /// </summary>
        public CatTipoCombustibleModels TipoCombustible
        {
            get { return _TipoCombustible; }
            set { _TipoCombustible = value; }
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
        
        /// <summary>
        /// Fecha con formato corto 
        /// </summary>
        public string FechaFormat
        {
            get { return _Fecha.ToString("dd/MM/yyyy"); }            
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

        private decimal _Precio;
        /// <summary>
        /// Precio por litro
        /// </summary>
        public decimal Precio
        {
            get { return _Precio; }
            set { _Precio = value; }
        }

        private decimal _Total;
        /// <summary>
        /// Monto total
        /// </summary>
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        public string ImporteFormato
        {
            get { return string.Format("{0:c}", _Total); }
        }

        private decimal _Rendimento;
        /// <summary>
        /// Rendimiento del combustible
        /// </summary>
        public decimal Rendimiento
        {
            get { return _Rendimento; }
            set { _Rendimento = value; }
        }

        private string _ImgTicket;
        /// <summary>
        /// Imagen del ticket de la carga de combustible
        /// </summary>
        public string ImgTicket
        {
            get { return _ImgTicket; }
            set { _ImgTicket = value; }
        }
       
        private DocumentoPorPagarDetalleModels _Documento;
        /// <summary>
        /// Objeto que contiene los datos del documento por pagar generado por la compra de combustible
        /// </summary>
        public DocumentoPorPagarDetalleModels Documento
        {
            get { return _Documento; }
            set { _Documento = value; }
        }

        private List<EntregaCombistibleModels> _ListaEntregas;
        /// <summary>
        /// Listado de entregas de combustible
        /// </summary>
        public List<EntregaCombistibleModels> ListaEntregas
        {
            get { return _ListaEntregas; }
            set { _ListaEntregas = value; }
        }

        private bool _BandSucursal;

        public bool BandSucursal
        {
            get { return _BandSucursal; }
            set { _BandSucursal = value; }
        }

        private bool _BandVehiculo;

        public bool BandVehiculo
        {
            get { return _BandVehiculo; }
            set { _BandVehiculo = value; }
        }

        private bool _BandFecha;

        public bool BandFecha
        {
            get { return _BandFecha; }
            set { _BandFecha = value; }
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