using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VinaSale.Infrastructure.Domain.Validation
{
    [Serializable]
    public class ValidationException : Exception
    {
        private readonly IList<ValidationResult> validationResults;

        public ValidationException(IList<ValidationResult> validationResults)
        {
            this.validationResults = validationResults;
        }

        public override string Message
        {
            get
            {
                return validationResults.Aggregate("Validation exeption:", (current, item) => string.Concat(current, " ", item.ErrorMessage));
            }
        }

        public IList<ValidationResult> ValidationResults
        {
            get
            {
                return validationResults;
            }
        }
    }
}
