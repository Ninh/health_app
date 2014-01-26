using System;
using System.Windows.Media;
using System.Windows.Data;

namespace LocalDatabaseSample
{
    // Convert task priority to a brush that can be bound to in the UI.
    // Added in Version 2 of the application.
    public class PriorityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Allow for tasks that have no priority.
            int? priority = (int?)value;

            // Assign color based on priority.
            switch (priority)
            {
                case null:
                    return new SolidColorBrush(Colors.White);
                case 1:
                    return new SolidColorBrush(Colors.Green);
                case 2:
                    return new SolidColorBrush(Colors.Yellow);
                case 3:
                    return new SolidColorBrush(Colors.Red);
                default:
                    return new SolidColorBrush(Colors.White);
            }
        }

        // Unused; required for IValueConverter implementation.
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        { throw new NotImplementedException(); }
    }
}
