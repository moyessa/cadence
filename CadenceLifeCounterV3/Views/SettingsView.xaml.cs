using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System.Display;
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
    public class SettingsViewModel : CadenceFlyoutModel
    {
        public SettingsViewModel(MainPage rootPage)
        {
            Title = "Settings";
            Glyph = "\uE115";
            Content = new SettingsView(rootPage);
        }
    }

    public sealed partial class SettingsView : UserControl
    {
        private MainPage rootPage;
        private DisplayRequest dispRequest = null;

        public SettingsView(MainPage rootPage)
        {
            this.InitializeComponent();

            this.rootPage = rootPage;

            SetStartingLifeRadioButton();
        }

        private void SetStartingLifeRadioButton()
        {
            int startingLife = rootPage.StartingLife;
            foreach (RadioButton radioButton in StartingLifeRadioButtonGrid.Children)
            {
                if (Convert.ToInt16(radioButton.Content as String) == startingLife)
                {
                    radioButton.IsChecked = true;
                    break;
                }
            }
        }

        private void StartingLifeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var num = Convert.ToInt16((sender as RadioButton).Content as String);

            rootPage.StartingLife = num;
        }

        private void ThemeToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            rootPage.SetTheme((sender as ToggleSwitch).IsOn ? ElementTheme.Dark : ElementTheme.Light);
        }

        private void ScreenOnToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleSwitch).IsOn)
            {
                if (dispRequest == null)
                {
                    // Activate a display-required request. If successful, the screen is 
                    // guaranteed not to turn off automatically due to user inactivity.
                    dispRequest = new DisplayRequest();
                    dispRequest.RequestActive();
                }
            }
            else
            {
                if (dispRequest != null)
                {
                    // Deactivate the display request and set the var to null.
                    dispRequest.RequestRelease();
                    dispRequest = null;
                }
            }
        }

        async void RateApp()
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9nblggh09s4b"));
        }

        public static async void Contact(bool includeLog)
        {
            // Create your new email message.
            var em = new EmailMessage();

            // Add as much EmailRecipient in it as you need using the following method.
            em.To.Add(new EmailRecipient("cadenceapp@outlook.com"));
            em.Subject = "Cadence Life Counter Support";

            if (includeLog)
            {
                //StorageFile file;
                //try
                //{
                //    file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("CadenceLog.txt");
                //}
                //catch (Exception)
                //{
                //    file = null;
                //}
                //if (file != null)
                //{
                //    em.Attachments.Add(new EmailAttachment("CadenceLog.txt", file));
                //}
            }

            // Show the email composer.
            await EmailManager.ShowComposeNewEmailAsync(em);
        }

        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {
            Contact(false);
        }

        private void RateButton_Click(object sender, RoutedEventArgs e)
        {
            RateApp();
        }
    }
}
