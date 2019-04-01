using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CompraFierroModels
    {
        public string IdCompraFierro { get; set; }
        public string IdCompra { get; set; }
        public string IdFierro { get; set; }
        public string NombreFierro { get; set; }
        public string Observacion { get; set; }
        public string NombreImagen { get; set; }

        public List<CompraFierroModels> ListaFierroCompra { get; set; }
        public List<CompraFierroModels> ListaFierroXCompra { get; set; }
        public string Conexion { get; set; }
        public string IDUsuario { get; set; }
    }
}