using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ben.Tools.Asp.Helpers
{
    // Validator.[Try]{Validate}[Object|Value|Property].
    public static class ValidatorHelper
    {
        public static List<ValidationResult> GetValidationResults(object modelToValidate)
        {
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(
                modelToValidate,
                new ValidationContext(modelToValidate, null, null),
                validationResults);

            return validationResults;
        }

        // Préférer ModelState.IsValid.
        public static bool IsValid(object modelToValidate)
        {
            return GetValidationResults(modelToValidate).Any();
        }

        public static IEnumerable<string> GetValidationMessages(object modelToValidate)
        {
            return GetValidationResults(modelToValidate).Select(validationResult => validationResult.ErrorMessage);
        }

        public static string GetValidationMessage(object modelToValidate)
        {
            return string.Join(Environment.NewLine, GetValidationMessages(modelToValidate));
        }
    }
}