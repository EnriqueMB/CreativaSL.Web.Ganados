using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Models
{
    public class CompraModels
    {
        #region Variables
        public DataTable TablaCompra { get; set; }
        public DateTime FechaHoraCompra { get; set; }
        public DateTime FechaHoraProgramada { get; set; }

        public string IDDocumentoXPagar { get; set; }
        public string IDRecepcion { get; set; }
        public string GuiaTransito { get; set; }
        public string CertZoosanitario { get; set; }
        public string CertTuberculosis { get; set; }
        public string CertBrucelosis { get; set; }
        public int GanadosPactadoMachos { get; set; }
        public int GanadosPactadoHembras { get; set; }
        public int GanadosPactadoTotal { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal KilosTotal { get; set; }
        public decimal MermaPromedio { get; set; }
        public bool Estatus { get; set; }
        public HttpPostedFileBase[] _ImgFierros { get; set; }

        public string IDUsuario { get; set; }
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

        public List<CatProveedorModels> ListaProveedores { get; set; }
        public List<CatFierroModels> ListaFierros { get; set; }
        public List<CatChoferModels> ListaChoferes { get; set; }
        public List<CatVehiculoModels> ListaVehiculos { get; set; }
        public List<GanadosModels> ListaGanados { get; set; }

        public DataTable TablaLugares { set; get; }
        #endregion

        #region Metodos

        //Constructor
        public CompraModels()
        {
            //Inicializamos los objetos
            Flete = new FleteModels();
            Ganado = new GanadosModels();
            TipoVehiculo = new CatTipoVehiculoModels();
            Marca = new CatMarcaVehiculoModels();
            Trayecto = new TrayectoModels();
            Lugar = new CatLugarModels();
            Sucursal = new CatSucursalesModels();


            Chofer = new CatChoferModels
            {
                IDChofer = "0",
                Nombre = "SELECCIONE UN CHOFER"
            };
            Vehiculo = new CatVehiculoModels
            {
                IDVehiculo = "0",
                Modelo = "SELECCION UN VEHICULO"

            };
            Proveedor = new CatProveedorModels
            {
                IDProveedor = "0",
                NombreRazonSocial = "SELECCIONE UN PROVEEDOR"
            };
            //Inicializamos las listas
            ListaFierros = new List<CatFierroModels>();
            ListaProveedores = new List<CatProveedorModels>();
            ListaVehiculos = new List<CatVehiculoModels>();
            ListaChoferes = new List<CatChoferModels>();
            ListaGanados = new List<GanadosModels>();
            //Agregamos los objetos con valores predeterminados (servirán en los combobox)
            ListaProveedores.Add(Proveedor);
            ListaChoferes.Add(Chofer);
            ListaVehiculos.Add(Vehiculo);
            //Valores predeterminados de los atributos
            IDProveedor = "0";
            

            
        }


        [Required(ErrorMessage = "Seleccione una imagen de un fierro, por lo menos.")]
        [Display(Name = "Imganes Fierros")]
        public HttpPostedFileBase[] ImgFierros { get; set; }

        #endregion 

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}