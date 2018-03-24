using CadenceLifeCounterV3.Models;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CadenceLifeCounterV3.Views
{
    public sealed partial class SecondaryCounterOverlay : UserControl
    {
        public List<SecondaryCounterModel> Counters = new List<SecondaryCounterModel>();
        public PlayerModel Player {
            get;
            set; }

        public SecondaryCounterOverlay()
        {
            this.InitializeComponent();

            this.Loaded += SecondaryCounterOverlay_Loaded;

           
        }

        private void SecondaryCounterOverlay_Loaded(object sender, RoutedEventArgs e)
        {
            var winsCounter = new SecondaryCounterModel()
            {
                CounterName = "Wins",
                Value = 0,
                //CounterColor = Windows.UI.Colors.Blue,
                CounterColor = new ColorSwatchModel(Windows.UI.Colors.Blue),
                CounterIcon = new SymbolIcon(Symbol.Emoji)
            };
            Counters.Add(winsCounter);

            var lossesCounter = new SecondaryCounterModel()
            {
                CounterName = "Losses",
                Value = 0,
                //CounterColor = Windows.UI.Colors.Red,
                CounterColor = new ColorSwatchModel(Windows.UI.Colors.Red),
                CounterIcon = new SymbolIcon(Symbol.Download)
            };
            Counters.Add(lossesCounter);
            var poisonCounter = new SecondaryCounterModel()
            {
                CounterName = "Poison",
                Value = 0,
                //CounterColor = Windows.UI.Colors.Green,
                CounterColor = new ColorSwatchModel(Windows.UI.Colors.Green),
                CounterIcon = new SymbolIcon(Symbol.AddFriend)
            };
            Counters.Add(poisonCounter);

            foreach (SecondaryCounterModel counter in Counters)
            {
                CountersPanel.Children.Add(new SecondaryCounterControl() { Counter = counter });
            }
        }

        private void Flyout_Closed(object sender, object e)
        {
            NewCounterTextBox.Text = "";
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var newCounter = new SecondaryCounterModel()
            {
                CounterName = NewCounterTextBox.Text == "" ? "Counter" : NewCounterTextBox.Text,
                Value = 0,
                //CounterColor = Windows.UI.Colors.DarkGray,
                CounterColor = new ColorSwatchModel(Windows.UI.Colors.DarkGray),
                CounterIcon = new SymbolIcon(Symbol.Placeholder)
            };
            Counters.Add(newCounter);

            CountersPanel.Children.Add(new SecondaryCounterControl() { Counter = newCounter });

            ButtonFlyout.Hide();
        }

        private void NewCounterTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                ConfirmButton_Click(null, null);
            }

        }

        private void HideSecondaryCounterButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
