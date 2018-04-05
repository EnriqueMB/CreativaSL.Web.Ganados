using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatClienteModels
    {
        public CatClienteModels()
        {
            _IDCliente = string.Empty;
            _IDSucursal = string.Empty;
            _NombreRazonSocial = string.Empty;
            _EsPersonaFisica = false;
            _IDRegimenFiscal = string.Empty;
            _RFC = string.Empty;
            _Direccion = string.Empty;
            _FechaIngreso = DateTime.Today;
            _NombreResponsable = string.Empty;
            _Telefono = string.Empty;
            _Celular = string.Empty;
            _CorreoElectronico = string.Empty;
            _NombreSucursal = string.Empty;
            _NombreRegimenFiscal = string.Empty;
            _ListaClientes = new List<CatClienteModels>();
            _ListaCmbSucursal = new List<CatSucursalesModels>();
            _ListaRegimenCMB = new List<CFDI_RegimenFiscalModels>();
            Conexion = string.Empty;
            Usuario = string.Empty;
        }
        
        private string _IDCliente;
        /// <summary>
        /// El identificador de el cliente
        /// </summary>
        public string IDCliente 
        {
            get { return _IDCliente; }
            set { _IDCliente = value; }
        }
        
        private string _IDSucursal;
        /// <summary>
        /// Identificador de la sucursal a la que pertenece el cliente
        /// </summary>
        [Required(ErrorMessage = "Seleccione una sucursal")]
        [Display(Name = "Sucursal")]        
        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private string _NombreRazonSocial;
        /// <summary>
        /// Nombre o Razón social del cliente ante el SAT
        /// </summary>
        [Required(ErrorMessage = "Debe ingresar la razón social del cliente")]
        [Display(Name = "Razón social")]
        [StringLength(300, ErrorMessage = "El número de caracteres del campo {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto(ErrorMessage = "Ingrese un dato válido para Nombre/Razón Social")]        
        public string NombreRazonSocial
        {
            get { return _NombreRazonSocial; }
            set { _NombreRazonSocial = value; }
        }

        private bool _EsPersonaFisica;
        /// <summary>
        /// Establece si el cliente es Persona Física o Persona Moral
        /// </summary>
        public bool EsPersonaFisica
        {
            get { return _EsPersonaFisica; }
            set { _EsPersonaFisica = value; }
        }

        private string _IDRegimenFiscal;
        /// <summary>
        /// Establecer el régimen fiscal en el que se encuentra el cliente
        /// </summary>
        [Required(ErrorMessage = "Seleccione un régimen fiscal")]
        [Display(Name = "Régimen Fiscal")]        
        public string IDRegimenFiscal
        {
            get { return _IDRegimenFiscal; }
            set { _IDRegimenFiscal = value; }
        }
        
        private string _RFC;
        /// <summary>
        /// Registro Federal de Contribuyente del cliente
        /// </summary>        
        [Required(ErrorMessage = "Debe ingresar el RFC")]
        [Display(Name = "rfc")]
        [RFC(ErrorMessage = "Ingrese un RFC válido")]
        public string RFC
        {
            get { return _RFC; }
            set { _RFC = value; }
        }

        private string _Direccion;
        /// <summary>
        /// Domicilio fiscal del cliente
        /// </summary>
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Display(Name = "Domicilio fiscal")]
        [Direccion(ErrorMessage = "Ingrese un domicilio fiscal válido")]
        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }

        private DateTime _FechaIngreso;
        /// <summary>
        /// Fecha de inicio de la relación
        /// </summary>
        [Required(ErrorMessage = "Debe seleccionar una fecha de inicio de relación")]
        [Display(Name = "Fecha de inicio de relación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaIngreso
        {
            get { return _FechaIngreso; }
            set { _FechaIngreso = value; }
        }
        
        private string _NombreResponsable;
        /// <summary>
        /// Nombre de la persona de contacto con el cliente
        /// </summary
        [Display(Name = "Nombre del contacto")]
        [Nombre(ErrorMessage = "Ingrese un nombre de contacto válido")]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        public string NombreResponsable
        {
            get { return _NombreResponsable; }
            set { _NombreResponsable = value; }
        }
        
        private string _Celular;
        /// <summary>
        /// Número de teléfono celular del cliente
        /// </summary>
        [Display(Name = "Celular")]
        [Telefono(ErrorMessage = "Ingrese un número de celular válido")]
        public string Celular
        {
            get { return _Celular; }
            set { _Celular = value; }
        }

        private string _Telefono;
        /// <summary>
        /// Número de teléfono convencional del cliente
        /// </summary>
        [Display(Name = "Teléfono")]
        [Telefono(ErrorMessage = "Ingrese un número telefónico válido")]
        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        private string _CorreoElectronico;
        /// <summary>
        /// Correo electrónico de contacto del cliente
        /// </summary>
        [Required(ErrorMessage = "Ingrese el correo electrónico")]
        [Display(Name = "Email")]
        [Correo(ErrorMessage = "Ingrese un correo electrónico válido")]
        public string CorreoElectronico
        {
            get { return _CorreoElectronico; }
            set { _CorreoElectronico = value; }
        }


        private string _NombreSucursal;
        /// <summary>
        /// Nombre de la sucursal a la que pertenece el cliente
        /// </summary>
        public string NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; }
        }

        private string _NombreRegimenFiscal;
        /// <summary>
        /// Descripción del régimen fiscal al que pertenece el cliente
        /// </summary>
        public string NombreRegimenFiscal
        {
            get { return _NombreRegimenFiscal; }
            set { _NombreRegimenFiscal = value; }
        }

        private List<CatClienteModels> _ListaClientes;
        /// <summary>
        /// Lista para llenar tabla de clientes
        /// </summary>
        public List<CatClienteModels> ListaClientes
        {
            get { return _ListaClientes; }
            set { _ListaClientes = value; }
        }

        private List<CatSucursalesModels> _ListaCmbSucursal;
        /// <summary>
        /// Lista para llenar combo de sucursal
        /// </summary>
        public List<CatSucursalesModels> ListaCmbSucursal
        {
            get { return _ListaCmbSucursal; }
            set { _ListaCmbSucursal = value; }
        }

        private List<CFDI_RegimenFiscalModels> _ListaRegimenCMB;
        /// <summary>
        /// Lista para llenar combo regímenes fiscales 
        /// </summary>
        public List<CFDI_RegimenFiscalModels> ListaRegimenCMB
        {
            get { return _ListaRegimenCMB; }
            set { _ListaRegimenCMB = value; }
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