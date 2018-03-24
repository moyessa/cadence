using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CadenceLifeCounterV3.Models;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CadenceLifeCounterV3.Views
{
    public sealed partial class PlayerNameTextBox : UserControl
    {
        public PlayerModel Player { get; set; }

        public PlayerNameTextBox()
        {
            this.InitializeComponent();

            Loaded += TextBox_Loaded;
        }

        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            var root = VisualTreeHelper.GetChild(NameTextBox, 0);

            var button = (root as FrameworkElement).FindName("RandomizeButton") as Button;
            if (button != null)
            {
                button.Tapped += RandomizeButton_Tapped;
            }
        }

        private void RandomizeButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NameTextBox.Text = "PUSHED THE BUTTON";
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Player.Name = (sender as TextBox).Text;
        }
    }
}
