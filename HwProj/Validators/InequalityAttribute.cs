using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HwProj.Validators
{
    public class InequalityAttribute : CompareAttribute
    {
        public InequalityAttribute(string otherProperty) : base(otherProperty)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var res = base.IsValid(value, validationContext);
            //почему так? спросите у майков (читайте исходники CompareAttribute)
            return res != null ? null : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}