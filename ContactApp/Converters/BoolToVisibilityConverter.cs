using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Data;

namespace ContactApp.Converters
{
    public class BoolToVisibilityConverter:IValueConverter
    {
        public bool Negate { get; set; }


        private Visibility _visibility;

        public BoolToVisibilityConverter()
        {
            _visibility = Visibility.Collapsed;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mode = (bool) value;

            // Negate True is Add Mode
            // Negate False is Edit Mode


            // Negate: True
            // EditMode: False

            // add mode visible

            // Negate: True
            // EditMode: True

            // add mode collapsed

            // Negate: False
            // Edit Mode: False

            // edit mode collapsed

            // Negate False
            // Edit Mode: True

            // edit mode visible


            if (Negate && !mode)
                return _visibility == Visibility.Visible;

            if (Negate && mode)
                return Visibility.Collapsed;

            if (!Negate && !mode)
                return Visibility.Collapsed;

            if (!Negate && mode)
                return Visibility.Visible;

            return _visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}