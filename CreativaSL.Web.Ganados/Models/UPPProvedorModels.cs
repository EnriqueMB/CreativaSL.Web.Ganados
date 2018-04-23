using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class UPPProvedorModels
    {
        private string _id_proveedor;

        public string id_proveedor
        {
            get { return _id_proveedor; }
            set { _id_proveedor = value; }
        }
        private string _UPP;
        [Required(ErrorMessage = "El UPP es obligatorio")]
        [Display(Name = "UPP")]
        public string UPP
        {
            get { return _UPP; }
            set { _UPP = value; }
        }
        private DateTime _fechaAlta;
        [Required(ErrorMessage = "La fecha de alta es obligatoria")]
        [Display(Name = "Fecha de alta")]
        public DateTime fechaAlta
        {
            get { return _fechaAlta; }
            set { _fechaAlta = value; }
        }
       
        private string _nombrePredio;
        [Required(ErrorMessage = "El nombre del predio es obligatorio")]
        [Display(Name = "Nombre del predio")]
        public string nombrePredio
        {
            get { return _nombrePredio; }
            set { _nombrePredio = value; }
        }
        private string _propietario;
        [Required(ErrorMessage = "El Nombe del propietario es obligatorio")]
        [Display(Name = "Nombre Propietario")]
        public string propietario
        {
            get { return _propietario; }
            set { _propietario = value; }
        }
        public HttpPostedFileBase[] ImagenB { get; set; }
        [Display(Name = "Imagen UPP")]
        public HttpPostedFileBase[] ImagenEdt { get; set; }
        private string _Imagen;

        public string Imagen
        {
            get { return _Imagen; }
            set { _Imagen = value; }
        }

        private bool _BandImg;

        public bool BandImg
        {
            get { return _BandImg; }
            set { _BandImg = value; }
        }

        private List<UPPProvedorModels> _listaUPPProveedores;

        public List<UPPProvedorModels> listaUPPProveedores
        {
            get { return _listaUPPProveedores; }
            set { _listaUPPProveedores = value; }
        }

        //LISTA DE PAISES, ESTADOS Y MUNICIPIOS
        private List<CatPaisModels> _listaPaises;


        public List<CatPaisModels> listaPaises
        {
            get { return _listaPaises; }
            set { _listaPaises = value; }
        }
        private List<CatEstadoModels> _listaEstado;

        public List<CatEstadoModels> listaEstado
        {
            get { return _listaEstado; }
            set { _listaEstado = value; }
        }
        private List<CatMunicipioModels> _listaMunicipio;

        public List<CatMunicipioModels> listaMunicipio
        {
            get { return _listaMunicipio; }
            set { _listaMunicipio = value; }
        }

        //PAIS, ESTADO, MUNICIPIO Y EJIDO DEL LUGAR
        private string _municipio;

        public string municipio
        {
            get { return _municipio; }
            set { _municipio = value; }
        }

        private int _id_municipio;

        public int id_municipio
        {
            get { return _id_municipio; }
            set { _id_municipio = value; }
        }
        private string _id_estadoCodigo;

        public string id_estadoCodigo
        {
            get { return _id_estadoCodigo; }
            set { _id_estadoCodigo = value; }
        }

        private int _id_estado;

        public int id_estado
        {
            get { return _id_estado; }
            set { _id_estado = value; }
        }
        private string _id_pais;

        public string id_pais
        {
            get { return _id_pais; }
            set { _id_pais = value; }
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