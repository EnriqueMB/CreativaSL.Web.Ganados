using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatJaulaModels
    {
        public CatJaulaModels() {
            _listaJaulas = new List<CatJaulaModels>();
            _listaSucursales = new List<CatSucursalesModels>();
            _IDJaula = string.Empty;
            _IDSucursal = string.Empty;
            _Matricula = string.Empty;
            _Estatus = true;
            //Datos de control
            activo = false;
            user = string.Empty;
            conexion = string.Empty;
            Completado = false;
            opcion = 0;
            resultado = string.Empty;
            ListaEmpresas = new List<CatEmpresaModels>();
            IDEmpresa = string.Empty;
        }
        /// <summary>
        /// LISTA DE JAULAS PARA INDEX
        /// </summary>

        public List<CatEmpresaModels> ListaEmpresas { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleccione una empresa.")]
        public string  IDEmpresa { get; set; }

        private List<CatJaulaModels> _listaJaulas;

        public List<CatJaulaModels> listaJaulas
        {
            get { return _listaJaulas; }
            set { _listaJaulas = value; }
        }

        private List<CatSucursalesModels> _listaSucursales;
        [Required(ErrorMessage = "La sucursal es obligatorio")]
        [Display(Name = "Sucursal")]
        public List<CatSucursalesModels> listaSucursales
        {
            get { return _listaSucursales; }
            set { _listaSucursales = value; }
        }

        /// <summary>
        /// ID DE JAULA
        /// </summary>
        private string _IDJaula;

        public string IDJaula
        {
            get { return _IDJaula; }
            set { _IDJaula = value; }
        }
        /// <summary>
        /// Se asigna para obtener el NOMBRE de la sucursal
        /// </summary>
        private string _nombreSucursal;

        public string nombreSucursal
        {
            get { return _nombreSucursal; }
            set { _nombreSucursal = value; }
        }

        /// <summary>
        /// ID SUCURSAL
        /// </summary>
        private string _IDSucursal;
      
        
        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }
        /// <summary>
        /// MATRICULA DE JAULA
        /// </summary>
        private string _Matricula;
        [Required(ErrorMessage = "La matricula es obligatoria")]
        [Display(Name = "nombre")]
        [StringLength(36, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s\-]*$", ErrorMessage = "Solo Letras, Números y '-'")]
        public string Matricula
        {
            get { return _Matricula; }
            set { _Matricula = value; }
        }
        /// <summary>
        /// ESTATUS DE JAULA
        /// </summary>
        private bool _Estatus;

        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        #region Datos de control
        public bool activo { get; set; }
        public string user { get; set; }
        public string conexion { get; set; }
        public string resultado { get; set; }
        public int opcion { get; set; }
        public bool Completado { get; set; }
        #endregion
    }
}