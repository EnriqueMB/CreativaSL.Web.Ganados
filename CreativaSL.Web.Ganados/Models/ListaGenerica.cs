using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
namespace CreativaSL.Web.Ganados.Models
{
    public class ListaGenerica
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public Image Imagen { get; set; }
    }
}