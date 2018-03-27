using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatTipoProveedorModels
    {
        private int _IDTipoProveedor;

        public int IDTipoProveedor
        {
            get { return _IDTipoProveedor; }
            set { _IDTipoProveedor = value; }
        }

        private string _Descripcion;
        [Required(ErrorMessage = "La Tipo de proveedor es obligatorio")]
        [Display(Name = "Tipo de proveedor")]
        [Texto(ErrorMessage = "Solo letras")]
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        private List<CatTipoProveedorModels> _listaTipoProveedores;

        public List<CatTipoProveedorModels> listaTipoProveedores
        {
            get { return _listaTipoProveedores; }
            set { _listaTipoProveedores = value; }
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