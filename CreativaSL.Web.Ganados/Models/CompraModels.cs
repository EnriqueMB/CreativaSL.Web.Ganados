using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Web;
using System.Linq;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Models
{
    public class CompraModels
    {
        #region Variables
        #region Variables de control
        public string Conexion { get; set; }
        public string Usuario { get; set; }
        public string Mensaje { get; set; }
        public bool Completado { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }
        #endregion

        public string ListadoPrecioRangoPeso { get; set; }

        //Para el index
        public DataTable TablaCompra { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha de la compra")]
        public DateTime FechaHoraTerminada { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha programada")]
        [Required(ErrorMessage = "Seleccione una fecha")]
        public DateTime FechaHoraProgramada { get; set; }

        public string GuiaTransito { get; set; }
        public string CertZoosanitario { get; set; }
        public string CertTuberculosis { get; set; }
        public string CertBrucelosis { get; set; }

        [RegularExpression("^[0-9]*$",
            ErrorMessage = "Solo número enteros positivos.")]
        [DisplayName("Ganado pactado machos")]
        public int GanadosPactadoMachos { get; set; }

        [RegularExpression("^[0-9]*$",
            ErrorMessage = "Solo número enteros positivos.")]
        [DisplayName("Ganado pactado hembras")]
        public int GanadosPactadoHembras { get; set; }

        [RegularExpression("^[0-9]*$",
            ErrorMessage = "Solo número enteros positivos.")]
        public int GanadosPactadoTotal { get; set; }

        [RegularExpression("^[0-9]*$",
            ErrorMessage = "Solo número enteros positivos.")]
        public int GanadosCompradoMachos { get; set; }
        [RegularExpression("^[0-9]*$",
            ErrorMessage = "Solo número enteros positivos.")]
        public int GanadosCompradoHembras { get; set; }
        [RegularExpression("^[0-9]*$",
            ErrorMessage = "Solo número enteros positivos.")]
        public int GanadosCompradoTotal { get; set; }



        public decimal MontoTotal { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal MontoPorPagar { get; set; }

        public decimal KilosTotal { get; set; }
        public decimal MermaPromedio { get; set; }
        public int Estatus { get; set; }
        public HttpPostedFileBase[] ImgFierros { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleccione un proveedor.")]
        public string IDProveedor { get; set; }
        public string IDSucursal { get; set; }
        public string IDCompra { get; set; }
        public string IDFlete { get; set; }
        public string IDUsuario { get; set; }
        public string IDDocumentoXPagar { get; set; }
        public string IDRecepcion { get; set; }
        public string IDEmpresa { get; set; }
        public string IDChofer { get; set; }
        public string IDVehiculo { get; set; }
        public string IDJaula { get; set; }
        public string IDRemolque { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleccione un lugar.")]
        public string IDPLugarProveedor { get; set; }

        public CatChoferModels Chofer { get; set; }
        public CompraGanadosModels CompraGanado { get; set; }
        public CatEstatusGanadoModels EstatusGanado { get; set; }
        public CatFierroModels Fierro { get; set; }
        public FleteModels Flete { get; set; }
        public GanadosModels Ganado { get; set; }
        public CatJaulaModels Jaula { get; set; }
        public CatLugarModels Lugar { get; set; }
        public CatMarcaVehiculoModels Marca { get; set; }
        public CatProveedorModels Proveedor { get; set; }
        public CatRemolqueModels Remolque { get; set; }
        public CatSucursalesModels Sucursal { get; set; }
        public TrayectoModels Trayecto { get; set; }
        public CatTipoVehiculoModels TipoVehiculo { get; set; }
        public CatVehiculoModels Vehiculo { get; set; }
        public DocumentoPorPagarModels DocPorPagar { get; set; }
        public CatTipoClasificacionModels TipoClasificacion { get; set; }
        public CFDI_FormaPagoModels FormaPago { get; set; }
        

        public List<CatChoferModels> ListaChoferes { get; set; }
        public List<CompraGanadosModels> ListaCompraGanado { get; set; }
        public List<CatEmpresaModels> ListaEmpresas { get; set; }
        public List<CatFierroModels> ListaFierros { get; set; }
        public List<GanadosModels> ListaGanados { get; set; }
        public List<GeneroGanadoModels> ListaGeneroGanado { get; set; }
        public List<CatJaulaModels> ListaJaulas { get; set; }
        public List<CatLugarModels> ListaLugares { get; set; }
        public List<CatProveedorModels> ListaProveedores { get; set; }
        public List<CatSucursalesModels> ListaSucursales { get; set; }
        public List<CatVehiculoModels> ListaVehiculos { get; set; }
        public List<CatRangoPesoCompraModels> ListaRangoPrecio { get; set; }
        public List<CatRemolqueModels> ListaRemolques { get; set; }
        public List<CatEstatusGanadoModels> ListaEstatusGanado { get; set; }
        public List<CatTipoClasificacionModels> ListaTipoClasificacion { get; set; }
        public List<CFDI_FormaPagoModels> ListaFormasPagos { get; set; }
        public List<CatLugarModels> ListaLugaresProveedor { get; set; }

        private CultureInfo CultureInfo = new CultureInfo("es-MX");
        #endregion

        #region Metodos

        //Constructor
        public CompraModels()
        {
            //Inicializamos los objetos
            Chofer = new CatChoferModels();
            CompraGanado = new CompraGanadosModels();
            EstatusGanado = new CatEstatusGanadoModels();
            Fierro = new CatFierroModels();
            Flete = new FleteModels();
            FormaPago = new CFDI_FormaPagoModels();
            Ganado = new GanadosModels();
            Jaula = new CatJaulaModels();
            Lugar = new CatLugarModels();
            Marca = new CatMarcaVehiculoModels();
            Proveedor = new CatProveedorModels();
            Remolque = new CatRemolqueModels();
            RespuestaAjax = new RespuestaAjax();
            Sucursal = new CatSucursalesModels();
            Trayecto = new TrayectoModels();
            TipoVehiculo = new CatTipoVehiculoModels();
            TipoClasificacion = new CatTipoClasificacionModels();
            Vehiculo = new CatVehiculoModels();

            //Inicializamos las listas
            ListaChoferes = new List<CatChoferModels>();
            ListaCompraGanado = new List<CompraGanadosModels>();
            ListaEstatusGanado = new List<CatEstatusGanadoModels>();
            ListaEmpresas = new List<CatEmpresaModels>();
            ListaFierros = new List<CatFierroModels>();
            ListaFormasPagos = new List<CFDI_FormaPagoModels>();
            ListaGanados = new List<GanadosModels>();
            ListaJaulas = new List<CatJaulaModels>();
            ListaLugares = new List<CatLugarModels>();
            ListaLugaresProveedor = new List<CatLugarModels>();
            ListaProveedores = new List<CatProveedorModels>();
            ListaRangoPrecio = new List<CatRangoPesoCompraModels>();
            ListaRemolques = new List<CatRemolqueModels>();
            ListaSucursales = new List<CatSucursalesModels>();
            ListaVehiculos = new List<CatVehiculoModels>();
            ListaGeneroGanado = new List<GeneroGanadoModels>();
            ListaTipoClasificacion = new List<CatTipoClasificacionModels>();

            //Valores predeterminados de los atributos
            IDChofer = string.Empty;
            IDCompra = string.Empty;
            IDDocumentoXPagar = string.Empty;
            IDEmpresa = string.Empty;
            IDFlete = string.Empty;
            IDJaula = string.Empty;
            IDPLugarProveedor = string.Empty;
            IDProveedor = string.Empty;
            IDRecepcion = string.Empty;
            IDRemolque = string.Empty;
            IDSucursal = string.Empty;
            IDUsuario = string.Empty;
            IDVehiculo = string.Empty;
            CertZoosanitario = string.Empty;
            CertTuberculosis = string.Empty;
            CertBrucelosis = string.Empty;
            Conexion = string.Empty;
            Estatus = -1;
            FechaHoraProgramada = DateTime.Now;
            FechaHoraTerminada = DateTime.Now;
            GuiaTransito = string.Empty;
            GanadosCompradoMachos = 0;
            GanadosCompradoHembras = 0;
            GanadosCompradoTotal = 0;
            GanadosPactadoMachos = 0;
            GanadosPactadoHembras = 0;
            GanadosPactadoTotal = 0;
            KilosTotal = 0;
            ListadoPrecioRangoPeso = string.Empty;
            MermaPromedio = 0;
            Mensaje = string.Empty;
            MontoPagado = 0;
            MontoPorPagar = 0;
            MontoTotal = 0;
            Completado = false;
            Mensaje = string.Empty;
            Usuario = string.Empty;
        }
        public void InicializarComboGeneroGanado()
        {
            GeneroGanadoModels genero = new GeneroGanadoModels
            {
                Id = true,
                Descripcion = "MACHO"
            };
            ListaGeneroGanado.Add(genero);
            genero = new GeneroGanadoModels
            {
                Id = false,
                Descripcion = "HEMBRA"
            };
            ListaGeneroGanado.Add(genero);

        }
        public void SumarGanadoPactado()
        {
            this.GanadosPactadoTotal = this.GanadosPactadoMachos + this.GanadosPactadoHembras;
        }
        public void SumarGanadoComprado()
        {
            this.GanadosCompradoTotal = this.GanadosCompradoMachos + this.GanadosCompradoHembras;
        }
        public void GetListaProveedor()
        {
            if(!string.IsNullOrEmpty(Proveedor.IDProveedor))
            {
                var item = ListaProveedores.FirstOrDefault(p => p.IDProveedor == Proveedor.IDProveedor);
                if (item == null)
                {
                    ListaProveedores.Add(Proveedor);
                    ListaProveedores = ListaProveedores.OrderBy(p => p.NombreRazonSocial).ToList();
                }
            }    
            
            CatProveedorModels pPredeterminado = new CatProveedorModels
            {
                IDProveedor = string.Empty,
                NombreRazonSocial = "SELECCIONE UN PROVEEDOR"
            };
            ListaProveedores.Insert(0, pPredeterminado);
        }
        public void GetListaChoferes()
        {
            if (!string.IsNullOrEmpty(Chofer.IDChofer))
            {
                var item = ListaChoferes.FirstOrDefault(c => c.IDChofer == Chofer.IDChofer);
                if (item == null)
                {
                    ListaChoferes.Add(Chofer);
                    ListaChoferes = ListaChoferes.OrderBy(c => c.Nombre).ToList();
                }
            }
           
            CatChoferModels cPredeterminado = new CatChoferModels
            {
                IDChofer = string.Empty,
                Nombre = "SELECCIONE UN CHOFER",
                
            };
            ListaChoferes.Insert(0, cPredeterminado);
        }
        public void GetListadoVehiculos()
        {
            if(!string.IsNullOrEmpty(Vehiculo.IDVehiculo))
            {
                var item = ListaVehiculos.FirstOrDefault(v => v.IDVehiculo == Vehiculo.IDVehiculo);
                if (item == null)
                {
                    ListaVehiculos.Add(Vehiculo);
                    ListaVehiculos = ListaVehiculos.OrderBy(v => v.nombreMarca).ToList();
                }
            }
            
            CatVehiculoModels vPredeterminado = new CatVehiculoModels
            {
                IDVehiculo = string.Empty,
                nombreMarca = "SELECCIONE UN VEHICULOS",
                Modelo = "GRUPO OCAMPO"
            };
            ListaVehiculos.Insert(0, vPredeterminado);
        }
        public void GetListadoJaulas()
        {
            if (!string.IsNullOrEmpty(Jaula.IDJaula))
            {
                var item = ListaJaulas.FirstOrDefault(j => j.IDJaula == Jaula.IDJaula);
                if (item == null)
                {
                    ListaJaulas.Add(Jaula);
                    ListaJaulas = ListaJaulas.OrderBy(j => j.Matricula).ToList();
                }
            }

            CatJaulaModels jPredeterminado = new CatJaulaModels
            {
                IDJaula = string.Empty,
                Matricula = "SELECCIONE UNA JAULA"
            };
            ListaJaulas.Insert(0, jPredeterminado);
        }
        public void GetListadoRemolque()
        {
            if(!string.IsNullOrEmpty(Remolque.IDRemolque))
            {
                var item = ListaRemolques.FirstOrDefault(r => r.IDRemolque == Remolque.IDRemolque);
                if (item == null)
                {
                    ListaRemolques.Add(Remolque);
                    ListaRemolques = ListaRemolques.OrderBy(r => r.placa).ToList();
                }
            }
            
            CatRemolqueModels rPredeterminado = new CatRemolqueModels
            {
                IDRemolque = string.Empty,
                placa = "SELECCIONE UN REMOLQUE"
            };
            ListaRemolques.Insert(0, rPredeterminado);
        }

        #endregion
    }
}