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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CadenceLifeCounterV3.Views
{
    public abstract class CadenceFlyoutModel
    {
        public String Title { get; set; }
        public String Glyph { get; set; }
        public UserControl Content { get; set; }
    }

    public sealed partial class CadenceFlyout : UserControl
    {
        private MainPage rootPage;

        public CadenceFlyoutModel CurrentViewModel { get; set; }

        public CadenceFlyout()
        {
            this.InitializeComponent();
        }

        internal void LinkToMainPage(MainPage mainPage)
        {
            rootPage = mainPage;

            CurrentViewModel = new PlayerCustomizationModel(rootPage);

            FlyoutContent.Children.Add(CurrentViewModel.Content);
        }
    }
}
