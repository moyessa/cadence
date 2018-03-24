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
    //public sealed class CadencePlayerPivotItem : PivotItem
    //{
    //    public CadencePlayerPivotItem()
    //    {
    //        Header = new TabHeader() { Label = "Player Customization", Glyph = "\uE125" };
    //        //Content = new TextBlock() { Text = "Player Customization Pivot Item" };
    //    }
    //}

    public sealed partial class CadenceTabStylePivot : UserControl
    {
        public List<PivotItem> CadencePivotItems { get; set; }

        private MainPage rootPage;

        public CadenceTabStylePivot()
        {
            this.InitializeComponent();
        }

        internal void LinkToMainPage(MainPage mainPage)
        {
            rootPage = mainPage;

            CadencePivotItems = new List<PivotItem>();

            var PlayerCustomizationViewModel = new PlayerCustomizationModel(rootPage);
            var PlayerCustomizationPivotItem = new PivotItem();
            PlayerCustomizationPivotItem.Header = new TabHeader() { Label = PlayerCustomizationViewModel.Title, Glyph = PlayerCustomizationViewModel.Glyph };
            PlayerCustomizationPivotItem.Content = PlayerCustomizationViewModel.Content;

            var SettingsViewModel = new SettingsViewModel(rootPage);
            var SettingsPivotItem = new PivotItem();
            SettingsPivotItem.Header = new TabHeader() { Label = SettingsViewModel.Title, Glyph = SettingsViewModel.Glyph };
            SettingsPivotItem.Content = SettingsViewModel.Content;

            var HistoryViewModel = new HistoryViewModel(rootPage);
            var HistoryPivotItem = new PivotItem();
            HistoryPivotItem.Header = new TabHeader() { Label = HistoryViewModel.Title, Glyph = HistoryViewModel.Glyph };
            HistoryPivotItem.Content = HistoryViewModel.Content;

            CadencePivotItems.Add(PlayerCustomizationPivotItem);
            CadencePivotItems.Add(SettingsPivotItem);
            CadencePivotItems.Add(HistoryPivotItem);






            List<String> symbols = new List<String>();
            //symbols.Add("\uE125");
            //symbols.Add("\uE179");
            //symbols.Add("\uE115");


            for (int i = 0; i < symbols.Count; i++)
            {
                PivotItem item = new PivotItem();
                item.Header = new TabHeader() { Label = "Item " + i, Glyph = symbols[i] };
                item.Content = new TextBlock() { Text = "Content of item " + i };
                CadencePivotItems.Add(item);
            }
        }
    }
}
