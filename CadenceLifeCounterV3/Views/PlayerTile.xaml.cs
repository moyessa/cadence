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
using Windows.UI.Composition;
using Windows.UI.Xaml.Hosting;

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

            SetupImplicitAnimations();
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

        private void SetupImplicitAnimations()
        {
            var compositor = ElementCompositionPreview.GetElementVisual(SecondaryCounterOverlay).Compositor;

            var showAnimationGroup = compositor.CreateAnimationGroup();

            var showOpacityAnimation = compositor.CreateScalarKeyFrameAnimation();
            showOpacityAnimation.Target = nameof(Visual.Opacity);
            showOpacityAnimation.InsertKeyFrame(0f, 0f);
            showOpacityAnimation.InsertKeyFrame(1.0f, 1.0f);
            showOpacityAnimation.Duration = TimeSpan.FromMilliseconds(300);

            showAnimationGroup.Add(showOpacityAnimation);

            var hideOpacityAnimation = compositor.CreateScalarKeyFrameAnimation();
            hideOpacityAnimation.Target = nameof(Visual.Opacity);
            hideOpacityAnimation.InsertKeyFrame(0f, 1.0f);
            hideOpacityAnimation.InsertKeyFrame(1.0f, 0.0f);
            hideOpacityAnimation.Duration = TimeSpan.FromMilliseconds(300);

            ElementCompositionPreview.SetImplicitShowAnimation(SecondaryCounterOverlay, showAnimationGroup);
            ElementCompositionPreview.SetImplicitHideAnimation(SecondaryCounterOverlay, hideOpacityAnimation);

        }

        private void DiceButton_Click(object sender, RoutedEventArgs e)
        {
            DiceOverlay.Visibility = Visibility.Visible;
        }
    }
}
