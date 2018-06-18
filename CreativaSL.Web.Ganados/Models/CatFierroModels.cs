using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatFierroModels
    {
        public CatFierroModels()
        {
            _IDFierro = string.Empty;
            _NombreFierro = string.Empty;
            _ImgFierro = string.Empty;
            _Observaciones = string.Empty;
            _ListaFierro = new List<CatFierroModels>();
            _ImagenContruida = string.Empty;
        }

        public RespuestaAjax RespuestaAjax { get; set; }

        private string _IDFierro;

        public string IDFierro
        {
            get { return _IDFierro; }
            set { _IDFierro = value; }
        }

        private string _NombreFierro;
        [Required(ErrorMessage = "El nombre fierro es obligatorio")]
        [Display(Name = "Nombre Fierro")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\(\)\-\,\.\;\:\s]*$", ErrorMessage = "Solo Letras y números")]
        public string NombreFierro
        {
            get { return _NombreFierro; }
            set { _NombreFierro = value; }
        }

        private string _ImgFierro;

        public string ImgFierro
        {
            get { return _ImgFierro; }
            set { _ImgFierro = value; }
        }

        private string _Observaciones;
        [Required(ErrorMessage = "La observación es obligatorio")]
        [Display(Name = "Observación")]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\(\)\-\,\.\;\:\s]*$", ErrorMessage = "Solo Letras y números")]
        public string Observaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
        }

        private List<CatFierroModels> _ListaFierro;

        public List<CatFierroModels> ListaFierro
        {
            get { return _ListaFierro; }
            set { _ListaFierro = value; }
        }

        private HttpPostedFileBase _foto;
        [Required(ErrorMessage = "La Imagen es obligatorio")]
        [Display(Name = "Imagen")]
        [FileExtensions(Extensions = "png,jpg,jpeg", ErrorMessage = "Solo imagenes")]
        public HttpPostedFileBase foto
        {
            get { return _foto; }
            set { _foto = value; }
        }

        private HttpPostedFileBase _foto2;
        [Display(Name = "Imagen")]
        [FileExtensions(Extensions = "png,jpg,jpeg", ErrorMessage = "Solo imagenes")]
        public HttpPostedFileBase foto2
        {
            get { return _foto2; }
            set { _foto2 = value; }
        }
        private string _ImagenContruida;

        public string ImagenContruida
        {
            get { return _ImagenContruida; }
            set { _ImagenContruida = value; }
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