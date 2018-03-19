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
        private DataTable _tablaCompra;
        private DateTime _FechaHoraCompra;
        private DateTime _FechaHoraProgramada;
        private string _IDProveedor;
        private string _IDSucursal;
        private string _IDCompra;
        private string _IDFlete;
        private string _IDDocumentoXPagar;
        private string _IDRecepcion;
        private string _GuiaTransito;
        private string _CertZoosanitario;
        private string _CertTuberculosis;
        private string _CertBrucelosis;
        private int _GanadosPactadoMachos;
        private int _GanadosPactadoHembras;
        private int _GanadosPactadoTotal;
        private decimal _MontoTotal;
        private decimal _KilosTotal;
        private decimal _MermaPromedio;
        private bool _Estatus;
        private HttpPostedFileBase[] _ImgFierros;

        public List<CatProveedorModels> ListaProveedores { get; set; }
        public string IDUsuario { get; set; }
        private CatProveedorModels Proveedor { get; set; }
        
        public  List<CatFierroModels> ListaFierros { get; set; }
    #endregion

    #region Metodos

        //Constructor
        public CompraModels()
        {
            ListaProveedores = new List<CatProveedorModels>();
            Proveedor = new CatProveedorModels
            {
                IDProveedor = "0",
                NombreRazonSocial = "SELECCIONE UN PROVEEDOR"
            };
            ListaProveedores.Add(Proveedor);
            IDProveedor = "0";

            ListaFierros = new List<CatFierroModels>();
            
        }


        [Required(ErrorMessage = "Seleccione una imagen de un fierro, por lo menos.")]
        [Display(Name = "Imganes Fierros")]
        public HttpPostedFileBase[] ImgFierros { get; set; }

        public string IDCompra
        {
            get { return _IDCompra; }
            set { _IDCompra = value; }
        }
        public string IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }
        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }
        public string IDFlete
        {
            get { return _IDFlete; }
            set { _IDFlete = value; }
        }
        public string IDDocuentoXPagar
        {
            get { return _IDDocumentoXPagar; }
            set { _IDDocumentoXPagar = value; }
        }
        public string IDRecepcion
        {
            get { return _IDRecepcion; }
            set { _IDRecepcion = value; }
        }
        public string GuiaTransito
        {
            get { return _GuiaTransito; }
            set { _GuiaTransito = value; }
        }
        public string CertZoosanitario
        {
            get { return _CertZoosanitario; }
            set { _CertZoosanitario = value; }
        }
        public string CertTuberculosis
        {
            get { return _CertTuberculosis; }
            set { _CertTuberculosis = value; }
        }
        public string CertBrucelosis
        {
            get { return _CertBrucelosis; }
            set { _CertBrucelosis = value; }
        }
        public DateTime FechaHoraCompra
        {
            get { return _FechaHoraCompra; }
            set { _FechaHoraCompra = value; }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaHoraProgramada
        {
            get { return _FechaHoraProgramada; }
            set { _FechaHoraProgramada = value; }
        }
        public int GanadosPactadoMachos
        {
            get { return _GanadosPactadoMachos; }
            set { _GanadosPactadoMachos = value; }
        }
        public int GanadosPactadoHembras
        {
            get { return _GanadosPactadoHembras; }
            set { _GanadosPactadoHembras = value; }
        }
        public int GanadosPactadoTotal
        {
            get { return _GanadosPactadoTotal; }
            set { _GanadosPactadoTotal = value; }
        }
        public decimal MontoTotal
        {
            get { return _MontoTotal; }
            set { _MontoTotal = value; }
        }
        public decimal KilosTotal
        {
            get { return _KilosTotal; }
            set { _KilosTotal = value; }
        }
        public decimal MermaPromedio
        {
            get { return _MermaPromedio; }
            set { _MermaPromedio = value; }
        }
        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        public DataTable TablaCompra
        {
            get { return _tablaCompra; }
            set { _tablaCompra = value; }
        }

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