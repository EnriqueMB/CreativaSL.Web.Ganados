using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Models.Validaciones
{
    public class InputFile : ValidationAttribute, IClientValidatable
    {
        private bool imgServer;
        private string extension;
        string[] explodedFile;
        private string nameFile;
        private int opcion;

        public InputFile(int opcion)
        {
            this.opcion = opcion;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HttpPostedFileBase input = (HttpPostedFileBase)value;

            int longitud = 0;
            var Empresa = (CatEmpresaModels)validationContext.ObjectInstance;

            //Si opcion 1 = LogoEmpresa
            //Si opcion 2 = LogoRFC

            if (opcion == 1)
            {
                imgServer = Empresa.ImagBDEmpresa;
            }
            else if(opcion == 2)
            {
                imgServer = Empresa.ImagBDRFC;
            }

            if (imgServer)
            {
                if(input!= null)
                {
                    //Checo la extension
                    nameFile = input.FileName;
                    explodedFile = nameFile.Split('.');
                    longitud = explodedFile.Length;
                    extension = explodedFile[longitud - 1];

                    //Extension valida solo png
                    if (string.Equals(extension, "png") || string.Equals(extension, "jpg") || string.Equals(extension, "jpeg") || string.Equals(extension, "bmp"))
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName + "solo extensión png, jpg, jpeg y bmp."));
                    }
                }
                else
                {
                    //Todo bien ya que hay imagen en el server
                    return ValidationResult.Success;
                }
            }
            else
            {
                if (input!= null)
                {
                    //Checo la extension
                    nameFile = input.FileName;
                    explodedFile = nameFile.Split('.');
                    longitud = explodedFile.Length;
                    extension = explodedFile[longitud - 1];

                    //Extension valida solo png
                    if (string.Equals(extension, "png") || string.Equals(extension, "jpg") || string.Equals(extension, "jpeg") || string.Equals(extension, "bmp"))
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName + "solo extensión png, jpg, jpeg y bmp."));
                    }
                }
                else
                {
                    //Error no hay imagen en el server
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            }
        }
        //new method  
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("imgserver", imgServer);
            rule.ValidationType = "validateimg";
            yield return rule;
        }

    }
}