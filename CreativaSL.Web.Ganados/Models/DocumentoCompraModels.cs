using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentoCompraModels
    {
        public DocumentoCompraModels()
        {
            RespuestaAjax = new RespuestaAjax();
        }

        public int IDTipoDocumento { get; set; }
        public string IDCompra {get; set; }
        public string IDDocumento { get; set; }
        public string Clave { get; set; }
        //string de la bd
        public string Imagen { get; set; }
        //imagen para mostrar
        public string MostrarImagen { get; set; }
        //extension de la imagen
        public string ExtensionImagenBase64 { get; set; }
        //File input
        [DisplayName("Imagen")]
        public HttpPostedFileBase ImagenPost { get; set; }
        //bandera de la bd
        public bool FlagImg { get; set; }

        //Select 
        public List<CatTipoDocumentoModels> ListaTipoDocumentos { get; set; }

        public string Conexion { get; set; }
        public string Usuario { get; set; }
        public RespuestaAjax RespuestaAjax { get; set; }

    }
}