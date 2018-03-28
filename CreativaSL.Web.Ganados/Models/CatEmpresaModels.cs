using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatEmpresaModels
    {
        [Required]
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
        [RFCAttribute]
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

        [Required]
        [DisplayName("logo de la empresa")]
        public string LogoEmpresa { get; set; }

        [DisplayName("horario de atención")]
        [StringLength(150, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 5)]
        public string HorarioAtencion { get; set; }

        [DisplayName("Representante")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 10)]
        public string Representante { get; set; }

        [Required]
        [DisplayName("logo del RFC")]
        public string LogoRFC { get; set; }

        public string Conexion { get; set; }
        public List<CatEmpresaModels> ListaEmpresas { get; set; }

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
        }
    }
}