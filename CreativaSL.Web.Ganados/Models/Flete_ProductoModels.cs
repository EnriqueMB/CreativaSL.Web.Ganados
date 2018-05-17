using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class Flete_ProductoModels
    {
        public string ID_Producto { get; set; }
        public string ID_Flete { get; set; }
        public int ID_UnidadMedida { get; set; }
        public double PesoAproximado { get; set; }
        public double Cantidad { get; set; }
        public string Observacion { get; set; }
        public string NumArete { get; set; }
        public string Genero { get; set; }

        public string Conexion { get; set; }
        public string Usuario { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }

        //Listas
        public List<Flete_ProductoModels> ListaProductos { get; set; }
        public string ListaStringIDProductoSeleccionado { get; set; }
    }
}