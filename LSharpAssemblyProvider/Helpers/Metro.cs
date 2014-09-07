using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GalaSoft.MvvmLight.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace LSharpAssemblyProvider.Helpers
{
    public static class Extensions
    {
        public static void AutoResizeColumnWidths(this DataGrid dataGrid, params DataGridLength[] cols)
        {
            if (cols.Length > dataGrid.Columns.Count)
                return;

            foreach (var column in dataGrid.Columns)
                column.Width = 0;

            dataGrid.UpdateLayout();

            if (cols.Length > 0)
            {
                for (int i = 0; i < cols.Length; i++)
                {
                    dataGrid.Columns[i].Width = cols[i];
                }
            }
            else
            {
                foreach (var column in dataGrid.Columns)
                    column.Width = DataGridLength.Auto;
            }

            dataGrid.UpdateLayout();
        }
    }

    public static class DialogService
    {
        public static Task<MessageDialogResult> ShowMessage(string title, string message, MessageDialogStyle dialogStyle)
        {
            Task<MessageDialogResult> result = null;
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                var metroWindow = (Application.Current.MainWindow as MetroWindow);
                metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
                result = metroWindow.ShowMessageAsync(title, message, dialogStyle, metroWindow.MetroDialogOptions);
            });
            Thread.Sleep(100);
            return result;
        }
    }

    public sealed class BooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public BooleanToVisibilityConverter() :
            base(Visibility.Visible, Visibility.Collapsed) { }
    }

    public sealed class BooleanInverterConverter : BooleanConverter<bool>
    {
        public BooleanInverterConverter() : base(true, false) { }
    }



    public class BooleanConverter<T> : IValueConverter
    {
        public BooleanConverter(T trueValue, T falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        public T True { get; set; }
        public T False { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && ((bool)value) ? True : False;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T && EqualityComparer<T>.Default.Equals((T)value, True);
        }
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
