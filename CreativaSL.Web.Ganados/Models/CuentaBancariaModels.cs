using CreativaSL.Web.Ganados.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CuentaBancariaModels
    {
        public CuentaBancariaModels()
        {
            _IDDatosBancarios = string.Empty;
            _Cliente = new CatClienteModels();
            _Banco = new CatBancoModels();
            _Titular = string.Empty;
            _NumTarjeta = string.Empty;
            _NumCuenta = string.Empty;
            _Clabe = string.Empty;
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

        private CatClienteModels _Cliente;
        /// <summary>
        /// Datos del cliente al que pertenece la cuenta bancaria
        /// </summary>
        public CatClienteModels Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }
        
        private CatBancoModels _Banco;
        /// <summary>
        /// Datos del banco en el que se encuentra la cuenta bancaria
        /// </summary>
        public CatBancoModels Banco
        {
            get { return _Banco; }
            set { _Banco = value; }
        }
        
        private string _Titular;
        /// <summary>
        /// Nombre de la persona titular de la cuenta bancaria
        /// </summary>
        public string Titular
        {
            get { return _Titular; }
            set { _Titular = value; }
        }

        private string _NumTarjeta;
        /// <summary>
        /// Numero de tarjeta de la cuenta (No requerido)
        /// </summary>
        public string NumTarjeta
        {
            get { return _NumTarjeta; }
            set { _NumTarjeta = value; }
        }

        private string _NumCuenta;
        /// <summary>
        /// Número de cuenta
        /// </summary>
        public string NumCuenta
        {
            get { return _NumCuenta; }
            set { _NumCuenta = value; }
        }

        private string _Clabe;
        /// <summary>
        /// Clabe interbancaria utilizada para transacciones entre bancos
        /// </summary>
        public string Clabe
        {
            get { return _Clabe; }
            set { _Clabe = value; }
        }
        
        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        public bool NuevoRegistro { get; set; }
        #endregion

        public CuentaBancariaViewModels GetViewCB()
        {
            try
            {
                return new CuentaBancariaViewModels
                {   IDBanco = this._Banco.IDBanco,
                    Titular = this._Titular,
                    NumTarjeta = this._NumTarjeta,
                    NumCuenta = this._NumCuenta,
                    Clabe = this._Clabe };
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }
    }
}