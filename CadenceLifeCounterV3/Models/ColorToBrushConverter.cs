﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace CadenceLifeCounterV3.Models
{
    class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (null == value)
            {
                return null;
            }
            // For a more sophisticated converter, check also the targetType and react accordingly..
            if (value is Color)
            {
                Color color = (Color)value;
                return new SolidColorBrush(color);
            }
            else if (value is ColorSwatchModel)
            {
                return ((ColorSwatchModel)value).GetBrush();
            }
            // You can support here more source types if you wish
            // For the example I throw an exception

            Type type = value.GetType();
            throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
