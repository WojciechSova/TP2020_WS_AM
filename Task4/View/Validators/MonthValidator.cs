using System;
using System.Globalization;
using System.Windows.Controls;

namespace View.Validators
{
    class MonthValidator : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int num = Int32.Parse((string)value);
            if (num < Min || num > Max)
            {
                return new ValidationResult(false, "Not valid month");
            }
            return ValidationResult.ValidResult;
        }
    }
}
