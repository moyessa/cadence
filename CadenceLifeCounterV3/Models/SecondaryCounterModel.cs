using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace CadenceLifeCounterV3.Models
{
    public class SecondaryCounterModel : INotifyPropertyChanged
    {
        public PlayerModel Player { get; set; }

        public String CounterName { get; set; }
        public IconElement CounterIcon { get; set; }


        private int _value;
        private ColorSwatchModel _counterColor = new ColorSwatchModel();

        public event PropertyChangedEventHandler PropertyChanged;

        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                int oldValue = _value;
                if (!oldValue.Equals(value))
                {
                    _value = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ColorSwatchModel CounterColor
        {
            get
            {
                return _counterColor;
            }
            set
            {
                if (!_counterColor.Equals(value))
                {
                    _counterColor = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
