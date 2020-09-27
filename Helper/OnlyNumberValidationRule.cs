using System.Globalization;
using System.Windows.Controls;

namespace FroniusReader.Helper
{
    public class OnlyNumberValidationRule : ValidationRule
    {
        public double Min { get; set; }
        public double Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var validationResult = new ValidationResult(true, null);

            if (value != null)
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    bool parsingOk = double.TryParse(value.ToString(), out double parsedValue);
                    if (parsingOk)
                    {
                        if ((parsedValue < Min) || (parsedValue > Max))
                        {
                            validationResult = new ValidationResult(false, $"Please enter a value in the range: {Min}-{Max}.");
                        }
                    }
                    else
                    {
                        validationResult = new ValidationResult(false, "Illegal Characters, Please Enter Numeric Value");
                    }
                }
                else
                {
                    validationResult = new ValidationResult(false, "String is empty. Please enter a number.");
                }
            }
            else
            {
                validationResult = new ValidationResult(false, "String is null. Please enter a number.");
            }

            return validationResult;
        }
    }
}
