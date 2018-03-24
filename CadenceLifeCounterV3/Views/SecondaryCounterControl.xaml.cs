﻿using CadenceLifeCounterV3.Models;
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
    public sealed partial class SecondaryCounterControl : UserControl
    {
        public SecondaryCounterModel Counter { get; set; }

        public SecondaryCounterControl()
        {
            this.InitializeComponent();
        }

        private void AddRepeatButton_Click(object sender, RoutedEventArgs e)
        {
            if (Counter.Player != null)
            {
                Counter.Player.Life++;
            }
            else
            {
                Counter.Value++;
            }
            
        }

        private void RemoveRepeatButton_Click(object sender, RoutedEventArgs e)
        {
            if (Counter.Player != null)
            {
                Counter.Player.Life--;
            }
            else
            {
                Counter.Value--;
            }

        }
    }
}