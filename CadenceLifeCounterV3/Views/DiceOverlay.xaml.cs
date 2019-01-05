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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CadenceLifeCounterV3.Views
{
    public sealed partial class DiceOverlay : UserControl
    {
        public PlayerModel Player { get; set; }

        static Random rand = new Random();

        public DiceOverlay()
        {
            this.InitializeComponent();

            LayoutRoot.SizeChanged += LayoutRoot_SizeChanged;

            DiceStoryboard.Duration = new Duration(new TimeSpan(0, 0, 3));
            TranslateXAnimation.Duration =
                TranslateYAnimation.Duration =
                RotationAnimation.Duration =
                DiceStoryboard.Duration;

            DieRectangle.Visibility = Visibility.Collapsed;
        }

        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DieRectangle.Visibility = Visibility.Collapsed;

            LayoutRoot.Clip = new RectangleGeometry() { Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height) };

            double minSizeBasedOnLayout = Math.Min(e.NewSize.Width, e.NewSize.Height) * (0.333);

            DieRectangle.Width = DieRectangle.Height = Math.Min(100, minSizeBasedOnLayout);
        }

        private int GetDieValue()
        {
            return rand.Next(1, 7);
        }

        private void GetFaceBasedOnValue(int dieValue)
        {
            string stateName = "Side" + dieValue;

            VisualStateManager.GoToState(this, stateName, false);

            MainPage.GetEventManager().LogEvent(new DiceEvent(Player.Name, dieValue));
        }

        public void RollAndAnimateDie()
        {
            RemoveDiceGrid.Visibility = Visibility.Visible;

            GetFaceBasedOnValue(GetDieValue());

            DieRectangle.Visibility = Visibility.Visible;

            TranslateXAnimation.From = GetTranslateXStart();
            TranslateXAnimation.To = GetTranslateXEnd();

            RotationAnimation.To = GetRotationStart();
            RotationAnimation.From = GetRotationEnd();

            TranslateYAnimation.From = GetTranslateYStart();
            TranslateYAnimation.To = GetTranslateYEnd();

            DiceStoryboard.Begin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RollAndAnimateDie();
        }

        int GetTranslateXStart()
        {
            int minVal = (int)(DieRectangle.ActualWidth * -2);
            int maxVal = (int)(DieRectangle.ActualWidth);
            return rand.Next(minVal, maxVal);
        }
        int GetTranslateXEnd()
        {
            int minVal = (int)(LayoutRoot.ActualWidth / 2);
            int maxVal = (int)(LayoutRoot.ActualWidth - DieRectangle.Width);
            return rand.Next(minVal, maxVal);
        }
        int GetTranslateYStart()
        {
            int minVal = (int)(-1.5 * DieRectangle.Height);
            int maxVal = (int)(LayoutRoot.ActualHeight / 3);
            return rand.Next(minVal, maxVal);
        }
        int GetTranslateYEnd()
        {
            int minVal = (int)(LayoutRoot.ActualHeight / 2);
            int maxVal = (int)(LayoutRoot.ActualHeight - (1.5 * DieRectangle.Height));
            return rand.Next(minVal, maxVal);
        }
        int GetRotationStart()
        {
            int minVal = 20;
            int maxVal = 50;
            return rand.Next(minVal, maxVal);
        }
        int GetRotationEnd()
        {
            int minVal = 0;
            int maxVal = 220;
            return rand.Next(minVal, maxVal);
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DiceStoryboard.Stop();
            DieRectangle.Visibility = Visibility.Collapsed;
            RemoveDiceGrid.Visibility = Visibility.Collapsed;
        }
    }
}
