using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Models
{
    public class CompraModels
    {
        #region Variables
        public DataTable TablaCompra { get; set; }
        public DateTime FechaHoraCompra { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [Display(Name ="Fecha programada")]
        [Required(ErrorMessage = "Seleccione una fecha")]
        public DateTime FechaHoraProgramada { get; set; }

        public string IDDocumentoXPagar { get; set; }
        public string IDRecepcion { get; set; }

        public string GuiaTransito { get; set; }
        public string CertZoosanitario { get; set; }
        public string CertTuberculosis { get; set; }
        public string CertBrucelosis { get; set; }
        
        [Range(0, 500)]
        [DisplayName("Ganado pactado machos")]
        public int GanadosPactadoMachos { get; set; }
        [Range(0, 500)]
        [DisplayName("Ganado pactado hembras")]
        public int GanadosPactadoHembras { get; set; }
        public int GanadosPactadoTotal { get; set; }

        public decimal MontoTotal { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal MontoPorPagar { get; set; }

        public decimal KilosTotal { get; set; }
        public decimal MermaPromedio { get; set; }
        public bool Estatus { get; set; }
        public HttpPostedFileBase[] _ImgFierros { get; set; }

        public string IDUsuario { get; set; }

        [Required(ErrorMessage = "Seleccione un proveedor.")]
        public string IDProveedor { get; set; }


        public string IDSucursal { get; set; }


        public string IDCompra { get; set; }
        public string IDFlete { get; set; }

        private CatProveedorModels Proveedor { get; set; }
        public FleteModels Flete { get; set; }
        public CatChoferModels Chofer { get; set; }
        public CatVehiculoModels Vehiculo { get; set; }
        public CatTipoVehiculoModels TipoVehiculo { get; set; }
        public CatMarcaVehiculoModels Marca { get; set; }
        public TrayectoModels Trayecto { get; set; }
        public CatLugarModels Lugar { get; set; }
        public CatSucursalesModels Sucursal { get; set; }
        public GanadosModels Ganado { get; set; }
        public CatJaulaModels Jaula { get; set; }
        
        public List<CatProveedorModels> ListaProveedores { get; set; }
        public List<CatFierroModels> ListaFierros { get; set; }
        public List<CatChoferModels> ListaChoferes { get; set; }
        public List<CatVehiculoModels> ListaVehiculos { get; set; }
        public List<GanadosModels> ListaGanados { get; set; }
        public List<CatLugarModels> ListaLugares { get; set; }
        public List<CatJaulaModels> ListaJaulas { get; set; }

        public DataTable TablaLugares { set; get; }
        private CultureInfo CultureInfo = new CultureInfo("es-MX");
        #endregion

        #region Metodos

        //Constructor
        public CompraModels()
        {
            //Inicializamos los objetos
            Chofer = new CatChoferModels();
            Flete = new FleteModels();
            Ganado = new GanadosModels();
            Jaula = new CatJaulaModels();
            Lugar = new CatLugarModels();
            Marca = new CatMarcaVehiculoModels();
            Sucursal = new CatSucursalesModels();
            TipoVehiculo = new CatTipoVehiculoModels();
            Trayecto = new TrayectoModels();
            
            //Inicializamos las listas
            ListaChoferes = new List<CatChoferModels>();
            ListaFierros = new List<CatFierroModels>();
            ListaGanados = new List<GanadosModels>();
            ListaJaulas = new List<CatJaulaModels>();
            ListaVehiculos = new List<CatVehiculoModels>();
            ListaLugares = new List<CatLugarModels>();
            ListaProveedores = new List<CatProveedorModels>();

            //Valores predeterminados de los atributos
            Mensaje = string.Empty;
            TipoResultado = 0;
    }


        [Display(Name = "Imagenes Fierros")]
        public HttpPostedFileBase[] ImgFierros { get; set; }

        #endregion 

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        //Si es 1: CORRECTO
        //Si es 2: ERROR
        public int TipoResultado { get; set; }
        public string Mensaje { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion

        #region Métodos
        public void SumarGanado()
        {
            this.GanadosPactadoTotal = this.GanadosPactadoMachos + this.GanadosPactadoHembras;
        }
        #endregion

        
    }
}