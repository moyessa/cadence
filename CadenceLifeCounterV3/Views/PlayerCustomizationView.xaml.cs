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
    public class PlayerCustomizationModel : CadenceFlyoutModel
    {
        public PlayerCustomizationModel(MainPage rootPage)
        {
            Title = "Player";

            Glyph = "\uE125";

            Content = new PlayerCustomizationView(rootPage);
        }
    }

    public sealed partial class PlayerCustomizationView : UserControl
    {
        private MainPage rootPage;

        public PlayerCustomizationView(MainPage rootPage)
        {
            this.InitializeComponent();

            this.rootPage = rootPage;

            foreach (PlayerModel p in rootPage.Players)
            {
                PlayerCustomizationViewItemsPanel.Children.Add(new PlayerCustomizationViewItem(p));
            }


            var playerRadioButtons = new List<RadioButton>();
            foreach (RadioButton rb in NumberOfPlayersGrid.Children)
            {
                playerRadioButtons.Add(rb);
            }

            playerRadioButtons[rootPage.GetNumberOfActivePlayers() - 1].IsChecked = true;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var num = Convert.ToInt16((sender as RadioButton).Content as String);

            rootPage.SetNumberOfActivePlayers(num);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            rootPage.ResetAllPlayersLife();
        }
    }
}
