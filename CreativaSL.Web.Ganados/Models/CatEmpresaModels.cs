using CreativaSL.Web.Ganados.Models.Validaciones;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatEmpresaModels
    {
        public string IDEmpresa { get; set; }

        [Required]
        [DisplayName("razón fiscal")]
        [StringLength(150, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 10)]
        public string RazonFiscal { get; set; }

        [Required]
        [DisplayName("direccion fiscal")]
        [StringLength(150, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 10)]
        public string DireccionFiscal { get; set; }

        [Required]
        [DisplayName("RFC")]
        [RFC(ErrorMessage = "Ingrese un RFC válido")]
        [StringLength(15, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 10)]
        public string RFC { get; set; }

        [Required]
        [DisplayName("número telefonico 1")]
        public string NumTelefonico1 { get; set; }

        [DisplayName("número telefonico 2")]
        public string NumTelefonico2 { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 5)]
        public string Email { get; set; }

        
        [DisplayName("logo de la empresa")]
        public HttpPostedFileBase LogoEmpresaHttp { get; set; }
        public string LogoEmpresa { get; set; }

        [DisplayName("horario de atención")]
        [StringLength(150, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 5)]
        public string HorarioAtencion { get; set; }

        [DisplayName("Representante")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 10)]
        public string Representante { get; set; }

        
        [DisplayName("logo del RFC")]
        public HttpPostedFileBase LogoRFCHttp { get; set; }
        public string LogoRFC { get; set; }

        public string Conexion { get; set; }
        public List<CatEmpresaModels> ListaEmpresas { get; set; }
        public string IDUsuario { get; set; }
        public string MensajeJson { get; set; }
        public bool Error { get; set; }

        public CatEmpresaModels()
        {
            IDEmpresa = string.Empty;
            RazonFiscal = string.Empty;
            DireccionFiscal = string.Empty;
            RFC = string.Empty;
            NumTelefonico1 = string.Empty;
            NumTelefonico2 = string.Empty;
            Email = string.Empty;
            LogoEmpresa = string.Empty;
            HorarioAtencion = string.Empty;
            Representante = string.Empty;
            LogoRFC = string.Empty;
            ListaEmpresas = new List<CatEmpresaModels>();
            Conexion = string.Empty;
            IDUsuario = string.Empty;
            Error = true;
            MensajeJson = "Ha ocurrido un error";
        }

        public string ImageToBase64(HttpPostedFileBase Image)
        {
            var fileContent = Image;
            string Base64 = null;
            if (fileContent != null && fileContent.ContentLength > 0)
            {
                //Obtengo el stream
                var stream = fileContent.InputStream;
                //Genero un array de bytes
                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(stream))
                {
                    fileData = binaryReader.ReadBytes(fileContent.ContentLength);
                }
                //Realizo la convercion a base 64
                Base64 = Convert.ToBase64String(fileData);
            }
            return Base64;
        }
        public string GenerarMensajeJson()
        {
            string MsjPrederterminado = "{\"Mensaje\" : \"" + MensajeJson + "\" , \"Error\": \" " + Error + "\"}";
            JObject json = JObject.Parse(MsjPrederterminado);
            return json.ToString();
        }
    }
}