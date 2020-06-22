using System.IO;
using System.Windows;

namespace PlanAPark
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    internal partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string filename = null;
            for (int i=0; i<e.Args.Length; i++)
            {
                if (!string.IsNullOrEmpty(e.Args[i]))
                {
                    filename = e.Args[i];
                }
            }
            var mainWindow = new MainWindow(filename);
            mainWindow.Show();
        }
    }
}
