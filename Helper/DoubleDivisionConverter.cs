
namespace FroniusReader.Helper
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DoubleDivisionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null && value != null)
            {
                double divisor = double.Parse(parameter.ToString(), CultureInfo.InvariantCulture);
                double inputValue = double.Parse(value.ToString(), CultureInfo.InvariantCulture);
                double returnValue = inputValue / divisor;
                return returnValue;
            }
            else
            {
                return Binding.DoNothing;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

