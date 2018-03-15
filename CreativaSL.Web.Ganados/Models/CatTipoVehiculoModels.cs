using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatTipoVehiculoModels
        
    {
        public CatTipoVehiculoModels() {
            _IDTipoVehiculo = 0;
            _Descripcion = string.Empty;
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Usuario = string.Empty;
            Opcion = 0;
        }
        private List<CatTipoVehiculoModels> _listaTipoVehiculos;

        public List<CatTipoVehiculoModels> listaTipoVehiculos
        {
            get { return _listaTipoVehiculos; }
            set { _listaTipoVehiculos = value; }
        }


        private int _IDTipoVehiculo;

        public int IDTipoVehiculo
        {
            get { return _IDTipoVehiculo; }
            set { _IDTipoVehiculo = value; }
        }

        private string _Descripcion;
        [Required(ErrorMessage = "El tipo de vehículo es obligatorio")]
        [Display(Name = " tipo de vehículo")]
        [StringLength(80, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\(\)\-\,\.\;\:\s]*$", ErrorMessage = "Solo Letras y números")]
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
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