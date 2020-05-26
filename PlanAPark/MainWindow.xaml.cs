using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PlanAPark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly double MetersToPixelsScale = 20d / 7d;
        
        private Style imageButtonStyle;
        private Point mousePosition;
        private Token draggedToken;
        private Token selectedToken;
        
        public MainWindow()
        {
            InitializeComponent();
            imageButtonStyle = TryFindResource("ImageButtonStyle") as Style;
            LoadAircraft();
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnMouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Token)
            {
                draggedToken = null;
            }
        }

        private void OnMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            airportCanvas.Focus();
            mousePosition = e.GetPosition(airportCanvas);
            selectedToken?.HideBorder();
            if (e.Source is Token)
            {
                draggedToken = e.Source as Token;
                selectedToken = draggedToken;
                selectedToken.ShowBorder();
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (draggedToken == null)
            {
                return;
            }
            
            Point currentPosition = e.GetPosition(airportCanvas);
            double deltaX = currentPosition.X - mousePosition.X;
            double deltaY = currentPosition.Y - mousePosition.Y;
            Canvas.SetTop(draggedToken, Canvas.GetTop(draggedToken) + deltaY);
            Canvas.SetLeft(draggedToken, Canvas.GetLeft(draggedToken) + deltaX);
            mousePosition = currentPosition;
        }

        private void SpawnNewToken(AircraftData aircraft)
        {
            var newToken = new Token();

            newToken.myImage.Source = new BitmapImage(
                    new Uri($"pack://application:,,,/Images/{aircraft.ImageFilename}",
                    UriKind.Absolute));

            newToken.myImage.Stretch = Stretch.Fill;
            newToken.Height = aircraft.LengthInMeters * MetersToPixelsScale;
            newToken.Width = aircraft.WidthInMeters * MetersToPixelsScale;
            var r = new Random();
            var x = r.Next(0, (int)airportCanvas.ActualWidth);
            var y = r.Next(0, (int)airportCanvas.ActualHeight);
            Canvas.SetLeft(newToken, x);
            Canvas.SetTop(newToken, y);
            airportCanvas.Children.Add(newToken);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.R)
            {
                selectedToken.Rotate(-15);
            }
            if (e.Key == Key.T)
            {
                selectedToken.Rotate(15);
            }
        }

        private void MenuItemSaveAs_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "Airshow Plan", // Default file name
                DefaultExt = ".airshow", // Default file extension
                Filter = "Airshow Plans (.airshow)|*.airshow" // Filter files by extension
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                Title = $"Plan A Park - {dlg.FileName}";
            }
            var items = airportCanvas.Children.OfType<Token>();
            foreach (var item in items)
            {
                Token t = item;
                var f = GetWindow(this.Parent).Resources;
                TokenDTO dto = new TokenDTO()
                {
                    Top = Canvas.GetTop(item),
                    Left = Canvas.GetLeft(item),
                    Aircraft = "foo"
                };
            }
        }

        private void LoadAircraft()
        {
            var aircraft = Resources.Values.OfType<AircraftData>();
            foreach (var data in aircraft)
            {
                var buttonImage = new Image();
                buttonImage.Source = new BitmapImage(
                    new Uri($"pack://application:,,,/Images/{data.ImageFilename}", 
                    UriKind.Absolute));

                buttonImage.Width = 48;
                buttonImage.Height = 48;

                var button = new Button();
                button.Style = imageButtonStyle;
                button.Content = buttonImage;
                button.Height = 48;
                button.Width = 48;
                button.Click += (object sender, RoutedEventArgs e) =>
                {
                    SpawnNewToken(data);
                };
                pnButtons.Children.Add(button);
            }   
        }
    }
}