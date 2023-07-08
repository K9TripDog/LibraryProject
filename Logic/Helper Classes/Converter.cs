using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Logic
{
    public class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double doubleValue && parameter is string ConvertType)
            {
                if (ConvertType == "C")
                {
                    return string.Format("{0:C}", doubleValue);
                }
                else if (ConvertType == "P")
                {
                    return string.Format("{0:P}", doubleValue/100);
                }
            }

            if (value is DateTime dateTime)
            {
               return dateTime.ToShortDateString();
            }
            else if (value is bool Late)
            {
                if (Late)
                    return "Item is late on return";
                else
                    return string.Empty;
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private string GetCurrencySymbol(string currencyCode)
        {
            // Add logic to retrieve the currency symbol based on the currency code
            return "$";
        }
    }
}
