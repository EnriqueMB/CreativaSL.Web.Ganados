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

        public string NombreSucursal { get; set; }

        [Required]
        [DisplayName("razón fiscal")]
        [StringLength(150, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 5)]
        public string RazonFiscal { get; set; }
        
        [Required]
        [DisplayName("direccion fiscal")]
        [StringLength(150, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 10)]
        public string DireccionFiscal { get; set; }

        [Required]
        [DisplayName("R.F.C.")]
        [RFC(ErrorMessage = "Ingrese un RFC válido")]
        [StringLength(15, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 10)]
        public string RFC { get; set; }

        [Required]
        [DisplayName("número telefonico 1")]
        [StringLength(15, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 7)]
        public string NumTelefonico1 { get; set; }

        [DisplayName("número telefonico 2")]
        [StringLength(15)]
        public string NumTelefonico2 { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 5)]
        public string Email { get; set; }

        [DisplayName("horario de atención")]
        [StringLength(150, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 5)]
        public string HorarioAtencion { get; set; }

        [DisplayName("Representante")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 10)]
        public string Representante { get; set; }

        //IMAGENES

        [InputFile(1)]
        [DisplayName("logo de la empresa")]
        public HttpPostedFileBase LogoEmpresaHttp { get; set; }
        public string LogoEmpresa { get; set; }

        [InputFile(2)]
        [DisplayName("logo del RFC")]
        public HttpPostedFileBase LogoRFCHttp { get; set; }
        public string LogoRFC { get; set; }
        

        public bool ImagBDEmpresa { get; set; }
        public bool ImagBDRFC { get; set; }


        public string Conexion { get; set; }
        public string IDUsuario { get; set; }

        public RespuestaAjax  RespuestaAjax { get; set; }
        public CatBancoModels Banco { get; set; }
        public CuentaBancariaModels CuentaBancaria { get; set; }

        public List<CatEmpresaModels> ListaEmpresas { get; set; }
        public List<CatBancoModels> ListaBancos;

        public string TablaCuentasBancarias { get; set; }

        public CatEmpresaModels()
        {
            IDEmpresa = "0";
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
            RespuestaAjax = new RespuestaAjax();
            Banco = new CatBancoModels();
            ListaBancos = new List<CatBancoModels>();
            TablaCuentasBancarias = string.Empty;
            CuentaBancaria = new CuentaBancariaModels();
            NombreSucursal = string.Empty;
            ImagBDEmpresa = false;
            ImagBDRFC = false;
        }

        public string ValidarStringImage(string validar)
        {
            if (string.IsNullOrEmpty(validar) || string.IsNullOrWhiteSpace(validar))
                validar = Auxiliar.SetDefaultImage();
                    
            return validar;
        }
    }
}