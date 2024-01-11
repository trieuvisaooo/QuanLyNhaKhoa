using Microsoft.UI.Xaml.Data;
using System;

namespace QuanLyNhaKhoa.Converters
{
    public class IntToVnd : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string result = value.ToString();
            if (result.Length > 3)
            {
                int count = 0;
                for (int i = result.Length - 1; i >= 0; i--)
                {
                    count++;
                    if (count == 3 && (i != 0))
                    {
                        result = result.Insert(i, ".");
                        count = 0;
                    }
                }
            }
            return $"{result}₫";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Convert string to int
            if (int.TryParse(value.ToString().Substring(1), out int result))
            {
                return result;
            }
            return 0; // Return DependencyProperty.UnsetValue to indicate conversion failure
        }
    }
}
