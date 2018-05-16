using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CuentaBancariasProveedorAlmacenModels
    {
        public CuentaBancariasProveedorAlmacenModels()
        {
            _IDDatosBancarios = string.Empty;
            _IDBanco = 0;
            _Titular = string.Empty;
            _NumCuenta = string.Empty;
            _NumTarjeta = string.Empty;
            _ClabeInterbancaria = string.Empty;
            _ListaCuentaProveedorAlmacen = new List<CuentaBancariasProveedorAlmacenModels>();
            Conexion = string.Empty;
            Usuario = string.Empty;
            _IDProveedorAlmacen = string.Empty;
            _Banco = new CatBancoModels();
            _ListaCmbBancos = new List<CatBancoModels>();
        }

        private CatBancoModels _Banco;

        public CatBancoModels Banco
        {
            get { return _Banco; }
            set { _Banco = value; }
        }

        private string _IDProveedorAlmacen;

        public string IDProveedorAlmacen
        {
            get { return _IDProveedorAlmacen; }
            set { _IDProveedorAlmacen = value; }
        }


        private string _IDDatosBancarios;

        public string IDDatosBancarios
        {
            get { return _IDDatosBancarios; }
            set { _IDDatosBancarios = value; }
        }

        private int _IDBanco;
        [Display(Name = "Banco")]
        [CombosInt(ErrorMessage = "Seleccione un banco")]
        [Required(ErrorMessage = "Seleccione un banco")]
        public int IDBanco
        {
            get { return _IDBanco; }
            set { _IDBanco = value; }
        }

        private string _Titular;
        [Display(Name = "Titular de la cuenta")]
        [Nombre(ErrorMessage = "Ingrese un nombre válido en {0}")]
        [Required(ErrorMessage = "Ingrese el nombre del {0}")]
        public string Titular
        {
            get { return _Titular; }
            set { _Titular = value; }
        }

        private string _NumTarjeta;
        [Display(Name = "Número de tarjeta")]
        [TarjetaCredito(ErrorMessage = "Ingrese un número de tarjeta válido")]
        public string NumTarjeta
        {
            get { return _NumTarjeta; }
            set { _NumTarjeta = value; }
        }

        private string _NumCuenta;
        [Display(Name = "Número de cuenta")]
        [NumeroCuenta(ErrorMessage = "Ingrese un número de cuenta válido ")]
        public string NumCuenta
        {
            get { return _NumCuenta; }
            set { _NumCuenta = value; }
        }

        private string _ClabeInterbancaria;
        [Display(Name = "CLABE interbancaria")]
        [ClabeInterbancaria(ErrorMessage = "Ingrese una CLABE interbancaria válida ")]
        public string ClabeInterbancaria
        {
            get { return _ClabeInterbancaria; }
            set { _ClabeInterbancaria = value; }
        }

        private List<CuentaBancariasProveedorAlmacenModels> _ListaCuentaProveedorAlmacen;

        public List<CuentaBancariasProveedorAlmacenModels> ListaCuentaProveedorAlmacen
        {
            get { return _ListaCuentaProveedorAlmacen; }
            set { _ListaCuentaProveedorAlmacen = value; }
        }

        private List<CatBancoModels> _ListaCmbBancos;

        public List<CatBancoModels> ListaCmbBancos
        {
            get { return _ListaCmbBancos; }
            set { _ListaCmbBancos = value; }
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