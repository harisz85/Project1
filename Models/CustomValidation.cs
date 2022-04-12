using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class DescriptionValidation : ValidationAttribute
    {
        private readonly int MaxNumber;

        public DescriptionValidation(int maxnumber) : base($"{0} please enter a shorter description")
        {
            MaxNumber = maxnumber;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //   var product = (Product)validationContext.ObjectInstance;

            var text = value.ToString();
            var errorMessage = FormatErrorMessage(validationContext.DisplayName);

            if (text.Split(' ').Length <= MaxNumber)
                return ValidationResult.Success;

            else
            {
                return new ValidationResult(errorMessage);
            }

        }
    }
}