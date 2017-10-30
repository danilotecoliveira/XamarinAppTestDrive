using System;
using System.Globalization;
using Xamarin.Forms;

namespace TestDrive.Converters
{
    public class AgendamentoConfirmadoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var confirmado = (bool)value;
            return (confirmado) ? Color.Black : Color.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
