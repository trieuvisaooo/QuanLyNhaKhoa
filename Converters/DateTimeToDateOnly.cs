using Microsoft.UI.Xaml.Data;
using System;
using System.Diagnostics;

namespace QuanLyNhaKhoa.Converters
{
    public class DateTimeToDateOnly : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                DateTime date = (DateTime)value;
                DateOnly dol = new DateOnly();
                string s = date.ToString("dd/MM/yyyy");

                return s;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                DateOnly dol = (DateOnly)value;
                DateTime dt = dol.ToDateTime(TimeOnly.MinValue);
                return dt;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ABC" + ex.Message);
                return DateTime.MinValue;
            }
        }
    }
}
