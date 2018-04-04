using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class CuentaBancariaViewModels
    {
        public CuentaBancariaViewModels()
        {
            _IDDatosBancarios = string.Empty;
            _IDCliente = string.Empty;
            _IDBanco = 0;
            _Titular = string.Empty;
            _NumTarjeta = string.Empty;
            _NumCuenta = string.Empty;
            _Clabe = string.Empty;
        }

        private string _IDCliente;
        /// <summary>
        /// Identificador del cliente al que pertenece la cuenta
        /// </summary>
        public string IDCliente
        {
            get { return _IDCliente; }
            set { _IDCliente = value; }
        }
        private string _IDDatosBancarios;
        /// <summary>
        /// Identificador de los datos bancarios del cliente
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
        [Display( Name = "Banco")]
        [CombosInt(ErrorMessage ="Seleccione un banco")]
        [Required(ErrorMessage = "Seleccione un banco")]
        public int IDBanco
        {
            get { return _IDBanco; }
            set { _IDBanco = value; }
        }

        private string _Titular;
        /// <summary>
        /// Nombre de la persona titular de la cuenta bancaria
        /// </summary>
        [Display(Name = "Titular de la cuenta")]
        [Nombre(ErrorMessage ="Ingrese un nombre válido en {0}")]
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
        [Display(Name ="Número de tarjeta")]
        [TarjetaCredito(ErrorMessage ="Ingrese un número de tarjeta válido")]
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
        [NumeroCuenta(ErrorMessage ="Ingrese un número de cuenta válido ")]
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

        private List<CatBancoModels> _ListaBancos;
        /// <summary>
        /// Lista para llenar combo Bancos
        /// </summary>
        public List<CatBancoModels> ListaBancos
        {
            get { return _ListaBancos; }
            set { _ListaBancos = value; }
        }


    }
}