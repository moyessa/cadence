using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace CadenceLifeCounterV3.Models
{
    public class ColorSwatchModel
    {
        private List<Color> _colors = new List<Color>();

        public ColorSwatchModel() { }

        public ColorSwatchModel(Color color)
        {
            _colors.Add(color);
        }

        public ColorSwatchModel(List<Color> colors)
        {
            _colors = colors;
        }

        public Brush SwatchBrush
        {
            get
            {
                return GetBrush();
            }
        }


        public Color SwatchColor
        {
            get
            {
                return _colors[0];
            }
            set
            {
                _colors.Add(value);
            }
        }

        public Brush GetBrush()
        {
            if (_colors.Count == 1)
            {
                return GetSingleColorGradientBrush();
            }   
            else if (_colors.Count > 1)
            {
                return GetMultiColorGradientBrush();
            }
            else
            {
                return new SolidColorBrush(Colors.Blue);
            }
        }

        private GradientBrush GetSingleColorGradientBrush()
        {
            Byte alpha = 160;

            LinearGradientBrush gradientBrush = new LinearGradientBrush()
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1)
            };

            Color startColor = _colors.ElementAt(0);
            Color endColor = Color.FromArgb(alpha, startColor.R, startColor.G, startColor.B);
            gradientBrush.GradientStops.Add(new GradientStop { Color = startColor, Offset = 0.0 });
            gradientBrush.GradientStops.Add(new GradientStop { Color = endColor, Offset = 1.0 });

            return gradientBrush;
        }

        private GradientBrush GetMultiColorGradientBrush()
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush()
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1)
            };

            double offsetLocation = 1.0 / (_colors.Count - 1);
            for (int i = 0; i < _colors.Count; i++)
            {
                gradientBrush.GradientStops.Add(new GradientStop { Color = _colors.ElementAt(i), Offset = offsetLocation * i });
            }

            return gradientBrush;
        }
    }
}
