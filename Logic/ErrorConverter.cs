using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using ExamSystem;

namespace ExamSystem.Logic
{
    class MyErrorConverter : IValueConverter
    {
        public object Convert(object value,
            Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            if (value is not ReadOnlyCollection<ValidationError> errors) return "";
            var s = "";
            foreach (var e in errors.Where(e => e.ErrorContent != null))
                s += " " + e.ErrorContent.ToString();
            return s;
        }
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
