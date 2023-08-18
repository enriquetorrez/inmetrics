using System.ComponentModel.DataAnnotations;

namespace InMetrics.BusinessRules
{
    public class GreaterThanZeroDecimalAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is decimal amount && amount <= 0)
                return new ValidationResult("Amount needs to be greater than zero");
            return ValidationResult.Success;
        }
    }
}
