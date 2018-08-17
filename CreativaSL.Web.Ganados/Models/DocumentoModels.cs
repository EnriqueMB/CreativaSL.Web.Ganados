using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentoModels
    {
        /// <summary>
        /// Puede ser el id de la compra, venta o flete
        /// </summary>
        public string Id_servicio { get; set; }
        public int CobrarFlete { get; set; }
        public string Id_documentoPorPagar { get; set; }
        public decimal PrecioUnitarioDocumentacion { get; set; }
        public int Id_conceptoSalidaDeduccion { get; set; }
        public List<CatTipoClasificacionModels> ListaConceptosSalidaDeduccion { get; set; }
        public List<CatTipoClasificacionCobroModels> ListaConceptosSalidaDeduccionCobro { get; set; }

        public RespuestaAjax RespuestaAjax { get; set; }
        public string Conexion { get; set; }
        public string Usuario { get; set; }

        public int IDTipoDocumento { get; set; }
        public string IDDocumento { get; set; }
        public string Clave { get; set; }
        //string de la bd
        public string ImagenServer { get; set; }
        //imagen para mostrar
        public string MostrarImagen { get; set; }
        //extension de la imagen
        public string ExtensionImagenBase64 { get; set; }
        //File input
        [DisplayName("Imagen")]
        public HttpPostedFileBase ImagenPost { get; set; }
        //Select 
        public List<CatTipoDocumentoModels> ListaTipoDocumentos { get; set; }

    }
}