using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatChoferModels
    {
        public CatChoferModels() {
          
            _id_licencia = string.Empty;
            _numLicencia = string.Empty;
            _vigencia = DateTime.Now;
            _IDChofer = string.Empty;
            _Nombre = string.Empty;
            _ApPaterno = string.Empty;
            _ApMaterno = string.Empty;
            _Licencia = true;
            _Estatus = false;
            //Datos de control
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Usuario = string.Empty;
            Opcion = 0;
        }
        /// <summary>
        /// LICENCIA DEL CHOFER
        /// </summary>
        private string _id_licencia;

        public string id_licencia
        {
            get { return _id_licencia; }
            set { _id_licencia = value; }
        }
       /// <summary>
       /// NUMERO DE LICENCIA DEL CHOFER
       /// </summary>
        private string _numLicencia;
        [Display(Name = "número licencia")]
        [StringLength(20, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s]*$", ErrorMessage = "Solo Letras y número")]
        public string numLicencia
        {
            get { return _numLicencia; }
            set { _numLicencia = value; }
        }
        /// <summary>
        /// VIGENCIA DE LA LICENCIA
        /// </summary>
        private DateTime _vigencia;

        public DateTime vigencia
        {
            get { return _vigencia; }
            set { _vigencia = value; }
        }
        private List<CatChoferModels> _ListaChoferes;

        public List<CatChoferModels> ListaChoferes
        {
            get { return _ListaChoferes; }
            set { _ListaChoferes = value; }
        }

        /// <summary>
        /// El Identificacidor de por chofer
        /// </summary>
        private string _IDChofer;

        public string IDChofer
        {
            get { return _IDChofer; }
            set { _IDChofer = value; }
        }
        /// <summary>
        /// El nombre del chofer
        /// </summary>
        private string _Nombre;
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "nombre")]
        [StringLength(80, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]*$", ErrorMessage = "Solo Letras")]
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        /// <summary>
        /// El Apellido Parteno del Chofer
        /// </summary>
        private string _ApPaterno;
        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [Display(Name = "apellido paterno")]
        [StringLength(70, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]*$", ErrorMessage = "Solo Letras")]
        public string ApPaterno
        {
            get { return _ApPaterno; }
            set { _ApPaterno = value; }
        }
        /// <summary>
        /// El Apellido Materno del Chofer
        /// </summary>
        private string _ApMaterno;
        [Display(Name = "apellido materno")]
        [StringLength(70, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]*$", ErrorMessage = "Solo Letras")]
        public string ApMaterno
        {
            get { return _ApMaterno; }
            set { _ApMaterno = value; }
        }
        /// <summary>
        /// Para ver si su Licencia del Chofer si tiene vigencia
        /// </summary>
        private bool _Licencia;

        public bool Licencia
        {
            get { return _Licencia; }
            set { _Licencia = value; }
        }
        /// <summary>
        /// Para saber si el chofer no se encuentra en viaje
        /// </summary>
        private bool _Estatus;

        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        private DataTable _TablaDatos;

        public DataTable TablaDatos
        {
            get { return _TablaDatos; }
            set { _TablaDatos = value; }
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