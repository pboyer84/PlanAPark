using System;
using System.Diagnostics;
using System.Windows;

namespace PlanAPark
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                if (MessageBoxResult.OK == MessageBox.Show("Go to the PlanAPark website? This will open your default browser.", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question))
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = e.Uri.ToString(),
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
            }
            catch (PlatformNotSupportedException)
            {
                MessageBox.Show($"Opening the default browser is not supported Universal Windows Platform (UWP) apps.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sorry, unable to launch the default browser.\n{ex.Message}");
            }
        }
    }
}