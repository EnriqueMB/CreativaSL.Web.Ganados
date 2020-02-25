using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CreativaSL.Web.Ganados.Models.Validaciones;
namespace CreativaSL.Web.Ganados.Models
{
    public class CuentaBancariaClienteModels
    {
        public CuentaBancariaClienteModels()
        {
            _IDDatosBancarios = string.Empty;
            _IDBanco = 0;
            _Banco = new CatBancoModels();
            _Titular = string.Empty;
            _NumTarjeta = string.Empty;
            _NumCuenta = string.Empty;
            _Clabe = string.Empty;
            Usuario = string.Empty;
            Conexion = string.Empty;
            _IDCliente = string.Empty;
            _ListaCmbBancos = new List<CatBancoModels>();
        }
        private string _IDCliente;
        /// <summary>
        /// Identificador del Proveedor al que pertenecen los datos bancarios
        /// </summary>
        public string IDCliente
        {
            get { return _IDCliente; }
            set { _IDCliente = value; }
        }


        private string _IDDatosBancarios;
        /// <summary>
        /// Identificador de los datos bancarios del Proveedor
        /// </summary>
        public string IDDatosBancarios
        {
            get { return _IDDatosBancarios; }
            set { _IDDatosBancarios = value; }
        }

        private int _IDBanco;
        /// <summary>
        /// Identificador del banco de la cuenta bancaria
        /// </summary>
        [Display(Name = "Banco")]
        [CombosInt(ErrorMessage = "Seleccione un banco")]
        [Required(ErrorMessage = "Seleccione un banco")]
        public int IDBanco
        {
            get { return _IDBanco; }
            set { _IDBanco = value; }
        }

        private CatBancoModels _Banco;

        public CatBancoModels Banco
        {
            get { return _Banco; }
            set { _Banco = value; }
        }


        private string _Titular;
        /// <summary>
        /// Nombre de la persona titular de la cuenta bancaria
        /// </summary>
        [Display(Name = "Titular de la cuenta")]
        [Nombre(ErrorMessage = "Ingrese un nombre válido en {0}")]
        [Required(ErrorMessage = "Ingrese el nombre del {0}")]
        public string Titular
        {
            get { return _Titular; }
            set { _Titular = value; }
        }

        private string _NumTarjeta;
        /// <summary>
        /// Numero de tarjeta de la cuenta (No requerido)
        /// </summary>
        [Display(Name = "Número de tarjeta")]
        [TarjetaCredito(ErrorMessage = "Ingrese un número de tarjeta válido")]
        public string NumTarjeta
        {
            get { return _NumTarjeta; }
            set { _NumTarjeta = value; }
        }

        private string _NumCuenta;
        /// <summary>
        /// Número de cuenta
        /// </summary>
        [Display(Name = "Número de cuenta")]
        [NumeroCuenta(ErrorMessage = "Ingrese un número de cuenta válido ")]
        public string NumCuenta
        {
            get { return _NumCuenta; }
            set { _NumCuenta = value; }
        }

        private string _Clabe;
        /// <summary>
        /// Clabe interbancaria utilizada para transacciones entre bancos
        /// </summary>
        [Display(Name = "CLABE interbancaria")]
        [ClabeInterbancaria(ErrorMessage = "Ingrese una CLABE interbancaria válida ")]
        public string Clabe
        {
            get { return _Clabe; }
            set { _Clabe = value; }
        }

        private List<CuentaBancariaProveedorModels> _ListaCuentaBancaria;

        public List<CuentaBancariaProveedorModels> ListaCuentaBancaria
        {
            get { return _ListaCuentaBancaria; }
            set { _ListaCuentaBancaria = value; }
        }

        private List<CatBancoModels> _ListaCmbBancos;

        public List<CatBancoModels> ListaCmbBancos
        {
            get { return _ListaCmbBancos; }
            set { _ListaCmbBancos = value; }
        }

        public string ImagenUrl { get; set; }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}