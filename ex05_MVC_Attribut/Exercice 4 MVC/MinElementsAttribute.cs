using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Exercice_5_MVC
{
    public class MinElementsAttribute : ValidationAttribute
    {
        private readonly int _minElements;

        public MinElementsAttribute(int minElements)
        {
            _minElements = minElements;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as ICollection;
            if (list != null && list.Count >= _minElements)
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessage ?? $"La liste doit contenir au moins {_minElements} élément(s).");
        }
    }
}