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

        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Empresa = (CatEmpresaModels)validationContext.ObjectInstance;
            imgServer = Empresa.ImagBDEmpresa;

            if ((Empresa.LogoEmpresaHttp == null) && (imgServer == false))
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            else
                return ValidationResult.Success;
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