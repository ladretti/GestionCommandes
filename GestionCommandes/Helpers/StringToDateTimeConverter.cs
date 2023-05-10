using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace GestionCommandes.Helpers
{
    class StringToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
                return new DateTimeOffset(((DateTime)value).ToUniversalTime());

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is DateTimeOffset dateTimeOffsetValue)
                return dateTimeOffsetValue.DateTime;

            return null;
        }


    }
}
