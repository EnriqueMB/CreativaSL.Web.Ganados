using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CompraFierroModels
    {
        public CompraFierroModels()
        {
            IdCompraFierro = string.Empty;
            IdCompra = string.Empty;
            IdFierro = string.Empty;
            NombreFierro = string.Empty;
            Observacion = string.Empty;
            NombreImagen = string.Empty;
            ListaFierroCompra = new List<CompraFierroModels>();
            ListaFierroXCompra = new List<CompraFierroModels>();
            Conexion = string.Empty;
            IDUsuario = string.Empty;
        }
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

        public int numeroFierro { get; set; }
        public int offset { get; set; }
        public int current { get; set; }
        public int totalFierro { get; set; }
        private int _fetchNext = 20;
        public int fetchNext
        {
            get { return _fetchNext; }
            set { _fetchNext = value; }
        }
    }
}