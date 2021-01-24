using System.Globalization;
using System.Windows.Controls;

namespace View.Validators
{
    class CardNumberValidator : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int num = ((string)value).Length;
            if (num < Min || num > Max)
            {
                return new ValidationResult(false, "Number's length must be between " + Min + " and " + Max);
            }
            return ValidationResult.ValidResult;
        }
    }
}
