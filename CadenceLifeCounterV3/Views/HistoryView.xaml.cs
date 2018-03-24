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
    public class HistoryViewModel : CadenceFlyoutModel
    {
        public HistoryViewModel(MainPage rootPage)
        {
            Title = "History";
            Glyph = "\uE179";
            Content = new HistoryView(rootPage);
        }
    }

    public sealed partial class HistoryView : UserControl
    {
        private MainPage rootPage;
        public HistoryView(MainPage rootPage)
        {
            this.InitializeComponent();

            this.rootPage = rootPage;

            rootPage.EventManager.LoggedEvents.CollectionChanged += LoggedEvents_CollectionChanged;
            ClearAllHistoryButton.IsEnabledChanged += ClearAllHistoryButton_IsEnabledChanged;

            //List<HistoryEvent> items = new List<HistoryEvent>();
            //for (int i = 0; i < 100; i++)
            //{
            //    items.Add(new ResetGameEvent());
            //    items.Add(new LifeEvent());
            //    items.Add(new DiceEvent());
            //    items.Add(new CoinEvent());
            //    items.Add(new NumberPlayersChangedEvent());
            //}

            //HistoryListView.ItemsSource = items;

            HistoryListView.ItemsSource = rootPage.EventManager.LoggedEvents;
        }

        private void ClearAllHistoryButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            NoHistoryToShowBlock.Visibility = ((bool)e.NewValue == true) ? Visibility.Collapsed : Visibility.Visible;
        }

        private void LoggedEvents_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                ClearAllHistoryButton.IsEnabled = false;
                
            }
            else
            {
                ClearAllHistoryButton.IsEnabled = (e.NewItems.Count > 0);
            }
        }

        private void ClearAllHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            rootPage.EventManager.ClearHistory();
        }
    }
}
