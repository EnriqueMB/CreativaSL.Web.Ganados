using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatCorralModels
    {
        public CatCorralModels()
        {
            Id_corral = 0;
            Descripcion = string.Empty;
            _ListaCorral= new List<CatCorralModels>();
            ListaSucursales = new List<CatSucursalesModels>();
        }
        [Required]
        public string Id_sucursal { get; set; }
        public List<CatSucursalesModels> ListaSucursales { get; set; }

        private int _Id_corral;

        public int Id_corral
        {
            get { return _Id_corral; }
            set { _Id_corral = value; }
        }
        private string _Descripcion;

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        private List<CatCorralModels> _ListaCorral;

        public List<CatCorralModels> ListaCorral
        {
            get { return _ListaCorral; }
            set { _ListaCorral = value; }
        }

         public string conexion { get; set; }
        public string Usuario { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public int Opcion { get; set; }



    }
}