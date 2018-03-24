using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CadenceLifeCounterV3.Views
{
    public sealed partial class TimerControl : UserControl
    {
        private DispatcherTimer timer;
        private bool _isPauseEnabled = false;
        private bool IsPauseEnabled
        {
            get { return _isPauseEnabled; }
            set
            {
                if (!value)
                {
                    timer.Stop();

                    PlayPauseButton.Content = new SymbolIcon(Symbol.Play);
                }
                else
                {
                    timer.Start();

                    PlayPauseButton.Content = new SymbolIcon(Symbol.Pause);
                }

                _isPauseEnabled = value;
            }
        }

        private TimeSpan _initialTime = new TimeSpan(0, 50, 00);
        public TimeSpan InitialTime
        {
            get
            {
                return _initialTime;
            }
            set
            {
                _initialTime = value;
                IsPauseEnabled = false;
                ResetTimer();
            }
        }
        public TimeSpan timeRemaining;

        private void ResetTimer()
        {
            timeRemaining = InitialTime;
            timeTextBlock.Text = FormatTimeString();
        }

        public TimerControl()
        {
            this.InitializeComponent();

            ResetTimer();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;

            SetupImplicitAnimations();
        }

        private void Timer_Tick(object sender, object e)
        {
            timeRemaining = timeRemaining.Subtract((sender as DispatcherTimer).Interval);

            if (timeRemaining.CompareTo(TimeSpan.Zero) <= 0)
            {
                timer.Stop();
                IsPauseEnabled = false;
            }

            timeTextBlock.Text = FormatTimeString();
        }


        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (timeRemaining.CompareTo(TimeSpan.Zero) <= 0)
            {
                ResetTimer();
            }

            IsPauseEnabled = !IsPauseEnabled;
        }

        private String FormatTimeString()
        {
            return ((timeRemaining.Hours).ToString()) + ":"
                           + MakeTwoDigitString(timeRemaining.Minutes) + ":"
                           + MakeTwoDigitString(timeRemaining.Seconds);
        }

        // TODO: Can use string formatting here instead.
        private String MakeTwoDigitString(int number)
        {
            string twoDigit;
            if (number < 10)
            {
                twoDigit = "0" + number;
                return twoDigit;
            }
            twoDigit = number.ToString();

            return twoDigit;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetTimer();
        }

        private void SetupImplicitAnimations()
        {
            var compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

            var showAnimationGroup = compositor.CreateAnimationGroup();

            var showOffsetAnimation = compositor.CreateVector3KeyFrameAnimation();
            showOffsetAnimation.Target = nameof(Visual.Offset);
            showOffsetAnimation.InsertKeyFrame(0f, new System.Numerics.Vector3(10,0,0));
            showOffsetAnimation.InsertExpressionKeyFrame(1.0f, "this.FinalValue");
            showOffsetAnimation.Duration = TimeSpan.FromMilliseconds(300);

            var showOpacityAnimation = compositor.CreateScalarKeyFrameAnimation();
            showOpacityAnimation.Target = nameof(Visual.Opacity);
            showOpacityAnimation.InsertKeyFrame(0f, 0f);
            showOpacityAnimation.InsertExpressionKeyFrame(1.0f, "this.FinalValue");
            showOpacityAnimation.Duration = TimeSpan.FromMilliseconds(300);

            showAnimationGroup.Add(showOpacityAnimation);
            showAnimationGroup.Add(showOffsetAnimation);

            ElementCompositionPreview.SetImplicitShowAnimation(TimerPanel, showAnimationGroup);

            var hideOpacityAnimation = compositor.CreateScalarKeyFrameAnimation();
            hideOpacityAnimation.Target = nameof(Visual.Opacity);
            hideOpacityAnimation.InsertKeyFrame(0f, 1.0f);
            hideOpacityAnimation.InsertKeyFrame(1.0f, 0.0f);
            hideOpacityAnimation.Duration = TimeSpan.FromMilliseconds(150);

            ElementCompositionPreview.SetImplicitHideAnimation(TimerPanel, hideOpacityAnimation);
        }
    }
}
