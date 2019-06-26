using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ServiciosMantenimientoModels
    {

        public ServiciosMantenimientoModels()
        {
            _IDServicio = string.Empty;
            _Sucursal = new CatSucursalesModels();
            _Vehiculo = new CatVehiculoModels();
            _Fecha = DateTime.MinValue;
            _FechaProxima = DateTime.MinValue;
            _ServiciosRealizados = string.Empty;
            _ImporteTotal = 0;
            _CssClassEstatus = string.Empty;
            _Estatus = string.Empty;
            _IDEstatus = 0;
            _ListaRemolques = new List<CatRemolqueModels>();
            _ListaVehiculos = new List<CatVehiculoModels>();
            _ListaDetalle = new List<ServiciosMantenimientoDetalleModels>();
        }

        private CatVehiculoModels _Vehiculo;

        public CatVehiculoModels Vehiculo
        {
            get { return _Vehiculo; }
            set { _Vehiculo = value; }
        }



        private string _IDServicio;

        public string IDServicio
        {
            get { return _IDServicio; }
            set { _IDServicio = value; }
        }

        private CatSucursalesModels _Sucursal;

        public CatSucursalesModels Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        } 

        private DateTime _Fecha;

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private DateTime _FechaProxima;

        public DateTime FechaProxima
        {
            get { return _FechaProxima; }
            set { _FechaProxima = value; }
        }

        private CatProveedorModels _Proveedor;

        public CatProveedorModels Proveedor
        {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }


        public string FechaFormat
        {
            get { return _Fecha.ToShortDateString(); }
        }
         public string FechaProxFormat
        {
            get { return _FechaProxima.ToShortDateString(); }
        }

        private string _ServiciosRealizados;

        public string ServiciosRealizados
        {
            get { return _ServiciosRealizados; }
            set { _ServiciosRealizados = value; }
        }


        private decimal _ImporteTotal;

        public decimal ImporteTotal
        {
            get { return _ImporteTotal; }
            set { _ImporteTotal = value; }
        }

        public string ImporteTotalFormat
        {
            get { return string.Format("{0:c}", _ImporteTotal); }
        }

        private int _IDEstatus;

        public int IDEstatus
        {
            get { return _IDEstatus; }
            set { _IDEstatus = value; }
        }
        
        private string _Estatus;

        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        private string _CssClassEstatus;

        public string CssClassEstatus
        {
            get { return _CssClassEstatus; }
            set { _CssClassEstatus = value; }
        }


        private List<CatVehiculoModels> _ListaVehiculos;
        /// <summary>
        /// Lista de vehículos a los que se les puede realizar mantenimiento
        /// </summary>
        public List<CatVehiculoModels> ListaVehiculos
        {
            get { return _ListaVehiculos; }
            set { _ListaVehiculos = value; }
        }

        private List<CatRemolqueModels> _ListaRemolques;
        /// <summary>
        /// Lista de remolques a los que se les puede realizar mantenimiento
        /// </summary>
        public List<CatRemolqueModels> ListaRemolques
        {
            get { return _ListaRemolques; }
            set { _ListaRemolques = value; }
        }

        private List<ServiciosMantenimientoDetalleModels> _ListaDetalle;

        public List<ServiciosMantenimientoDetalleModels> ListaDetalle
        {
            get { return _ListaDetalle; }
            set { _ListaDetalle = value; }
        }


        #region Datos De Control
        public bool NuevoRegistro { get; set; }
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion

    }
}