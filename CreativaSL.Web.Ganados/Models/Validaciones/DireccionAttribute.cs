﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CreativaSL.Web.Ganados.Models.Validaciones
{
    public class DireccionAttribute : ValidationAttribute        
   {
        public DireccionAttribute() : base("El campo {0} es inválido")
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string pattern = @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\w\s\.\-\#]*$";
            if (value != null)
            {
                if (!Regex.IsMatch(value.ToString(), pattern))
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}