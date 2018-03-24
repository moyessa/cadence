using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CadenceLifeCounterV3.Models;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CadenceLifeCounterV3
{
    public sealed partial class MainPage : Page
    {
        public static int MaximumNumberOfPlayers = 4;

        private int _startingLife = 20;
        public int StartingLife
        {
            get
            {
                return _startingLife;
            }
            set
            {
                _startingLife = value;
                ResetAllPlayersLife();
            }
        }

        public List<PlayerModel> Players = new List<PlayerModel>();

        private Dictionary<String, LayoutManager> layoutManagers;

        public EventManager EventManager = new EventManager();

        public static EventManager GetEventManager()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            MainPage page = rootFrame.Content as MainPage;
            if (page != null)
            {
                return page.EventManager;
            }
            return null;
        }

        public MainPage()
        {
            this.InitializeComponent();

            this.SizeChanged += MainPage_SizeChanged;

            CreateAllPlayers();

            SetupLayoutManagers();

            CadencePivot.LinkToMainPage(this);
        }


        private void CreateAllPlayers()
        {
            for (int i = 0; i < MaximumNumberOfPlayers; i++)
            {
                Players.Add(new PlayerModel());

                Players[i].Name = "Player " + i;
            }

            SetNumberOfActivePlayers(4, false);
            ResetAllPlayersLife();

            Players[0].Color = new ColorSwatchModel(App.GetCadenceBlueColor());
            Players[1].Color = new ColorSwatchModel(App.GetCadenceBlackColor());
            Players[2].Color = new ColorSwatchModel(App.GetCadenceYellowColor());
            Players[3].Color = new ColorSwatchModel(App.GetCadenceRedColor());
        }

        public int GetNumberOfActivePlayers()
        {
            int totalNumberOfActivePlayers = 0;
            for (int i = 0; i < MaximumNumberOfPlayers; i++)
            {
                if (Players[i].IsActive) { totalNumberOfActivePlayers++; }
            }
            return totalNumberOfActivePlayers;
        }

        private void SetupLayoutManagers()
        {
            layoutManagers = new Dictionary<String, LayoutManager>();
            for (int i = 0; i < MaximumNumberOfPlayers; i++)
            {
                layoutManagers.Add(LayoutManager.GetManagerKey(i, true), LayoutManager.CreateLayout(i, true, PlayerTileRootGrid));
                layoutManagers.Add(LayoutManager.GetManagerKey(i, false), LayoutManager.CreateLayout(i, false, PlayerTileRootGrid));
            }
        }

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateTileLayout();
        }

        private void UpdateTileLayout()
        {
            bool isLandscape = LayoutManager.DetermineIfWindowIsLandscape();
            GetManager(isLandscape).LayoutGrid();
        }

        LayoutManager GetManager(bool isLandscape)
        {
            int adjustedNumberOfPlayers = GetNumberOfActivePlayers() - 1;

            // Don't switch the second and third players when four players are active between landscape and horizontal.
            if (adjustedNumberOfPlayers == 3) isLandscape = true;

            return layoutManagers[LayoutManager.GetManagerKey(adjustedNumberOfPlayers, isLandscape)];
        }

        public void SetNumberOfActivePlayers(int numberOfActivePlayers, bool updateLayout = true)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].IsActive = (i + 1) <= numberOfActivePlayers;
            }

            if (updateLayout)
            {
                UpdateTileLayout();
            }

            EventManager.LogEvent(new NumberPlayersChangedEvent(numberOfActivePlayers));
        }

        private void TogglePaneButton_Checked(object sender, RoutedEventArgs e)
        {
            RootSplitView.IsPaneOpen = !RootSplitView.IsPaneOpen;
        }

        public void ResetAllPlayersLife()
        {
            foreach (PlayerModel p in Players)
            {
                p.Life = StartingLife;
            }

            EventManager.LogEvent(new ResetGameEvent(StartingLife));
        }

        public void SetTheme(ElementTheme theme)
        {
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            this.RequestedTheme = theme;
            if (theme == ElementTheme.Dark)
            {
                // Dark
                titleBar.BackgroundColor = Colors.Black;
                titleBar.ForegroundColor = Colors.White;
                titleBar.ButtonBackgroundColor = Colors.Black;
            }
            else
            {
                // Light
                titleBar.BackgroundColor = Colors.White;
                titleBar.ForegroundColor = Colors.Black;
                titleBar.ButtonBackgroundColor = Colors.White;
            }
        }







        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartingLife = 50;
        }
    }
}
