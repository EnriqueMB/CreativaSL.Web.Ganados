using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CuentaBancariaModels
    {
        private string _IDDatosBancarios;
        /// <summary>
        /// Identificador de los datos bancarios del cliente
        /// </summary>
        public string IDDatosBancarios
        {
            get { return _IDDatosBancarios; }
            set { _IDDatosBancarios = value; }
        }

        private string _IDCliente;
        /// <summary>
        /// Identificador del cliente al que pertenecen los datos bancarios
        /// </summary>
        public string IDCliente
        {
            get { return _IDCliente; }
            set { _IDCliente = value; }
        }

        private int _IDBanco;
        /// <summary>
        /// Identificador del banco de la cuenta bancaria
        /// </summary>
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
        #endregion
    }
}