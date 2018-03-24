using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CadenceLifeCounterV3.Models
{
    public class PlayerModel : INotifyPropertyChanged
    {
        private string _name = "";
        private int _life = 0;
        private ColorSwatchModel _color = new ColorSwatchModel();
        private bool _isActive = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (!_name.Equals(value))
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Life
        {
            get
            {
                return _life;
            }
            set
            {
                int oldLifeValue = _life;
                if (!oldLifeValue.Equals(value))
                {

                    var eventManager = MainPage.GetEventManager();
                    if (eventManager != null)
                    {
                        eventManager.LogEvent(new LifeEvent(Name, Color, oldLifeValue, value));
                    }

                    _life = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ColorSwatchModel Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (!_color.Equals(value))
                {
                    _color = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (!_isActive.Equals(value))
                {
                    _isActive = value;
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

        public PlayerModel()
        {
            Name = "A MTG Player";
            Life = 20;
        }
    }
}
