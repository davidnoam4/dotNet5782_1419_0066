﻿using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PL
{
    public class NotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool) value;
        }
    };

    public class MultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Cast<bool>().Any(x => x) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FromColorTextToIsEnable : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Cast<SolidColorBrush>().Any(x =>  x == Brushes.Red) ? false : true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IdTextToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id;
            if (value.ToString() == "" || !value.ToString().All(char.IsDigit))
                id = 0;
            else
                id = int.Parse(value.ToString());

            if (id < 100000 || id > 999999) // Check that it's 6 digits.
                return Brushes.Red;
            
            return Brushes.SlateGray;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IdCustomerTextToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id;
            if (value.ToString() == "" || !value.ToString().All(char.IsDigit))
                id = 0;
            else
                id = int.Parse(value.ToString());

            if (id < 10000000 || id > 99999999) // Check that it's 6 digits.
                return Brushes.Red;
            
            return Brushes.SlateGray;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ModelOrNameTextToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "")
                return Brushes.Red;
            
            return Brushes.SlateGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ComboBoxToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Brushes.Red;
            
            return Brushes.SlateGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class LocationTextToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { 
            double location;
            if (!double.TryParse(value.ToString(), out location))
                location = -2;
            else
                location = System.Convert.ToDouble(value.ToString());

            if (location < -1 || location > 1)
                return Brushes.Red; 
            
            return Brushes.SlateGray;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ChargeSlotsTextToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int chargeSlots;
            if (value.ToString() == "" || !value.ToString().All(char.IsDigit))
                chargeSlots = -1;
            else
                chargeSlots = int.Parse(value.ToString());

            if (chargeSlots < 0) 
                return Brushes.Red;
            
            return Brushes.SlateGray;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class EmptyListToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) 
                return false;
            
            return true;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PhoneTextToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int phone;
            if (value.ToString().Length != 10 || value.ToString().Substring(0, 2) != "05" ||
                !int.TryParse(value.ToString().Substring(2, value.ToString().Length), out phone))  // check format phone
                return Brushes.Red;

            return Brushes.SlateGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
