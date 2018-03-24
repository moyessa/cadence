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
    public sealed partial class PlayerTile : UserControl
    {
        public PlayerModel Player { get; set; }

        public PlayerTile()
        {
            this.InitializeComponent();

            //Player = new PlayerModel();
        }

        private void HealthRepeatButton_Click(object sender, RoutedEventArgs e)
        {
            int lifeChangeAmount = Convert.ToInt32((sender as RepeatButton).Tag);

            Player.Life += lifeChangeAmount;
        }

        private void SecondaryCounterButton_Click(object sender, RoutedEventArgs e)
        {
            SecondaryCounterOverlay.Visibility = Visibility.Visible;
        }
    }
}
