using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatBancoModels
    {

        public CatBancoModels()
        {
            _IDBanco = 0;
            _Descripcion = string.Empty;
            _Imagen = string.Empty;
            _listaBancos = new List<CatBancoModels>();
        }

        private int _IDBanco;
        /// <summary>
        /// Identificador del banco
        /// </summary>
        public int IDBanco
        {
            get { return _IDBanco; }
            set { _IDBanco = value; }
        }

        [Required(ErrorMessage = "Debe ingresar el nombre del banco")]
        [Display(Name = "Nombre del banco")]
        [StringLength(100, ErrorMessage = "El número de caracteres del campo {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto(ErrorMessage = "Ingrese un dato válido para {0}")]
        private string _Descripcion;
        /// <summary>
        /// Descripción o nombre del banco
        /// </summary>
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        //public string NombreBanco { get; set; }

        [Required(ErrorMessage = "Seleccione la imagen del Banco")]
        [Display(Name = "Imagen Banco")]


        public HttpPostedFileBase[] ImagenB { get; set; }
        [Display(Name = "Imagen Banco")]
        public HttpPostedFileBase[] ImagenEdt { get; set; }

        private string _Imagen;
        /// <summary>
        /// Imagen en formato base64 del banco 
        /// </summary>
        public string Imagen
        {
            get { return _Imagen; }
            set { _Imagen = value; }
        }


        private List<CatBancoModels> _listaBancos;

        public List<CatBancoModels> listaBancos
        {
            get { return _listaBancos; }
            set { _listaBancos = value; }
        }
        private bool _BandImg;

        public bool BandImg
        {
            get { return _BandImg; }
            set { _BandImg = value; }
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