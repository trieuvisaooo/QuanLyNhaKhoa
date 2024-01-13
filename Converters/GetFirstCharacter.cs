using Microsoft.UI.Xaml.Data;
using System;
using System.Linq;

namespace QuanLyNhaKhoa.Converters
{
    public class GetFirstCharacter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Convert string to (string)
            string stringValue = value?.ToString();

            if (!string.IsNullOrEmpty(stringValue))
            {
                // Split the string into words
                string[] words = stringValue.Split(' ');

                // Get the last word
                string lastWord = words.LastOrDefault();

                if (!string.IsNullOrEmpty(lastWord))
                {
                    // Get the first character of the last word
                    return $"{lastWord[0]}";
                }
            }

            // Handle the case where the string is null or empty
            return "_";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Convert string to int
            return value;// Return DependencyProperty.UnsetValue to indicate conversion failure
        }
    }
}
